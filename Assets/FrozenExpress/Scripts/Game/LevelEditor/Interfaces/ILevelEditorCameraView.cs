using UnityEngine;

namespace FrozenExpress.Game.LevelEditor
{
    public interface ILevelEditorCameraView
    {
        Quaternion Rotation { get; set; }
        Vector3 Position { get; set; }
    }
}
