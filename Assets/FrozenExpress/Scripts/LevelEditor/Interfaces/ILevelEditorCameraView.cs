using UnityEngine;

namespace FrozenExpress.LevelEditor
{
    public interface ILevelEditorCameraView
    {
        Quaternion Rotation { get; set; }
        Vector3 Position { get; set; }
    }
}
