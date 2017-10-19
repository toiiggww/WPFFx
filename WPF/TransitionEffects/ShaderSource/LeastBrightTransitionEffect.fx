float progress : register(C0);
sampler2D implicitInput : register(s0);
sampler2D oldInput : register(s1);


float4 LeastBright(float2 uv)
{
	int c = 4;
	int c2 = 3;
	float oc = (c -1) / 2;
	float oc2 = (c2 -1) / 2;
	float offset = 0.01 * progress;
	
	float leastBright = 1;
	float4 leastBrightColor;
	for(int y=0; y<c; y++)
	{
		for(int x=0; x<c2; x++)
		{
			float2 newUV = uv + (float2(x, y) - float2(oc2, oc)) * offset;
			float4 color = tex2D(oldInput, newUV);
			float brightness = dot(color.rgb, float3(1,1.1,0.9));
			if(brightness < leastBright)
			{
				leastBright = brightness;
				leastBrightColor = color;
			}
		}
	}
	
	float4 impl = tex2D(implicitInput, uv);
	    
	return lerp(leastBrightColor,impl, progress);
}

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------
float4 main(float2 uv : TEXCOORD0) : COLOR0
{
	return LeastBright(uv);
}

