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
    }

    private void Update() {
        
        line.SetPosition(1, transform.position);
    }

    private void FixedUpdate() {
        if (!dried) {
            rb.velocity = dir * lineVelocity * Time.fixedDeltaTime;
        }
        else {
            rb.velocity = Vector2.zero;
        }
    }
}
