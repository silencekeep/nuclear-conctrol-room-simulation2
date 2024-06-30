  Shader "CrossSection/Bumped Diffuse" {
    Properties {
      _Color ("Main Color", Color) = (1,1,1,1)
      _MainTex ("Texture", 2D) = "white" {}
      _BumpMap ("Bumpmap", 2D) = "bump" {}
      
      _SectionColor ("Section Color", Color) = (1,0,0,1)
    }

    SubShader {
      	Tags { "RenderType" = "Opaque" }
		LOD 300
	      
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
	    fixed4 _Color;
		#pragma multi_compile __ CLIP_PLANE CLIP_TWO_PLANES CLIP_SPHERE CLIP_CUBE
		 
		#include "CGIncludes/section_clipping_CS.cginc"
	      
	    struct Input {
			float2 uv_MainTex;
	        float2 uv_BumpMap;
	        float3 worldPos;
	    };
	    sampler2D _MainTex;
	    sampler2D _BumpMap;
	      
	    void surf (Input IN, inout SurfaceOutput o) {
			PLANE_CLIP(IN.worldPos);
	        o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * _Color;
	        o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
	    }
	    ENDCG
	      
    } 

    FallBack "Legacy Shaders/Diffuse"
  }