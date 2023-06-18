using OpenTK.Graphics.OpenGL4;

namespace Example.Common
{
    /// <summary>
    /// Vertex Buffer Object wrapper <see href="https://www.khronos.org/opengl/wiki/Vertex_Specification#Vertex_Buffer_Object">VBO</see>
    /// </summary>
    public class VertexBuffer
    {
        /// <summary>
        /// Handle of the buffer object
        /// </summary>
        private readonly int _handle;
        /// <summary>
        /// Target to which the buffer object is bound
        /// </summary>
        private readonly BufferTarget _target;

        /// <summary>
        /// Generate buffer object <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGenBuffers.xhtml">glGenBuffers</see>
        /// and bind it to a taregt <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glBindBuffer.xhtml">glBindBuffer </see>
        /// </summary>
        /// <remarks>
        /// Private constructor is used as base for public ones
        /// </remarks>
        /// <param name="target">Target to which the buffer object is bound</param>
        private VertexBuffer(BufferTarget target)
        {
            _handle = GL.GenBuffer();
            _target = target;
            GL.BindBuffer(_target, _handle);
        }

        /// <summary>
        /// Create and initialize a buffer object's data store <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glBufferData.xhtml">glBufferData</see>
        /// </summary>
        /// <remarks>
        /// Float type is primarly used to store vertex values
        /// </remarks>
        /// <param name="target">Target to which the buffer object is bound</param>
        /// <param name="data">Data that will be copied into the data store for initialization</param>
        /// <param name="usageHint">Expected usage pattern of the data store</param>
        public VertexBuffer(BufferTarget target, float[] data, BufferUsageHint usageHint) : this(target)
        {
            GL.BufferData(_target, data.Length * sizeof(float), data, usageHint);
        }

        /// <summary>
        /// Create and initialize a buffer object's data store <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glBufferData.xhtml">glBufferData</see>
        /// </summary>
        /// <remarks>
        /// Integer type is primarly used to store vertex indices
        /// </remarks>
        /// <param name="target">Target to which the buffer object is bound</param>
        /// <param name="data">Data that will be copied into the data store for initialization</param>
        /// <param name="usageHint">Expected usage pattern of the data store</param>
        public VertexBuffer(BufferTarget target, int[] data, BufferUsageHint usageHint) : this(target)
        {
            GL.BufferData(_target, data.Length * sizeof(int), data, usageHint);
        }

        /// <summary>
        /// Bind a named buffer object <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glBindBuffer.xhtml">glBindBuffer</see>
        /// </summary>
        public void Bind()
        {
            GL.BindBuffer(_target, _handle);
        }
    }
}
