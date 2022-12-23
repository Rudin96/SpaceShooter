using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

[BurstCompile]
partial struct EnemySpawnSystem : ISystem
{
    ComponentLookup<WorldTransform> m_WorldTransformLookup;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        m_WorldTransformLookup = state.GetComponentLookup<WorldTransform>(true);
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        m_WorldTransformLookup.Update(ref state);

        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);


        var enemySpawnJob = new EnemySpawn
        {
            WorldTransformLookup = m_WorldTransformLookup,
            ECB = ecb
        };

        enemySpawnJob.Schedule();
    }
}

[BurstCompile]
partial struct EnemySpawn : IJobEntity
{
    [ReadOnly] public ComponentLookup<WorldTransform> WorldTransformLookup;
    public EntityCommandBuffer ECB;

    void Execute(in EnemySpawnAspect enemy)
    {
        var instance = ECB.Instantiate(enemy.EnemyPrefab);
        var spawnLocalToWorld = WorldTransformLookup[enemy.EnemySpawnTransform];
        var playerLocalToWorldPos = WorldTransformLookup[enemy.Destination];
        var enemyTransform = LocalTransform.FromPosition(spawnLocalToWorld.Position);



        ECB.SetComponent(instance, enemyTransform);
        ECB.SetComponent(instance, new Enemy
        {
            Speed = spawnLocalToWorld.Up() * 10.0f,
            StartPos = new float3(10.0f, 0.0f, 0.0f),
            Destination = playerLocalToWorldPos.Position
        });
    }
}