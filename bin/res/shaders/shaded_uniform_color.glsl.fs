
#version 150

// in
in vec3 fNormal;

// out
out vec4 oColor;

// uniform
uniform vec4 uColor;

float lambert(vec3 N, vec3 L)
{
  vec3 nrmN = normalize(N);
  vec3 nrmL = normalize(L);
  float result = dot(nrmN, nrmL);
  return max(result, 0.0);
}

void main()
{
	vec3 lightDir = vec3( 0.0, 0.0, -1.0 );
	float brightness = lambert( fNormal, lightDir );
	
	if ( brightness != 0 )
		oColor = uColor * brightness;
	else
		oColor = uColor;
} 