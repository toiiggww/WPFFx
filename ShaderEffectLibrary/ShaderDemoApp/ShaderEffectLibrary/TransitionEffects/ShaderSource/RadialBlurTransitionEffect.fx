float progress : register(C0);
sampler2D implicitInput : register(s0);
sampler2D oldInput : register(s1);

struct VS_OUTPUT
{
    float4 Position  : POSITION;
    float4 Color     : COLOR0;
    float2 UV        : TEXCOORD0;
};

float4 RadialBlur(float2 uv)
{
	float2 center = float2(0.5,0.5);
	float2 toUV = uv - center;
	float2 normToUV = toUV;
	
	
	float4 c1 = float4(0,0,0,0);
	int count = 24;
	float s = progress * 0.02;
	
	for(int i=0; i<count; i++)
	{
		c1 += tex2D(oldInput, uv - normToUV * s * i); 
	}
	
	c1 /= count;
    float4 c2 = tex2D(implicitInput, uv);

	return lerp(c1, c2, progress);
}

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------
float4 main(VS_OUTPUT input) : COLOR0
{
	return RadialBlur(input.UV);
}

