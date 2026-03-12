using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterMaster
{
    public InputActionAsset InputActions;

    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;
    private InputAction _fireAction;

    private CharacterController _characterController;
    private PlayerModelViewer _playerModelViewer;
    private Transform _cameraTransform;
    private Vector2 _movementInput;
    private Vector2 _lookInput;
    private Vector3 _move;
    private float _currentXRotation;

    public Vector2 Sensitivity = Vector2.one;

    public override void Setup(CharacterBody body)
    {
        base.Setup(body);

        _moveAction = InputActions.FindAction("Move");
        _lookAction = InputActions.FindAction("Look");
        _jumpAction = InputActions.FindAction("Jump");
        _fireAction = InputActions.FindAction("PrimaryFire");

        _moveAction.Enable();
        _lookAction.Enable();
        _jumpAction.Enable();
        _fireAction.Enable();

        _playerModelViewer = GetComponentInChildren<PlayerModelViewer>();
        _characterController = CharacterBody.CharacterController;
        _cameraTransform = Camera.main.transform;

        _cameraTransform.position = CharacterBody.Placeholders.HeadPlaceholder.position;
        _cameraTransform.parent = CharacterBody.Placeholders.HeadPlaceholder;

        IsSetup = true;
    }

    public void Update()
    {
        if (!IsSetup) return;

        _movementInput = _moveAction.ReadValue<Vector2>();
        _lookInput = _lookAction.ReadValue<Vector2>();

        _currentXRotation -= _lookInput.y * Sensitivity.y;
        _currentXRotation = Mathf.Clamp(_currentXRotation, -90f, 90f);

        _cameraTransform.localRotation = Quaternion.Euler(_currentXRotation, 0f, 0f);   //this smells a bit...

        transform.Rotate(Vector3.up, _lookInput.x * Sensitivity.x);

        var planarForward = _cameraTransform.forward;
        planarForward.y = 0f;
        planarForward.Normalize();

        _move = (planarForward * _movementInput.y) + (_cameraTransform.right * _movementInput.x);
        _move *= CharacterBody.Stats.Stats.MovementSpeed;
        _move.y = _characterController.velocity.y;
        _move.y += Globals.GRAVITY * Time.deltaTime;

        if (_characterController.isGrounded && _jumpAction.WasPressedThisFrame())
        {
            _move += CharacterBody.Stats.Stats.JumpingVelocity;
        }

        _characterController.Move(_move * Time.deltaTime);
    }
}
