using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

readonly partial struct EnemyAspect : IAspect
{
    public readonly Entity Self;

    readonly TransformAspect Transform;

    readonly RefRW<Enemy> Enemy;

    public float3 Position
    {
        get => Transform.LocalPosition;
        set => Transform.LocalPosition = value;
    }

    public float3 StartPosition
    {
        get => Enemy.ValueRO.StartPos;
    }

    public float3 Destination
    {
        get => Enemy.ValueRO.Destination;
    }

    public float3 Speed
    {
        get => Enemy.ValueRO.Speed;
        set => Enemy.ValueRW.Speed = value;
    }
}
