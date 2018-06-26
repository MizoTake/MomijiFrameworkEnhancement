Shader "Custom/CircleDisolveSplit"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Radius ("Radius", Range(0,1)) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
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
			};

			sampler2D _MainTex;
			float _Radius;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				i.uv *= 10;
				i.uv.xy *= _Time;
				_Radius = sin(_Time.y);
				i.uv = frac(i.uv);
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed aspect = _ScreenParams.y / _ScreenParams.x;
				// if(sqrt(pow(0.5 - i.uv.x, 2) + pow((0.5 - i.uv.y) * aspect, 2)) < _Radius) {
				// 	discard;
				// }
				float check = step(_Radius, sqrt(pow(0.5 - i.uv.x, 2) + pow((0.5 - i.uv.y) * aspect, 2)));
				if (check == 0) {
					discard;
				}
				col *= check;

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
