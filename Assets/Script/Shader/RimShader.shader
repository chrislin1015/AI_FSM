//MIT License

//Copyright (c) [2016] [Chris Lin]

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

Shader "Chris/Rim" 
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Main Color", Color) = (.5,.5,.5,1)
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
            float3 viewDir;
        };

        sampler2D _MainTex;
        float4 _RimColor;
        float _RimPower;
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutput o) 
        {
            half _Rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Emission = _RimColor.rgb * pow(_Rim, _RimPower);
            o.Alpha = c.a;
        }
        ENDCG
    }

    Fallback "Diffuse"
}