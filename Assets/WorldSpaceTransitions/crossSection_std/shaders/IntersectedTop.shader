Shader "CrossSection/Others/IntersectedTop" {

	Properties{
		_Color ("Color", Color) = (1,1,1,1)
		_StencilMask("Stencil Mask", Range(0, 255)) = 1
	}

    SubShader {
		Tags { "RenderType"="Clipping" "Queue"="Geometry+2"}
			Stencil
	{
		Ref [_StencilMask]
		Comp Always
		Pass Replace
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
            CGPROGRAM
			#include "CGIncludes/section_clipping_CS.cginc"
			#pragma multi_compile __ CLIP_PLANE CLIP_TWO_PLANES CLIP_SPHERE CLIP_CUBE CLIP_TUBES CLIP_BOX
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    } 
}