using System;
using UnityEngine;

namespace Gameplay.Misc
{
    public class CharacterDeathExplosionHandler : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] meshes;

        private void OnEnable()
        {
            foreach (var mesh in meshes)
            {
                mesh.AddForce(transform.position * 20, ForceMode.Force);
                mesh.useGravity = true;
            }
        }
    }
}