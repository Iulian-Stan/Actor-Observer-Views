using ActorObserverViews.GlWrappers;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace Example.Common
{
    public class Node
    {
        private readonly GlVertexArray _vertexArray;
        private readonly GlShader _shader;
        private readonly Dictionary<string, GlShaderUniform> _uniforms;

        private Matrix4 _model = Matrix4.Identity;

        public Node(GlVertexArray vertexArray, GlShader shader)
        {
            _vertexArray = vertexArray;
            _shader = shader;
            _uniforms = new Dictionary<string, GlShaderUniform>();
            _uniforms["model"] = new ShaderUniformM4(Matrix4.Identity);
        }

        public void AddUniform(string key, GlShaderUniform uniform)
        {
            _uniforms[key] = uniform;
        }

        public void SetTransformation(Matrix4 tranformation)
        {
            _uniforms["model"] = new ShaderUniformM4(tranformation);
        }

        public void Draw(Matrix4 view, Matrix4 projection)
        {
            _uniforms["view"] = new ShaderUniformM4(view);
            _uniforms["projection"] = new ShaderUniformM4(projection);
            _shader.Use();
            foreach (KeyValuePair<string, int> entry in _shader.UniformLocations)
            {
                _uniforms[entry.Key].SetUniform(entry.Value);
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
