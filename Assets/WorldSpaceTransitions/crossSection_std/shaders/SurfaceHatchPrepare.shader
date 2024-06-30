Shader "CrossSection/SurfaceShader/Standard/HatchPrepare" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		_SectionColor ("Section Color", Color) = (1,0,0,1)
		_StencilMask("Stencil Mask", Range(0, 255)) = 1
		[Toggle] _inverse("inverse", Float) = 0
		[Toggle(RETRACT_BACKFACES)] _retractBackfaces("retractBackfaces", Float) = 0

		//[HideInInspector] _SectionPoint("_SectionPoint", Vector) = (0,0,0,1)	//expose as local properties
		//[HideInInspector] _SectionPlane("_SectionPlane", Vector) = (1,0,0,1)	//expose as local properties

		//[HideInInspector] _SectionCentre("_SectionCentre", Vector) = (0,0,0,1)	//expose as local properties
		//[HideInInspector] _SectionDirX("_SectionDirX", Vector) = (1,0,0,1)	//expose as local properties
		//[HideInInspector] _SectionDirY("_SectionDirY", Vector) = (0,1,0,1)	//expose as local properties
		//[HideInInspector] _SectionDirZ("_SectionDirZ", Vector) = (0,0,1,1)	//expose as local properties
	}
	SubShader {
		Tags { "RenderType"="Clipping" }
		LOD 200

		// ------------------------------------------------------------------
		Stencil
		{
			Ref [_StencilMask]
			CompBack Always
			PassBack Replace

			CompFront Always
			PassFront Zero
		}

		Cull off
				
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard addshadow vertex:vert
		#pragma multi_compile __ CLIP_PLANE CLIP_CORNER CLIP_CUBE
		//#pragma multi_compile_local __ CLIP_PLANE CLIP_CORNER // to get enumerated keywords as local.
		#pragma shader_feature_local RETRACT_BACKFACES
		#include "CGIncludes/section_clipping_CS.cginc"

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
			float myface: VFACE;
		};

		half _BackfaceExtrusion;

		void vert (inout appdata_full v, out Input data) {
			UNITY_INITIALIZE_OUTPUT(Input,data);
			//data.myface = dot(v.normal, ObjSpaceViewDir(v.vertex));
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
		half _Metallic;
		fixed4 _Color;
		fixed4 _SectionColor;

		void surf (Input IN, inout SurfaceOutputStandard o) {

			if(IN.myface<0&&_SectionColor.a<0.5) discard; else

			PLANE_CLIP(IN.worldPos);

			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			
			// Metallic and smoothness come from slider variables
		if(IN.myface>0) 
		{
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		else
		{
			o.Albedo = float3(0,0,0);
			o.Emission = _SectionColor.rgb;
			o.Smoothness = float3(0,0,0);
			o.Metallic = float3(0,0,0);

		}

		}
		ENDCG
	}
	FallBack "Diffuse"
}
