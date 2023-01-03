using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
partial struct ProjectileCollisionSystem : ISystem
{
    EntityQuery _obstacleQuery;

    ComponentLookup<WorldTransform> _worldTransformLookup;
    ComponentLookup<Player> _playerLookup;

    NativeArray<Entity> _obstacles;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        _obstacleQuery = state.GetEntityQuery(ComponentType.ReadOnly<Enemy>());
        _worldTransformLookup = state.GetComponentLookup<WorldTransform>(true);
        _playerLookup = state.GetComponentLookup<Player>(true);

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

        _worldTransformLookup.Update(ref state);
        _playerLookup.Update(ref state);

        _obstacles = _obstacleQuery.ToEntityArray(Allocator.Persistent);

        new ProjectileCollisionJob { ECB = ecb.AsParallelWriter(), TransformLookup = _worldTransformLookup, Obstacles = _obstacles, PlayerLookup = _playerLookup }.ScheduleParallel();
    }
}


[WithAll(typeof(Projectile))]
[BurstCompile]
partial struct ProjectileCollisionJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter ECB;
    [ReadOnly] public ComponentLookup<WorldTransform> TransformLookup;
    [ReadOnly] public ComponentLookup<Player> PlayerLookup;

    [ReadOnly] public NativeArray<Entity> Obstacles;

    void Execute([ChunkIndexInQuery]int chunkIndex, Entity entity)
    {
        for (int i = 0; i < Obstacles.Length; i++)
        {
            var obstacle = Obstacles[i];

            var obstacleTransformLookup = TransformLookup[obstacle];
            var projTransformLookup = TransformLookup[entity];

            var length = math.length(obstacleTransformLookup.Position - projTransformLookup.Position);


            bool condition = length < 1.0f;

            if (condition)
            {
                //ECB.DestroyEntity(chunkIndex, entity);
                ECB.DestroyEntity(chunkIndex, Obstacles[i]);
                //UnityEngine.Debug.Log($"Destroy: {i}");
            }
        }
    }
}