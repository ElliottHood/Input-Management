using UnityEngine;
using UnityEngine.InputSystem;

namespace InputManagement
{
    /// <summary>
    /// Attached PlayerInput component should use UnityEvents, and each input action should correspond to one of the functions in this script
    /// 
    /// In Edit > Project Settings > Script Execution Order, the value for this script should be negative, so that input is obtained before other scripts run
    /// 
    /// When Creating A New Input:
    /// - Add mapping in inputActions
    /// - Add corresponding variable in FrameInput
    /// - Add new inputAction function in the InputActions region of this script
    /// - Add C# Event from PlayerInput component to the new function in the scenne view
    /// 
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    [DefaultExecutionOrder(-100)]
    public class InputManager : MonoBehaviour
    {
        public InputState Input { get; private set; }
        public float ThisUpdateTime { get; private set; }

        public void Update()
        {
            ThisUpdateTime = Time.time;
        }

        #region PlayerInput

        private PlayerInput playerInput;

        private void OnValidate()
        {
            GetComponent<PlayerInput>().notificationBehavior = PlayerNotifications.InvokeUnityEvents;
        }

        #endregion


        #region Enabling / Disabling

        public void Enable()
        {
            playerInput.enabled = true;
        }

        public void Disable()
        {
            playerInput.enabled = false;
        }

        #endregion


        #region Scene Singleton

        [SerializeField] private bool isSingleton = true;

        public static InputManager Instance { get; private set; }

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();

            if (isSingleton)
            {
                if (Instance != null && Instance != this)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Instance = this;
                    Input = new InputState();
                }
            }
            else
            {
                Input = new InputState();
            }
        }

        #endregion


        #region Input Mode

        private const string PLAYER_ACTION_MAP = "Player";
        private const string UI_ACTION_MAP = "UI";

        public enum InputMode
        {
            Player,
            UI
        }

        public void SwitchInputMode(InputMode inputMode)
        {
            switch (inputMode)
            {
                case InputMode.Player:
                    playerInput.SwitchCurrentActionMap(PLAYER_ACTION_MAP);
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
                case InputMode.UI:
                    playerInput.SwitchCurrentActionMap(UI_ACTION_MAP);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                default:
                    break;
            }
        }

        #endregion


        #region Input Actions

        /// <summary>
        /// Movement / aim callback structure
        /// </summary>
        /// <param name="context"></param>
        public void Move(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Input.move.SetValue(context.ReadValue<Vector2>());
            }
            else if (context.canceled)
            {
                Input.move.SetValue(Vector2.zero);
            }
        }
        public void Aim(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Input.aim.SetValue(context.ReadValue<Vector2>());
            }
            else if (context.canceled)
            {
                Input.aim.SetValue(Vector2.zero);
            }
        }


        /// <summary>
        /// Copy paste this code for more input actions
        /// </summary>
        /// <param name="context"></param>

        public void Jump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Input.jump.OnPress();
            }
            else if (context.canceled)
            {
                Input.jump.OnRelease();
            }
        }

        public void Primary(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Input.primary.OnPress();
            }
            else if (context.canceled)
            {
                Input.primary.OnRelease();
            }
        }

        public void Secondary(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Input.secondary.OnPress();
            }
            else if (context.canceled)
            {
                Input.secondary.OnRelease();
            }
        }

        public void Interact(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Input.interact.OnPress();
            }
            else if (context.canceled)
            {
                Input.interact.OnRelease();
            }
        }

        public void Crouch(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Input.crouch.OnPress();
            }
            else if (context.canceled)
            {
                Input.crouch.OnRelease();
            }
        }

        public void Sprint(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Input.sprint.OnPress();
            }
            else if (context.canceled)
            {
                Input.sprint.OnRelease();
            }
        }

        public void Pause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Input.pause.OnPress();
            }
            else if (context.canceled)
            {
                Input.pause.OnRelease();
            }
        }

        #endregion

    }

}