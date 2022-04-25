#version 330 core

uniform vec4 ourColor;

void main() {    
    gl_Position = gl_in[0].gl_Position;
    EmitVertex();

    gl_Position = gl_in[0].gl_Position;
    EmitVertex();
    
    EndPrimitive();
}  