Shader "Custom/Unlit/TriPlanar"
{
    Properties
        {
            _MainTex ("Texture", 2D) = "white" {}
        }
        SubShader
        {
            Tags { "RenderType"="Opaque" "DisableBatching"="True" }
            LOD 100
            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fog
                #include "UnityCG.cginc"
                struct appdata
                {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
                };
                struct v2f
                {
                    float4 pos : SV_POSITION;
                    float3 uvPos : TEXCOORD0;
                    float3 uvBlend : TEXCOORD1;
                    UNITY_FOG_COORDS(2)
                };
                sampler2D _MainTex;
                float4 _MainTex_ST;
                v2f vert (appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    float3 scale = float3(
                        length(unity_ObjectToWorld._m00_m10_m20),
                        length(unity_ObjectToWorld._m01_m11_m21),
                        length(unity_ObjectToWorld._m02_m12_m22)
                        );
                    o.uvPos = v.vertex.xyz * scale;
                    o.uvBlend = v.normal.xyz / scale;
                    o.uvBlend /= dot(abs(o.uvBlend), float3(1,1,1));
                    UNITY_TRANSFER_FOG(o,o.pos);
                    return o;
                }
                fixed4 frag (v2f i) : SV_Target
                {
                    float3 blend = abs(i.uvBlend);
                    float2 uv;
                    if (blend.x > max(blend.y, blend.z)) {
                        uv = i.uvPos.yz;
                    } else if (blend.z > blend.y) {
                        uv = i.uvPos.xy;
                    } else {
                        uv = i.uvPos.xz;
                    }
                    fixed4 col = tex2D(_MainTex, TRANSFORM_TEX(uv, _MainTex));
                    UNITY_APPLY_FOG(i.fogCoord, col);
                    return col;
                }
            ENDCG
        }
    }
}
