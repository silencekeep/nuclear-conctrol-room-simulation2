Shader "CrossSection/SurfaceShader/Standard/HatchPrepare2Passes" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		_SectionColor ("Section Color", Color) = (1,0,0,1)
		_StencilMask("Stencil Mask", Range(0, 255)) = 1
		[Toggle] _inverse("inverse", Float) = 0
		//[Toggle(RETRACT_BACKFACES)] _retractBackfaces("retractBackfaces", Float) = 0

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

		Pass
		{

			Cull Front
			//ColorMask 0

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile  __ CLIP_PLANE CLIP_BOX CLIP_CORNER
			#include "UnityCG.cginc"
			#include "CGIncludes/section_clipping_CS.cginc"
			fixed4 _SectionColor;
			struct appdata {
				float4 vertex : POSITION;
			};
			struct v2f {
				float4 pos : SV_POSITION;
				float3 wpos : TEXCOORD1;
			};

			v2f vert(appdata_full v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.wpos = worldPos;
				return o;
			}
			half4 frag(v2f i) : SV_Target {
			#if CLIP_BOX
				PLANE_CLIPWITHCAPS(i.wpos);
			#else
				PLANE_CLIP(i.wpos);
			#endif
				return _SectionColor;
			}
			ENDCG

		}

		Cull back
		Offset 1, 0
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard addshadow
		#pragma multi_compile __ CLIP_PLANE
		//#pragma shader_feature RETRACT_BACKFACES
		#include "CGIncludes/section_clipping_CS.cginc"

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
			//float myface: VFACE;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		//fixed4 _SectionColor;

		void surf (Input IN, inout SurfaceOutputStandard o) {

			//if(IN.myface<0&&_SectionColor.a<0.5) discard; else

			#if CLIP_BOX
			PLANE_CLIPWITHCAPS(IN.worldPos);
			#else
			PLANE_CLIP(IN.worldPos);
			#endif

			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			
			// Metallic and smoothness come from slider variables

			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;

		}
		ENDCG
	}
	FallBack "Diffuse"
}
