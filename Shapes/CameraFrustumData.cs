namespace Example.Shapes
{
    public class CameraFrustumData
    {
        public static readonly float[] Vertices =
        {
        //   X   Y   Z
            -1,  1, -1, // 0 Left-Top-Far
            -1, -1, -1, // 1 Left-Bottom-Far
             1, -1, -1, // 2 Right-Bottom-Far
             1,  1, -1, // 3 Right-Top-Far
            -1,  1,  1, // 4 Left-Top-Near
            -1, -1,  1, // 5 Left-Bottom-Near
             1, -1,  1, // 6 Right-Bottom-Near
             1,  1,  1  // 7 Right-Top-Near
        };

        public static readonly int[] Indices =
        {
             0, 1, 2, 0, 2, 3, // Far side
             4, 5, 6, 4, 6, 7, // Near side
             0, 1, 4, 1, 4, 5, // Left side
             1, 2, 5, 2, 5, 6, // Bottom side
             2, 3, 6, 3, 6, 7, // Right side
             3, 0, 7, 0, 7, 4  // Top Side
        };
    }
}
