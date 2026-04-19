using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    [SerializeField] Transform _cameraTransform;
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _lookSensitivity = 2f;

    CharacterController _characterController;
    Vector2 _moveInput;
    Vector2 _lookInput;
    float _cameraPitchRotation = 0f;

    const float _minCameraRotation = -60f;
    const float _maxCameraRotation = 60f;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleLook();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = Vector3.zero;

        // Calculate movement relative to player's forward and right directions
        moveDirection += _playerTransform.forward * _moveInput.y * _moveSpeed;
        moveDirection += _playerTransform.right * _moveInput.x * _moveSpeed;

        _characterController.SimpleMove(moveDirection);
    }

    private void HandleLook()
    {
        // Rotate player around Y axis (horizontal look)
        _playerTransform.Rotate(Vector3.up * _lookInput.x * _lookSensitivity);

        // Rotate camera on X axis (vertical look) with clamping
        _cameraPitchRotation -= _lookInput.y * _lookSensitivity;
        _cameraPitchRotation = Mathf.Clamp(_cameraPitchRotation, _minCameraRotation, _maxCameraRotation);

        _cameraTransform.localRotation = Quaternion.Euler(_cameraPitchRotation, 0f, 0f);
    }

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        _lookInput = value.Get<Vector2>();
    }
}
