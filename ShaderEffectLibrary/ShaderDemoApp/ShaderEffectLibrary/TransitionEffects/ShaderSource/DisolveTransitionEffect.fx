float progress : register(C0);
float randomSeed : register(C1);
sampler2D implicitInput : register(s0);
sampler2D oldInput : register(s1);
sampler2D noiseInput : register(s2);

struct VS_OUTPUT
{
    float4 Position  : POSITION;
    float4 Color     : COLOR0;
    float2 UV        : TEXCOORD0;
};

float4 Disolve(float2 uv)
{
	float noise = tex2D(noiseInput, frac(uv + randomSeed)).x;
	if(noise > progress)
	{
		return tex2D(oldInput, uv);
    }
    else
    {
		return tex2D(implicitInput, uv);
	}
}

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------
float4 main(VS_OUTPUT input) : COLOR0
{
	return Disolve(input.UV);
}

