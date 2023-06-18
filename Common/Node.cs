using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace Example.Common
{
    public class Node
    {
        private readonly VertexArray _vertexArray;
        private readonly Shader _shader;
        private readonly List<ShaderUniform> _uniforms;

        private Matrix4 _model = Matrix4.Identity;

        public Node(VertexArray vertexArray, Shader shader)
        {
            _vertexArray = vertexArray;
            _shader = shader;
            _uniforms = new List<ShaderUniform>();
        }

        public void AddUniform(ShaderUniform uniform)
        {
            _uniforms.Add(uniform);
        }

        public void SetTransformation(Matrix4 tranformation)
        {
            _model = tranformation;
        }

        public void Draw(Matrix4 view, Matrix4 projection)
        {
            _shader.Use();
            _shader.SetMatrix4("model", _model);
            _shader.SetMatrix4("view", view);
            _shader.SetMatrix4("projection", projection);
            foreach (var uniform in _uniforms)
            {
                uniform.Apply(_shader);
            }
            _vertexArray.Bind();
            int size;
            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out size);
            switch (_vertexArray.DrawElementsType)
            {
                case DrawElementsType.UnsignedInt: size /= 4; break;
                case DrawElementsType.UnsignedShort: size /= 2; break;
                default: break;
            }
            GL.DrawElements(_vertexArray.PrimitiveType, size, _vertexArray.DrawElementsType, 0);
        }
    }
}
