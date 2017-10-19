float progress : register(C0);
float randomSeed : register(C1);
sampler2D implicitInput : register(s0);
sampler2D oldInput : register(s1);
sampler2D cloudInput : register(s2);

struct VS_OUTPUT
{
    float4 Position  : POSITION;
    float4 Color     : COLOR0;
    float2 UV        : TEXCOORD0;
};

float4 SampleWithBorder(float4 border, sampler2D tex, float2 uv)
{
	if (any(saturate(uv) - uv))
	{
		return border;
	}
	else
	{
		return tex2D(tex, uv);
	}
}

float4 DropFade(float2 uv)
{
	float offset = -tex2D(cloudInput, float2(uv.x / 5, randomSeed)).x;
	float4 c1 = SampleWithBorder(float4(0,0,0,0), oldInput, float2(uv.x, uv.y + offset * progress));
    float4 c2 = tex2D(implicitInput, uv);

	if (c1.a <= 0.0)
		return c2;
	else
		return lerp(c1, c2, progress);
}

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------
float4 main(VS_OUTPUT input) : COLOR0
{
	return DropFade(input.UV);
}

