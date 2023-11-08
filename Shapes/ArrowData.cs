using ActorObserverViews.Util;
using System;

namespace Example.Shapes
{
    public static class ArrowData
    {
        public static float[] Points(ArrowStruct arrow)
        {
            // The arrow consists of 2 components: tail (a cylinder) and peak (a cone)
            // Its vertices are defined as follows:
            // 1. first point is the center of the tail bottom circle (always the origin [0,0,0]) (points = 1)
            // 2. folowed by points defining the tail bottom circle (points = arrow.sectors)
            // 3. next come the points defining tail top circle (points = arrow.sectors)
            // 4. tail top circle center, is also the peak base center (points = arrow.sectors)
            // 5. peak base circle points (points = 1)
            // 6. final point is the peaks top point (points = 1)
            var vertices = new float[(arrow.Sectors * 3 + 3) * 3];
            var points = PointsArray.FromArray(vertices);
            // 1. non need to initialize is [0,0,0] by default
            // 2. & 3. tail top & bottom circles
            for (int i = 1; i <= arrow.Sectors; i++)
            {
                // tail bottom circle
                points[i].X = 0;
                points[i].Y = arrow.TailRadius * (float)Math.Cos(i * 2 * Math.PI / arrow.Sectors);
                points[i].Z = arrow.TailRadius * (float)Math.Sin(i * 2 * Math.PI / arrow.Sectors);

                // tail top circle
                points[i + arrow.Sectors].X = arrow.TailHeight;
                points[i + arrow.Sectors].Y = arrow.TailRadius * (float)Math.Cos(i * 2 * Math.PI / arrow.Sectors);
                points[i + arrow.Sectors].Z = arrow.TailRadius * (float)Math.Sin(i * 2 * Math.PI / arrow.Sectors);
            }
            int offset = arrow.Sectors * 2 + 1;
            // 4. tail top circle center (Y = Z = 0)
            points[offset].X = arrow.TailHeight;
            // 5. peak base circle
            for (int i = 1; i <= arrow.Sectors; i++)
            {
                points[offset + i].X = arrow.TailHeight;
                points[offset + i].Y = arrow.PeakRadius * (float)Math.Cos(i * 2 * Math.PI / arrow.Sectors);
                points[offset + i].Z = arrow.PeakRadius * (float)Math.Sin(i * 2 * Math.PI / arrow.Sectors);
            }
            offset += arrow.Sectors + 1;
            // 6. peak top point (Y = Z = 0)
            points[offset].X = arrow.TailHeight + arrow.PeakHeight;

            return vertices;
        }

        public static int[] Triangles(ArrowStruct arrow)
        {
            // Arrow indices are defined as follows
            // 1. tail bottom face (triangles = arrow.sectors)
            // 2. tail curved surface (triangles = 2 * arrow.sectors)
            // 3. tail top face (triangles = arrow.sectors)
            // 4. peak base (triangles = arrow.sectors)
            // 3. peak curved surface (triangles = arrow.sectors)
            // Note: indeces order considers OpenGL default face culling
            var indices = new int[arrow.Sectors * 6 * 3];
            var arrowTriangles = PoligonsArray.FromArray(indices, 3);
            // 1. tail bottom face
            for (int i = 0; i < arrow.Sectors; i++)
            {
                arrowTriangles[i][0] = 0;
                arrowTriangles[i][1] = (i + 1) % arrow.Sectors + 1;
                arrowTriangles[i][2] = i + 1;
            }
            int offset = arrow.Sectors;
            // 2. tail curved surface (rectangular sectors)
            for (int i = 0; i < arrow.Sectors; ++i)
            {
                // first half of the rectangle (1 point from top + 2 points from bottom)
                arrowTriangles[offset + 2 * i][0] = i + arrow.Sectors + 1;
                arrowTriangles[offset + 2 * i][1] = i + 1;
                arrowTriangles[offset + 2 * i][2] = (i + 1) % arrow.Sectors + 1;
                // second half of the rectangle (1 point from bottom + 2 points from top)
                arrowTriangles[offset + 2 * i + 1][0] = (i + 1) % arrow.Sectors + 1;
                arrowTriangles[offset + 2 * i + 1][1] = (i + 1) % arrow.Sectors + 1 + arrow.Sectors;
                arrowTriangles[offset + 2 * i + 1][2] = i + arrow.Sectors + 1;
            }
            offset += 2 * arrow.Sectors;
            // 3. tail top face
            for (int i = 0; i < arrow.Sectors; i++)
            {
                arrowTriangles[offset + i][0] = 2 * arrow.Sectors + 1;
                arrowTriangles[offset + i][1] = i + 1 + arrow.Sectors;
                arrowTriangles[offset + i][2] = (i + 1) % arrow.Sectors + 1 + arrow.Sectors;
            }
            offset += arrow.Sectors;
            // 4. peak base face
            for (int i = 0; i < arrow.Sectors; i++)
            {
                arrowTriangles[offset + i][0] = 2 * arrow.Sectors + 1;
                arrowTriangles[offset + i][1] = (i + 1) % arrow.Sectors + 1 + 2 * arrow.Sectors + 1;
                arrowTriangles[offset + i][2] = i + 1 + 2 * arrow.Sectors + 1;
            }
            offset += arrow.Sectors;
            // 5. peak curved surface
            for (int i = 0; i < arrow.Sectors; i++)
            {
                arrowTriangles[offset + i][0] = 3 * arrow.Sectors + 2;
                arrowTriangles[offset + i][1] = i + 1 + 2 * arrow.Sectors + 1;
                arrowTriangles[offset + i][2] = (i + 1) % arrow.Sectors + 1 + 2 * arrow.Sectors + 1;
            }

            return indices;
        }
    }
}
