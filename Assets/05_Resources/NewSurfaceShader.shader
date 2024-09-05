Shader "Custom/DoubleSidedStandard"
{
    Properties
    {
        _MainTex ("Base Color (RGB)", 2D) = "white" {}
        _Metallic ("Metallic", Range(0,1)) = 0
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _OcclusionMap ("Occlusion", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200
        
        // Cull Off으로 양면 렌더링 활성화
        Cull Off
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            sampler2D _MainTex;
            sampler2D _BumpMap;
            sampler2D _OcclusionMap;
            float _Metallic;
            float _Smoothness;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 기본 색상 텍스처
                fixed4 col = tex2D(_MainTex, i.uv);
                
                // 노멀 맵
                float3 normal = tex2D(_BumpMap, i.uv).rgb * 2 - 1;
                normal = normalize(normal);
                
                // 쉐이딩 계산 (간단한 Lambert 조명 모델)
                float3 lightDir = normalize(float3(0.0, 0.0, 1.0)); // 단순 방향광
                float NdotL = max(0.0, dot(i.normal, lightDir));
                fixed4 diffuse = col * NdotL;

                // 메탈릭 및 스무스
                float metallic = _Metallic;
                float smoothness = _Smoothness;
                fixed4 ambient = fixed4(0.1, 0.1, 0.1, 1.0) * col; // 간단한 앰비언트 조명

                return ambient + diffuse;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}