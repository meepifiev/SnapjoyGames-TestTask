Shader "Project/Double Sided Color"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
        }

        Pass
        {
            Cull Off

            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment

            #include "UnityCG.cginc"

            struct VertexInput
            {
                float4 vertex : POSITION;
            };

            struct FragmentInput
            {
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color;

            FragmentInput Vertex(VertexInput input)
            {
                FragmentInput output;
                output.vertex = UnityObjectToClipPos(input.vertex);
                return output;
            }

            fixed4 Fragment(FragmentInput input) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}
