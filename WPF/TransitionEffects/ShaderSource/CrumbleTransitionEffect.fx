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

float4 Crumple(float2 uv)
{
	float2 offset = tex2D(cloudInput, float2(uv.x / 5, frac(uv.y / 5 + min(0.9, randomSeed)))).xy * 2.0 - 1.0;
	float p = progress * 2;
	if (p > 1.0)
	{
		p = 1.0 - (p - 1.0);
	}
	float4 c1 = tex2D(oldInput, frac(uv + offset * p));
    float4 c2 = tex2D(implicitInput, frac(uv + offset * p));

	return lerp(c1, c2, progress);
}

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------
float4 main(VS_OUTPUT input) : COLOR0
{
	return Crumple(input.UV);
}

