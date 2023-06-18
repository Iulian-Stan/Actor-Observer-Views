using OpenTK.Mathematics;
using System;

namespace Example.Common
{
    // This is the camera class as it could be set up after the tutorials on the website.
    // It is important to note there are a few ways you could have set up this camera.
    // For example, you could have also managed the player input inside the camera class,
    // and a lot of the properties could have been made into functions.

    // TL;DR: This is just one of many ways in which we could have set up the camera.
    // Check out the web version if you don't know why we are doing a specific thing or want to know more about the code.
    public class Camera
    {
        // Camera position
        private Vector3 _position;

        // Z axis in the camera (viewer) space
        private Vector3 _front;

        // Y axis in the camera (viewer) space
        private Vector3 _up;

        // X axis in the camera (viewer) space
        private Vector3 _right;

        // Rotation around the X axis (radians)
        private float _pitch;

        // Rotation around the Y axis (radians)
        private float _yaw;

        // Field of view of the camera (radians)
        private float _fov;

        // Aspect ratio of the viewport (used for the projection matrix)
        private float _aspectRatio;

        // Distance to the near clipping plane of the frustrum (viewing volume)
        private float _nearDistance;

        // Distance to the far clipping plane of the frustrum (viewing volume)
        private float _farDistance;

        private readonly float POSITION_SENSIVITY = 0.05f;
        private readonly float DIRECTION_SENSIVITY = 0.02f;
        private readonly float FOV_SENSIVITY = 0.02f;

        public Camera(Vector3 position, float pitch, float yaw, float fov, float aspectRatio, float nearDistance, float farDistance)
        {
            _position = position;
            _pitch = pitch;
            _yaw = yaw;
            _fov = fov;
            _aspectRatio = aspectRatio;
            _nearDistance = nearDistance;
            _farDistance = farDistance;
            UpdateVectors();
        }

        // We convert from degrees to radians as soon as the property is set to improve performance.
        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(_pitch);
            private set
            {
                // We clamp the pitch value between -89 and 89 to prevent the camera from going upside down, and a bunch
                // of weird "bugs" when you are using euler angles for rotation.
                // If you want to read more about this you can try researching a topic called gimbal lock
                var angle = MathHelper.Clamp(value, -89f, 89f);
                _pitch = MathHelper.DegreesToRadians(angle);
                UpdateVectors();
            }
        }

        // We convert from degrees to radians as soon as the property is set to improve performance.
        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(_yaw);
            private set
            {
                _yaw = MathHelper.DegreesToRadians(value);
                UpdateVectors();
            }
        }

        // The field of view (FOV) is the vertical angle of the camera view.
        // This has been discussed more in depth in a previous tutorial,
        // but in this tutorial, you have also learned how we can use this to simulate a zoom feature.
        // We convert from degrees to radians as soon as the property is set to improve performance.
        public float Fov
        {
            get => MathHelper.RadiansToDegrees(_fov);
            private set
            {
                var angle = MathHelper.Clamp(value, 1f, 90f);
                _fov = MathHelper.DegreesToRadians(angle);
            }
        }

        public Vector3 Position
        {
            get => _position;
        }

        // Get the view matrix using the amazing LookAt function described more in depth on the web tutorials
        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(_position, _position + _front, _up);
        }

        // Get the projection matrix using the same method we have used up until this point
        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(_fov, _aspectRatio, _nearDistance, _farDistance);
        }

        // This function is going to update the direction vertices using some of the math learned in the web tutorials.
        private void UpdateVectors()
        {
            // First, the front matrix is calculated using some basic trigonometry.
            _front.X = MathF.Cos(_pitch) * MathF.Cos(_yaw);
            _front.Y = MathF.Sin(_pitch);
            _front.Z = MathF.Cos(_pitch) * MathF.Sin(_yaw);

            // We need to make sure the vectors are all normalized, as otherwise we would get some funky results.
            _front = Vector3.Normalize(_front);

            // Calculate both the right and the up vector using cross product.
            // Note that we are calculating the right from the global up; this behaviour might
            // not be what you need for all cameras so keep this in mind if you do not want a FPS camera.
            _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
            _up = Vector3.Normalize(Vector3.Cross(_right, _front));
        }

        public void ChangePosition(int dx, int dy, int dz)
        {
            _position += _front * POSITION_SENSIVITY * dz;
            _position += _up * POSITION_SENSIVITY * dy;
            _position += _right * POSITION_SENSIVITY * dx;
        }

        public void ChangeAR(float aspectRatio)
        {
            _aspectRatio = aspectRatio;
        }

        public void ChangeDirection(float deltaX, float deltaY)
        {
            Yaw += deltaX * DIRECTION_SENSIVITY;
            Pitch -= deltaY * DIRECTION_SENSIVITY; // Reversed since y-coordinates range from bottom to top
        }

        public void ChangeFOV(float delta)
        {
            Fov -= delta * FOV_SENSIVITY;
        }
    }
}
