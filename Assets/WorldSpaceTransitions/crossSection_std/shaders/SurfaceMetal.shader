Shader "CrossSection/SurfaceShader/Metal" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_MetallicGlossMap("Metallic", 2D) = "white" {}

		_SectionColor ("Section Color", Color) = (1,0,0,1)
		[Toggle] _inverse("inverse", Float) = 0
		[Toggle(RETRACT_BACKFACES)] _retractBackfaces("retractBackfaces", Float) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		Cull off
				
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard addshadow vertex:vert
		#pragma multi_compile __ CLIP_PLANE CLIP_TWO_PLANES CLIP_SPHERE CLIP_CUBE CLIP_TUBES CLIP_BOX
		#pragma shader_feature RETRACT_BACKFACES 
		#include "CGIncludes/section_clipping_CS.cginc"

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _MetallicGlossMap;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
			float myface : VFACE;
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

		half _Glossiness;
		fixed4 _Color;
		fixed4 _SectionColor;

		void surf (Input IN, inout SurfaceOutputStandard o) {

			if(IN.myface<0&&_SectionColor.a<0.5) discard; else 
			
			#if CLIP_BOX
			PLANE_CLIPWITHCAPS(IN.worldPos);
			#else
			PLANE_CLIP(IN.worldPos);
			#endif

			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			if(IN.myface>0) o.Albedo = c.rgb;
			// smoothness come from slider variables, metallic from map
			fixed4 metal = tex2D(_MetallicGlossMap, IN.uv_MainTex);
			if(IN.myface>0) 
			{
				o.Metallic = metal.r;
				o.Smoothness = metal.a * _Glossiness;
				o.Alpha = c.a;
			}
			else
			{
				o.Albedo = float3(0,0,0);
				o.Emission = _SectionColor.rgb;
			}
		}
		ENDCG
	}
	FallBack "Diffuse"
}
