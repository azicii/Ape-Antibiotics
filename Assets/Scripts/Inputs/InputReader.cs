using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader")]
public class InputReader : ScriptableObject, PlayerInputs.INormalGameplayActions
{
    // This is the API that will be used moving forward to read player inputs

    #region Initialization

    private PlayerInputs playerInputs;

    private void OnEnable()
    {
        // Create a new instance
        playerInputs = new PlayerInputs();

        // Set Input System Callbacks
        playerInputs.NormalGameplay.SetCallbacks(this);

        // Start with the enabled Action Map
        // This is for debugging and a game manager can control this at a later stage [Tegomlee].
        EnableNormalGameplayActionMap();
    }

    #endregion

    #region Events API

    // Movement and Aim
    public event Action<Vector2> MovementEvent;
    public event Action<Vector2> AimEvent;

    // Default actions
    public event Action<bool> SprintEvent;
    public event Action<bool> CrouchEvent;
    public event Action JumpStartedEvent;
    public event Action JumpCancelledEvent;

    // Combat and Utility actions
    public event Action CombatOneStartedEvent;
    public event Action CombatOneCancelledEvent;

    #endregion

    #region Enable and Disable Action Maps

    public void EnableNormalGameplayActionMap()
    {
        // Disable other Action Maps

        // Enable Normal Gameplay Action Map
        playerInputs.NormalGameplay.Enable();
    }

    #endregion

    #region Normal Gameplay Action Interface

    public void OnAim(InputAction.CallbackContext context)
    {
        AimEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnCombatAbility1(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            CombatOneStartedEvent?.Invoke();
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            CombatOneCancelledEvent?.Invoke();
        }
    }

    public void OnCrouchSlide(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            CrouchEvent?.Invoke(true);
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            CrouchEvent?.Invoke(false);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            JumpStartedEvent?.Invoke();
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            JumpCancelledEvent?.Invoke();
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            SprintEvent?.Invoke(true);
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            SprintEvent?.Invoke(false);
        }
    }

    #endregion
}
