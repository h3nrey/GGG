using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(Rigidbody2D))]
public class StickyGlueBehaviour : MonoBehaviour
{
    private LineRenderer line;
    [SerializeField] float dryTimer;
    [SerializeField] bool dried;
    private Vector3 dir;
    [SerializeField] float lineVelocity;
    private Rigidbody2D rb;
    public float range;
    public LayerMask playerLayer;
    private bool closeToPlayer;

    private Rigidbody2D playerRb => PlayerBehaviour.Player.rb;

    private void Start() {
        dir = PlayerBehaviour.Player.lookDir;
        line = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        line.SetPosition(0, PlayerBehaviour.Player.gunMuzzle.position);
        PlayerBehaviour.Player.canUseGun = false;
        PlayerBehaviour.Player.canShoot = false;
        PlayerBehaviour.Player.rb.velocity = Vector2.zero;
        StartCoroutine(Dry());
    }

    private IEnumerator Dry() {
        yield return new WaitForSeconds(dryTimer);
        dried = true;
        yield return new WaitForSeconds(dryTimer + 0.2f);
        DestroyOnDry();
    }

    private void DestroyOnDry() {
        Destroy(this.gameObject);
    }

    private void Update() {
        line.SetPosition(0, PlayerBehaviour.Player.gunMuzzle.position);
        line.SetPosition(1, transform.position);
    }

    private void FixedUpdate() {
        if (!dried) {
            rb.velocity = dir * lineVelocity * Time.fixedDeltaTime;
            
        }
        else {
            rb.velocity = Vector2.zero;
            PlayerBehaviour.Player.rb.MovePosition(Vector2.MoveTowards(PlayerBehaviour.Player.rb.position, line.GetPosition(1), PlayerBehaviour.Player.stickyGlueFollowSpeed * Time.fixedDeltaTime));
        }

        if(PlayerBehaviour.Player.rb.position == (Vector2) line.GetPosition(1) || closeToPlayer) {
            PlayerBehaviour.Player.canUseGun = true;
            PlayerBehaviour.Player.canShoot = true;
            print("can use");
        }

        closeToPlayer = Physics2D.OverlapCircle(transform.position, range, playerLayer);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
