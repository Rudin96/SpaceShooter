using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

readonly partial struct EnemySpawnAspect : IAspect
{
    readonly RefRO<Spawner> m_EnemySpawner;

    public Entity EnemyPrefab => m_EnemySpawner.ValueRO.EnemyPrefab;
    public Entity EnemySpawnTransform => m_EnemySpawner.ValueRO.EnemySpawnTransform;
}
