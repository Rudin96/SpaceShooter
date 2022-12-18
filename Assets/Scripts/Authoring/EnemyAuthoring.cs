using Unity.Entities;
using Unity.Mathematics;

class EnemyAuthoring : UnityEngine.MonoBehaviour
{
    public UnityEngine.Transform PlayerPrefab;

    class EnemyBaker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            AddComponent<Enemy>();
        }
    }
}

struct Enemy : IComponentData
{
    public float3 Speed;
}