using System.Collections;
using System.Collections.Generic;
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
        enemy.Position += enemy.Speed * DeltaTime;

        var sqrlength = math.lengthsq(enemy.Position - enemy.StartPosition);

        if(sqrlength > 10.0f)
        {
            ECB.DestroyEntity(chunkIndex, enemy.Self);
        }
    }
}

[BurstCompile]
partial struct CannonBallSystem : ISystem
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