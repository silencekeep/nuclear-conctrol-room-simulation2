// Modified version of UnityStandardCore.cginc
uniform fixed4 _SectionColor;
half _BackfaceExtrusion;
fixed _negativeScale;

#ifndef UNITY_STANDARD_CORE_CLIPPED_INCLUDED
#define UNITY_STANDARD_CORE_CLIPPED_INCLUDED

#include "UnityStandardCore.cginc"
#include "CGIncludes/section_clipping_CS.cginc"

// ------------------------------------------------------------------
//  Base forward pass (directional light, emission, lightmaps, ...)

struct VertexOutputForwardClipBase
{
	UNITY_POSITION(pos);
	float4 tex							: TEXCOORD0;
	float4 eyeVec 						: TEXCOORD1;		// eyeVec.xyz | fogCoord
	float4 tangentToWorldAndPackedData[3]    : TEXCOORD2;    // [3x3:tangentToWorld | 1x3:viewDirForParallax or worldPos]
	half4 ambientOrLightmapUV			: TEXCOORD5;	// SH or Lightmap UV
	UNITY_LIGHTING_COORDS(6,7)

		// next ones would not fit into SM2.0 limits, but they are always for SM3.0+
#if UNITY_REQUIRE_FRAG_WORLDPOS || PLANE_CLIPPING_ENABLED
	#if !UNITY_PACK_WORLDPOS_WITH_TANGENT
		float3 posWorld					: TEXCOORD8;
	#endif
#endif

	UNITY_VERTEX_INPUT_INSTANCE_ID
	UNITY_VERTEX_OUTPUT_STEREO
};

VertexOutputForwardClipBase vertForwardClipBase (VertexInput v)
{
	#ifdef RETRACT_BACKFACES

		float3 viewDir = ObjSpaceViewDir(v.vertex);
        float dotProduct = dot(v.normal, viewDir);
		if(dotProduct<0) {
			float3 worldPos = mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1)).xyz;
			float3 worldNorm = UnityObjectToWorldNormal(v.normal);
			worldPos -= worldNorm * _BackfaceExtrusion;
			v.vertex.xyz = mul(unity_WorldToObject, float4(worldPos, 1)).xyz;
		}
	#endif

	UNITY_SETUP_INSTANCE_ID(v);	
	VertexOutputForwardClipBase o;
	UNITY_INITIALIZE_OUTPUT(VertexOutputForwardClipBase, o);
	UNITY_TRANSFER_INSTANCE_ID(v, o);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

	float4 posWorld = mul(unity_ObjectToWorld, v.vertex);
	#if UNITY_REQUIRE_FRAG_WORLDPOS || PLANE_CLIPPING_ENABLED
		#if UNITY_PACK_WORLDPOS_WITH_TANGENT
            o.tangentToWorldAndPackedData[0].w = posWorld.x;
            o.tangentToWorldAndPackedData[1].w = posWorld.y;
            o.tangentToWorldAndPackedData[2].w = posWorld.z;
        #else
            o.posWorld = posWorld.xyz;
        #endif
	#endif
	o.pos = UnityObjectToClipPos(v.vertex);
	
	o.tex = TexCoords(v);
	o.eyeVec.xyz = NormalizePerVertexNormal(posWorld.xyz - _WorldSpaceCameraPos);
	float3 normalWorld = UnityObjectToWorldNormal(v.normal);
	#ifdef _TANGENT_TO_WORLD
		float4 tangentWorld = float4(UnityObjectToWorldDir(v.tangent.xyz), v.tangent.w);

		float3x3 tangentToWorld = CreateTangentToWorldPerVertex(normalWorld, tangentWorld.xyz, tangentWorld.w);
        o.tangentToWorldAndPackedData[0].xyz = tangentToWorld[0];
        o.tangentToWorldAndPackedData[1].xyz = tangentToWorld[1];
        o.tangentToWorldAndPackedData[2].xyz = tangentToWorld[2];
    #else
        o.tangentToWorldAndPackedData[0].xyz = 0;
        o.tangentToWorldAndPackedData[1].xyz = 0;
        o.tangentToWorldAndPackedData[2].xyz = normalWorld;
	#endif
	//We need this for shadow receving
    UNITY_TRANSFER_LIGHTING(o, v.uv1);

	o.ambientOrLightmapUV = VertexGIForward(v, posWorld, normalWorld);
	
	#ifdef _PARALLAXMAP
		TANGENT_SPACE_ROTATION;
		half3 viewDirForParallax = mul (rotation, ObjSpaceViewDir(v.vertex));
        o.tangentToWorldAndPackedData[0].w = viewDirForParallax.x;
        o.tangentToWorldAndPackedData[1].w = viewDirForParallax.y;
        o.tangentToWorldAndPackedData[2].w = viewDirForParallax.z;
	#endif

	//UNITY_TRANSFER_FOG(o,o.pos);
    UNITY_TRANSFER_FOG_COMBINED_WITH_EYE_VEC(o,o.pos);//
	return o;
}

