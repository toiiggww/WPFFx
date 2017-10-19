float progress : register(C0);
sampler2D implicitInput : register(s0);
sampler2D oldInput : register(s1);
sampler2D trigInput : register(s2);

struct VS_OUTPUT
{
    float4 Position  : POSITION;
    float4 Color     : COLOR0;
    float2 UV        : TEXCOORD0;
};

float4 CircularBlur(float2 uv)
{
	float2 center = float2(0.5,0.5);
	float2 toUV = uv - center;
	float distanceFromCenter = length(toUV);
	float2 normToUV = toUV / distanceFromCenter;
	float angle = tex2D(trigInput, (normToUV + 1) * 0.5).z;
	
	float4 c1 = float4(0,0,0,0);
	float s = progress * 0.005;
    int count = 7;
	
	for(int i=0; i<count; i++)
	{
		float newAngle = angle - i*s;
		float2 newUV = (tex2D(trigInput, frac(newAngle - 0.5)).xy * 2.0 - 1.0) * distanceFromCenter + center;
		c1 += tex2D(oldInput, newUV);
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
	return CircularBlur(input.UV);
}

