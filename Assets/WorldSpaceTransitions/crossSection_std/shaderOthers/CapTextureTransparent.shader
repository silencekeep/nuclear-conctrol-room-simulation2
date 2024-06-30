Shader "CrossSection/CapTransparent/Texture"
{
Properties
{
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_StencilMask("Stencil Mask", Range(0, 255)) = 1
	[Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("Stencil Comparison", Int) = 3
}

SubShader
{
	Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
	LOD 200

	Stencil
	{
		Ref [_StencilMask]
		Comp [_StencilComp]
	}

	CGPROGRAM
	#pragma surface surf Standard alpha:fade

	sampler2D _MainTex;
	fixed4 _Color;

	struct Input
	{
		float2 uv_MainTex;
	};

	void surf (Input IN, inout SurfaceOutputStandard o)
	{
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	
	ENDCG
}
//FallBack "Diffuse" //uncommenting this will make this shader to cast shadows with whole area (not limited with stencil pass)

}