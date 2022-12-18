using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;

[BurstCompile]
partial struct EnemyMovementJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter ECB;

    public float DeltaTime;

    void Execute([ChunkIndexInQuery] int chunkIndex, ref EnemySpawnAspect enemy)
    {
        
    }
}

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

    public void OnUpdate(ref SystemState state)
    {
        
    }
}
