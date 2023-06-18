using OpenTK.Mathematics;

namespace Example.Primitives
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
            set
            {
                int index = key * 3;
                _array[index] = value.X;
                _array[index + 1] = value.Y;
                _array[index + 2] = value.Z;
            }
        }

        private PointsArray(float[] array)
        {
            _array = array;
        }

        public static PointsArray FromArray(float[] array)
        {
            // Each 3D Point requires 3 elements (1 per dimension)
            System.Diagnostics.Trace.Assert(array.Length % 3 == 0);
            return new PointsArray(array);
        }

        public PointsArray(PointsArray points)
        {
            _array = new float[points._array.Length];
            for (int i = 0; i < points._array.Length / 3; ++i)
            {
                this[i] = points[i];
            }
        }

        public void Rotate(Matrix3 rotation)
        {
            for (int i = 0; i < _array.Length / 3; ++i)
            {
                this[i].Rotate(rotation);
            }
        }
    }
}
