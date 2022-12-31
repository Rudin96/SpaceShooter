using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class WeaponAuthoring : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public Transform ProjectileTransform;
    class WeaponBaker : Baker<WeaponAuthoring>
    {
        public override void Bake(WeaponAuthoring authoring)
        {
            AddComponent(new Weapon
            {
                ProjectilePrefab = GetEntity(authoring.ProjectilePrefab),
                ProjectileSpawn = GetEntity(authoring.ProjectileTransform)
            });

            AddComponent<Shooting>();
        }
    }
}

struct Weapon : IComponentData
{
    public Entity ProjectilePrefab;
    public Entity ProjectileSpawn;
}