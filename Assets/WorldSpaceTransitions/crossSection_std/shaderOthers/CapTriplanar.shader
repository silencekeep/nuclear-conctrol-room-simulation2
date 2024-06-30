Shader "CrossSection/Cap/Triplanar"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_StencilMask("Stencil Mask", Range(0, 255)) = 255
		[Enum(UnityEngine.Rendering.StencilOp)] _StencilOp("Stencil Operation", Int) = 2
		[Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp("Stencil Comparison", Int) = 3 //equal
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" }

		LOD 200
		
		Stencil{
			Ref [_StencilMask]
			Comp [_StencilComp]
			Pass[_StencilOp]
		}


		CGPROGRAM
		#pragma surface surf Standard alpha:fade
		//#include "UnityCG.cginc"

		fixed4 _Color;
		sampler2D _MainTex;
		float4 _MainTex_ST;
		uniform float4x4 _WorldToBoxMatrix;

		//static const float3x3 projMatrix = float3x3(_SectionDirX.xyz, _SectionDirY.xyz, _SectionDirZ.xyz);
		static const float3x3 projMatrix = float3x3(_WorldToBoxMatrix[0].xyz, _WorldToBoxMatrix[1].xyz, _WorldToBoxMatrix[2].xyz);
	      
		struct Input {
			float3 worldNormal;
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
	        
			float2 UV;
			fixed4 c;
			float3 projPos = mul(projMatrix, IN.worldPos);
			float3 projNorm = mul(projMatrix, IN.worldNormal);

			if(abs(projNorm.x)>abs(projNorm.y)&&abs(projNorm.x)>abs(projNorm.z))
			{
				UV = projPos.zy; // side
				c = tex2D(_MainTex, float2(UV.x*_MainTex_ST.x, UV.y*_MainTex_ST.y)); // use WALLSIDE texture
			}
			else if(abs(projNorm.z)>abs(projNorm.y)&&abs(projNorm.z)>abs(projNorm.x))
			{
				UV = projPos.xy; // front
				c = tex2D(_MainTex, float2(UV.x*_MainTex_ST.x, UV.y*_MainTex_ST.y)); // use WALL texture
			}
			else
			{
				UV = projPos.xz; // top
				c = tex2D(_MainTex, float2(UV.x*_MainTex_ST.x, UV.y*_MainTex_ST.y)); // use FLR texture
			}
			o.Albedo = c.rgb *_Color.rgb;
			o.Alpha = c.a *_Color.a;
			
		}
		ENDCG
	}
	Fallback "Legacy Shaders/Transparent/VertexLit"
}
