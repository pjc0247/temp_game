Shader "Custom/Texture2DOpqaue" {
    Properties{
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
    _Opacity("Opacity", Range(0,1)) = 1.0
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
        SubShader{
        Tags{ "RenderType" = "Opaque" "Queue" = "Opaque" }

        LOD 200

        CGPROGRAM
#pragma surface surf Standard NoLighting
#pragma target 3.0

        sampler2D _MainTex;

    struct Input {
        float2 uv_MainTex;
    };

    half _Opacity;
    half _Metallic;
    fixed4 _Color;

    void surf(Input IN, inout SurfaceOutputStandard o) {
        // Albedo comes from a texture tinted by color
        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
        o.Albedo = c.rgb;
        o.Alpha = c.a * _Opacity;
    }
    fixed4 LightingNoLighting(SurfaceOutputStandard s, fixed3 lightDir, fixed atten)
    {
        fixed4 c;
        c.rgb = s.Albedo;
        c.a = s.Alpha;
        return c;
    }

    ENDCG
    }
        FallBack "Diffuse"
}
