namespace ActorObserverViews.Util
{
    public class PoligonsArray
    {
        private readonly int[] _array;
        private readonly int _poligonSize;

        public Poligon this[int key]
        {
            get => new Poligon(_array, key, _poligonSize);
        }

        private PoligonsArray(int[] array, int poligonSize)
        {
            _array = array;
            _poligonSize = poligonSize;
        }

        public static PoligonsArray FromArray(int[] array, int poligonSize)
        {
            System.Diagnostics.Trace.Assert(array.Length % poligonSize == 0);
            return new PoligonsArray(array, poligonSize);
        }

        public class Poligon
        {
            private int[] _indices;
            private int _index { get; set; }

            public int this[int key]
            {
                private get => _indices[_index + key];
                set => _indices[_index + key] = value;
            }

            internal Poligon(int[] indices, int index, int size)
            {
                _indices = indices;
                _index = index * size;
                // Poligon should not exceed indices array boundaries
                System.Diagnostics.Debug.Assert(indices.Length > _index);
            }
        }
    }
}
