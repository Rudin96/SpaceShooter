using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.InputSystem;

class PlayerAuthoring : UnityEngine.MonoBehaviour
{
    private UnityEngine.Vector3 _lookVector;
    private UnityEngine.Vector3 _moveVector;

    public void SetLookVector(InputAction.CallbackContext callbackContext) { _lookVector = callbackContext.ReadValue<UnityEngine.Vector2>(); }
    public void SetMoveVector(InputAction.CallbackContext callbackContext) { _moveVector = callbackContext.ReadValue<UnityEngine.Vector2>(); UnityEngine.Debug.Log(_moveVector); }

    class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring playerAuthoring)
        {
            AddComponent(new Player
            {
                Position = playerAuthoring.transform.position,
                Speed = playerAuthoring._moveVector
            });
        }
    }
}

struct Player : IComponentData
{
    public quaternion Rotation;
    public float3 Speed;
    public float3 Position;
}