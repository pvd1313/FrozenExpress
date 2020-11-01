using UnityEngine;

namespace FrozenExpress.Game.DungeonEditor
{
    public interface ILevelEditorCameraView
    {
        Quaternion Rotation { get; set; }
        Vector3 Position { get; set; }
    }
}
