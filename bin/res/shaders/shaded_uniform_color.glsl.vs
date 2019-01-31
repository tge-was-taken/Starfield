
#version 330 core

// in
layout( location = 0 ) in vec3 vPosition;
layout( location = 1 ) in vec3 vNormal;

// out
out vec3 fNormal;

// uniforms
uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;

void main()
{
	mat4 modelView = uView * uModel;
	fNormal = ( modelView * vec4( vNormal, 0.0 ) ).xyz;
	gl_Position = uProjection * modelView * vec4( vPosition, 1.0 );
}