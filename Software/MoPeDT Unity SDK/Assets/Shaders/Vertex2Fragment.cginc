struct v2f
{
    half4 pos       : POSITION;
    fixed4 color    : COLOR0;
    half2 uv        : TEXCOORD0;
    half4 vert      : TEXCOORD1;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct v2fw
{
    half4 pos       : POSITION;
    fixed4 color    : COLOR0;
    half2 uv        : TEXCOORD0;
    half4 vert      : TEXCOORD1;
    float4 worldPos : TEXCOORD2;
    float4 screenPos : TEXCOORD3;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};