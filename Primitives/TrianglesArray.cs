namespace Example.Primitives
{
    public class TrianglesArray
    {
        private readonly int[] _array;

        public Triangle this[int key]
        {
            get => new Triangle(_array, key * 3);
        }

        private TrianglesArray(int[] array)
        {
            _array = array;
        }

        public static TrianglesArray FromArray(int[] array)
        {
            // Each Triangle requires 3 elements (1 per peak)
            System.Diagnostics.Trace.Assert(array.Length % 3 == 0);
            return new TrianglesArray(array);
        }
    }
}
