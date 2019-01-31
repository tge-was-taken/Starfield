#version 330

// in
in vec3 fPosition;

// out
out vec4 oColor;

// uniforms
uniform vec4 uColor;
uniform vec4 uBlendColor;
uniform float uMinZ;

void main()
{
    float blend = ( fPosition.z / uMinZ ) * 4;
	oColor.r = min( uColor.r * blend, uBlendColor.r );
    oColor.g = min( uColor.g * blend, uBlendColor.g );
    oColor.b = min( uColor.b * blend, uBlendColor.b );
    //oColor = uColor;
}