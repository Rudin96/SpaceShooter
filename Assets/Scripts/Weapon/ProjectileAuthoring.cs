using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ProjectileAuthoring : MonoBehaviour
{
    public float Speed = 100.0f;

    class ProjectileBaker : Baker<ProjectileAuthoring>
    {
        public override void Bake(ProjectileAuthoring authoring)
        {
            AddComponent<Projectile>();
        }
    }
}

struct Projectile : IComponentData
{
    public float3 Speed;
}