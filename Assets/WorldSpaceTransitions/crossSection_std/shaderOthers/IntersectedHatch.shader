Shader "CrossSection/Others/IntersectedHatch"
{
Properties
{
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_StencilMask("Stencil Mask", Range(0, 255)) = 0
	[Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("Stencil Comparison", Int) = 3
}

SubShader
{
	Tags { "RenderType"="Opaque" "Queue" = "Geometry+2" }
	LOD 200

	Stencil
	{
		Ref [_StencilMask]
		Comp [_StencilComp]
	}

	CGPROGRAM
	#pragma surface surf Lambert 

	sampler2D _MainTex;
	fixed4 _Color;

	struct Input
	{
		float2 uv_MainTex;
	};

	void surf (Input IN, inout SurfaceOutput o)
	{
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	
	ENDCG
}
	//Fallback "VertexLit"

}