// ------------------------------------------------------------------
//  Additive forward pass (one light per pass)

struct VertexOutputForwardClipAdd
{
	UNITY_POSITION(pos);
	float4 tex							: TEXCOORD0;
	float4 eyeVec 						: TEXCOORD1;	// eyeVec.xyz | fogCoord
	float4 tangentToWorldAndLightDir[3]	: TEXCOORD2;	// [3x3:tangentToWorld | 1x3:lightDir]
    float3 posWorld                     : TEXCOORD5;
    UNITY_LIGHTING_COORDS(6, 7)

	// next ones would not fit into SM2.0 limits, but they are always for SM3.0+
#if defined(_PARALLAXMAP)
		half3 viewDirForParallax			: TEXCOORD8;
#endif

	UNITY_VERTEX_OUTPUT_STEREO
};

VertexOutputForwardClipAdd vertForwardClipAdd (VertexInput v)
{
	UNITY_SETUP_INSTANCE_ID(v);
	VertexOutputForwardClipAdd o;
	UNITY_INITIALIZE_OUTPUT(VertexOutputForwardClipAdd, o);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

	float4 posWorld = mul(unity_ObjectToWorld, v.vertex);
	o.pos = UnityObjectToClipPos(v.vertex);

	o.tex = TexCoords(v);
	o.eyeVec.xyz = NormalizePerVertexNormal(posWorld.xyz - _WorldSpaceCameraPos);
    o.posWorld = posWorld.xyz;
	float3 normalWorld = UnityObjectToWorldNormal(v.normal);
	#ifdef _TANGENT_TO_WORLD
		float4 tangentWorld = float4(UnityObjectToWorldDir(v.tangent.xyz), v.tangent.w);

		float3x3 tangentToWorld = CreateTangentToWorldPerVertex(normalWorld, tangentWorld.xyz, tangentWorld.w);
		o.tangentToWorldAndLightDir[0].xyz = tangentToWorld[0];
		o.tangentToWorldAndLightDir[1].xyz = tangentToWorld[1];
		o.tangentToWorldAndLightDir[2].xyz = tangentToWorld[2];
	#else
		o.tangentToWorldAndLightDir[0].xyz = 0;
		o.tangentToWorldAndLightDir[1].xyz = 0;
		o.tangentToWorldAndLightDir[2].xyz = normalWorld;
	#endif
	//We need this for shadow receiving
    //UNITY_TRANSFER_SHADOW(o, v.uv1);
	    //We need this for shadow receiving and lighting
    UNITY_TRANSFER_LIGHTING(o, v.uv1);

	float3 lightDir = _WorldSpaceLightPos0.xyz - posWorld.xyz * _WorldSpaceLightPos0.w;
	#ifndef USING_DIRECTIONAL_LIGHT
		lightDir = NormalizePerVertexNormal(lightDir);
	#endif
	o.tangentToWorldAndLightDir[0].w = lightDir.x;
	o.tangentToWorldAndLightDir[1].w = lightDir.y;
	o.tangentToWorldAndLightDir[2].w = lightDir.z;

	#ifdef _PARALLAXMAP
		TANGENT_SPACE_ROTATION;
		o.viewDirForParallax = mul (rotation, ObjSpaceViewDir(v.vertex));
	#endif
	
    UNITY_TRANSFER_FOG_COMBINED_WITH_EYE_VEC(o, o.pos);//
	return o;
}

#if UNITY_REQUIRE_FRAG_WORLDPOS || PLANE_CLIPPING_ENABLED
    #if UNITY_PACK_WORLDPOS_WITH_TANGENT
        #define IN_WORLDPOS_CLIP(i) half3(i.tangentToWorldAndPackedData[0].w,i.tangentToWorldAndPackedData[1].w,i.tangentToWorldAndPackedData[2].w)
    #else
        #define IN_WORLDPOS_CLIP(i) i.posWorld
    #endif
