using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Utils;

public class woodpeckerBehaviour : MonoBehaviour
{
    public Woodpecker _woodpecker;
    private Rigidbody2D rb;

    //states
    [ReadOnly] public bool aimed;
    [ReadOnly] public bool onLookingRoutine;
    [ReadOnly] public bool canLaunch;

    #region data
    private float lookingRange;
    private float launchForce;
    private LayerMask playerLayer;

    //looking
    private float compressingCooldown;
    private float rotateSpeed;

    #endregion

    private void OnEnable() {
        if(_woodpecker) {
            playerLayer = _woodpecker.PlayerMask;
            lookingRange = _woodpecker.lookingRange;
            rotateSpeed = _woodpecker.rotateSpeed;
            launchForce = _woodpecker.launchForce;
            compressingCooldown = _woodpecker.compressingCooldown;
        }
    }
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
       
    }

    private void Update() {
        AimingPlayer();
    }

    private void FixedUpdate() {
        HandleRanges();
        handleLaunchRay();
    }

    private void AimingPlayer() {
        Vector2 lookDir = transform.position - PlayerBehaviour.Player.transform.position;
        if (onLookingRoutine) {
            transform.up = Vector2.MoveTowards(transform.up, lookDir, Time.deltaTime * rotateSpeed);
            if ((Vector2)transform.up == lookDir) {
                Launch();
            }

        }
    }

    private void HandleRanges() {
        onLookingRoutine = Physics2D.OverlapCircle(transform.position, lookingRange, playerLayer);
    }

    private void handleLaunchRay() {
        if(onLookingRoutine) {
            aimed = Physics2D.Raycast(transform.position, -transform.up, lookingRange, playerLayer);
            if (Has.Changed("aimed", aimed) && aimed == true) {
                StartCoroutine(CompressingToLaunch());
            }
        }
        
    }

    private void Launch() {
        rb.AddForce(-transform.up * launchForce, ForceMode2D.Impulse);
    }

    IEnumerator CompressingToLaunch() {
        yield return new WaitForSeconds(compressingCooldown);
        Launch();
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lookingRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, -transform.up * lookingRange);
    }
}
