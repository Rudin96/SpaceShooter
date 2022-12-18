using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Weapon _weapon;

    [SerializeField] private float _movementSpeed = 150.0f;

    private Vector2 _inputVector;
    private Transform _selfTransform;

    private Mouse _mouse;

    public void SetInputVector(InputAction.CallbackContext callbackContext) => _inputVector = callbackContext.ReadValue<Vector2>();

    private void Start()
    {
        _selfTransform= GetComponent<Transform>();
        _mouse = Mouse.current;
    }

    private void Update()
    {
        Move(Time.deltaTime);
        Vector3 mouseWorldPosAdjusted = _camera.ScreenToWorldPoint(_mouse.position.ReadValue()) + Vector3.forward * 10.0f;
        _selfTransform.rotation = Quaternion.FromToRotation(Vector3.up, mouseWorldPosAdjusted - _selfTransform.position);
    }

    private void Move(float deltaTime)
    {
        _selfTransform.position += (Vector3)_inputVector * _movementSpeed * deltaTime;
    }
}