#else
	#define IN_WORLDPOS_CLIP(i) half3(0,0,0)
	#define IN_WORLDPOS_FWDADD_CLIP(i) half3(0,0,0)
#endif

#if UNITY_REQUIRE_FRAG_WORLDPOS
	#define IN_WORLDPOS_FWDADD_CLIP(i) i.posWorld 
#endif

#define FRAGMENT_SETUP_CLIP(x) FragmentCommonData x = \
	FragmentSetup(i.tex, i.eyeVec, IN_VIEWDIR4PARALLAX(i), i.tangentToWorldAndPackedData, IN_WORLDPOS_CLIP(i));

#define FRAGMENT_SETUP_FWDADD_CLIP(x) FragmentCommonData x = \
	FragmentSetup(i.tex, i.eyeVec, IN_VIEWDIR4PARALLAX_FWDADD(i), i.tangentToWorldAndLightDir, IN_WORLDPOS_FWDADD_CLIP(i));


#if (SHADER_TARGET >= 30)
half4 fragForwardClipBaseInternal (VertexOutputForwardClipBase i, fixed f)
{
	#if UNITY_VFACE_AFFECTED_BY_PROJECTION
      f = f * _ProjectionParams.x; // take possible upside down rendering into account
	#endif
#else
half4 fragForwardClipBaseInternal (VertexOutputForwardClipBase i)
{
fixed f = 1;
#endif

	UNITY_APPLY_DITHER_CROSSFADE(i.pos.xy);

	FRAGMENT_SETUP_CLIP(s)
	
	#if UNITY_PACK_WORLDPOS_WITH_TANGENT
	PLANE_CLIP(float3(i.tangentToWorldAndPackedData[0].w, i.tangentToWorldAndPackedData[1].w, i.tangentToWorldAndPackedData[2].w))
	#else
	PLANE_CLIP(s.posWorld)
	#endif

	UNITY_SETUP_INSTANCE_ID(i);
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

#if defined(_ALPHAPREMULTIPLY_ON) || defined(_ALPHABLEND_ON) || defined(_ALPHATEST_ON)//
	if(f<0)	discard;
#endif

	UnityLight mainLight = MainLight ();
    UNITY_LIGHT_ATTENUATION(atten, i, s.posWorld); 

	half occlusion = Occlusion(i.tex.xy);
	UnityGI gi = FragmentGI (s, occlusion, i.ambientOrLightmapUV, atten, mainLight);

	half4 c = UNITY_BRDF_PBS (s.diffColor, s.specColor, s.oneMinusReflectivity, s.smoothness, s.normalWorld, -s.eyeVec, gi.light, gi.indirect);
	c.rgb += Emission(i.tex.xy);

	#if PLANE_CLIPPING_ENABLED
	c.rgb = f*(1-2*_negativeScale)>=0 ? c.rgb: _SectionColor.rgb;
	#endif

    UNITY_EXTRACT_FOG_FROM_EYE_VEC(i);//
    UNITY_APPLY_FOG(_unity_fogCoord, c.rgb);//
    return OutputForward (c, s.alpha);
}

half4 fragForwardClipAddInternal(VertexOutputForwardClipAdd i)
{
	UNITY_APPLY_DITHER_CROSSFADE(i.pos.xy);

	UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

	FRAGMENT_SETUP_FWDADD_CLIP(s)

    UNITY_LIGHT_ATTENUATION(atten, i, s.posWorld); 

	PLANE_CLIP(s.posWorld)

    UnityLight light = AdditiveLight (IN_LIGHTDIR_FWDADD(i), atten);
    UnityIndirect noIndirect = ZeroIndirect ();

	half4 c = UNITY_BRDF_PBS(s.diffColor, s.specColor, s.oneMinusReflectivity, s.smoothness, s.normalWorld, -s.eyeVec, light, noIndirect);

    UNITY_EXTRACT_FOG_FROM_EYE_VEC(i);//
    UNITY_APPLY_FOG_COLOR(_unity_fogCoord, c.rgb, half4(0,0,0,0)); // fog towards black in additive pass//
    return OutputForward (c, s.alpha);
}

// ------------------------------------------------------------------
//  Deferred pass

struct VertexOutputDeferredClip
{
	
	UNITY_POSITION(pos);
	float4 tex							: TEXCOORD0;
	float3 eyeVec 						: TEXCOORD1;
	float4 tangentToWorldAndPackedData[3]: TEXCOORD2;    // [3x3:tangentToWorld | 1x3:viewDirForParallax or worldPos]
	half4 ambientOrLightmapUV			: TEXCOORD5;	// SH or Lightmap UVs

#if UNITY_REQUIRE_FRAG_WORLDPOS || PLANE_CLIPPING_ENABLED
	#if !UNITY_PACK_WORLDPOS_WITH_TANGENT
        float3 posWorld                     : TEXCOORD6;
    #endif
#endif

	UNITY_VERTEX_INPUT_INSTANCE_ID
	UNITY_VERTEX_OUTPUT_STEREO
};

VertexOutputDeferredClip vertDeferredClip(VertexInput v)
{
	#ifdef RETRACT_BACKFACES
		float3 viewDir = ObjSpaceViewDir(v.vertex);
        float dotProduct = dot(v.normal, viewDir);
		if(dotProduct<0) {
			float3 worldPos = mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1)).xyz;
			float3 worldNorm = UnityObjectToWorldNormal(v.normal);
			worldPos -= worldNorm * _BackfaceExtrusion;
			v.vertex.xyz = mul(unity_WorldToObject, float4(worldPos, 1)).xyz;
		}
	#endif
	
	UNITY_SETUP_INSTANCE_ID(v);
    VertexOutputDeferredClip o;
    UNITY_INITIALIZE_OUTPUT(VertexOutputDeferredClip, o);
	UNITY_TRANSFER_INSTANCE_ID(v, o);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

	float4 posWorld = mul(unity_ObjectToWorld, v.vertex);
