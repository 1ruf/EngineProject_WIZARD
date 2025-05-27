using Players;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private CharacterController charController;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultily = 1.4f;
    [SerializeField] private float rotationSpeed = 8f;

    private Player _player;
    private Transform _camTargetTrm;
    private Transform _playerTrm;
    private PlayerInputSO _input;
    private Vector3 _moveDir;

    private bool _isSprint;



    public void Initialize(Player player)
    {
        _player = player;
        _input = player.Input;
        _camTargetTrm = player.camTarget;
        _playerTrm = player.transform;

        _input.OnSprintPressed += OnSprint;
    }
    private void OnDestroy()
    {
        _input.OnSprintPressed -= OnSprint;
    }
    private void OnSprint(bool obj)
    {
        _isSprint = obj;
    }

    private void FixedUpdate()
    {
        CalcMovement();
        SetRotation();
        Move();
    }

    private void SetRotation()
    {
        if (_moveDir.sqrMagnitude > 0.01f)
        {
            Vector3 lookDir = new Vector3(_moveDir.x, 0, _moveDir.z);
            Quaternion targetRot = Quaternion.LookRotation(lookDir);
            _playerTrm.rotation = Quaternion.Slerp(_playerTrm.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void CalcMovement()
    {
        _moveDir = Vector3.zero;
        if (_player.CanMove)
        {
            float speed = moveSpeed;

            if (_isSprint) // 예시: Shift 누르면 달리기
                speed *= sprintMultily;

            _moveDir = GetMovement().normalized * speed;
        }
        else
        {
            _moveDir = Vector3.zero;
        }
    }


    private Vector3 GetMovement()
    {
        Vector3 camForward = _camTargetTrm.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = _camTargetTrm.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 move = camForward * _input.MovementKey.y + camRight * _input.MovementKey.x;
        return move;
    }


    private void Move()
    {
        charController.Move(_moveDir * Time.fixedDeltaTime);
    }
}
