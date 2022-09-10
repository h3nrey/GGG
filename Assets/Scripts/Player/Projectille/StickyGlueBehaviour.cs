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

    private void Start() {
        dir = PlayerBehaviour.Player.lookDir;
        line = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        line.SetPosition(0, PlayerBehaviour.Player.gunMuzzle.position);

        StartCoroutine(Dry());
    }

    private IEnumerator Dry() {
        yield return new WaitForSeconds(dryTimer);
        print("dried");
        dried = true;
        yield return new WaitForSeconds(dryTimer + 0.2f);
        DestroyOnDry();
    }

    private void DestroyOnDry() {
        Destroy(this.gameObject);
    }

    private void Update() {
        
        line.SetPosition(1, transform.position);
    }

    private void FixedUpdate() {
        if (!dried) {
            rb.velocity = dir * lineVelocity * Time.fixedDeltaTime;
            PlayerBehaviour.Player.canMove = false;
            PlayerBehaviour.Player.canUseGun = false;
            PlayerBehaviour.Player.canShoot = false;
        }
        else {
            rb.velocity = Vector2.zero;
            PlayerBehaviour.Player.canMove = true;
            PlayerBehaviour.Player.rb.MovePosition(Vector2.MoveTowards(PlayerBehaviour.Player.rb.position, line.GetPosition(1), PlayerBehaviour.Player.stickyGlueFollowSpeed * Time.fixedDeltaTime));
        }

        if(PlayerBehaviour.Player.rb.position == (Vector2)line.GetPosition(1)) {
            PlayerBehaviour.Player.canUseGun = true;
            PlayerBehaviour.Player.canShoot = true;
        }
    }


}
