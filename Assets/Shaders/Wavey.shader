Shader "Unlit/Wavey"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _texture ("texture", 2D) = "black" {}
        _Strength("Strength", Float) = 0.5
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
            float4 _MainTex_ST;
            sampler2D _texture;
            float4 _texture_ST;
            float _Strength;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _texture);
                o.vertex.x = o.vertex.x + sin(_Time.y * 5) * 0.005 + sin(_Time.y * o.vertex.y * 20) * 0.01;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_texture, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