#if UNITY_REQUIRE_FRAG_WORLDPOS || PLANE_CLIPPING_ENABLED
        #if UNITY_PACK_WORLDPOS_WITH_TANGENT
            o.tangentToWorldAndPackedData[0].w = posWorld.x;
            o.tangentToWorldAndPackedData[1].w = posWorld.y;
            o.tangentToWorldAndPackedData[2].w = posWorld.z;
        #else
            o.posWorld = posWorld.xyz;
        #endif
#endif
	o.pos = UnityObjectToClipPos(v.vertex);

	o.tex = TexCoords(v);
	o.eyeVec = NormalizePerVertexNormal(posWorld.xyz - _WorldSpaceCameraPos);
	float3 normalWorld = UnityObjectToWorldNormal(v.normal);
#ifdef _TANGENT_TO_WORLD
	float4 tangentWorld = float4(UnityObjectToWorldDir(v.tangent.xyz), v.tangent.w);

	float3x3 tangentToWorld = CreateTangentToWorldPerVertex(normalWorld, tangentWorld.xyz, tangentWorld.w);
        o.tangentToWorldAndPackedData[0].xyz = tangentToWorld[0];
        o.tangentToWorldAndPackedData[1].xyz = tangentToWorld[1];
        o.tangentToWorldAndPackedData[2].xyz = tangentToWorld[2];
    #else
        o.tangentToWorldAndPackedData[0].xyz = 0;
        o.tangentToWorldAndPackedData[1].xyz = 0;
        o.tangentToWorldAndPackedData[2].xyz = normalWorld;
    #endif

	o.ambientOrLightmapUV = 0;
#ifdef LIGHTMAP_ON
	o.ambientOrLightmapUV.xy = v.uv1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
#elif UNITY_SHOULD_SAMPLE_SH
	o.ambientOrLightmapUV.rgb = ShadeSHPerVertex(normalWorld, o.ambientOrLightmapUV.rgb);
#endif
#ifdef DYNAMICLIGHTMAP_ON
	o.ambientOrLightmapUV.zw = v.uv2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
#endif

#ifdef _PARALLAXMAP
	TANGENT_SPACE_ROTATION;
	half3 viewDirForParallax = mul(rotation, ObjSpaceViewDir(v.vertex));
    o.tangentToWorldAndPackedData[0].w = viewDirForParallax.x;
    o.tangentToWorldAndPackedData[1].w = viewDirForParallax.y;
    o.tangentToWorldAndPackedData[2].w = viewDirForParallax.z;
#endif

	return o;
}

