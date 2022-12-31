using Unity.Entities;

readonly partial struct WeaponAspect : IAspect
{
    readonly RefRO<Weapon> m_Weapon;

    public Entity ProjectilePrefab => m_Weapon.ValueRO.ProjectilePrefab;
    public Entity ProjectileSpawn => m_Weapon.ValueRO.ProjectileSpawn;
}