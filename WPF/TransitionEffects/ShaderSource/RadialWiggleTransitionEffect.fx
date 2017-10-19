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


float4 RadialWiggle(float2 uv)
{
	float2 center = float2(0.5,0.5);
	float2 toUV = uv - center;
	float distanceFromCenter = length(toUV);
	float2 normToUV = toUV / distanceFromCenter;
	float angle = (atan2(normToUV.y, normToUV.x) + 3.141592) / (2.0 * 3.141592);
	float offset1 = tex2D(cloudInput, float2(angle, frac(progress/3 + distanceFromCenter/5 + randomSeed))).x * 2.0 - 1.0;
	float offset2 = offset1 * 2.0 * min(0.3, (1-progress)) * distanceFromCenter;
	offset1 = offset1 * 2.0 * min(0.3, progress) * distanceFromCenter;
	
	float4 c1 = tex2D(oldInput, frac(center + normToUV * (distanceFromCenter + offset1))); 
    float4 c2 = tex2D(implicitInput, frac(center + normToUV * (distanceFromCenter + offset2)));

	return lerp(c1, c2, progress);
}

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------
float4 main(VS_OUTPUT input) : COLOR0
{
	return RadialWiggle(input.UV);
}

