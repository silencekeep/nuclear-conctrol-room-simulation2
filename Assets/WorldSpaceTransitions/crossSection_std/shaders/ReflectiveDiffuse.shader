Shader "CrossSection/Reflective/Diffuse" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
	_MainTex ("Base (RGB) RefStrength (A)", 2D) = "white" {} 
	_Cube ("Reflection Cubemap", Cube) = "_Skybox" {}

	_SectionColor ("Section Color", Color) = (1,0,0,1)

}


SubShader {
	LOD 200
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


			Cull back
			CGPROGRAM
			#pragma surface surf Lambert

			sampler2D _MainTex;
			samplerCUBE _Cube;

			fixed4 _Color;
			fixed4 _ReflectColor;
			#pragma multi_compile __ CLIP_PLANE CLIP_TWO_PLANES CLIP_SPHERE  CLIP_CUBE
		 
			#include "CGIncludes/section_clipping_CS.cginc"

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
	
				fixed4 reflcol = texCUBE (_Cube, IN.worldRefl);
				reflcol *= tex.a;
				o.Emission = reflcol.rgb * _ReflectColor.rgb;
				o.Alpha = reflcol.a * _ReflectColor.a;
			}
			ENDCG
		}
	
		FallBack "Legacy Shaders/Reflective/VertexLit"
	} 