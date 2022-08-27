using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour Player;

    [Header("Movement")]
    public Vector2 moveInput;
    public float movementSpeed;

    [Header("Mouse")]
    public Vector2 mousePos;

    [Header("Gun")]
    public Transform gun;

    [Header("Components")]
    public Rigidbody2D rb;

    private void Awake() {
        if (Player == null)
            Player = this;
    }

    private void FixedUpdate() {
        Movement();
        
    }

    private void Update() {
        SettingFacing();
    }

    private void Movement() {
        rb.velocity = moveInput * movementSpeed * Time.fixedDeltaTime;
    }

    private void SettingFacing() {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        gun.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        //Vector2 direc = (Vector2)transform.position - mousePos;

        //gun.right = direc;
    }
}
