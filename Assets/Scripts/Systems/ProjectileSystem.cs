using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
partial struct ProjectileSystem : ISystem
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

        var projectileJob = new ProjectileJob
        {
            ECB = ecb.AsParallelWriter(),
            DeltaTime = SystemAPI.Time.DeltaTime
        };

        projectileJob.ScheduleParallel();
    }
}

[BurstCompile]
partial struct ProjectileJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter ECB;

    public float DeltaTime;

    void Execute([ChunkIndexInQuery] int chunkIndex, ref ProjectileAspect projectile)
    {
        projectile.Position += projectile.Speed * DeltaTime;

        if (math.abs(projectile.Position.x) > 40.0f || math.abs(projectile.Position.y) > 40.0f)
        {
            ECB.DestroyEntity(chunkIndex, projectile.Self);
        }
    }
}