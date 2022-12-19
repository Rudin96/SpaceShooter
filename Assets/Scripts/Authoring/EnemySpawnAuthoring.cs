using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class EnemySpawnAuthoring : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public Transform PlayerTransform;

    class EnemySpawnBaker : Baker<EnemySpawnAuthoring>
    {
        public override void Bake(EnemySpawnAuthoring authoring)
        {
            AddComponent(new EnemySpawner
            {
                EnemyPrefab = GetEntity(authoring.EnemyPrefab),
                EnemySpawnTransform = GetEntity(authoring.transform),
                PlayerTransform = GetEntity(authoring.PlayerTransform.transform)
            });
        }
    }
}

struct EnemySpawner : IComponentData
{
    public Entity EnemyPrefab;
    public Entity EnemySpawnTransform;
    public Entity PlayerTransform;
}