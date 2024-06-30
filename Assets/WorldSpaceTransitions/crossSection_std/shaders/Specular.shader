Shader "CrossSection/Specular" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}

	_SectionColor ("Section Color", Color) = (1,0,0,1)

}




SubShader {
	LOD 300
	Tags { "RenderType"="Opaque" }


	   //  crossection pass (backfaces + fog)
        Cull front // cull only front faces
         
		CGPROGRAM
		#pragma surface surf Lambert nodynlightmap 
		#pragma multi_compile __ CLIP_PLANE CLIP_TWO_PLANES
		#include "UnityCG.cginc"
		#include "CGIncludes/section_clipping_CS.cginc"

		fixed4 _SectionColor;

		struct Input {
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			PLANE_CLIP(IN.worldPos);
			fixed4 c = _SectionColor;
			o.Emission =  c.rgb;
		}
		ENDCG 

		//-----------------------------------------------

		Cull back
		CGPROGRAM
		#pragma surface surf BlinnPhong

		sampler2D _MainTex;
		fixed4 _Color;
		half _Shininess;
		 #pragma multi_compile __ CLIP_PLANE CLIP_TWO_PLANES
		 
		 #include "CGIncludes/section_clipping_CS.cginc"

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			PLANE_CLIP(IN.worldPos);
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = tex.rgb * _Color.rgb;
			o.Gloss = tex.a;
			o.Alpha = tex.a * _Color.a;
			o.Specular = _Shininess;
		}
		ENDCG
	}

	Fallback "Legacy Shaders/VertexLit"
	}