using UnityEngine;

namespace FrozenExpress.Scripts.Game.LevelEditor.Interfaces
{
    public interface ILevelEditorCameraView
    {
        Quaternion Rotation { get; set; }
        Vector3 Position { get; set; }
    }
}
