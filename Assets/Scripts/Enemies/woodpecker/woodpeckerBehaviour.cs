using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Utils;

public enum direction {
    horizontal = 0,
    vertical = 1,

}

public class woodpeckerBehaviour : MonoBehaviour
{
    public Woodpecker _woodpecker;
    private Animator anim;
    private Rigidbody2D rb;


    public enum animTags {
        compressing,
        launching,
    };

    //states
    [ReadOnly] public bool aimed;
    [ReadOnly] public bool insideLookingRange;
    [ReadOnly] private bool onLookingRoutine;
    [ReadOnly] public bool canLaunch;

    #region data
    private float lookingRange;
    private float launchForce;
    private LayerMask playerLayer;

    //looking
    private float compressingCooldown;
    private float rotateSpeed;

    //normal routine
    [EnumFlags]
    public direction flags;
    public Vector2 startPos;
    private float walkSpeed;
    private Vector2 walkDir;
    [Range(-1, 1)]
    [SerializeField] private int walkSense;

    #endregion

    private void OnEnable() {
        if(_woodpecker) {
            playerLayer = _woodpecker.PlayerMask;
            lookingRange = _woodpecker.lookingRange;
            rotateSpeed = _woodpecker.rotateSpeed;
            launchForce = _woodpecker.launchForce;
            compressingCooldown = _woodpecker.compressingCooldown;
            walkSpeed = _woodpecker.walkSpeed;
            walkDir = _woodpecker.walkDir;
        }
    }
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startPos = transform.position;
       
    }

    private void Update() {
        AimingPlayer();
    }

    private void FixedUpdate() {
        HandleRanges();
        handleLaunchRay();
        HandleNormalRoutine();
    }

    private void AimingPlayer() {
        Vector2 lookDir = transform.position - PlayerBehaviour.Player.transform.position;
        if (insideLookingRange) {
            transform.up = Vector2.MoveTowards(transform.up, lookDir, Time.deltaTime * rotateSpeed);
            if ((Vector2)transform.up == lookDir) {
                Launch();
            }

        }
    }

    private void HandleRanges() {
        insideLookingRange = Physics2D.OverlapCircle(transform.position, lookingRange, playerLayer);
        if(Has.Changed("insideLookingRange", insideLookingRange) && insideLookingRange == false) {
            StartCoroutine(ChangeOnLookingRoutine(false));
        }
    }

    private IEnumerator ChangeOnLookingRoutine(bool state) {
        yield return new WaitForSeconds(compressingCooldown);
        onLookingRoutine = state;
    }

    private void HandleNormalRoutine() {
        if(!onLookingRoutine) {
            rb.velocity = walkDir * walkSpeed * walkSense * Time.fixedDeltaTime;
        }
    }

    private void handleLaunchRay() {
        if(insideLookingRange) {
            aimed = Physics2D.Raycast(transform.position, -transform.up, lookingRange, playerLayer);
            if (Has.Changed("aimed", aimed) && aimed == true) {
                StartCoroutine(CompressingToLaunch());
            }
        }
        
    }

    public void ChangeWalkSense() {
        walkSpeed *= -1;
    }

    private void Launch() {
        rb.AddForce(-transform.up * launchForce, ForceMode2D.Impulse);
    }

    IEnumerator CompressingToLaunch() {
        rb.velocity = Vector2.zero;
        anim.SetTrigger("compressing");
        yield return new WaitForSeconds(compressingCooldown);
        Launch();
    }

    public void StopEntity() {
        print("Teste");
        rb.velocity = Vector2.zero;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lookingRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, -transform.up * lookingRange);
    }
}
