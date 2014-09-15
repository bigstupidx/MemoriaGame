Shader "Custom/Checker" {
	Properties {
		_Color ("Main Color", Color) = (0,0,0,1)
		_Color2 ("Main Color 2", Color) = (1,1,1,1)
		_NumX ("Number Quads X", float) = 1
		_NumZ ("Number Quads Z", float) = 1
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}


	SubShader {
		Tags { "RenderType"="Opaque" }
		Pass {
			Name "BASE"
			Cull Off
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			float4 _Color2;
			float _NumZ;
			float _NumX;

			
			struct appdata {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				float3 normal : NORMAL;
			};
			
			struct v2f {
			
				float4 pos : POSITION;
				float2 texcoord : TEXCOORD0;
				float3 cubenormal : TEXCOORD1;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0));
				return o;
			}
			
			float4 frag (v2f i) : COLOR
			{   
				 //Repetir UV
				float2 ftexcoord;
   
				ftexcoord.x = fmod(_NumX/2.*i.texcoord.x ,1);
				ftexcoord.y = fmod(_NumZ/2.*i.texcoord.y,1);
				
				//Recuperar Color Textura
   				float4 cColorT; 
   				float row123;
   				float col12;
  				row123 = step(1/2.,ftexcoord.x) -  step(1/2., 1 - ftexcoord.x);
			   	col12 = step(1/2.,ftexcoord.y) - step(1/2.,1-ftexcoord.y);
			   	
			   	 cColorT = _Color*(1.0 - row123*col12) + _Color2*row123*col12 ;
			   	 return cColorT;
			   	
			}
			ENDCG			
		}
	} 

	SubShader {
		Tags { "RenderType"="Opaque" }
		Pass {
			Name "BASE"
			Cull Off
			SetTexture [_MainTex] {
				constantColor [_Color]
				Combine texture * constant
			} 
		}
	} 
	
	Fallback "VertexLit"
}
