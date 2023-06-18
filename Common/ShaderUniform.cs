using OpenTK.Mathematics;

namespace Example.Common
{
    public abstract class ShaderUniform
    {
        protected readonly string _name;

        public ShaderUniform(string name)
        {
            _name = name;
        }

        public abstract void Apply(Shader shader);
    }

    public class ShaderUniformInt : ShaderUniform
    {
        private int _value;

        public ShaderUniformInt(string name, int value) : base(name)
        {
            _value = value;
        }

        public override void Apply(Shader shader)
        {
            shader.SetInt(_name, _value);
        }
    }

    public class ShaderUniformFloat : ShaderUniform
    {
        private float _value;

        public ShaderUniformFloat(string name, float value) : base(name)
        {
            _value = value;
        }

        public override void Apply(Shader shader)
        {
            shader.SetFloat(_name, _value);
        }
    }

    public class ShaderUniformV3 : ShaderUniform
    {
        private Vector3 _value;

        public ShaderUniformV3(string name, Vector3 value) : base(name)
        {
            _value = value;
        }

        public override void Apply(Shader shader)
        {
            shader.SetVector3(_name, _value);
        }
    }

    public class ShaderUniformM4 : ShaderUniform
    {
        private Matrix4 _value;

        public ShaderUniformM4(string name, Matrix4 value) : base(name)
        {
            _value = value;
        }

        public override void Apply(Shader shader)
        {
            shader.SetMatrix4(_name, _value);
        }
    }
}
