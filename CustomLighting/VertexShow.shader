Shader "VertexShow"
{
    SubShader
    {
        Pass
        {
            ZWrite Off
            ZTest Always
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
           #pragma vertex vert
		   #pragma geometry geom
           #pragma fragment frag
           #pragma target 5.0

           #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                half3 normal : NORMAL;
            };

            struct v2g
            {
				float4 pos : SV_POSITION;
                half faceSign: TEXCOORD0;
            };

			struct g2f
			{
				float4 pos : POSITION;
                half faceSign: TEXCOORD1;
			};

            half _PointSize;
            float4x4 _TransformMatrix;
            
            v2g vert (appdata v)
            {
                v2g o;
				o.pos = mul(_TransformMatrix, v.vertex);
                half3 viewDir = ObjSpaceViewDir(o.pos);
                o.faceSign = dot(viewDir, mul(_TransformMatrix, v.normal));
                return o;
            }

			[maxvertexcount(4)]
	        void geom(point v2g IN[1], inout TriangleStream<g2f> triStream)
			{
	        	float size = _PointSize;
	        	float halfS = _PointSize * 0.5;
	        	g2f pIn = (g2f)0;
	        	for (int x = 0; x < 2; x++) {
	        		for (int y = 0; y < 2; y++) {
	        			float4x4 billboardMatrix = UNITY_MATRIX_V;
	        			billboardMatrix._m03 = billboardMatrix._m13 = billboardMatrix._m23 = billboardMatrix._m33 = 0;
	        			pIn.pos = IN[0].pos + mul(float4((float2(x, y) * 2 - float2(1, 1)) * halfS, 0, 1), billboardMatrix);
	        			pIn.pos = UnityObjectToClipPos(pIn.pos);
						pIn.faceSign = IN[0].faceSign;
	        			triStream.Append(pIn);
	        		}
	        	}
	        	triStream.RestartStrip();
	        }

            float4 frag (g2f i) : SV_Target
            {
                float4 color = 1;
                color.a = smoothstep(-0.1, 1, i.faceSign);
                return color;
            }
            ENDCG
        }
    }
}