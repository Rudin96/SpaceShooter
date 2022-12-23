using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class WeaponAuthoring : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    class WeaponBaker : Baker<WeaponAuthoring>
    {
        public override void Bake(WeaponAuthoring authoring)
        {
            AddComponent(new Weapon
            {
                ProjectilePrefab = GetEntity(authoring.ProjectilePrefab)
            });
        }
    }
}

struct Weapon : IComponentData
{
    public Entity ProjectilePrefab;
}