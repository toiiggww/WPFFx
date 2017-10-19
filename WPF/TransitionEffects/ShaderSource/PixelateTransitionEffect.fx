float progress : register(C0);
sampler2D implicitInput : register(s0);
sampler2D oldInput : register(s1);

float4 Pixelate(float2 uv)
{
	float pixels;
	float segment_progress;
	if (progress < 0.5)
	{
		segment_progress = 1 - progress * 2;
	}
	else
	{		
		segment_progress = (progress - 0.5) * 2;

	}
    
    pixels = 5 + 1000 * segment_progress * segment_progress;
	float2 newUV = round(uv * pixels) / pixels;
	
    float4 c1 = tex2D(oldInput, newUV);
    float4 c2 = tex2D(implicitInput, newUV);

	float lerp_progress = saturate((progress - 0.4) / 0.2);
	return lerp(c1,c2, lerp_progress);	
}

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------
float4 main(float2 uv : TEXCOORD0) : COLOR0
{
	return Pixelate(uv);
}

