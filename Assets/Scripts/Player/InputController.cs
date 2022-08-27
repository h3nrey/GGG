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

    private Vector2 mousePos {
        get => PlayerBehaviour.Player.mousePos;
        set => PlayerBehaviour.Player.mousePos = value;
    }

    public void Move(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
    }

    public void GetMousePos() {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); 

    }
}
