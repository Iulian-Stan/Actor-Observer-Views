namespace Example.Shapes
{
    public class PlaneData
    {
        public static readonly float[] Vertices =
        {
        //   X   Y   Z
            -1,  1,  0, // 0 Left-Top
            -1, -1,  0, // 1 Left-Bottom
             1, -1,  0, // 2 Right-Bottom
             1,  1,  0  // 3 Right-Top
        };

        public static readonly int[] Indices =
        {
             0, 1, 2,
             0, 2, 3
        };
    }
}
