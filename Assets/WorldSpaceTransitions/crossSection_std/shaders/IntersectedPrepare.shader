Shader "CrossSection/Others/IntersectedPrepare" {

	Properties{
		_Color ("Color", Color) = (1,1,1,1)
		[HideInInspector] _StencilMask("Stencil Mask", Range(0, 255)) = 0
		[Enum(None,0,Alpha,1,Red,8,Green,4,Blue,2,RGB,14,RGBA,15)] _ColorMask("Color Mask", Int) = 15
	}


    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Geometry+1"}
		ZWrite off
		ColorMask[_ColorMask]
        Stencil {
            Ref [_StencilMask]
            Comp always
			PassBack IncrSat
			PassFront DecrSat
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


        Pass {
            Cull Front
         //ZTest Less
        
            CGPROGRAM
			#include "CGIncludes/section_clipping_CS.cginc"
			#pragma multi_compile __ CLIP_PLANE CLIP_TWO_PLANES CLIP_SPHERE CLIP_CORNER CLIP_TUBES CLIP_BOX
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }

	    Pass {

            //ZTest Less
            CGPROGRAM
			#include "CGIncludes/section_clipping_CS.cginc"
			#pragma multi_compile __ CLIP_PLANE CLIP_TWO_PLANES CLIP_SPHERE CLIP_CORNER CLIP_TUBES CLIP_BOX
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    } 
}