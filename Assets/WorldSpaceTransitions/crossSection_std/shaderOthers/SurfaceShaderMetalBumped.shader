Shader "SurfaceShader/Metal/BumpedOcclusion" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_MetallicGlossMap("Metallic", 2D) = "white" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
        _Occlusion ("Occlusion Map", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _MetallicGlossMap;
		sampler2D _BumpMap;
        sampler2D _Occlusion;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;    
		};

		half _Glossiness;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 Occ = tex2D(_Occlusion, IN.uv_MainTex);  
			o.Albedo = c.rgb * Occ.rgb;
			// smoothness come from slider variables, metallic from map
			fixed4 metal = tex2D(_MetallicGlossMap, IN.uv_MainTex);
			o.Metallic = metal.r;
			o.Smoothness = metal.a * _Glossiness;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
