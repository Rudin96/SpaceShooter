using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

class PlayerAuthoring : MonoBehaviour
{
    class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring playerAuthoring)
        {
            AddComponent<Player>();
        }
    }
}

struct Player : IComponentData
{
    public float3 Position;
    public quaternion Rotation;
    public float3 Speed;
}