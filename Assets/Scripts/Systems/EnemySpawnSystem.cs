using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
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
            ECB = ecb.AsParallelWriter(),
            RandomSpawn = UnityEngine.Random.Range(-30.0f, 30.0f)
        };

        enemySpawnJob.ScheduleParallel();
    }
}

[BurstCompile]
partial struct EnemySpawn : IJobEntity
{
    [ReadOnly] public ComponentLookup<WorldTransform> WorldTransformLookup;
    public EntityCommandBuffer.ParallelWriter ECB;

    public float RandomSpawn;

    void Execute([ChunkIndexInQuery]int chunkIndexInQuery, ref EnemySpawnAspect enemy)
    {
        var instance = ECB.Instantiate(chunkIndexInQuery, enemy.EnemyPrefab);
        var spawnLocalToWorld = WorldTransformLookup[enemy.EnemySpawnTransform];
        var playerLocalToWorldPos = WorldTransformLookup[enemy.Destination];
        var enemyTransform = LocalTransform.FromPosition(spawnLocalToWorld.Position);
        enemyTransform.Position = new float3(RandomSpawn, RandomSpawn, 0.0f);

        ECB.SetComponent(chunkIndexInQuery, instance, enemyTransform);
        ECB.SetComponent(chunkIndexInQuery, instance, new Enemy
        {
            StartPos = enemyTransform.Position,
            Destination = playerLocalToWorldPos.Position
        });
    }
}