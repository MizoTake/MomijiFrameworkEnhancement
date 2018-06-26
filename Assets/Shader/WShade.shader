Shader "Custom/WShade" {

	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
	}

	CGINCLUDE
	#include "UnityCG.cginc"

	sampler2D _Color;
	
	float4 frag(v2f_img i) : SV_Target
	{
		float2 st = i.uv;
		st = (st - float2(0.5, 0.38)) * float2(2.1, 2.8);

		return pow(st.x, 2) + pow(st.y - sqrt(abs(st.x)), 2);
	}

	ENDCG
	
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			ENDCG
		}
	}
}
