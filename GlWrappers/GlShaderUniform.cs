using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace ActorObserverViews.GlWrappers
{
    /// <summary>
    /// Interface used to set uniform on current shader
    /// </summary>
    public interface IGlShaderUniform
    {
        /// <summary>
        /// Set uniform value at specific location
        /// </summary>
        /// <param name="location">Uniform location in shader program</param>
        public void SetUniform(int location);
    }

    /// <summary>
    /// Interface used to set integer uniform on current shader
    /// </summary>
    public class ShaderUniformInt : IGlShaderUniform
    {
        private int _value;

        public ShaderUniformInt(int value)
        {
            _value = value;
        }

        public void SetUniform(int location)
        {
            GL.Uniform1(location, _value);
        }
    }

    /// <summary>
    /// Interface used to set float uniform on current shader
    /// </summary>
    public class ShaderUniformFloat : IGlShaderUniform
    {
        private float _value;

        public ShaderUniformFloat(float value)
        {
            _value = value;
        }

        public void SetUniform(int location)
        {
            GL.Uniform1(location, _value);
        }
    }

    /// <summary>
    /// Interface used to set vector3 uniform on current shader
    /// </summary>
    public class ShaderUniformV3 : IGlShaderUniform
    {
        private Vector3 _value;

        public ShaderUniformV3(Vector3 value)
        {
            _value = value;
        }

        public void SetUniform(int location)
        {
            GL.Uniform3(location, _value);
        }
    }

    /// <summary>
    /// Interface used to set matrix4 uniform on current shader
    /// </summary>
    public class ShaderUniformM4 : IGlShaderUniform
    {
        private Matrix4 _value;

        public ShaderUniformM4(Matrix4 value)
        {
            _value = value;
        }

        public void SetUniform(int location)
        {
            GL.UniformMatrix4(location, true, ref _value);
        }
    }
}
