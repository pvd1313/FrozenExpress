using UnityEngine;

namespace FrozenExpress.Game.Core.Player
{
    public class PlayerSpawnPointView : MonoBehaviour, IPlayerSpawnPointView
    {
        public Vector3 Position => transform.position;
    }
}
