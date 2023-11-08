using ActorObserverViews.GlWrappers;
using ActorObserverViews.Util;
using Example.Common;
using Example.Shapes;
using Example.Util;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Wpf;
using System;
using System.Windows;
using System.Windows.Input;

namespace Example
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow
    {
        private bool IsCursorLocked = false;
        private float angle = 0;
        private static Camera ActiveCamera;

        private Scene _actorScene;
        private Scene _observerScene;
        private Scene _axesScene;

        public void Initialize()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Blend);
            //GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.ClearColor(0.6f, 0.6f, 0.6f, 1.0f);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            // Initialize Shaders
            var shaderColor = new GlShader("Resources/shaders/shader_col.vert", "Resources/shaders/shader_col.frag");
            var shaderTexture = new GlShader("Resources/shaders/shader_tex.vert", "Resources/shaders/shader_tex.frag");

            // Initialize Textures
            var cubeTexture = new GlTexture("Resources/textures/cube.png", TextureUnit.Texture0);

            // Create vertex and indices buffers for arrows
            var arrow = new ArrowStruct(0.7f, 0.1f, 0.3f, 0.2f, 10);
            var xArrowPoints = ArrowData.Points(arrow);
            var yArrowPoints = ArrowData.Points(arrow);
            PointsArray.FromArray(yArrowPoints).Rotate(Matrix3.CreateRotationY(MathHelper.DegreesToRadians(-90)));
            var zArrowPoints = ArrowData.Points(arrow);
            PointsArray.FromArray(zArrowPoints).Rotate(Matrix3.CreateRotationZ(MathHelper.DegreesToRadians(90)));
            var vertexAttribute = new VertexAttributeStruct(3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            var xVertexBuffer = new GlBuffer(BufferTarget.ArrayBuffer, xArrowPoints, BufferUsageHint.StaticDraw);
            var yVertexBuffer = new GlBuffer(BufferTarget.ArrayBuffer, yArrowPoints, BufferUsageHint.StaticDraw);
            var zVertexBuffer = new GlBuffer(BufferTarget.ArrayBuffer, zArrowPoints, BufferUsageHint.StaticDraw);
            var arrowTriangles = ArrowData.Triangles(arrow);
            var elementBuffer = new GlBuffer(BufferTarget.ElementArrayBuffer, arrowTriangles, BufferUsageHint.StaticDraw);

            // Create X arrow node (used in axes scenes)
            var xArrowVertexArray = new GlVertexArray();
            xArrowVertexArray.BindBuffer(xVertexBuffer);
            xArrowVertexArray.BindBuffer(elementBuffer);
            xArrowVertexArray.BindAttribute(0, vertexAttribute);
            var xArrowNode = new Node(xArrowVertexArray, shaderColor);
            xArrowNode.AddUniform("fragColor", new ShaderUniformV3(Vector3.UnitX));

            // Create Y arrow node (used in axes scenes)
            var yArrowVertexArray = new GlVertexArray();
            yArrowVertexArray.BindBuffer(yVertexBuffer);
            yArrowVertexArray.BindBuffer(elementBuffer);
            yArrowVertexArray.BindAttribute(0, vertexAttribute);
            var yArrowNode = new Node(yArrowVertexArray, shaderColor);
            yArrowNode.AddUniform("fragColor", new ShaderUniformV3(Vector3.UnitY));

            // Create Z arrow node (used in axes scenes)
            var zArrowVertexArray = new GlVertexArray();
            zArrowVertexArray.BindBuffer(zVertexBuffer);
            zArrowVertexArray.BindBuffer(elementBuffer);
            zArrowVertexArray.BindAttribute(0, vertexAttribute);
            var zArrowNode = new Node(zArrowVertexArray, shaderColor);
            zArrowNode.AddUniform("fragColor", new ShaderUniformV3(Vector3.UnitZ));

            // Create cube node (used in the main scene)
            var cubeVertexArray = new GlVertexArray();
            cubeVertexArray.BindBuffer(new GlBuffer(BufferTarget.ArrayBuffer, CubeData.Vertices, BufferUsageHint.StaticDraw));
            cubeVertexArray.BindBuffer(new GlBuffer(BufferTarget.ElementArrayBuffer, CubeData.Indices, BufferUsageHint.StaticDraw));
            cubeVertexArray.BindAttribute(0, new VertexAttributeStruct(3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0));
            cubeVertexArray.BindAttribute(1, new VertexAttributeStruct(2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float)));
            var cubeNode = new Node(cubeVertexArray, shaderTexture);
            cubeNode.AddUniform("tex", new ShaderUniformInt(cubeTexture.Index));

            // Create camera view (used in observer scene)
            var frustumVertexArray = new GlVertexArray();
            frustumVertexArray.BindBuffer(new GlBuffer(BufferTarget.ArrayBuffer, CameraFrustumData.Vertices, BufferUsageHint.StaticDraw));
            frustumVertexArray.BindBuffer(new GlBuffer(BufferTarget.ElementArrayBuffer, CameraFrustumData.Indices, BufferUsageHint.StaticDraw));
            frustumVertexArray.BindAttribute(0, vertexAttribute);
            var frustumNode = new Node(frustumVertexArray, shaderColor);
            frustumNode.AddUniform("fragColor", new ShaderUniformV3(new Vector3(.8f, .8f, .8f)));

            // Create actor scene
            var actorCamera = new Camera(Vector3.UnitZ * 3, 0, -MathHelper.PiOver2, MathHelper.PiOver2, 1.0f, 0.01f, 5f);
            _actorScene = new Scene(actorCamera, cubeNode);

            // Create observer scene
            var observerCamera = new Camera(-Vector3.UnitX * 9, 0, 0, MathHelper.PiOver2, 1.0f, 0.01f, 100f);
            _observerScene = new Scene(observerCamera, cubeNode, frustumNode);

            // Create axes scene
            var axesCamera = new Camera(Vector3.UnitZ * 3, 0, -MathHelper.PiOver2, MathHelper.PiOver2, 1.0f, 0.01f, 100f);
            _axesScene = new Scene(axesCamera, xArrowNode, yArrowNode, zArrowNode);
        }

        public MainWindow() {
            InitializeComponent();

            var glwSettings = new GLWpfControlSettings {MajorVersion = 2, MinorVersion = 1 };

            ActorViewControl.Start(glwSettings);
            ActorAxesViewControl.Start(glwSettings);
            ObserverViewControl.Start(glwSettings);
            ObserverAxesViewControl.Start(glwSettings);

            Initialize();

            ActiveCamera = _actorScene.GetCamera();
        }

        private void Window_KeyDown(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Escape)
            {
                Mouse.OverrideCursor = null;
                IsCursorLocked = false;
            }
            if (!IsCursorLocked) return;

            if (args.Key == Key.W)
            {
                ActiveCamera.ChangePosition(0, 0, 1); // Forward
            }
            if (args.Key == Key.S)
            {
                ActiveCamera.ChangePosition(0, 0, -1); // Backwards
            }
            if (args.Key == Key.A)
            {
                ActiveCamera.ChangePosition(-1, 0, 0); // Left
            }
            if (args.Key == Key.D)
            {
                ActiveCamera.ChangePosition(1, 0, 0); // Right
            }
            if (args.Key == Key.Space)
            {
                ActiveCamera.ChangePosition(0, 1, 0); // Up
            }
            if (args.Key == Key.LeftShift)
            {
                ActiveCamera.ChangePosition(0, -1, 0); // Down
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsCursorLocked) return;
            Point center = WindowHelper.GetWindowCenter();
            Mouse.OverrideCursor = Cursors.None;
            WindowHelper.SetCursorPos((int)center.X, (int)center.Y);
            IsCursorLocked = true;
        }

        private void Window_MouseMove(object sender, MouseEventArgs args)
        {
            if (!IsCursorLocked) return;
            Point position = PointToScreen(Mouse.GetPosition(this));
            Point center = WindowHelper.GetWindowCenter();
            float deltaX = (float)(position.X - center.X);
            float deltaY = (float)(position.Y - center.Y);
            ActiveCamera.ChangeDirection(deltaX, deltaY);
            WindowHelper.SetCursorPos((int)center.X, (int)center.Y);
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs args)
        {
            if (!IsCursorLocked) return;
            ActiveCamera.ChangeFOV(args.Delta);
        }

        private void ActorViewControl_Render(TimeSpan delta)
        {
            angle = (angle - 0.01f) % MathHelper.TwoPi;
            var model = Matrix4.CreateRotationY(angle);
            var nodes = _actorScene.GetNodes();
            foreach (var node in nodes)
            {
                node.SetTransformation(model);
            }
            _actorScene.Render();
        }

        private void ActorViewControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _actorScene.GetCamera().ChangeAR((float)(e.NewSize.Width / e.NewSize.Height));
        }

        private Matrix4 GetCameraRotation(Camera camera)
        {
            return Matrix4.CreateRotationY(MathHelper.PiOver2 + MathHelper.DegreesToRadians(camera.Yaw)) * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-camera.Pitch));
        }

        private void ActorAxesViewControl_Render(TimeSpan delta)
        {
            var model = GetCameraRotation(_actorScene.GetCamera());
            var nodes = _axesScene.GetNodes();
            foreach (var node in nodes)
            {
                node.SetTransformation(model);
            }
            _axesScene.Render();
        }

        private void ActorAxesViewControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _axesScene.GetCamera().ChangeAR((float)(e.NewSize.Width / e.NewSize.Height));
        }

        private void ObserverViewControl_Render(TimeSpan delta)
        {
            var model = Matrix4.CreateRotationY(angle);
            var nodes = _observerScene.GetNodes();
            nodes[0].SetTransformation(model);
            var camera = _actorScene.GetCamera();
            nodes[1].SetTransformation(Matrix4.Invert(camera.GetViewMatrix() * camera.GetProjectionMatrix()));
            GL.Disable(EnableCap.CullFace);
            _observerScene.Render();
            GL.Enable(EnableCap.CullFace);
        }

        private void ObserverViewControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _observerScene.GetCamera().ChangeAR((float)(e.NewSize.Width / e.NewSize.Height));
        }

        private void ObserverAxesViewControl_Render(TimeSpan delta)
        {
            var model = GetCameraRotation(_observerScene.GetCamera());
            var nodes = _axesScene.GetNodes();
            foreach (var node in nodes)
            {
                node.SetTransformation(model);
            }
            _axesScene.Render();
        }

        private void ObserverAxesViewControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _axesScene.GetCamera().ChangeAR((float)(e.NewSize.Width / e.NewSize.Height));
        }
    }
}
