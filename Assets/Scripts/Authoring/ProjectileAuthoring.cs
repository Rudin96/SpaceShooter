using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ProjectileAuthoring : MonoBehaviour
{
    public float ProjectileSpeed = 100.0f;

    class ProjectileBaker : Baker<ProjectileAuthoring>
    {
        public override void Bake(ProjectileAuthoring authoring)
        {
            AddComponent(new Projectile
            {
                ProjectileSpeed= authoring.ProjectileSpeed
            });
        }
    }
}

struct Projectile : IComponentData
{
    public float3 Speed;
    public float ProjectileSpeed;
}