#version 330 core
#extension GL_ARB_separate_shader_objects : enable

// https://asliceofrendering.com/scene%20helper/2020/01/05/InfiniteGrid/

in vec3 nearPoint; // nearPoint calculated in vertex shader
in vec3 farPoint;  // farPoint calculated in vertex shader
in mat4 fragView;
in mat4 fragProj;

layout(location = 0) out vec4 outColor;

float near = 0.01;
float far = 5;

vec4 grid(vec3 fragPos3D, float scale, bool drawAxis) {
    vec2 coord = fragPos3D.xz * scale;
    vec2 derivative = fwidth(coord);
    vec2 grid = abs(fract(coord - 0.5) - 0.5) / derivative;
    float line = min(grid.x, grid.y);
    float minimumz = min(derivative.y, 1);
    float minimumx = min(derivative.x, 1);
    vec4 color = vec4(0.2, 0.2, 0.2, 1.0 - min(line, 1.0));
    // z axis
    if(fragPos3D.x > -0.1 * minimumx && fragPos3D.x < 0.1 * minimumx)
        color.z = 1.0;
    // x axis
    if(fragPos3D.z > -0.1 * minimumz && fragPos3D.z < 0.1 * minimumz)
        color.x = 1.0;
    return color;
}

float computeDepth(vec3 pos) {
    vec4 clip_space_pos = vec4(pos.xyz, 1.0) * fragView * fragProj;
    return 0.5 + 0.5 * clip_space_pos.z / clip_space_pos.w;
}

float computeLinearDepth(vec3 pos) {
    vec4 clip_space_pos = vec4(pos.xyz, 1.0) * fragView * fragProj;
    float clip_space_depth = 0.5 + 0.5 * (clip_space_pos.z / clip_space_pos.w);
    float linearDepth = near * far / (far - clip_space_depth * (far - near)); // get linear value between 0.01 and 5
    return linearDepth / (far - near); // normalize
}

void main() {
    float t = -nearPoint.y / (farPoint.y - nearPoint.y);
    vec3 fragPos3D = nearPoint + t * (farPoint - nearPoint);

    gl_FragDepth = computeDepth(fragPos3D);
    outColor = grid(fragPos3D, 10, true) * float(t > 0);

    float linearDepth = computeLinearDepth(fragPos3D);
    float fading = max(0, (1 - linearDepth));

    outColor = (grid(fragPos3D, 10, true) + grid(fragPos3D, 1, true)) * float(t > 0); // adding multiple resolution for the grid
    outColor.a *= fading;
}