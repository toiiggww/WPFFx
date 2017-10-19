float progress : register(C0);
float twistAmount : register(C1);
float frequency : register(C2);
sampler2D implicitInput : register(s0);
sampler2D oldInput : register(s1);

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

float4 BandedSwirl(float2 uv)
{
	float2 center = float2(0.5,0.5);
	float2 toUV = uv - center;
	float distanceFromCenter = length(toUV);
	float2 normToUV = toUV / distanceFromCenter;
	float angle = atan2(normToUV.y, normToUV.x);
	
	angle += sin(distanceFromCenter * frequency) * twistAmount * progress;
	float2 newUV;
	sincos(angle, newUV.y, newUV.x);
	newUV = newUV * distanceFromCenter + center;
	
	float4 c1 = tex2D(oldInput, frac(newUV));
    float4 c2 = tex2D(implicitInput, uv);

    return lerp(c1,c2, progress);
}

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------
float4 main(float2 uv : TEXCOORD0) : COLOR0
{
	return BandedSwirl(uv);
}

