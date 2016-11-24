Shader "Moregeek/Rim" 
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Main Color", Color) = (.5,.5,.5,1)
        //_BumpMap ("Bumpmap", 2D) = "bump" {}
        _RimColor ("RimColor", Color) = (0.26, 0.19, 0.16, 0.0)
        _RimPower ("RimPower", Range(0.5, 8.0)) = 3.0
    }

    SubShader
    {
        Tags 
        { 
            "RenderType" = "Opaque" 
        }
        CGPROGRAM
        #pragma surface surf Lambert

        struct Input 
        {
            float2 uv_MainTex;
            //float2 uv_BumpMap;
            float3 viewDir;
        };

        sampler2D _MainTex;
        //sampler2D _BumpMap;
        float4 _RimColor;
        float _RimPower;
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutput o) 
        {
            //o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
            //o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
            half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
            //saturate限制值于[0,1]之间
            //o.Emission = _RimColor.rgb * pow (rim, _RimPower);

            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            //o.Albedo = c.rgb;
            o.Emission = c.rgb + (_RimColor.rgb * pow (rim, _RimPower));
            o.Alpha = c.a;
        }
        ENDCG
    }

    Fallback "Diffuse"
}