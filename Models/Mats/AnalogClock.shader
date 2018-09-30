




Shader "Custom/AnalogClock" {
	Properties{
		_MainColor("Color", Color) = (0,0,0,0)
		_MainTex("Texture", 2D) = "white" {}
		_SPR("Seconds Per Revolution", Float) = 60 
	}
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _MainColor;
		float _Pi = 3.1415926535897932384626433832795;
		float _SPR;
		float _Amount;
		float _VertexOffset;
		float _VertexRadius;
		float _TimeOffset;
		float _TotalOffset;
		float _RadianOffset;
		float _VertexYFrac;

		void vert(inout appdata_full v) {


			_TimeOffset = radians(_Time[1] * 360 / _SPR);

			_VertexRadius	 = length(float2(v.vertex.x, v.vertex.y));
			_VertexYFrac = v.vertex.y / _VertexRadius;

			clamp(_VertexYFrac, -1, 1);

			if (v.vertex.x == 0 && v.vertex.y > 0) {
				_VertexOffset = 3.1415926535897932384626433832795 / 2;
			}
			else if (v.vertex.x == 0 && v.vertex.y < 0) {
				_VertexOffset = 3.1415926535897932384626433832795 * 3 / 2;
			}
			else if (v.vertex.x >= 0) {
				_VertexOffset = asin(_VertexYFrac);
			}
			else if (v.vertex.y >= 0)
			{
				_VertexOffset = 3.1415926535897932384626433832795 - asin(_VertexYFrac);
			}
			else
			{
				_VertexOffset = 3.1415926535897932384626433832795 + asin(-_VertexYFrac);
			}
			
			_TotalOffset	 = _TimeOffset + _VertexOffset;

			v.vertex.x = cos(_TotalOffset) * _VertexRadius;
			v.vertex.y = sin(_TotalOffset) * _VertexRadius;
		}

		sampler2D _MainTex;

		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _MainColor;
		}

		ENDCG
	}
		Fallback "Diffuse"
}