Shader "CrossSection/Unlit/Configurable"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Color("Main Color", Color) = (1,1,1,1)
		[Enum(UnityEngine.Rendering.CullMode)] _Cull("__cull", Float) = 2.0
		[Enum(None,0,Alpha,1,Red,8,Green,4,Blue,2,RGB,14,RGBA,15)] _ColorMask("Color Mask", Int) = 15

		_StencilMask("Stencil Mask", Range(0, 255)) = 255
		[Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp("Stencil Comparison", Int) = 3
		[Enum(UnityEngine.Rendering.StencilOp)] _StencilOp("Stencil Operation", Int) = 0
		[Enum(Off,0,On,1)] _ZWrite("ZWrite", Int) = 1
		//[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Int) = 4*/
	}
		SubShader
		{
			//Tags { "RenderType"="Opaque" }
			LOD 200
		Stencil
		{
			Ref[_StencilMask]
			CompBack Always
			PassBack Replace

			CompFront Always
			PassFront Zero
			//PassFront Zero
			//Fail [_StencilFail]
			//ZFail [_StencilZFail]
		}
		ColorMask[_ColorMask]
		ZWrite[_ZWrite]
		//ZTest[_ZTest]
		Cull [_Cull]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
			#pragma multi_compile __ CLIP_PLANE
			#include "CGIncludes/section_clipping_CS.cginc"

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				float3 worldPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				#if CLIP_PLANE
				PLANE_CLIP(i.worldPos);
				#endif
				// sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
