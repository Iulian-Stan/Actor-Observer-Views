using Example.Common;
using OpenTK.Graphics.OpenGL4;

namespace ActorObserverViews.GlWrappers
{
    /// <summary>
    /// Vertex Array Object wrapper <see href="https://www.khronos.org/opengl/wiki/Vertex_Specification#Vertex_Array_Object">VAO</see>
    /// </summary>
    public class GlVertexArray
    {
        /// <summary>
        /// Hnadle of the generated buffer object
        /// </summary>
        private int _handle;

        /// <summary>
        /// kind of primitives to render <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDrawElements.xhtml">glDrawElements</see>
        /// </summary>
        public PrimitiveType PrimitiveType { get; private set; }

        /// <summary>
        /// The type of the indices <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDrawElements.xhtml">glDrawElements</see>
        /// </summary>
        public DrawElementsType DrawElementsType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="primitiveType"></param>
        /// <param name="drawElements"></param>
        public GlVertexArray(PrimitiveType primitiveType = PrimitiveType.Triangles, DrawElementsType drawElements = DrawElementsType.UnsignedInt)
        {
            _handle = GL.GenVertexArray();
            PrimitiveType = primitiveType;
            DrawElementsType = drawElements;
        }

        /// <summary>
        /// Bind vertex array to vertex array object and defines vertex generic attribute data <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glVertexAttribPointer.xhtml">glVertexAttribPointer</see>
        /// </summary>
        /// <remarks>
        /// Attributes should match the same order defined in shader program
        /// </remarks>
        /// <param name="buffer">Vertex buffer object</param>
        /// <param name="attributes">Vertex attributes (optional)</param>
        public void BindBuffer(GlBuffer buffer)
        {
            GL.BindVertexArray(_handle);
            buffer.Bind();
        }

        /// <summary>
        /// Bind vertex array to vertex array object and defines vertex generic attribute data <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glVertexAttribPointer.xhtml">glVertexAttribPointer</see>
        /// </summary>
        /// <remarks>
        /// Attributes should match the same order defined in shader program
        /// </remarks>
        /// <param name="buffer">Vertex buffer object</param>
        /// <param name="attributes">Vertex attributes (optional)</param>
        public void BindAttribute(int index, VertexAttributeStruct attribute)
        {
            GL.BindVertexArray(_handle);
            GL.EnableVertexAttribArray(index);
            GL.VertexAttribPointer(index, attribute.Size, attribute.Type, attribute.Normalized, attribute.Stride, attribute.Offset);
        }

        /// <summary>
        /// Bind a vertex array object <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glBindVertexArray.xhtml">glBindVertexArray</see>
        /// </summary>
        public void Bind()
        {
            GL.BindVertexArray(_handle);
        }
    }
}
