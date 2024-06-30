Shader "CrossSection/Others/CapPrepareUnlit" {

	Properties{
		_Color ("Color", Color) = (1,1,1,1)
		_StencilMask("Stencil Mask", Range(0, 255)) = 0
		[Enum(None,0,Alpha,1,Red,8,Green,4,Blue,2,RGB,14,RGBA,15)] _ColorMask("Color Mask", Int) = 15
		[Enum(UnityEngine.Rendering.CullMode)] _Culling("Cull Mode", Int) = 2
		[Enum(UnityEngine.Rendering.StencilOp)] _StencilOp("Stencil Operation", Int) = 2
	}


    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Geometry"}
		//ZWrite off
		ColorMask[_ColorMask]
        Stencil {
            Ref [_StencilMask]
            Comp always
			Pass[_StencilOp]
        }
		
		Pass {
			Cull[_Culling]
			//ZTest Less
			Name "Unlit"
			CGPROGRAM
			#include "CGIncludes/section_clipping_CS.cginc"
			#pragma multi_compile __ CLIP_PLANE CLIP_TWO_PLANES CLIP_SPHERE CLIP_CORNER CLIP_TUBES CLIP_BOX CLIP_CUBE
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}

        CGINCLUDE
		#include "CGIncludes/section_clipping_CS.cginc"
		float4 _Color;
            struct appdata {
                float4 vertex : POSITION;
            };
            struct v2f {
                float4 pos : SV_POSITION;
				float3 wpos: TEXCOORD0;
            };
            v2f vert(appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
				float3 worldPos = mul (unity_ObjectToWorld, v.vertex).xyz;
				o.wpos = worldPos;
                return o;
            }
            half4 frag(v2f i) : SV_Target {
				PLANE_CLIP(i.wpos);
                return _Color;
            }
        ENDCG

    } 
}