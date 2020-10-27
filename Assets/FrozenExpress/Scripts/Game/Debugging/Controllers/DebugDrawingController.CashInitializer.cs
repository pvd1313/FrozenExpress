using System;
using System.Linq;
using FrozenExpress.Scripts.Game.Debugging.Data;
using FrozenExpress.Scripts.Game.Debugging.Enums;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Debugging.Controllers
{
    partial class DebugDrawingController
    {
        private static class CashInitializer
        {
            public static bool TryInititialize(DebugDrawingController owner, DebugDrawingConfig config)
            {
                var initializationValid = true;

                var expectedColors = Enum.GetValues(typeof(EDebugColor)) as EDebugColor[];
                var expectedShaders = Enum.GetValues(typeof(EDebugShader)) as EDebugShader[];
                var expectedMeshes = Enum.GetValues(typeof(EDebugShape)) as EDebugShape[];

                var colorCount = (int) expectedColors.Max() + 1;
                var shaderCount = (int) expectedShaders.Max() + 1;
                var meshCount = (int) expectedMeshes.Max() + 1;

                if (colorCount > 32)
                {
                    LogError("Too many colors");
                    return false;
                }

                if (meshCount > 64)
                {
                    LogError("Too many meshes");
                    return false;
                }

                if (shaderCount > 16)
                {
                    LogError("Too many shaders");
                    return false;
                }

                owner._colorCount = colorCount;
                owner._meshes = new Mesh[meshCount];
                owner._colors = new Color[colorCount];
                owner._shaders = new Shader[shaderCount];
                owner._materials = new Material[shaderCount * colorCount];
                owner._materialInitializationMap = new bool[shaderCount * colorCount];

                foreach (var mesh in expectedMeshes)
                {
                    var meshData = config.Meshes.FirstOrDefault(m => m.Shape == mesh);
                    if (meshData == null)
                    {
                        LogError($"Mesh '{mesh}' was not found");
                        initializationValid = false;
                        continue;
                    }

                    owner._meshes[(int) mesh] = meshData.Mesh;
                }

                foreach (var color in expectedColors)
                {
                    var colorData = config.Colors.FirstOrDefault(c => c.Color == color);
                    if (colorData == null)
                    {
                        LogError($"Color '{color}' was not found");
                        initializationValid = false;
                        continue;
                    }

                    owner._colors[(int) color] = colorData.Value;
                }

                foreach (var shader in expectedShaders)
                {
                    var shaderData = config.Shaders.FirstOrDefault(c => c.Shader == shader);
                    if (shaderData == null)
                    {
                        LogError($"Shader '{shader}' was not found");
                        initializationValid = false;
                        continue;
                    }

                    owner._shaders[(int) shader] = shaderData.Instance;
                }

                return initializationValid;
            }

            private static void LogError(string message)
            {
                Debug.LogError($"{nameof(DebugDrawingController)} :: {message}");
            }
        }
    }
}