using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
partial struct WeaponShootingSystem : ISystem
{
    private bool _shooting;

    ComponentLookup<Shooting> _shootingLookup;
    ComponentLookup<WorldTransform> _transformLookup;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        _shootingLookup = state.GetComponentLookup<Shooting>();
        _transformLookup = state.GetComponentLookup<WorldTransform>();
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
        _shootingLookup.Update(ref state);
        _transformLookup.Update(ref state);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            _shooting = true;
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            _shooting = false;
        }

        var weaponToggleJob = new WeaponToggleJob
        {
            ShootingLookup = _shootingLookup,
            IsShooting = _shooting
        };

        var WeaponShootJob = new WeaponShootJob
        {
            TransformLookup = _transformLookup,
            ECB = ecb.AsParallelWriter()
        };

        weaponToggleJob.ScheduleParallel();
        WeaponShootJob.ScheduleParallel();
    }
}

[WithAll(typeof(Shooting))]
[BurstCompile]
partial struct WeaponShootJob : IJobEntity
{
    [ReadOnly] public ComponentLookup<WorldTransform> TransformLookup;
    public EntityCommandBuffer.ParallelWriter ECB;

    void Execute([ChunkIndexInQuery]int chunkIndexInQuery, ref WeaponAspect weaponAspect)
    {
        var instance = ECB.Instantiate(chunkIndexInQuery, weaponAspect.ProjectilePrefab);
        var spawnLocalToWorld = TransformLookup[weaponAspect.ProjectileSpawn];
        var projectileTransform = LocalTransform.FromPosition(spawnLocalToWorld.Position);

        ECB.SetComponent(chunkIndexInQuery, instance, projectileTransform);
        ECB.SetComponent(chunkIndexInQuery, instance, new Projectile
        {
            Speed = spawnLocalToWorld.Up() * 20.0f
        });
    }
}

[WithAll(typeof(Weapon))]
[BurstCompile]
partial struct WeaponToggleJob : IJobEntity
{
    [NativeDisableParallelForRestriction] public ComponentLookup<Shooting> ShootingLookup;
    public bool IsShooting;

    void Execute(Entity entity)
    {
        ShootingLookup.SetComponentEnabled(entity, IsShooting);
    }
}