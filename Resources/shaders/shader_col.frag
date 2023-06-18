#version 330

out vec4 outputColor;

uniform vec3 fragColor; 

void main()
{
    outputColor = vec4(fragColor, 0.5);
}