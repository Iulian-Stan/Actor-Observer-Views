using Example.Common;
using Example.Primitives;
using OpenTK.Mathematics;

namespace Example.Shapes
{
    public class CameraFrustum
    {
        public static float[] Vertices =
        {
            -1, 1, -1, -1, -1, -1, 1, -1, -1, 1, 1, -1, // far plane
            -1, 1,  1, -1, -1,  1, 1, -1,  1, 1, 1,  1  // near plane
        };

        public static readonly int[] Indices =
        {
             0, 1, 2, 0, 2, 3,
             4, 5, 6, 4, 6, 7,
             0, 1, 4, 1, 4, 5,
             1, 2, 5, 2, 5, 6,
             2, 3, 6, 3, 6, 7,
             3, 0, 7, 0, 7, 4
        };
    }
}
