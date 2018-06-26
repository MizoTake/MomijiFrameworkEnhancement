Shader "CircleDisolve(Instanced)"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
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
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            sampler2D _MainTex;
			float _Radius;
			float4 _MainTex_ST;

            UNITY_INSTANCING_BUFFER_START (MyProperties)
            UNITY_DEFINE_INSTANCED_PROP (float4, _Color)
#define _Color_arr MyProperties
            UNITY_INSTANCING_BUFFER_END(MyProperties)
           
            v2f vert (appdata v)
            {
                v2f o;

                UNITY_SETUP_INSTANCE_ID (v);
                UNITY_TRANSFER_INSTANCE_ID (v, o);

                o.vertex = UnityObjectToClipPos (v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);

                return o;
            }
           
            fixed4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID (i);
                
                fixed aspect = _ScreenParams.y / _ScreenParams.x;
				if(sqrt(pow(0.5 - i.uv.x, 2) + pow((0.5 - i.uv.y) * aspect, 2)) < _Radius) {
					discard;
				}

				fixed4 col = tex2D(_MainTex, i.uv) * UNITY_ACCESS_INSTANCED_PROP (_Color_arr, _Color);
				UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
