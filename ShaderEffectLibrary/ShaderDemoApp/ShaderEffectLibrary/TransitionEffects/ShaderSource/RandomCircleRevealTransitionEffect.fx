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

float4 RandomCircle(float2 uv)
{
	float radius = progress * 0.70710678;
	float2 fromCenter = uv - float2(0.5,0.5);
	float len = length(fromCenter);
	
	float2 toUV = normalize(fromCenter);
	float angle = (atan2(toUV.y, toUV.x) + 3.141592) / (2.0 * 3.141592);
	radius += progress * tex2D(cloudInput, float2(angle, frac(randomSeed + progress / 5.0))).r;
	
	if(len < radius)
	{
		return tex2D(implicitInput, uv);
	}
	else
	{
		return tex2D(oldInput, uv);
	}
}

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------
float4 main(VS_OUTPUT input) : COLOR0
{
	return RandomCircle(input.UV);
}

