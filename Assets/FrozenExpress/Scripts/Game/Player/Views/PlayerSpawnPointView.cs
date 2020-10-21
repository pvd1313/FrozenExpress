using UnityEngine;

namespace FrozenExpress.Game.Player
{
    public class PlayerSpawnPointView : MonoBehaviour, IPlayerSpawnPointView
    {
        public Vector3 Position => transform.position;
    }
}
