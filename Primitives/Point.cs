namespace Example.Primitives
{
    public class Point
    {
        private float[] _vertices;
        private int _index;

        public float X
        {
            get => _vertices[_index];
            set
            {
                _vertices[_index] = value;
            }
        }
        public float Y
        {
            get => _vertices[_index + 1];
            set
            {
                _vertices[_index + 1] = value;
            }
        }
        public float Z
        {
            get => _vertices[_index + 2];
            set
            {
                _vertices[_index + 2] = value;
            }
        }

        public Point(float[] vertices, int index)
        {
            _vertices = vertices;
            _index = index;
        }

        public void Rotate(OpenTK.Mathematics.Matrix3 rotation)
        {
            var x = rotation.M11 * X + rotation.M21 * Y + rotation.M31 * Z;
            var y = rotation.M12 * X + rotation.M22 * Y + rotation.M32 * Z;
            var z = rotation.M13 * X + rotation.M23 * Y + rotation.M33 * Z;
            X = x;
            Y = y;
            Z = z;
        }
    }
}