void fragDeferredClip(
	VertexOutputDeferredClip i,
	#if (SHADER_TARGET >= 30)
		in fixed facing : VFACE,
	#endif
	out half4 outGBuffer0 : SV_Target0,
	out half4 outGBuffer1 : SV_Target1,
	out half4 outGBuffer2 : SV_Target2,
	out half4 outEmission : SV_Target3			// RT3: emission (rgb), --unused-- (a)
#if defined(SHADOWS_SHADOWMASK) && (UNITY_ALLOWED_MRT_COUNT > 4)
    ,out half4 outShadowMask : SV_Target4       // RT4: shadowmask (rgba)
#endif
)
{
#if (SHADER_TARGET < 30)
	outGBuffer0 = 1;
	outGBuffer1 = 1;
	outGBuffer2 = 0;
	outEmission = 0;
	fixed facing = 1;
	#if defined(SHADOWS_SHADOWMASK) && (UNITY_ALLOWED_MRT_COUNT > 4)
		outShadowMask = 1;
    #endif
	return;
#endif

	UNITY_APPLY_DITHER_CROSSFADE(i.pos.xy);

	FRAGMENT_SETUP_CLIP(s)
    UNITY_SETUP_INSTANCE_ID(i);

	#if UNITY_PACK_WORLDPOS_WITH_TANGENT
	PLANE_CLIP(float3(i.tangentToWorldAndPackedData[0].w, i.tangentToWorldAndPackedData[1].w, i.tangentToWorldAndPackedData[2].w))
	#else
	PLANE_CLIP(s.posWorld)
	#endif

	#if UNITY_VFACE_AFFECTED_BY_PROJECTION
      facing = facing * _ProjectionParams.x; // take possible upside down rendering into account
	#endif

#if defined(_ALPHAPREMULTIPLY_ON) || defined(_ALPHABLEND_ON) || defined(_ALPHATEST_ON)//
	if(facing<0)	discard;
#endif
	if(_SectionColor.a<0.5 && facing<0) discard;

	// no analytic lights in this pass
	UnityLight dummyLight = DummyLight();
	half atten = 1;

	// only GI
	half occlusion = Occlusion(i.tex.xy);
#if UNITY_ENABLE_REFLECTION_BUFFERS
	bool sampleReflectionsInDeferred = (facing*(1-2*_negativeScale)<0);
#else
	bool sampleReflectionsInDeferred = true;
#endif

	UnityGI gi = FragmentGI(s, occlusion, i.ambientOrLightmapUV, atten, dummyLight, sampleReflectionsInDeferred);

	half3 emissiveColor = UNITY_BRDF_PBS(s.diffColor, s.specColor, s.oneMinusReflectivity, s.smoothness, s.normalWorld, -s.eyeVec, gi.light, gi.indirect).rgb;

#ifdef _EMISSION
	emissiveColor += Emission(i.tex.xy);
#endif

#ifndef UNITY_HDR_ON
	emissiveColor.rgb = exp2(-emissiveColor.rgb);
#endif
	#if !PLANE_CLIPPING_ENABLED
	facing = 1;
	#endif

	if(facing*(1-2*_negativeScale)<0) emissiveColor = float4(1,1,1,1) -_SectionColor;

	UnityStandardData data;
	data.diffuseColor = (facing*(1-2*_negativeScale)>0)?s.diffColor:_SectionColor ;
	data.occlusion = (facing*(1-2*_negativeScale)>0)?occlusion:float4(0,0,0,1);
	data.specularColor = (facing*(1-2*_negativeScale)>0)?s.specColor:float4(0,0,0,1);
	data.smoothness = (facing*(1-2*_negativeScale)>0)?s.smoothness:float4(0,0,0,1);
	data.normalWorld = (facing*(1-2*_negativeScale)>0)?s.normalWorld:float4(0,0,0,1);

	UnityStandardDataToGbuffer(data, outGBuffer0, outGBuffer1, outGBuffer2);

	// Emisive lighting buffer
	outEmission = half4(emissiveColor, 1);

	// Baked direct lighting occlusion if any
    #if defined(SHADOWS_SHADOWMASK) && (UNITY_ALLOWED_MRT_COUNT > 4)
        outShadowMask = UnityGetRawBakedOcclusions(i.ambientOrLightmapUV.xy, IN_WORLDPOS(i));
    #endif
}
#endif // UNITY_STANDARD_CORE_CLIPPED_INCLUDED