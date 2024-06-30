Shader "CrossSection/Reflective/Specular" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	_ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	_Cube ("Reflection Cubemap", Cube) = "_Skybox" {}

	_SectionColor ("Section Color", Color) = (1,0,0,1)
    //_SectionPlane ("SectionPlane (x, y, z)", vector) = (0.707,0,-0.2)
    //_SectionPoint ("SectionPoint (x, y, z)", vector) = (0,0,0)
    //_SectionOffset ("SectionOffset",float) = 0

}




SubShader {
	LOD 300
	Tags { "RenderType"="Opaque" }


		//  crossection pass (backfaces)
		Cull front
		CGPROGRAM
		#pragma surface surf Lambert nodynlightmap 
		#pragma multi_compile __ CLIP_PLANE CLIP_TWO_PLANES CLIP_SPHERE CLIP_CUBE
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

		// --------------------------------------------------------------------

		Cull back // cull only front faces
		CGPROGRAM
		#pragma surface surf BlinnPhong

		sampler2D _MainTex;
		samplerCUBE _Cube;

		fixed4 _Color;
		fixed4 _ReflectColor;
		 #pragma multi_compile __ CLIP_PLANE CLIP_TWO_PLANES CLIP_SPHERE CLIP_CUBE
		 
		 #include "CGIncludes/section_clipping_CS.cginc"
		half _Shininess;

		struct Input {
			float2 uv_MainTex;
			float3 worldRefl;
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			PLANE_CLIP(IN.worldPos);
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 c = tex * _Color;
			o.Albedo = c.rgb;
			o.Gloss = tex.a;
			o.Specular = _Shininess;
	
			fixed4 reflcol = texCUBE (_Cube, IN.worldRefl);
			reflcol *= tex.a;
			o.Emission = reflcol.rgb * _ReflectColor.rgb;
			o.Alpha = reflcol.a * _ReflectColor.a;
		}
		ENDCG
		}

	FallBack "Legacy Shaders/Reflective/VertexLit"
	}