using Akrab;
using DotRun3d.Player;
using UnityEngine;

namespace DonRun3D.Player
{
    public class PlayerView : CustomBehaviour, IPlayerView
    {
        [SerializeField] CapsuleCollider _collider;
        [SerializeField] Rigidbody _rigidbody;
    }

}