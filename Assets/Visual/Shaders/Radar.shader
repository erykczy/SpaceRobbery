Shader "Unlit/Radar"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)
        _FadeOut("Fade Out", Float) = 1
        _ViewAngle("View Angle", Float) = 90
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"
            float _ViewAngle;
            float _FadeOut;
            float4 _Color;
            
            float vecAngle(float2 a, float2 b)
            {
                return acos(dot(a, b) / (length(a) * length(b)));
            }

            fixed4 frag (v2f_img i) : SV_Target
            {
                float2 upVec = float2(0, 1);
                float2 pos = i.uv - 0.5;

                //float angle = (1.0 - dot(normalize(pos), upVec)) / 2.0 * 180;
                float angle = vecAngle(normalize(pos), upVec) * 57.2957795;
                float distance = length(pos);
                float normalizedDistance = distance * 2;

                return fixed4(_Color.rgb, _Color.a * pow(normalizedDistance, _FadeOut) * step(angle, _ViewAngle / 2.0) * step(normalizedDistance, 1));
            }

            ENDCG
        }
    }
}
