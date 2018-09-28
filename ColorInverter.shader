﻿Shader "Custom/ColorInverter" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		[MaterialToggle] _Emissive ("Emissve", Float) = 0
		_EmissionColor ("Emission Color", Color) = (0,0,0,1)
		_InvertAmount ("Invert Amount", Range(-1, 1)) = -1.0
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Noise ("Noise", 2D) = "White" {}
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
		sampler2D _Noise;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Noise;
		};

		float _Emissive;
		half _Glossiness;
		half _Metallic;
		half _InvertAmount;
		fixed4 _Color;
		fixed4 _EmissionColor;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			fixed4 a = c.a;

			half _NoisyInverseAmount = _InvertAmount + (tex2D(_Noise, IN.uv_Noise + _Time/20 +.5).r + tex2D(_Noise, IN.uv_Noise -_Time/20).r);

			if (_NoisyInverseAmount <= 1) {
				_NoisyInverseAmount = -1;
				}
			else if (_NoisyInverseAmount > 1) {
				_NoisyInverseAmount = 1;
			}

			fixed4 _InverseColor = (1, 1, 1, 1) - _Color;
			fixed4 _NewColor = _Color + _NoisyInverseAmount * (_InverseColor - _Color);
			c = tex2D(_MainTex, IN.uv_MainTex) * _NewColor;


			if (_Emissive == 1) {
				fixed4 _InverseEmission = (1, 1, 1, 1) - _EmissionColor;
				fixed4 _NewEmission = _EmissionColor + _NoisyInverseAmount * (_InverseEmission - _EmissionColor);
				fixed4 e = tex2D(_MainTex, IN.uv_MainTex) * _NewEmission;
				o.Emission = e;
			}
			


			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = a;

		}
		ENDCG
	}
	FallBack "Diffuse"
}
