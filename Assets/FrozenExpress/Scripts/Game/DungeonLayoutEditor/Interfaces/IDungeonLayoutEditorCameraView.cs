using UnityEngine;

namespace FrozenExpress.Game.DungeonLayoutEditor
{
    public interface IDungeonLayoutEditorCameraView
    {
        Quaternion Rotation { get; set; }
        Vector3 Position { get; set; }
    }
}
