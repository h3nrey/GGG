using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour Player;

    [Header("Movement")]
    public float moveSpeed;
    public Vector2 vel;

    [Header("Dodge")]
    public float dodgeCooldown;
    public bool canDodge;
    public float dodgeForce;

    [Header("Input")]
    public Vector2 moveInput;
    public Vector2 lastMoveInput = new Vector2(1, 0);
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
        canDodge = true;
    }

    private void Awake() {
        if (Player == null)
            Player = this;
    }

    private void FixedUpdate() {
        vel = rb.velocity;
        Movement();
    }

    private void Update() {
        HandleFacing();
        SetAnimation();
    }

    private void Movement() {
        rb.velocity = moveInput * moveSpeed * Time.fixedDeltaTime;
    }

    public void ExecuteDodge() {
        if(Mathf.Abs(vel.magnitude) > 0.01 && canDodge) {
            rb.MovePosition((Vector2)transform.position + lastMoveInput * dodgeForce * Time.fixedDeltaTime);
            StartCoroutine(HandleDodgeCooldown());
        }
    }

    public IEnumerator HandleDodgeCooldown() {
        canDodge = false;
        yield return new WaitForSeconds(dodgeCooldown);
        canDodge = true;
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
