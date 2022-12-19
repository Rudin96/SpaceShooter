using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

readonly partial struct EnemySpawnAspect : IAspect
{
    readonly RefRO<EnemySpawner> m_EnemySpawner;

    public Entity EnemyPrefab => m_EnemySpawner.ValueRO.EnemyPrefab;

    public Entity EnemySpawnTransform => m_EnemySpawner.ValueRO.EnemySpawnTransform;

    public Entity Destination => m_EnemySpawner.ValueRO.PlayerTransform;
}
