Shader "VertexEffect"
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

            half _PointSize;
            float4x4 _TransformMatrix;
		    float4 _MousePosition;
			sampler2D _EventDataTex;
			float4 _SpherePos;
			float _SphereRad;
		   
            struct appdata
            {
                 float4 vertex : POSITION;
                 half3 normal : NORMAL;
            };
		   
            struct v2g
            {
			 	float4 pos : SV_POSITION;
                half faceSign: TEXCOORD0;
			 	float4 col : TEXCOORD1;
				bool clicked : TEXCOORD2;
				int id : TEXCOORD3;
            };

            struct g2f
		    {
			     float4 pos : POSITION;
                 half faceSign: TEXCOORD4;
			 	 float4 col : TEXCOORD5;
				 bool clicked : TEXCOORD6;
				 int id : TEXCOORD7;
            };

            v2g vert (appdata v, uint id : SV_VertexID)
			{
			     v2g o;
			     o.pos = mul(_TransformMatrix, v.vertex);
			     half3 viewDir = ObjSpaceViewDir(o.pos);
			     float4 scrn = ComputeScreenPos(UnityObjectToClipPos(o.pos));
				 float4 wpos = mul(unity_ObjectToWorld, o.pos);
			     scrn.xy /= scrn.w;
				 o.col = 0;
			     o.col += (length(scrn.xy - _MousePosition.xy) < 0.1) ? float4(1, 0.3, 0, 1) : 0; 
				 o.col += (length(wpos.xyz - _SpherePos.xyz) < _SphereRad) ? float4(0, 0, 1, 1) : 0;
				 o.col.a = saturate(o.col.a);
				 o.clicked = (length(scrn.xy - _MousePosition.xy) < 0.02) ? true : false;
				 o.id = id;
				 o.faceSign = dot(viewDir, mul(_TransformMatrix, v.normal));
                 return o;
            }

			[maxvertexcount(4)]
	        void geom(point v2g IN[1], inout TriangleStream<g2f> triStream)
			{
	        	float size = _PointSize;
	        	float halfS = _PointSize * 0.5 * IN[0].col.a;
	        	g2f pIn = (g2f)0;
	        	for (int x = 0; x < 2; x++) {
	        		for (int y = 0; y < 2; y++) {
	        			float4x4 billboardMatrix = UNITY_MATRIX_V;
	        			billboardMatrix._m03 = billboardMatrix._m13 = billboardMatrix._m23 = billboardMatrix._m33 = 0;
	        			pIn.pos = IN[0].pos + mul(float4((float2(x, y) * 2 - float2(1, 1)) * halfS, 0, 1), billboardMatrix);
	        			pIn.pos = UnityObjectToClipPos(pIn.pos);
						pIn.faceSign = IN[0].faceSign;
						pIn.col = IN[0].col;
						pIn.clicked = IN[0].clicked;
						pIn.id = IN[0].id;
	        			triStream.Append(pIn);
	        		}
	        	}
	        	triStream.RestartStrip();
	        }

            float4 frag (g2f i) : SV_Target
            {
                float4 color = i.col;
                color.a = smoothstep(-0.1, 1, i.faceSign);
                return color;
            }
            ENDCG
        }
    }
}