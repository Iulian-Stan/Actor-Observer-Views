using OpenTK.Graphics.OpenGL4;

namespace Example.Common
{
    public class Scene
    {
        private readonly Camera _camera;
        private readonly Node[] _nodes;

        public Scene(Camera camera, params Node[] nodes)
        {
            _camera = camera;
            _nodes = nodes;
        }

        public Camera GetCamera()
        {
            return _camera;
        }

        public Node[] GetNodes()
        {
            return _nodes;
        }

        public void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            foreach (var node in _nodes)
            {
                node.Draw(_camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            }
        }
    }
}
