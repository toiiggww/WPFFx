float progress : register(C0);
float2 slideAmount : register(C1);
sampler2D implicitInput : register(s0);
sampler2D oldInput : register(s1);

struct VS_OUTPUT
{
    float4 Position  : POSITION;
    float4 Color     : COLOR0;
    float2 UV        : TEXCOORD0;
};

float4 SlideLeft(float2 uv)
{
	uv += slideAmount * progress;
	if(any(saturate(uv)-uv))
	{	
		uv = frac(uv);
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
	return SlideLeft(input.UV);
}

