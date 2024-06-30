  Shader "CrossSection/Bumped Specular" {
    Properties {
      _Color ("Main Color", Color) = (1,1,1,1)
      _SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 0)
	  _Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	  _MainTex ("Texture", 2D) = "white" {}
	  _BumpMap ("Bumpmap", 2D) = "bump" {}
	  
      _SectionColor ("Section Color", Color) = (1,0,0,1)
      //_SectionPlane ("SectionPlane (x, y, z)", vector) = (0.707,0,-0.2)
      //_SectionPoint ("SectionPoint (x, y, z)", vector) = (0,0,0)
      //_SectionOffset ("SectionOffset",float) = 0
    }
    
    
    SubShader {
      Tags { "RenderType" = "Opaque" }
      LOD 400

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

		  //------------------------ 
	      
	      Cull Back
	      
	      CGPROGRAM
	      #pragma surface surf BlinnPhong
	      fixed4 _Color;
		  #pragma multi_compile __ CLIP_PLANE CLIP_TWO_PLANES
          #include "CGIncludes/section_clipping_CS.cginc"
	      half _Shininess;
	      
	      struct Input {
	          float2 uv_MainTex;
	          float2 uv_BumpMap;
	          float3 worldPos;
	      };
	      sampler2D _MainTex;
	      sampler2D _BumpMap;
	      
	      void surf (Input IN, inout SurfaceOutput o) {
	          PLANE_CLIP(IN.worldPos);
	          fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	          o.Albedo = tex.rgb * _Color.rgb;
	          o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
	          o.Specular = _Shininess;
	          o.Gloss = tex.a;
	          o.Alpha = tex.a * _Color.a;
	      }
	      ENDCG

    } 

    Fallback "Specular"
  }