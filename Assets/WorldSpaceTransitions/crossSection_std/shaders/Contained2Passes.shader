Shader "CrossSection/Contained2Passes"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		[Enum(UnityEngine.Rendering.StencilOp)] _FrontStencilOp("Front Stencil Operation", Int) = 2
		[Enum(UnityEngine.Rendering.StencilOp)] _BackStencilOp("Back Stencil Operation", Int) = 2
		_StencilMaskFront("Stencil Mask Front", Range(0, 255)) = 1
		_StencilMaskBack("Stencil Mask Back", Range(0, 255)) = 2
	}

	SubShader
	{
		Tags { "RenderType" = "Clipping" }
		LOD 100

		Cull Front
		//Colormask 0
		Stencil
		{
			Ref[_StencilMaskBack]
			PassBack [_BackStencilOp]
			//PassFront [_FrontStencilOp]
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile __ CLIP_PLANE
			#include "CGIncludes/section_clipping_CS.cginc"
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 wpos : TEXCOORD1;
			};

			fixed4 _Color;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.wpos = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				PLANE_CLIP(i.wpos.xyz);
				return _Color;
			}
			ENDCG
		}

		Cull Back
		Stencil
		{
			Ref[_StencilMaskFront]
			//PassBack[_BackStencilOp]
			PassFront [_FrontStencilOp]
			}

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile __ CLIP_PLANE
				#include "CGIncludes/section_clipping_CS.cginc"
				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
				};

				struct v2f
				{
					float4 vertex : SV_POSITION;
					float4 wpos : TEXCOORD1;
				};

				fixed4 _Color;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.wpos = mul(unity_ObjectToWorld, v.vertex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					PLANE_CLIP(i.wpos.xyz);
					return _Color;
				}
				ENDCG
			}
	}
	Fallback "Legacy Shaders/VertexLit"
}
