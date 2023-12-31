﻿using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.IO;

namespace ActorObserverViews.GlWrappers
{
    public class GlShader
    {
        /// <summary>
        /// Handle of the shader program object
        /// </summary>
        private readonly int _handle;

        /// <summary>
        /// Dictionary of uniform locations defined in shader program
        /// </summary>
        public readonly Dictionary<string, int> UniformLocations;

        /// <summary>
        /// Create and compile fragment and vertex shader programs
        /// </summary>
        /// <param name="vertPath">Path to the vertex shader file</param>
        /// <param name="fragPath">Path to the fragment shader file</param>
        public GlShader(string vertPath, string fragPath)
        {
            // Create and compile verteg shader
            var vertexShader = CreateShader(ShaderType.VertexShader, vertPath);
            CompileShader(vertexShader);

            // Create and compile  fragment shader
            var fragmentShader = CreateShader(ShaderType.FragmentShader, fragPath);
            CompileShader(fragmentShader);

            // These two shaders must then be merged into a shader program, which can then be used by OpenGL.
            // To do this, create a program...
            _handle = GL.CreateProgram();

            // Attach both shaders...
            GL.AttachShader(_handle, vertexShader);
            GL.AttachShader(_handle, fragmentShader);

            // And then link them together.
            LinkProgram(_handle);

            // When the shader program is linked, it no longer needs the individual shaders attached to it; the compiled code is copied into the shader program.
            // Detach them, and then delete them.
            GL.DetachShader(_handle, vertexShader);
            GL.DetachShader(_handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);

            // The shader is now ready to go, but first, we're going to cache all the shader uniform locations.
            // Querying this from the shader is very slow, so we do it once on initialization and reuse those values
            // later.

            // First, we have to get the number of active uniforms in the shader.
            GL.GetProgram(_handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

            // Next, allocate the dictionary to hold the locations.
            UniformLocations = new Dictionary<string, int>();

            // Loop over all the uniforms,
            for (var i = 0; i < numberOfUniforms; i++)
            {
                // get the name of this uniform,
                var key = GL.GetActiveUniform(_handle, i, out _, out _);

                // get the location,
                var location = GL.GetUniformLocation(_handle, key);

                // and then add it to the dictionary.
                UniformLocations.Add(key, location);
            }
        }

        /// <summary>
        /// Create a shader object <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glCreateShader.xhtml">glCreateShader</see>
        /// abd set the source code of the shader program <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glShaderSource.xhtml">glShaderSource</see>
        /// </summary>
        /// <param name="type">The type of shader to create</param>
        /// <param name="path">Path to the shader program source code</param>
        /// <returns></returns>
        private static int CreateShader(ShaderType type, string path)
        {
            // Create an empty shader
            var shader = GL.CreateShader(type);

            // Bind the GLSL source code
            GL.ShaderSource(shader, File.ReadAllText(path));

            return shader;
        }

        /// <summary>
        /// Compile shader program <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glCompileShader.xhtml">glCompileShader</see>
        /// </summary>
        /// <param name="shader">Shader object handle</param>
        /// <exception cref="Exception">Error occured during shader compilation</exception>
        private static void CompileShader(int shader)
        {
            // Try to compile the shader
            GL.CompileShader(shader);

            // Check for compilation errors
            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                // We can use `GL.GetShaderInfoLog(shader)` to get information about the error.
                var infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
            }
        }

        /// <summary>
        /// Link shader program <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glLinkProgram.xhtml">glLinkProgram</see>
        /// </summary>
        /// <param name="program">Shader object handle</param>
        /// <exception cref="Exception">Error occured during shader linking</exception>
        private static void LinkProgram(int program)
        {
            // We link the program
            GL.LinkProgram(program);

            // Check for linking errors
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int)All.True)
            {
                // We can use `GL.GetProgramInfoLog(program)` to get information about the error.
                throw new Exception($"Error occurred whilst linking Program({program})");
            }
        }

        /// <summary>
        /// Install shader object as part of current rendering state <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUseProgram.xhtml">glUseProgram</see>
        /// </summary>
        public void Use()
        {
            GL.UseProgram(_handle);
        }
    }
}
