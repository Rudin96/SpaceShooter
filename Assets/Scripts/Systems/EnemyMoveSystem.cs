using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

[BurstCompile]
partial struct EnemyJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter ECB;

    public float DeltaTime;

    void Execute([ChunkIndexInQuery] int chunkIndex, ref EnemyAspect enemy)
    {
        enemy.Speed = math.normalize(enemy.Destination - enemy.Position) * 7.0f;

        enemy.Position += enemy.Speed * DeltaTime;

        var playerDistanceLength = math.length(enemy.Position - enemy.Destination);
        var outOfBoundsLength = math.length(enemy.Position - enemy.StartPosition);

        if(playerDistanceLength < 1.0f || outOfBoundsLength > 30.0f)
        {
            ECB.DestroyEntity(chunkIndex, enemy.Self);
        }
    }
}

[BurstCompile]
partial struct EnemyMoveSystem : ISystem
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
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        var enemyJob = new EnemyJob
        {
            ECB = ecb.AsParallelWriter(),
            DeltaTime = SystemAPI.Time.DeltaTime
        };

        enemyJob.ScheduleParallel();
    }
}