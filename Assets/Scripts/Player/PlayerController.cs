using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    [SerializeField] private float _movementSpeed = 150.0f;

    //[SerializeField] private InputActionAsset _inputActions;

    private Vector2 _inputVector;
    private Vector2 _lookVector;
    private Transform _selfTransform;
    //private PlayerInput _playerInput;
    private Camera _camera;

    //private Mouse _mouse;

    //public void SetInputVector(InputAction.CallbackContext callbackContext) { _inputVector = callbackContext.ReadValue<Vector2>(); Debug.Log("Input Vector!"); }
    //public void SetLookVector(InputAction.CallbackContext callbackContext) => _lookVector = callbackContext.ReadValue<Vector2>();

    private void Awake()
    {
        //_playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        _selfTransform= GetComponent<Transform>();
        //_mouse = Mouse.current;
        _camera = CameraSingleton.Instance;
        //foreach (var actionMap in _inputActions.actionMaps)
        //{
        //    Debug.Log(actionMap.name);
        //}
    }

    private void Update()
    {
        Move(Time.deltaTime);
        //Vector3 lookDirection = _playerInput.currentControlScheme == "KBM" ? (_camera.ScreenToWorldPoint(_mouse.position.ReadValue()) + Vector3.forward * 10.0f) - _selfTransform.position : _lookVector;
        //_selfTransform.rotation = Quaternion.FromToRotation(Vector3.up, lookDirection);
    }

    private void Move(float deltaTime)
    {
        _selfTransform.position += (Vector3)_inputVector * _movementSpeed * deltaTime;
    }
}
