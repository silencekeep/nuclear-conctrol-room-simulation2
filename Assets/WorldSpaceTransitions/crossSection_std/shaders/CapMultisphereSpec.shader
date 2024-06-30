Shader "CrossSection/Cap/MultisphereExampleSpecular"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		//_StencilMask("Stencil Mask", Range(0, 255)) = 255
		[Enum(UnityEngine.Rendering.CullMode)] _Culling("Cull Mode", Int) = 1
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Int) = 5
		[Enum(Off,0,On,1)] _ZWrite("ZWrite", Int) = 1
		_SectionPlane("_SectionPlane", Vector) = (0,1,0,0)
		_SectionPoint("_SectionPoint", Vector) = (0,0,0,0)
		[Toggle(CLIP_PLANE)] _clipPlane("clipPlane", Float) = 0
		//_SectionScale("_SectionScale", Vector) = (0,1,0,0)
		//_SectionCentre("_SectionCentre", Vector) = (0,0,0,0)
		[Toggle(CLIP_BOX)] _clipBox("clipBox", Float) = 0

	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue" = "Transparent-1" }

		LOD 100
		

		Cull[_Culling]
		ZTest[_ZTest]
		ZWrite[_ZWrite]

		CGPROGRAM
		#pragma surface surf StandardSpecular alpha:fade
		//#pragma surface surf Lambert
		#pragma multi_compile __ CLIP_SPHERES
		#pragma multi_compile __ CLIP_PLANE
		#pragma multi_compile __ CLIP_BOX
		#pragma target 3.0
		#include "CGIncludes/section_clipping_CS.cginc"


		sampler2D _MainTex;
		float4 _MainTex_ST;
		half _Glossiness;
		fixed4 _Color;
#if !CLIP_BOX
		uniform float4x4 _WorldToBoxMatrix;
#endif
		static const float3x3 projMatrix = float3x3(_WorldToBoxMatrix[0].xyz, _WorldToBoxMatrix[1].xyz, _WorldToBoxMatrix[2].xyz);
	      
		struct Input {
			float3 worldNormal;
			float3 worldPos;
			float myface : VFACE;
		};

		void surf (Input IN, inout SurfaceOutputStandardSpecular o) {
	          
			PLANE_CLIP(IN.worldPos);
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
			o.Albedo = c.rgb * _Color.rgb;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a * _Color.a;
			if (IN.myface > 0) o.Alpha *= 0.5f;
		}
		ENDCG
	}
	Fallback "Legacy Shaders/Transparent/VertexLit"
}
