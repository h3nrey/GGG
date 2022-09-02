using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour Player;

    [Header("Movement")]
    public float moveSpeed;
    public Vector2 vel;

    [Header("Dodge")]
    [Tooltip("the value will change depeding on the dodge animation time")]
    public float dodgeCooldown;
    public bool canDodge;
    public float dodgeForce;

    [Header("Input")]
    public Vector2 moveInput;
    public Vector2 lastMoveInput = new Vector2(1, 0);
    public Vector2 mousePos;
    public Vector2 lookDir;

    #region GUN
    [Foldout("Gun")]
    public Transform gun;
    [Foldout("Gun")]
    public GameObject projectillePrefab;
    [Foldout("Gun")]
    public float projectilleSpeed;
    [Foldout("Gun")]
    public Transform gunMuzzle;
    [Foldout("Gun")]
    public float gunCooldown;
    [Foldout("Gun")]
    public float gunCooldownCounter;
    [Foldout("Gun")]
    public bool canShoot = true;
    #endregion

    [Header("Ammunation")]
    public int gunAmmo = 10;

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
        SettingLookDir();
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

    private void SettingLookDir() {
        lookDir = mousePos - (Vector2)transform.position;
    }
    

}
