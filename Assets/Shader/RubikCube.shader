// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/RubikCube"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_PointSize("Point Size", Float) = 1.0
	}
	SubShader
	{
		LOD 200

		Pass
		{
			CGPROGRAM

			#pragma only_renderers d3d11
			#pragma target 4.0

			#pragma vertex vert
			#pragma geometry geom
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#define TAM 36
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct geoIn
			{
				float4 pos : SV_POSITION;
				float4 col : COLOR0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 col : COLOR0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _PointSize;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			[maxvertexcount(TAM)]
			void geom(point geoIn vert[1], inout TriangleStream<v2f> triStream) 
			{
				float f = _PointSize/20.0f;

				const float4 vc[TAM] = { float4( -f,  f,  f, 0.0f), float4(  f,  f,  f, 0.0f), float4(  f,  f, -f, 0.0f),    //Top                                 
                                          float4(  f,  f, -f, 0.0f), float4( -f,  f, -f, 0.0f), float4( -f,  f,  f, 0.0f),    //Top
                                          
                                          float4(  f,  f, -f, 0.0f), float4(  f,  f,  f, 0.0f), float4(  f, -f,  f, 0.0f),     //Right
                                          float4(  f, -f,  f, 0.0f), float4(  f, -f, -f, 0.0f), float4(  f,  f, -f, 0.0f),     //Right
                                          
                                          float4( -f,  f, -f, 0.0f), float4(  f,  f, -f, 0.0f), float4(  f, -f, -f, 0.0f),     //Front
                                          float4(  f, -f, -f, 0.0f), float4( -f, -f, -f, 0.0f), float4( -f,  f, -f, 0.0f),     //Front
                                          
                                          float4( -f, -f, -f, 0.0f), float4(  f, -f, -f, 0.0f), float4(  f, -f,  f, 0.0f),    //Bottom                                         
                                          float4(  f, -f,  f, 0.0f), float4( -f, -f,  f, 0.0f), float4( -f, -f, -f, 0.0f),     //Bottom
                                          
                                          float4( -f,  f,  f, 0.0f), float4( -f,  f, -f, 0.0f), float4( -f, -f, -f, 0.0f),    //Left
                                          float4( -f, -f, -f, 0.0f), float4( -f, -f,  f, 0.0f), float4( -f,  f,  f, 0.0f),    //Left
                                          
                                          float4( -f,  f,  f, 0.0f), float4( -f, -f,  f, 0.0f), float4(  f, -f,  f, 0.0f),    //Back
                                          float4(  f, -f,  f, 0.0f), float4(  f,  f,  f, 0.0f), float4( -f,  f,  f, 0.0f)     //Back
                                          };

				const float2 UV1[TAM] = { float2( 0.0f,    0.0f ), float2( 1.0f,    0.0f ), float2( 1.0f,    0.0f ),         //Esta em uma ordem
                                           float2( 1.0f,    0.0f ), float2( 1.0f,    0.0f ), float2( 1.0f,    0.0f ),         //aleatoria qualquer.
                                           
                                           float2( 0.0f,    0.0f ), float2( 1.0f,    0.0f ), float2( 1.0f,    0.0f ), 
                                           float2( 1.0f,    0.0f ), float2( 1.0f,    0.0f ), float2( 1.0f,    0.0f ),
                                           
                                           float2( 0.0f,    0.0f ), float2( 1.0f,    0.0f ), float2( 1.0f,    0.0f ), 
                                           float2( 1.0f,    0.0f ), float2( 1.0f,    0.0f ), float2( 1.0f,    0.0f ),
                                           
                                           float2( 0.0f,    0.0f ), float2( 1.0f,    0.0f ), float2( 1.0f,    0.0f ), 
                                           float2( 1.0f,    0.0f ), float2( 1.0f,    0.0f ), float2( 1.0f,    0.0f ),
                                           
                                           float2( 0.0f,    0.0f ), float2( 1.0f,    0.0f ), float2( 1.0f,    0.0f ), 
                                           float2( 1.0f,    0.0f ), float2( 1.0f,    0.0f ), float2( 1.0f,    0.0f ),
                                           
                                           float2( 0.0f,    0.0f ), float2( 1.0f,    0.0f ), float2( 1.0f,    0.0f ), 
                                           float2( 1.0f,    0.0f ), float2( 1.0f,    0.0f ), float2( 1.0f,    0.0f )                                            
                                             }; 

				const int TRI_STRIP[TAM]  = {  0, 1, 2,  3, 4, 5,
                                                6, 7, 8,  9,10,11,
                                               12,13,14, 15,16,17,
                                               18,19,20, 21,22,23,
                                               24,25,26, 27,28,29,
                                               30,31,32, 33,34,35  
                                               }; 
				
				v2f v[TAM];
				int i;
				
				// Assign new vertices positions 
				for (i=0;i<TAM;i++) 
				{ 
					v[i].vertex = vert[0].pos + vc[i];
					v[i].col = vert[0].col;    
				}

				// Assign UV values
				for (i=0;i<TAM;i++) v[i].uv = TRANSFORM_TEX(UV1[i], _MainTex); 
				
				// Position in view space
				for (i=0;i<TAM;i++)
				{
					v[i].vertex = UnityObjectToClipPos(v[i].vertex);
				}
					
				// Build the cube tile by submitting triangle strip vertices
				for (i=0;i<TAM/3;i++)
				{ 
					triStream.Append(v[TRI_STRIP[i*3+0]]);
					triStream.Append(v[TRI_STRIP[i*3+1]]);
					triStream.Append(v[TRI_STRIP[i*3+2]]);    
									
					triStream.RestartStrip();
				}
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
