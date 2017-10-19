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

float4 Blood(float2 uv)
{
	float offset = min(progress + progress * tex2D(cloudInput, float2(uv.x, randomSeed)).r, 1.0);
	uv.y -= offset;
	
	if(uv.y > 0.0)
	{
		return tex2D(oldInput, uv);
	}
	else
	{
		return tex2D(implicitInput, frac(uv));
	}
}

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------
float4 main(VS_OUTPUT input) : COLOR0
{
	return Blood(input.UV);
}

