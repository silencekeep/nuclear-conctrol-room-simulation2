Shader "CrossSection/Lightmapped/Diffuse" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_LightMap ("Lightmap (RGB)", 2D) = "black" {}

	_SectionColor ("Section Color", Color) = (1,0,0,1)
}


SubShader {
	LOD 200
	Tags { "RenderType" = "Opaque" }

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
			if( _SectionColor.a <0.5f) discard;
			PLANE_CLIP(IN.worldPos);
			fixed4 c = _SectionColor;
			o.Albedo = c.rgb;
			o.Emission =  c.rgb;
		}
		ENDCG 

		//-----------------------------------------------

		Cull back
		CGPROGRAM

		#pragma surface surf Lambert nodynlightmap

		 #pragma multi_compile __ CLIP_PLANE CLIP_TWO_PLANES
		 
		 #include "CGIncludes/section_clipping_CS.cginc"

		struct Input {
		  float2 uv_MainTex;
		  float2 uv2_LightMap;
		  float3 worldPos;
		};
		sampler2D _MainTex;
		sampler2D _LightMap;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o)
		{
		  PLANE_CLIP(IN.worldPos);
		  o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * _Color;
		  half4 lm = tex2D (_LightMap, IN.uv2_LightMap);
		  o.Emission = lm.rgb*o.Albedo.rgb;
		  o.Alpha = lm.a * _Color.a;
		}
		ENDCG
	}
FallBack "Legacy Shaders/Lightmapped/VertexLit"
}
