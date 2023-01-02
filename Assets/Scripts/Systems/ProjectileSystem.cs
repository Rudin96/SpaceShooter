using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

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

        //EntityQuery enemyQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(Enemy));

        //var projCollisionJob = new ProjectileCollisionJob
        //{
        //    ECB = ecb.AsParallelWriter()
        //};

        //projCollisionJob.ScheduleParallel();

        projectileJob.ScheduleParallel();
    }
}

[BurstCompile]
partial struct ProjectileCollisionJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter ECB;
    public NativeArray<Entity> Enemies;

    void Execute(ref ProjectileAspect projectile)
    {
        UnityEngine.Debug.Log("Collision");
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