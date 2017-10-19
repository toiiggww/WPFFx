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

float4 RotateCrumple(float2 uv)
{
	float2 offset = (tex2D(cloudInput, float2(uv.x / 10, frac(uv.y /10 + min(0.9, randomSeed)))).xy * 2.0 - 1.0);
	float2 center = uv + offset/10.0;
	float2 toUV = uv - center;
	float len = length(toUV);
	float2 normToUV = toUV / len;
	float angle = atan2(normToUV.y,normToUV.x);
	
	angle += 3.141592*2.0*progress;
	float2 newOffset;
	sincos(angle,newOffset.y, newOffset.x); 
	newOffset *= len;
	
	float4 c1 = tex2D(oldInput, frac(center + newOffset));
    float4 c2 = tex2D(implicitInput, frac(center + newOffset));

	return lerp(c1, c2, progress);
}

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------
float4 main(VS_OUTPUT input) : COLOR0
{
	return RotateCrumple(input.UV);
}

