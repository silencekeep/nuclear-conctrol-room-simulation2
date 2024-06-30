Shader "CrossSection/Decal" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_DecalTex ("Decal (RGBA)", 2D) = "black" {}

	_SectionColor ("Section Color", Color) = (1,0,0,1)
    //_SectionPlane ("SectionPlane (x, y, z)", vector) = (0.707,0,-0.2)
    //_SectionPoint ("SectionPoint (x, y, z)", vector) = (0,0,0)
    //_SectionOffset ("SectionOffset",float) = 0
}

SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 250
	

				//  crossection pass (backfaces + fog)
        Cull front // cull only front faces
         
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

		//------------------------

		Cull back
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		sampler2D _DecalTex;
		fixed4 _Color;
		#pragma multi_compile __ CLIP_PLANE CLIP_TWO_PLANES CLIP_SPHERE CLIP_CUBE
		 
		#include "CGIncludes/section_clipping_CS.cginc"

		struct Input {
			float2 uv_MainTex;
			float2 uv_DecalTex;
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			PLANE_CLIP(IN.worldPos);
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			half4 decal = tex2D(_DecalTex, IN.uv_DecalTex);
			c.rgb = lerp (c.rgb, decal.rgb, decal.a);
			c *= _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
		}

	Fallback "Legacy Shaders/Diffuse"
	}
