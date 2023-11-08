namespace Example.Shapes
{
    public static class CubeData
    {
        public static readonly float[] Vertices =
        {
            // Position           Texture
            // X      Y      Z    X     Y    
             0.5f,  0.5f,  0.5f, .50f, .75f, // front top right
             0.5f, -0.5f,  0.5f, .50f, .50f, // front bottom right
            -0.5f, -0.5f,  0.5f, .25f, .50f, // front bottom left
            -0.5f,  0.5f,  0.5f, .25f, .75f, // front top left

            -0.5f,  0.5f,  0.5f, .25f, .75f, // left top right
            -0.5f, -0.5f,  0.5f, .25f, .50f, // left bottom right
            -0.5f, -0.5f, -0.5f, .00f, .50f, // left bottom left
            -0.5f,  0.5f, -0.5f, .00f, .75f, // left top left

            -0.5f,  0.5f, -0.5f, 1.0f, .75f, // back top right
            -0.5f, -0.5f, -0.5f, 1.0f, .50f, // back bottom right
             0.5f, -0.5f, -0.5f, .75f, .50f, // back bottom left
             0.5f,  0.5f, -0.5f, .75f, .75f, // back top left

             0.5f,  0.5f, -0.5f, .75f, .75f, // right top right
             0.5f, -0.5f, -0.5f, .75f, .50f, // right bottom right
             0.5f, -0.5f,  0.5f, .50f, .50f, // right bottom left
             0.5f,  0.5f,  0.5f, .50f, .75f  // right top left
        };

        public static readonly int[] Indices =
        {
             0,  3,  1,  1,  3,  2,
             4,  7,  5,  5,  7,  6,
             8, 11,  9,  9, 11, 10,
            12, 15, 13, 13, 15, 14
        };
    }
}