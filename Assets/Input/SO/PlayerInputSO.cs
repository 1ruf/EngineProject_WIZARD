using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Players
{
    [CreateAssetMenu(fileName = "PlayerInput", menuName = "SO/PlayerInput", order = 0)]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        [SerializeField] private LayerMask whatIsGround;
        
        public event Action OnAttackPressed;
        public event Action OnFKeyPressed;
        public event Action OnSkillCreatePressed;
        public event Action OnPointerLockPressed;

        public event Action<bool> OnSprintPressed;
        public event Action<bool> OnLookPressed;
        public event Action<bool> Stack1;
        public event Action<bool> Stack2;
        public event Action<bool> Stack3;

        public Vector2 MovementKey { get; private set; }
        private Controls _controls;

        public Vector2 MouseScroll { get; private set; }

        private Vector3 _worldPosition; //이게 마우스의 월드 좌표
        private Vector2 _screenPosition; //이게 마우스가 위치한 화면좌표

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 movementKey = context.ReadValue<Vector2>();
            MovementKey = movementKey;
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnAttackPressed?.Invoke();
        }

        public Vector3 GetWorldPosition()
        {
            Camera mainCam = Camera.main; //Unity2022부터 내부 캐싱이 되서 그냥 써도 돼.
            Debug.Assert(mainCam != null, "No main camera in this scene");
            
            Ray cameraRay = mainCam.ScreenPointToRay(_screenPosition);
            if (Physics.Raycast(cameraRay, out RaycastHit hit, mainCam.farClipPlane, whatIsGround))
            {
                _worldPosition = hit.point;
            }

            return _worldPosition;
        }
        
        public void OnPointer(InputAction.CallbackContext context)
        {
            _screenPosition = context.ReadValue<Vector2>();
        }

        public void OnSecretSkill(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnFKeyPressed?.Invoke();
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnSprintPressed?.Invoke(true);
            else if (context.canceled)
                OnSprintPressed?.Invoke(false);
        }

        public void OnStack1(InputAction.CallbackContext context)
        {
            if (context.performed)
                Stack1?.Invoke(true);
            else if (context.canceled)
                Stack1?.Invoke(false);
        }

        public void OnStack2(InputAction.CallbackContext context)
        {
            if (context.performed)
                Stack2?.Invoke(true);
            else if (context.canceled)
                Stack2?.Invoke(false);
        }

        public void OnStack3(InputAction.CallbackContext context)
        {
            if (context.performed)
                Stack3?.Invoke(true);
            else if (context.canceled)
                Stack3?.Invoke(false);
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnLookPressed?.Invoke(true);
            else if (context.canceled)
                OnLookPressed?.Invoke(false);
        }

        public void OnScroll(InputAction.CallbackContext context)
        {
            Vector2 scroll = context.ReadValue<Vector2>();
            MouseScroll = scroll;
        }

        public void OnSkillCreate(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnSkillCreatePressed?.Invoke();
        }

        public void OnMouceLock(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnPointerLockPressed?.Invoke();
            }
        }
    }
}