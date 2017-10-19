float progress : register(C0);
sampler2D implicitInput : register(s0);
sampler2D oldInput : register(s1);

struct VS_OUTPUT
{
    float4 Position  : POSITION;
    float4 Color     : COLOR0;
    float2 UV        : TEXCOORD0;
};

float4 Fade(float2 uv)
{
    float4 c1 = tex2D(oldInput, uv);
    float4 c2 = tex2D(implicitInput, uv);

    return lerp(c1,c2, progress);
}

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------
float4 main(VS_OUTPUT input) : COLOR0
{
	return Fade(input.UV);
}

