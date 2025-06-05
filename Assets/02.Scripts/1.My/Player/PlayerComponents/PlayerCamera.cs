using Core;
using Core.Events;
using Players;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private CinemachineCamera _vCam;
    [SerializeField] private CinemachineBasicMultiChannelPerlin multiChannelPerlin;
    [SerializeField] private EventChannelSO cameraChannel;

    [SerializeField] private Transform camTarget;
    [SerializeField] private float scrollSpeed = 0.1f;

    [SerializeField] private float shakeThreshold = 0.01f;
    [SerializeField] private float decreaseSpeed = 1f;

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
        cameraChannel.AddListener<CameraShakeEvent>(HandleCameraShake);
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
        _vCam.Lens.FieldOfView = Mathf.Lerp(_vCam.Lens.FieldOfView, presseed ? 30f : 60f, Time.time);
    }
    private void SetPointerLock(bool value)
    {
        Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
