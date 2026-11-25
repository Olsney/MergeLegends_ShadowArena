Shader "UI/DarkFantasyMagicRing"
{
    Properties
    {
        _Color ("Dark Fill", Color) = (0.002, 0.0005, 0.004, 0.90)

        _Center ("Center", Vector) = (0.5, 0.5, 0, 0)
        _Radius ("Radius", Float) = 0.16
        _Aspect ("Aspect", Float) = 1

        _OutlineColor ("Magic Color", Color) = (0.72, 0.008, 0.07, 1)
        _HotColor ("Hot Color", Color) = (1.0, 0.025, 0.008, 1)

        _OutlineWidth ("Outline Width", Float) = 0.006
        _GlowWidth ("Glow Width", Float) = 0.032
        _GlowStrength ("Glow Strength", Float) = 1.5

        _PulseSpeed ("Pulse Speed", Float) = 1.5
        _PulseStrength ("Pulse Strength", Float) = 0.35

        _FlowSpeed ("Energy Flow Speed", Float) = 0.65
        _FlowWidth ("Energy Flow Width", Float) = 0.16
        _FlowStrength ("Energy Flow Strength", Float) = 1.5

        _InnerGlow ("Inner Glow", Float) = 0.18
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
            "CanUseSpriteAtlas"="True"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            fixed4 _Color;

            float2 _Center;
            float _Radius;
            float _Aspect;

            fixed4 _OutlineColor;
            fixed4 _HotColor;

            float _OutlineWidth;
            float _GlowWidth;
            float _GlowStrength;

            float _PulseSpeed;
            float _PulseStrength;

            float _FlowSpeed;
            float _FlowWidth;
            float _FlowStrength;

            float _InnerGlow;

            v2f vert(appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 p =
                    (i.uv - _Center) *
                    float2(_Aspect, 1.0);

                float dist =
                    length(p);

                float edge =
                    dist - _Radius;

                float aa =
                    max(
                        fwidth(dist),
                        0.00001
                    );

                float inside =
                    1.0 -
                    smoothstep(
                        -aa,
                        aa,
                        edge
                    );

                float outside =
                    1.0 -
                    inside;

                float ring =
                    1.0 -
                    smoothstep(
                        _OutlineWidth,
                        _OutlineWidth + aa,
                        abs(edge)
                    );

                float glow =
                    1.0 -
                    smoothstep(
                        0.0,
                        _GlowWidth,
                        abs(edge)
                    );

                glow *= glow;

                float pulse =
                    sin(
                        _Time.y *
                        _PulseSpeed
                    ) * 0.5 + 0.5;

                float breathing =
                    1.0 +
                    pulse *
                    _PulseStrength;

                float angle01 =
                    frac(
                        (atan2(p.y, p.x) + UNITY_PI) /
                        (UNITY_PI * 2.0)
                    );

                float flow =
                    frac(
                        angle01 -
                        _Time.y *
                        _FlowSpeed *
                        0.08
                    );

                float flowDist =
                    abs(
                        flow - 0.5
                    );

                flowDist =
                    min(
                        flowDist,
                        1.0 - flowDist
                    );

                float energy =
                    1.0 -
                    smoothstep(
                        0.0,
                        _FlowWidth,
                        flowDist
                    );

                energy *= energy;

                float ringIntensity =
                    breathing *
                    (
                        0.82 +
                        energy *
                        _FlowStrength
                    );

                float3 ringColor =
                    lerp(
                        _OutlineColor.rgb,
                        _HotColor.rgb,
                        saturate(
                            energy * 0.9
                        )
                    );

                float3 color =
                    _Color.rgb *
                    outside;

                color +=
                    ringColor *
                    ring *
                    ringIntensity;

                color +=
                    _OutlineColor.rgb *
                    glow *
                    _GlowStrength *
                    breathing;

                color +=
                    _HotColor.rgb *
                    glow *
                    energy *
                    0.85;

                float inner =
                    1.0 -
                    smoothstep(
                        0.0,
                        _Radius * 0.95,
                        dist
                    );

                float innerPulse =
                    sin(
                        _Time.y * 1.3
                    ) * 0.5 + 0.5;

                color +=
                    _OutlineColor.rgb *
                    inner *
                    innerPulse *
                    _InnerGlow;

                color =
                    saturate(color);

                float alpha =
                    outside *
                    _Color.a;

                alpha =
                    max(
                        alpha,
                        ring *
                        (
                            0.75 +
                            energy * 0.3
                        )
                    );

                alpha =
                    max(
                        alpha,
                        glow *
                        (
                            0.28 +
                            pulse * 0.08
                        )
                    );

                alpha =
                    max(
                        alpha,
                        inner *
                        innerPulse *
                        _InnerGlow
                    );

                return fixed4(
                    color,
                    saturate(alpha)
                );
            }

            ENDCG
        }
    }
}
