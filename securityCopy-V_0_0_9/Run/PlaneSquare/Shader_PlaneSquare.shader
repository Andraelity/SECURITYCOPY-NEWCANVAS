Shader "Unlit/Shader_PlaneSquare"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {


        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        // Cull front   
        LOD 100
        // Tags { "RenderType"="Opaque" }
        // LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert alpha
            #pragma fragment frag alpha
            // make fog work

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            // StructuredBuffer<float> bufferImage;
            StructuredBuffer<float4> bufferImageColorPlane;
            
            int _WIDTH;
            int _HEIGHT;

            float traduccion(float toTranslate)
            {
                return toTranslate * -1;
            }


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }


            int _SetScreen ;


            fixed4 frag (v2f i) : SV_Target
            {
                
                // THIS CODE MEANS
                // HOW TO RUN THE PIXELS OF THE SCREEN.
                
                float2 uv =  i.uv ;
                fixed4 col = float4(1,1,1,1);

                float2 toOneUV = uv;


                int2 xy = int2(_WIDTH,_HEIGHT) * toOneUV;

                float output = xy.y * _WIDTH + xy.x;

                float4 p = bufferImageColorPlane[output];

                // float4 p = float4(1,1,1,1);
                
                col = fixed4(p.x,p.y,p.z,p.w);

                return col;


            }

            ENDCG
        }
    }
}
