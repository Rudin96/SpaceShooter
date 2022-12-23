using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

readonly partial struct PlayerAspect : IAspect
{
    public readonly Entity Self;

    readonly TransformAspect Transform;

    readonly RefRW<Player> Player;

    public float3 Position
    {
        get => Transform.LocalPosition; set => Transform.LocalPosition = value;
    }

    public float Speed
    {
        get => Player.ValueRO.Speed; set => Player.ValueRW.Speed = value;
    }
}
