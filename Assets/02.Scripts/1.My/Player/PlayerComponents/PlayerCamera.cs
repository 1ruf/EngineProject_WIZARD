using Core;
using Core.Events;
using Players;
using Unity.Cinemachine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening.Core;

public class PlayerCamera : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private CinemachineCamera _vCam;
    [SerializeField] private CinemachineBasicMultiChannelPerlin multiChannelPerlin;
    [SerializeField] private EventChannelSO cameraChannel;

    [SerializeField] private Transform camTarget;

    [SerializeField] private float shakeThreshold = 0.01f;
    [SerializeField] private float decreaseSpeed = 1f;

    private Player _player;
    private PlayerInputSO _input;
    private Transform _plrTrm;

    private bool _isPointerLocked;

    private void OnEnable()
    {
        SetPointerLock(true);
    }
    public void Initialize(Player player)
    {
        _player = player;
        _input = player.Input;
        _plrTrm = player.transform;

        AddListeners();
    }
    public void SetShake(float power)
    {
        multiChannelPerlin.AmplitudeGain = power;
    }
    private void FixedUpdate()
    {
        DecreaseShake();
    }
    private void OnDestroy()
    {
        RemoveListeners();
    }

    #region listeners
    private void AddListeners()
    {
        _input.OnLookPressed += HandleLookPressed;
        _input.OnPointerLockPressed += HandlePointerLock;
        cameraChannel.AddListener<CameraShakeEvent>(HandleCameraShake);
    }

    private void HandlePointerLock()
    {
        SetPointerLock(!_isPointerLocked);
    }

    private void RemoveListeners()
    {
        _input.OnLookPressed -= HandleLookPressed;
        cameraChannel.RemoveListener<CameraShakeEvent>(HandleCameraShake);
    }
    #endregion

    private void HandleCameraShake(CameraShakeEvent callback)
    {
        multiChannelPerlin.AmplitudeGain = callback.Power;
        multiChannelPerlin.FrequencyGain = callback.Speed;
    }
    private void DecreaseShake()
    {
        float grain = multiChannelPerlin.AmplitudeGain;
        if (grain < shakeThreshold && grain > -shakeThreshold)
        {
            multiChannelPerlin.AmplitudeGain = 0;
            return;
        }
        multiChannelPerlin.AmplitudeGain -= decreaseSpeed * Time.deltaTime;
    }
    private void HandleLookPressed(bool presseed)
    {
        if (_player.CanMove == false) return;
        _vCam.Lens.FieldOfView = Mathf.Lerp(_vCam.Lens.FieldOfView, presseed ? 30f : 60f, Time.time);
    }
    private void SetPointerLock(bool value)
    {
        _isPointerLocked = value;
        Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
    }
    public void SceneChange(bool value)
    {
        if (value)
        {
            Camera main = Camera.main;
            main.DOFieldOfView(60f, 0.5f).SetEase(Ease.InOutQuad);
        }
        else
        {
            _vCam.Lens.FieldOfView = Mathf.Lerp(150f, 60f, Time.time);
        }
        ///_vCam.enabled = value;
    }
}
