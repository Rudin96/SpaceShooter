using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;

[BurstCompile]
partial struct EnemyMovementSystem : ISystem
{

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        var dir = float3.zero;
        dir.x = 1.0f;
        foreach (var transform in SystemAPI.Query<TransformAspect>().WithAll<Enemy>())
        {
            //dir.x = 1.0f;
            //float3 dir = float3.zero;
            transform.LocalPosition += dir * deltaTime * 6.0f;
        }
    }
}
