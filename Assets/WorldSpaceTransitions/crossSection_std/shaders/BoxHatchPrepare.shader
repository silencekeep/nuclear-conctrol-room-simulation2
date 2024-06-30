Shader "Clipping/Box/HatchPrepare" {
	Properties{

	_StencilMask("Stencil Mask", Range(0, 255)) = 255
	[HideInInspector][Toggle] _inverse("inverse", Float) = 0
	[Enum(None,0,Alpha,1,Red,8,Green,4,Blue,2,RGB,14,RGBA,15)] _ColorMask("Color Mask", Int) = 15
	[Toggle(RETRACT_BACKFACES)] _retractBackfaces("retractBackfaces", Float) = 0 

	//[HideInInspector] _SectionPoint("_SectionPoint", Vector) = (0,0,0,1)	//expose as local properties
	//[HideInInspector] _SectionPlane("_SectionPlane", Vector) = (1,0,0,1)	//expose as local properties
	//[HideInInspector] _SectionPlane2("_SectionPlane2", Vector) = (1,0,0,1)	//expose as local properties

	//[HideInInspector] _SectionCentre("_SectionCentre", Vector) = (0,0,0,1)	//expose as local properties
	//[HideInInspector] _SectionDirX("_SectionDirX", Vector) = (1,0,0,1)	//expose as local properties
	//[HideInInspector] _SectionDirY("_SectionDirY", Vector) = (0,1,0,1)	//expose as local properties
	//[HideInInspector] _SectionDirZ("_SectionDirZ", Vector) = (0,0,1,1)	//expose as local properties
	//[HideInInspector] _SectionScale("_SectionScale", Vector) = (0,0,1,1)	//expose as local properties
	
	}
	SubShader {
		Tags {"Queue" = "Geometry" "RenderType"="Transparent" }
		LOD 200

		Stencil
		{
			Ref [_StencilMask]
			CompBack Always
			PassBack Replace

			CompFront Always
			PassFront Zero
		}

		
	Cull Off
	ColorMask [_ColorMask]

	CGPROGRAM
	#pragma surface surf NoLighting  noambient vertex:vert
	#pragma multi_compile __ CLIP_BOX CLIP_CORNER CLIP_TWO_PLANES
	//#pragma multi_compile_local __ CLIP_BOX CLIP_TWO_PLANES // to get enumerated keywords as local.
	#pragma shader_feature RETRACT_BACKFACES
	#include "CGIncludes/section_clipping_CS.cginc"

	struct Input {
		float3 worldPos;
	};

	half _BackfaceExtrusion;

	void vert (inout appdata_full v) {
		#if RETRACT_BACKFACES
		float3 viewDir = ObjSpaceViewDir(v.vertex);
		float dotProduct = dot(v.normal, viewDir);
		if(dotProduct<0) {
			float3 worldPos = mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1)).xyz;
			float3 worldNorm = UnityObjectToWorldNormal(v.normal);
			worldPos -= worldNorm * _BackfaceExtrusion;
			v.vertex.xyz = mul(unity_WorldToObject, float4(worldPos, 1)).xyz;
		}
		#endif
	}

	fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
	{
		fixed4 c;
		c.rgb = s.Albedo;
		c.a = s.Alpha;
		return c;
	}

	void surf(Input IN, inout SurfaceOutput o)
	{
		#if CLIP_BOX || CLIP_TWO_PLANES
		PLANE_CLIPWITHCAPS(IN.worldPos);
		#else
		PLANE_CLIP(IN.worldPos);
		#endif
		o.Albedo = float3(1,1,1);
		o.Alpha = 1;
	}
	ENDCG

	}
		FallBack "Diffuse"
	
}
