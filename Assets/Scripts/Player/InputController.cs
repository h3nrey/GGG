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

    public void Move(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();

        if (moveInput != Vector2.zero) lastInput = moveInput;
    }

    public void Shoot(InputAction.CallbackContext context) {
        if(context.started) {
            PlayerBehaviour.Player._gunController.createProjectille();
            PlayerBehaviour.Player._gunController.CallCooldownCoroutine();
        }
    }

    public void GetMousePos() {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); 
    }

    public void Dodge(InputAction.CallbackContext context) {
        if(context.started) {
            PlayerBehaviour.Player.ExecuteDodge();
        }
    }
}
