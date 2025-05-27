using Players;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCamera : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private CinemachineCamera _vCam;

    [SerializeField] private Transform camTarget;
    [SerializeField] private float scrollSpeed = 0.1f;

    private PlayerInputSO _input;
    private Transform _plrTrm;

    private void OnEnable()
    {
        SetPointerLock(true);
    }
    public void Initialize(Player player)
    {
        _input = player.Input;
        _plrTrm = player.transform;

        AddListeners();
    }
    private void OnDestroy()
    {
        RemoveListeners();
    }

    #region listeners
    private void AddListeners()
    {
        _input.OnLookPressed += HandleLookPressed;
    }
    private void RemoveListeners()
    {
        _input.OnLookPressed -= HandleLookPressed;
    }
    #endregion

    //private void FixedUpdate()
    //{
    //    Vector3 camPos = _plrTrm.position;
    //    camTarget.position += new Vector3(0, camTarget.position.y + _input.MouseScroll.y * scrollSpeed, 0);
    //}

    private void HandleLookPressed(bool presseed)
    {
        _vCam.Lens.FieldOfView = presseed ? 30f : 60f;
    }
    private void SetPointerLock(bool value)
    {
        Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
