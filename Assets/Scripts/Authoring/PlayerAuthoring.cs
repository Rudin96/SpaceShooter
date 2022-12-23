using Unity.Entities;
using Unity.Mathematics;

class PlayerAuthoring : UnityEngine.MonoBehaviour
{
    [UnityEngine.SerializeField] private float Speed = 10.0f;
    class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring playerAuthoring)
        {
            AddComponent(new Player
            {
                Position = playerAuthoring.transform.position,
                Speed = playerAuthoring.Speed
            });
        }
    }
}

struct Player : IComponentData
{
    public quaternion Rotation;
    public float Speed;
    public float3 Position;
}