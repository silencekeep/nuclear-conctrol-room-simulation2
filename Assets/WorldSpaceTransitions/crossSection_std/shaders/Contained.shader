Shader "CrossSection/Contained"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		[Enum(UnityEngine.Rendering.StencilOp)] _FrontStencilOp("Front Stencil Operation", Int) = 2
		[Enum(UnityEngine.Rendering.StencilOp)] _BackStencilOp("Back Stencil Operation", Int) = 6
		_StencilMask("Stencil Mask", Range(0, 255)) = 1
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Cull Off
		Stencil
		{
			Ref[_StencilMask]
			PassBack [_BackStencilOp]
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
