// Modified version of UnityStandardShadow.cginc

#ifndef UNITY_STANDARD_SHADOW_CLIP_INCLUDED
#define UNITY_STANDARD_SHADOW_CLIP_INCLUDED

#include "UnityStandardShadow.cginc"
#include "CGIncludes/section_clipping_CS.cginc"

#ifndef UNITY_STANDARD_USE_SHADOW_OUTPUT_STRUCT
	#if PLANE_CLIPPING_ENABLED
		#define UNITY_STANDARD_USE_SHADOW_OUTPUT_STRUCT 1
		#define CLEAR_UNITY_STANDARD_USE_SHADOW_OUTPUT_STRUCT
	#endif
#endif

#ifdef UNITY_STANDARD_USE_SHADOW_OUTPUT_STRUCT 

struct VertexOutputShadowCasterClip
{
	V2F_SHADOW_CASTER_NOPOS
	#if defined(UNITY_STANDARD_USE_SHADOW_UVS)
		float2 tex : TEXCOORD1;

		#if defined(_PARALLAXMAP)
			half3 viewDirForParallax : TEXCOORD2;

			#if PLANE_CLIPPING_ENABLED
				float3 posWorld : TEXCOORD3;
			#endif
		#else // defined(_PARALLAXMAP)
			#if PLANE_CLIPPING_ENABLED
				float3 posWorld : TEXCOORD2;
			#endif // PLANE_CLIPPING_ENABLED
		#endif // defined(_PARALLAXMAP)
	#else // defined(UNITY_STANDARD_USE_SHADOW_UVS)
		#if PLANE_CLIPPING_ENABLED
			float3 posWorld : TEXCOORD1;
		#endif // PLANE_CLIPPING_ENABLED
	#endif // defined(UNITY_STANDARD_USE_SHADOW_UVS)
};
#endif

// We have to do these dances of outputting SV_POSITION separately from the vertex shader,
// and inputting VPOS in the pixel shader, since they both map to "POSITION" semantic on
// some platforms, and then things don't go well.


void vertShadowCasterClip (VertexInput v
	, out float4 opos : SV_POSITION
	#ifdef UNITY_STANDARD_USE_SHADOW_OUTPUT_STRUCT
	, out VertexOutputShadowCasterClip o
	#endif
	#ifdef UNITY_STANDARD_USE_STEREO_SHADOW_OUTPUT_STRUCT
    , out VertexOutputStereoShadowCaster os
    #endif
)
{
	UNITY_SETUP_INSTANCE_ID(v);
	#ifdef UNITY_STANDARD_USE_STEREO_SHADOW_OUTPUT_STRUCT
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(os);
	#endif

	#if PLANE_CLIPPING_ENABLED
		float4 posWorld = mul(unity_ObjectToWorld, v.vertex);
		o.posWorld = posWorld.xyz;
	#endif
	TRANSFER_SHADOW_CASTER_NOPOS(o,opos)
	#if defined(UNITY_STANDARD_USE_SHADOW_UVS)
		o.tex = TRANSFORM_TEX(v.uv0, _MainTex);

		#ifdef _PARALLAXMAP
			TANGENT_SPACE_ROTATION;
			o.viewDirForParallax = mul (rotation, ObjSpaceViewDir(v.vertex));
		#endif
	#endif
}

half4 fragShadowCasterClip (UNITY_POSITION(vpos)
#ifdef UNITY_STANDARD_USE_SHADOW_OUTPUT_STRUCT
	, VertexOutputShadowCasterClip i
#endif
) : SV_Target
{
	PLANE_CLIP(i.posWorld)

	#if defined(UNITY_STANDARD_USE_SHADOW_UVS)
		#if defined(_PARALLAXMAP) && (SHADER_TARGET >= 30)
			half3 viewDirForParallax = normalize(i.viewDirForParallax);
			fixed h = tex2D (_ParallaxMap, i.tex.xy).g;
			half2 offset = ParallaxOffset1Step (h, _Parallax, viewDirForParallax);
			i.tex.xy += offset;
        #endif

		half alpha = tex2D(_MainTex, i.tex).a * _Color.a;
		#if defined(_ALPHATEST_ON)
			clip (alpha - _Cutoff);
		#endif
		#if defined(_ALPHABLEND_ON) || defined(_ALPHAPREMULTIPLY_ON)
			#if defined(_ALPHAPREMULTIPLY_ON)
				half outModifiedAlpha;
				PreMultiplyAlpha(half3(0, 0, 0), alpha, SHADOW_ONEMINUSREFLECTIVITY(i.tex), outModifiedAlpha);
				alpha = outModifiedAlpha;
			#endif
			#if defined(UNITY_STANDARD_USE_DITHER_MASK)
				// Use dither mask for alpha blended shadows, based on pixel position xy
				// and alpha level. Our dither texture is 4x4x16.
				#ifdef LOD_FADE_CROSSFADE
                    #define _LOD_FADE_ON_ALPHA
                    alpha *= unity_LODFade.y;
                #endif
				half alphaRef = tex3D(_DitherMaskLOD, float3(vpos.xy*0.25,alpha*0.9375)).a;
				clip (alphaRef - 0.01);
			#else
				clip (alpha - _Cutoff);
			#endif
		#endif
	#endif // #if defined(UNITY_STANDARD_USE_SHADOW_UVS)

	#ifdef LOD_FADE_CROSSFADE
        #ifdef _LOD_FADE_ON_ALPHA
            #undef _LOD_FADE_ON_ALPHA
        #else
            UnityApplyDitherCrossFade(vpos.xy);
        #endif
    #endif

	SHADOW_CASTER_FRAGMENT(i)
}

#ifdef CLEAR_UNITY_STANDARD_USE_SHADOW_OUTPUT_STRUCT
	#undef CLEAR_UNITY_STANDARD_USE_SHADOW_OUTPUT_STRUCT
	#undef UNITY_STANDARD_USE_SHADOW_OUTPUT_STRUCT
#endif

#endif // UNITY_STANDARD_SHADOW_CLIP_INCLUDED
