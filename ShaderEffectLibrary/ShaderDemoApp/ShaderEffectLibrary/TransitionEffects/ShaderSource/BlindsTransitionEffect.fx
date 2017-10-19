float progress : register(C0);
sampler2D implicitInput : register(s0);
sampler2D oldInput : register(s1);

struct VS_OUTPUT
{
    float4 Position  : POSITION;
    float4 Color     : COLOR0;
    float2 UV        : TEXCOORD0;
};

float4 Blinds(float2 uv)
{		
	if(frac(uv.y * 5) < progress)
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
	return Blinds(input.UV);
}

