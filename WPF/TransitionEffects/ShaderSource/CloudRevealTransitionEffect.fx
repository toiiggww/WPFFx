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

float4 CloudReveal(float2 uv)
{
	float cloud = tex2D(cloudInput, uv).r;
    float4 c1 = tex2D(oldInput, uv);
    float4 c2 = tex2D(implicitInput, uv);
	
	float a;
	
	if (progress < 0.5)
	{
		a = lerp(0.0, cloud, progress / 0.5);
	}
	else
	{
		a = lerp(cloud, 1.0, (progress - 0.5) / 0.5);
	}
	
    return (a < 0.5) ? c1 : c2;
}

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------
float4 main(VS_OUTPUT input) : COLOR0
{
	return CloudReveal(input.UV);
}

