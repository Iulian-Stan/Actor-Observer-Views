namespace Example.Primitives
{
    public class Triangle
    {
        private int[] _indices;
        private int _index { get; set; }

        public int P1
        {
            get => _indices[_index];
            set
            {
                _indices[_index] = value;
            }
        }
        public int P2
        {
            get => _indices[_index + 1];
            set
            {
                _indices[_index + 1] = value;
            }
        }
        public int P3
        {
            get => _indices[_index + 2];
            set
            {
                _indices[_index + 2] = value;
            }
        }

        public Triangle(int[] indices, int index)
        {
            _indices = indices;
            _index = index;
        }
    }
}
