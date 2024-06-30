  Shader "Clipping/Box/Bumped Specular" {
    Properties {
      _Color ("Main Color", Color) = (1,1,1,1)
      _SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 0)
	  _Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	  _MainTex ("Texture", 2D) = "white" {}
	  _BumpMap ("Bumpmap", 2D) = "bump" {}
	  
	  _StencilMask("Stencil Mask", Range(0, 255)) = 255
    }
    
    
    SubShader {
		//Tags {"Queue" = "Transparent-1" "RenderType"="Transparent" }
		LOD 200

		Stencil
		{
			Ref [_StencilMask]
			CompBack Always
			PassBack Replace

			CompFront Always
			PassFront Zero
		}
      LOD 400
     
	      
	      CGPROGRAM
	      #pragma surface surf BlinnPhong
		  #pragma multi_compile __ CLIP_BOX CLIP_CORNER
          #include "CGIncludes/section_clipping_CS.cginc"
	      half _Shininess;
	      
	      struct Input {
	          float2 uv_MainTex;
	          float2 uv_BumpMap;
	          float3 worldPos;
	      };
	      sampler2D _MainTex;
	      sampler2D _BumpMap;
	      fixed4 _Color;
	      
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

		  	Cull Off
			ColorMask 0
			CGPROGRAM
			#pragma surface surf NoLighting  noambient
			#pragma multi_compile __ CLIP_BOX
			#pragma shader_feature RETRACT_BACKFACES
			#include "CGIncludes/section_clipping_CS.cginc"

			struct Input {
				float3 worldPos;
			};

			fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
			{
				fixed4 c;
				c.rgb = s.Albedo;
				c.a = s.Alpha;
				return c;
			}

			void surf(Input IN, inout SurfaceOutput o)
			{
				PLANE_CLIPWITHCAPS(IN.worldPos);
				o.Albedo = float3(0,0,0);
				o.Alpha = 1;
			}
			ENDCG

			}

    Fallback "Specular"
  }