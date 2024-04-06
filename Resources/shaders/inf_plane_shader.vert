#version 330 core
#extension GL_ARB_separate_shader_objects : enable

// https://asliceofrendering.com/scene%20helper/2020/01/05/InfiniteGrid/

uniform mat4 view;
uniform mat4 projection;

layout(location = 0) in vec3 aPosition;

out vec3 nearPoint;
out vec3 farPoint;
out mat4 fragView;
out mat4 fragProj;

vec3 UnprojectPoint(float x, float y, float z, mat4 view, mat4 projection) {
    mat4 viewInv = inverse(view);
    mat4 projInv = inverse(projection);
    vec4 unprojectedPoint = vec4(x, y, z, 1.0) * projInv * viewInv;
    return unprojectedPoint.xyz / unprojectedPoint.w;
}

void main() {
    nearPoint = UnprojectPoint(aPosition.x, aPosition.y, 0.0, view, projection).xyz; // unprojecting on the near plane
    farPoint = UnprojectPoint(aPosition.x, aPosition.y, 1.0, view, projection).xyz; // unprojecting on the far plane
    gl_Position = vec4(aPosition, 1.0); // using directly the clipped coordinates;
    fragView = view;
    fragProj = projection;
}