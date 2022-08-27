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
    public Vector2 lookDir;

    [Header("Gun")]
    public Transform gun;
    public GameObject projectillePrefab;
    public float projectilleSpeed;
    public Transform gunMuzzle;
    public float gunCooldown;
    public float gunCooldownCounter;
    public bool canShoot = true;

    [Header("Ammunation")]
    public int gunAmmo = 10;

    [Header("Visuals")]
    public GameObject Graphic;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator anim;

    [Header("Other Scripts")]
    public GunController _gunController;

    private void Start() {
        canShoot = true;
    }

    private void Awake() {
        if (Player == null)
            Player = this;
    }

    private void FixedUpdate() {
        Movement();
        
    }

    private void Update() {
        HandleFacing();
        SetAnimation();
    }

    private void Movement() {
        rb.velocity = moveInput * movementSpeed * Time.fixedDeltaTime;
    }


    private void HandleFacing() {
        lookDir = mousePos - (Vector2)transform.position;
        if(lookDir.x > 1) {
            Graphic.transform.localScale = new Vector3(1f, Graphic.transform.localScale.y, Graphic.transform.localScale.z);
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.eulerAngles.z));
            
        } else if(lookDir.x < 1) {
            Graphic.transform.localScale = new Vector3(-1f, Graphic.transform.localScale.y, Graphic.transform.localScale.z);
            //transform.rotation = Quaternion.Euler(new Vector3(0, 180, transform.eulerAngles.z));
        }
    }

    private void SetAnimation() {
        lookDir = mousePos - (Vector2)transform.position;
        anim.SetFloat("lookDirX", lookDir.normalized.x);
        anim.SetFloat("lookDirY", lookDir.normalized.y);
        anim.SetFloat("speed", rb.velocity.magnitude);
    }
    

}
