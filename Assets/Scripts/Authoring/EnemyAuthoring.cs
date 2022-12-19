using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour
{
    class EnemyBaker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            AddComponent<Enemy>();
        }
    }
}

struct Enemy : IComponentData
{
    public float3 Speed;
    public float3 StartPos;
    public float3 Destination;
}