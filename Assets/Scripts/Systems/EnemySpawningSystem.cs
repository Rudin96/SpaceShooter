using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;

[BurstCompile]
partial struct EnemySpawningSystem : ISystem
{
    ComponentLookup<WorldTransform> m_LocalToWorldTransformFromEntity;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        m_LocalToWorldTransformFromEntity = state.GetComponentLookup<WorldTransform>(true);
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        m_LocalToWorldTransformFromEntity.Update(ref state);
        
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        var enemySpawnJob = new EnemySpawn
        {
            WorldTransformLookup = m_LocalToWorldTransformFromEntity,
            ECB = ecb
        };

        enemySpawnJob.Schedule();
    }
}

partial struct EnemySpawn : IJobEntity
{
    [ReadOnly] public ComponentLookup<WorldTransform> WorldTransformLookup;
    public EntityCommandBuffer ECB;

    void Execute(in EnemySpawnAspect enemySpawnAspect)
    {
        var instance = ECB.Instantiate(enemySpawnAspect.EnemyPrefab);
        var spawnLocalToWorld = WorldTransformLookup[enemySpawnAspect.EnemySpawnTransform];
        var enemyTransform = LocalTransform.FromPosition(spawnLocalToWorld.Position);
        ECB.SetComponent(instance, enemyTransform);
        ECB.SetComponent(instance, new Enemy
        {
            Speed = spawnLocalToWorld.Forward() * 10.0f
        });
    }
}