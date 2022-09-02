using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualController : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sprite;

    #region properties
    private Vector2 lookDir => PlayerBehaviour.Player.lookDir;
    private Rigidbody2D rb => PlayerBehaviour.Player.rb;
    #endregion

    private void Start() {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update() {
        HandleFacing();
        SetAnimation();
    }

    private void HandleFacing() {
        if (lookDir.x > 1) {
           transform.localScale = new Vector3(1f,transform.localScale.y,transform.localScale.z);

        } else if (lookDir.x < 1) {
           transform.localScale = new Vector3(-1f,transform.localScale.y,transform.localScale.z);
        }
    }

    private void SetAnimation() {
        anim.SetFloat("lookDirX", lookDir.normalized.x);
        anim.SetFloat("lookDirY", lookDir.normalized.y);
        anim.SetFloat("speed", rb.velocity.magnitude);
    }
}
