using OpenTK.Mathematics;

namespace ActorObserverViews.Util
{
    public class PointsArray
    {
        private readonly float[] _array;

        public Point this[int key]
        {
            get
            {
                return new Point(_array, key * 3);
            }
        }

        private PointsArray(float[] array)
        {
            _array = array;
        }

        public static PointsArray FromArray(float[] array)
        {
            return new PointsArray(array);
        }

        public void Rotate(Matrix3 rotation)
        {
            for (int i = 0; i < _array.Length / 3; ++i)
            {
                this[i].Rotate(rotation);
            }
        }

        public class Point
        {
            private float[] _array;
            private int _index;

            public float X
            {
                private get => _array[_index];
                set => _array[_index] = value;
            }
            public float Y
            {
                private get => _array[_index + 1];
                set => _array[_index + 1] = value;
            }
            public float Z
            {
                private get => _array[_index + 2];
                set => _array[_index + 2] = value;
            }

            internal Point(float[] vertices, int index)
            {
                _array = vertices;
                _index = index;
            }

            public void Rotate(Matrix3 rotation)
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
}
