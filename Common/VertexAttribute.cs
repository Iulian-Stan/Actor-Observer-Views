using OpenTK.Graphics.OpenGL4;

namespace Example.Common
{
    /// <summary>
    /// Vertex Attribute information <see href="https://www.khronos.org/opengl/wiki/Vertex_Specification#Vertex_buffer_offset_and_stride">vertex buffer attributes</see>
    /// </summary>
    public struct VertexAttribute
    {
        public readonly int Size;
        public readonly VertexAttribPointerType Type;
        public readonly bool Normalized;
        public readonly int Stride;
        public readonly int Offset;

        public VertexAttribute(int size, VertexAttribPointerType type, bool normalized, int stride, int offset)
        {
            Size = size;
            Type = type;
            Normalized = normalized;
            Stride = stride;
            Offset = offset;
        }
    }
}
