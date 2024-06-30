Shader "SpaceMask/SurfaceTriplanar/TransparentSpecular"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Color2("Main Color2", Color) = (1,1,1,1)
		_MainTex2("Base2 (RGB)", 2D) = "white" {}
		_Glossiness2("Smoothness2", Range(0,1)) = 0.5

		[Enum(UnityEngine.Rendering.CullMode)] _Culling("Cull Mode", Int) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" }

		LOD 100

		Cull[_Culling]

		CGPROGRAM
		#pragma surface surf StandardSpecular alpha:fade
		//#pragma surface surf Lambert
		#pragma multi_compile __ CLIP_SPHERES
		#pragma multi_compile __ CLIP_PLANE
		#pragma multi_compile __ CLIP_BOX
		#include "CGIncludes/section_clipping_CS.cginc"


		sampler2D _MainTex;
		float4 _MainTex_ST;
		half _Glossiness;
		fixed4 _Color;
		sampler2D _MainTex2;
		float4 _MainTex2_ST;
		half _Glossiness2;
		fixed4 _Color2;

#if !CLIP_BOX
		uniform float4x4 _WorldToBoxMatrix;
#endif
		static const float3x3 projMatrix = float3x3(_WorldToBoxMatrix[0].xyz, _WorldToBoxMatrix[1].xyz, _WorldToBoxMatrix[2].xyz);

	      
		struct Input {
			float3 worldNormal;
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutputStandardSpecular o) {
	        
			half _MGlossiness;
			fixed4 _MColor;

			bool _masked_out = false;
#if CLIP_SPHERES || CLIP_PLANE || CLIP_BOX
			_masked_out = OUT_MASKED(IN.worldPos);
#endif
			if (_masked_out)
			{
				//_Tex = _MainTex;
				//_Tex_ST = _MainTex_ST;
				_MGlossiness = _Glossiness2;
				_MColor = _Color2;
			}
			else
			{
				//_Tex = _MainTex2;
				//_Tex_ST = _MainTex2_ST;
				_MGlossiness = _Glossiness;
				_MColor = _Color;
			}

			float2 UV;
			fixed4 c;
			float3 projPos = mul(projMatrix, IN.worldPos);
			float3 projNorm = mul(projMatrix, IN.worldNormal);

			if(abs(projNorm.x)>abs(projNorm.y)&&abs(projNorm.x)>abs(projNorm.z))
			{
				UV = projPos.zy; // side
				if (_masked_out)
				{
					c = tex2D(_MainTex2, float2(UV.x*_MainTex2_ST.x, UV.y*_MainTex2_ST.y)); // use WALLSIDE texture
				}
				else
				{
					c = tex2D(_MainTex, float2(UV.x*_MainTex_ST.x, UV.y*_MainTex_ST.y)); // use WALLSIDE texture
				}
			}
			else if(abs(projNorm.z)>abs(projNorm.y)&&abs(projNorm.z)>abs(projNorm.x))
			{
				UV = projPos.xy; // front
				if (_masked_out)
				{
					c = tex2D(_MainTex2, float2(UV.x*_MainTex2_ST.x, UV.y*_MainTex2_ST.y)); // use WALL texture
				}
				else
				{
					c = tex2D(_MainTex, float2(UV.x*_MainTex_ST.x, UV.y*_MainTex_ST.y)); // use WALL texture
				}
			}
			else
			{
				UV = projPos.xz; // top
				if (_masked_out)
				{
					c = tex2D(_MainTex2, float2(UV.x*_MainTex2_ST.x, UV.y*_MainTex2_ST.y)); // use FLR texture
				}
				else
				{
					c = tex2D(_MainTex, float2(UV.x*_MainTex_ST.x, UV.y*_MainTex_ST.y)); // use FLR texture
				}
			}
			o.Albedo = c.rgb * _MColor.rgb;
			o.Smoothness = _MGlossiness;
			o.Alpha = c.a * _MColor.a;
		}
		ENDCG
	}
	Fallback "Legacy Shaders/Transparent/VertexLit"
}
