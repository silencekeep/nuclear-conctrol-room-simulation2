Shader "CrossSection/Diffuse" {
    Properties {
      _Color ("Main Color", Color) = (1,1,1,1)
	  _MainTex ("Texture", 2D) = "white" {}
	  
      _SectionColor ("Section Color", Color) = (1,0,0,1)
      //_SectionPlane ("SectionPlane (x, y, z)", vector) = (0.707,0,-0.2)
      //_SectionPoint ("SectionPoint (x, y, z)", vector) = (0,0,0)
      //_SectionOffset ("SectionOffset",float) = 0
    }
    
    
    SubShader {
    	Tags { "RenderType"="Opaque" }
		LOD 200

		//  crossection pass (backfaces)

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
			o.Albedo = c.rgb;
			o.Emission =  c.rgb;
			o.Alpha = c.a;
		}
		ENDCG

		//------------------------

		Cull back
		CGPROGRAM
		#pragma surface surf Lambert
		#pragma multi_compile __ CLIP_PLANE CLIP_TWO_PLANES CLIP_SPHERE CLIP_CUBE
		#include "UnityCG.cginc"
		#include "CGIncludes/section_clipping_CS.cginc"

		sampler2D _MainTex;
		fixed4 _Color;
	      
		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutput o) {
	          
			PLANE_CLIP(IN.worldPos);
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = tex.rgb * _Color.rgb;

			o.Alpha = tex.a * _Color.a;
		}
		ENDCG

	} 

    Fallback "Legacy Shaders/VertexLit"
}