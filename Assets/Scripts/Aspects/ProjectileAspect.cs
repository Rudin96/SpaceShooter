using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

readonly partial struct ProjectileAspect : IAspect
{
    public readonly Entity Self;

    readonly TransformAspect Transform;

    readonly RefRW<Projectile> Projectile;

    public float3 Position
    {
        get => Transform.LocalPosition;
        set => Transform.LocalPosition = value;
    }

    public float3 Speed
    {
        get => Projectile.ValueRO.Speed;
        set => Projectile.ValueRW.Speed = value;
    }

    public float ProjectileSpeed => Projectile.ValueRO.ProjectileSpeed;
}