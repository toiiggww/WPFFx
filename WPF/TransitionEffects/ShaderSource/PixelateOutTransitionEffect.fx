float progress : register(C0);
sampler2D implicitInput : register(s0);
sampler2D oldInput : register(s1);


float4 PixelateOut(float2 uv)
{
	float pixels = max(4, 100 * (1.0 - progress));
	float2 newUV = round(uv * pixels) / pixels;
    float4 c1 = tex2D(oldInput, newUV);
    float4 c2 = tex2D(implicitInput, uv);

	if (progress > 0.8)
	{
		float new_progress = (progress - 0.8) * 5;
		return lerp(c1,c2, new_progress);	
	}
	else
	{
		return c1;
	}
}

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------
float4 main(float2 uv : TEXCOORD0) : COLOR0
{
	return PixelateOut(uv);
}

