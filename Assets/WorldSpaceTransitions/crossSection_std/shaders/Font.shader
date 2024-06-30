// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Clipping/GUI/Text Shader" {
	Properties{
		_MainTex("Font Texture", 2D) = "white" {}
		_Color("Text Color", Color) = (1,1,1,1)
	}

		SubShader{

			Tags {
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
			}
			Lighting Off Cull Off ZTest Always ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			Pass {
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
				#pragma multi_compile __ CLIP_PLANE CLIP_SPHERE CLIP_BOX CLIP_CORNER
				#include "UnityCG.cginc"
				#include "CGIncludes/section_clipping_CS.cginc"

				struct appdata_t {
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float2 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f {
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord : TEXCOORD0;
					UNITY_VERTEX_OUTPUT_STEREO
				#if CLIP_PLANE || CLIP_SPHERE || CLIP_BOX || CLIP_CORNER
					float3	worldPos	: TEXCOORD1;
				#endif
				};

				sampler2D _MainTex;
				uniform float4 _MainTex_ST;
				uniform fixed4 _Color;

				v2f vert(appdata_t v)
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.color = v.color * _Color;
					o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				#if CLIP_PLANE || CLIP_SPHERE || CLIP_BOX || CLIP_CORNER
					o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					PLANE_CLIP(i.worldPos);
					//if (i.worldPos.x < 0) discard;
					fixed4 col = i.color;
					col.a *= tex2D(_MainTex, i.texcoord).a;
					return col;
				}
				ENDCG
			}
		}
}

