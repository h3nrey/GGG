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
        SettingGunFacing();
        SetAnimation();
    }

    private void HandleFacing() {
        if (lookDir.x > 0) {
           transform.localScale = new Vector3(1f,transform.localScale.y,transform.localScale.z);

        } else if (lookDir.x < 0) {
           transform.localScale = new Vector3(-1f,transform.localScale.y,transform.localScale.z);
        }
    }

    private void SetAnimation() {
        anim.SetFloat("lookDirX", lookDir.normalized.x);
        anim.SetFloat("lookDirY", lookDir.normalized.y);
        anim.SetFloat("speed", rb.velocity.magnitude);
    }

    private void SettingGunFacing() {
        Vector2 lookDir = PlayerBehaviour.Player.mousePos - PlayerBehaviour.Player.rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        float yScale = 0;

        if (angle > -90 && angle < 90) {
            yScale = 1;
        }
        else if (angle > 90 || angle < -90) {
            yScale = -1;
        }

        if (PlayerBehaviour.Player.canUseGun) {
            PlayerBehaviour.Player.gun.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            PlayerBehaviour.Player.gun.localScale = new Vector3(PlayerBehaviour.Player.gun.localScale.x, yScale, PlayerBehaviour.Player.gun.localScale.z);
        }
    }
}
