using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BisCAD.Common
{
    public class BernsteinShader
    {
        public readonly int Handle;

        private readonly Dictionary<string, int> _uniformLocations;

        public BernsteinShader(string vertPath, string geomPath, string fragPath)
        {
            var shaderSource = File.ReadAllText(vertPath);
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, shaderSource);
            CompileShader(vertexShader);

            shaderSource = File.ReadAllText(fragPath);
            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, shaderSource);
            CompileShader(fragmentShader);

            shaderSource = File.ReadAllText(geomPath);
            var geometryShader = GL.CreateShader(ShaderType.GeometryShader);
            GL.ShaderSource(geometryShader, shaderSource);
            CompileShader(geometryShader);

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, geometryShader);
            GL.AttachShader(Handle, fragmentShader);


            // Set the input type of the primitives we are going to feed the geometry shader, this should be the same as
            // the primitive type given to GL.Begin. If the types do not match a GL error will occur (todo: verify GL_INVALID_ENUM, on glBegin)
            GL.Ext.ProgramParameter(Handle, AssemblyProgramParameterArb.GeometryInputType, (int)BeginMode.Points);
            // Set the output type of the geometry shader. Becasue we input Lines we will output LineStrip(s).
            GL.Ext.ProgramParameter(Handle, AssemblyProgramParameterArb.GeometryOutputType, (int)BeginMode.LineStrip);

            // We must tell the shader program how much vertices the geometry shader will output (at most).
            // One simple way is to query the maximum and use that.
            // NOTE: Make sure that the number of vertices * sum(components of active varyings) does not
            // exceed MaxGeometryTotalOutputComponents.
            GL.Ext.ProgramParameter(Handle, AssemblyProgramParameterArb.GeometryVerticesOut, 50);


            LinkProgram(Handle);
            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, geometryShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(geometryShader);
            GL.DeleteShader(vertexShader);


            GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

            _uniformLocations = new Dictionary<string, int>();

            for (var i = 0; i < numberOfUniforms; i++)
            {
                var key = GL.GetActiveUniform(Handle, i, out _, out _);
                var location = GL.GetUniformLocation(Handle, key);
                _uniformLocations.Add(key, location);
            }
        }

        private static void CompileShader(int shader)
        {
            GL.CompileShader(shader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
            }
        }

        private static void LinkProgram(int program)
        {
            GL.LinkProgram(program);

            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int)All.True)
            {
                throw new Exception($"Error occurred whilst linking Program({program})");
            }
        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }

        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(Handle, attribName);
        }



        public void SetInt(string name, int data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(_uniformLocations[name], data);
        }

        public void SetFloat(string name, float data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(_uniformLocations[name], data);
        }

        public void SetMatrix4(string name, Matrix4 data)
        {
            GL.UseProgram(Handle);
            GL.UniformMatrix4(_uniformLocations[name], true, ref data);
        }

        public void SetVector3(string name, Vector3 data)
        {
            GL.UseProgram(Handle);
            GL.Uniform3(_uniformLocations[name], data);
        }
    }
}