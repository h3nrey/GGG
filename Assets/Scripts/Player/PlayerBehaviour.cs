using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour Player;

    [Header("Movement")]
    public float moveSpeed;
    public Vector2 vel;
    public bool canMove;

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
    public Vector2 mouseScroll;
    public bool pressingShootButton;

    [Header("Layers")]
    public LayerMask wallLayer;
    public int enemyLayer;

    #region GUN
    [Foldout("Gun")]
    public Transform gun;
    [Foldout("Gun")]
    public SpriteRenderer gunSprite;
    [Foldout("Gun")]
    public GameObject[] projectillePrefabs;
    [Foldout("Gun")]
    public float projectilleSpeed;
    [Foldout("Gun")]
    public Transform gunMuzzle;
    [Foldout("Gun")]
    public float gunRangeDistance;
    [Foldout("Gun")]
    public float gunCooldown;
    [Foldout("Gun")]
    public float gunCooldownCounter;
    [Foldout("Gun")]
    public bool canShoot = true;
    [Foldout("Gun")]
    public bool canUseGun = true;
    [Foldout("Gun")]
    public float stickyGlueFollowSpeed;
    [Foldout("Gun")]
    public Transform projectillesHolder;
    #endregion

    [Header("Heat Controll")]
    [ProgressBar("maxGunHeat", EColor.Orange)]
    public float currentGunHeat = 0;
    public float maxGunHeat = 50;
    public float heatMultiplier;
    public float coolMultipler;
    public float HeatPerShoot = 5;

    [Header("Ammunation")]
    public int ammo = 10;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator anim;
    public Collider2D collider;

    [Header("Other Scripts")]
    public GunController _gunController;
    public LifeController _lifeController;

    private void Start() {
        canShoot = true;
        canDodge = true;
        canMove = true;
    }

    private void Awake() {
        if (Player == null)
            Player = this;
        _lifeController = GetComponent<LifeController>();
    }

    private void FixedUpdate() {
        vel = rb.velocity;
        Movement();
        Physics2D.IgnoreLayerCollision(6, 7);
    }

    private void Update() {
        SettingLookDir();
    }

    private void Movement() {
        if(canMove) 
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
        lookDir.Normalize();
    }


    

}
