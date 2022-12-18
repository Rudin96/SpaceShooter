using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class SpawnerAuthoring : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public Transform Player;

    class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            AddComponent(new Spawner
            {
                EnemyPrefab = GetEntity(authoring.EnemyPrefab),
                EnemySpawnTransform = GetEntity(authoring.transform)
            });
        }
    }
}

struct Spawner : IComponentData
{
    public Entity EnemyPrefab;
    public Entity EnemySpawnTransform;
}