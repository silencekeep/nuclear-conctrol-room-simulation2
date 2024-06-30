Shader "CrossSection/EdgeEffectMask"
{	
	Properties
	{
		_ColorFront ("ColorFront", Color) = (1, 0, 0, 1)
		_ColorBack("ColorBack", Color) = (0, 1, 0, 1)
	}
	SubShader
	{
		Pass
		{
			Cull Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile __ CLIP_PLANE CLIP_TWO_PLANES CLIP_SPHERE CLIP_CUBE CLIP_CORNER CLIP_TUBES
			#include "CGIncludes/section_clipping_CS.cginc"
			#include "UnityCG.cginc"
			
			// Properties
            sampler2D_float _CameraDepthNormalsTexture;

			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 texCoord : TEXCOORD0;
			};

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float3 texCoord : TEXCOORD0;
                float linearDepth : TEXCOORD1;
                float4 screenPos : TEXCOORD2;
				float4 wpos : TEXCOORD3;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
				float3 worldPos = mul(unity_ObjectToWorld, float4(input.vertex)).xyz;
				output.wpos = float4(worldPos.xyz, 1);
				output.pos = UnityObjectToClipPos(input.vertex);
                output.texCoord = input.texCoord;          
                output.screenPos = ComputeScreenPos(output.pos);
                output.linearDepth = -(UnityObjectToViewPos(input.vertex).z * _ProjectionParams.w);

                return output;
			}

			half4 _ColorFront;
			half4 _ColorBack;

			float4 frag(vertexOutput input, float isFrontFace:VFACE) : COLOR
			{
				PLANE_CLIP(input.wpos);
				float4 c = float4(0, 0, 0, 1);
                // decode depth texture info
				float2 uv = input.screenPos.xy / input.screenPos.w; // normalized screen-space pos
				//float camDepth = SAMPLE_DEPTH_TEXTURE(_CameraDepthNormalsTexture, uv);
				float4 enc = tex2D(_CameraDepthNormalsTexture, uv);
				float camDepth = DecodeFloatRG(enc.zw);
				//camDepth = Linear01Depth (camDepth); // converts z buffer value to depth value from 0..1
                float diff = saturate(input.linearDepth - camDepth);
                if(diff < 0.001)
                c = isFrontFace > 0? _ColorFront : _ColorBack;

                return c;
                //return float4(camDepth, camDepth, camDepth, 1); // test camera depth value
                //return float4(input.linearDepth, input.linearDepth, input.linearDepth, 1); // test our depth
                //return float4(diff, diff, diff, 1);
			}

			ENDCG
		}
    }
}