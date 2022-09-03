using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour {
    private void Update() {
        GetMousePos();
    }
    private Vector2 moveInput{
        get => PlayerBehaviour.Player.moveInput;
        set => PlayerBehaviour.Player.moveInput = value;
    }

    private Vector2 lastInput {
        get => PlayerBehaviour.Player.lastMoveInput;
        set => PlayerBehaviour.Player.lastMoveInput = value;
    }

    private Vector2 mousePos {
        get => PlayerBehaviour.Player.mousePos;
        set => PlayerBehaviour.Player.mousePos = value;
    }

    public void MoveInput(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();

        if (moveInput != Vector2.zero) lastInput = moveInput;
    }

    public void ShootInput(InputAction.CallbackContext context) {

        if(context.started) {
            PlayerBehaviour.Player._gunController.Shoot();
        }
    }

    public void GetMousePos() {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); 
    }

    public void DodgeInput(InputAction.CallbackContext context) {
        if(context.started) {
            PlayerBehaviour.Player.ExecuteDodge();
        }
    }
    public void WarmingUpInput(InputAction.CallbackContext context) {
        if (context.performed) {
            PlayerBehaviour.Player.pressingShootButton = true;
        }

        if (context.canceled) {
            PlayerBehaviour.Player.pressingShootButton = false;
        }
    }

    public void ScrollMouse(InputAction.CallbackContext context) {
        if(context.performed) {
            PlayerBehaviour.Player.mouseScroll = context.ReadValue<Vector2>();
            PlayerBehaviour.Player._gunController.SwtichWeapon();
        }
    }
}
