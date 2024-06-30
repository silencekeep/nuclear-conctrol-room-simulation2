Shader "Hidden/CrossSection/Normals Texture"
{
    Properties
    {
    }
    SubShader
    {
        Tags 
		{ 
			"RenderType" = "Clipping" 
		}
		Cull Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#pragma multi_compile __ CLIP_PLANE CLIP_TWO_PLANES CLIP_SPHERE CLIP_CUBE CLIP_TUBES
			#include "CGIncludes/section_clipping_CS.cginc"
			#include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
				float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
				float3 viewNormal : NORMAL;
				float4 wpos : TEXCOORD1;
				UNITY_VERTEX_OUTPUT_STEREO
            };

            //sampler2D _MainTex;
            //float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				float3 worldPos = mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1)).xyz;
				o.wpos = float4(worldPos.xyz, 1);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.viewNormal = COMPUTE_VIEW_NORMAL;
				//o.viewNormal = mul((float3x3)UNITY_MATRIX_M, v.normal);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
				PLANE_CLIP(i.wpos);
				return float4(normalize(i.viewNormal) * 0.5 + 0.5, 0);
            }
            ENDCG
        }
    }
}
