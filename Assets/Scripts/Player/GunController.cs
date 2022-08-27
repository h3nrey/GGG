using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private bool canShoot {
        get => PlayerBehaviour.Player.canShoot;
        set => PlayerBehaviour.Player.canShoot = value;
    }

    private int gunAmmo {
        get => PlayerBehaviour.Player.gunAmmo;
        set => PlayerBehaviour.Player.gunAmmo = value;
    }

    private Vector2 mousePos => PlayerBehaviour.Player.mousePos;

    private GameObject projectillePrefab => PlayerBehaviour.Player.projectillePrefab;
    private Transform gunMuzzle => PlayerBehaviour.Player.gunMuzzle;
    private Transform gun => PlayerBehaviour.Player.gun;

    private float projectilleSpeed => PlayerBehaviour.Player.projectilleSpeed;

    private float gunCooldown => PlayerBehaviour.Player.gunCooldown;

    private void Update() {
        SettingFacing();
    }

    public void createProjectille() {
        if (canShoot && gunAmmo > 0) {
            GameObject projectille = Instantiate(projectillePrefab, gunMuzzle.position, gun.rotation);
            Rigidbody2D projectilleRb = projectille.GetComponent<Rigidbody2D>();
            projectilleRb.velocity = gun.right * projectilleSpeed * Time.fixedDeltaTime;
            gunAmmo--;

        }
    }

    public void CallCooldownCoroutine() {
        StartCoroutine(HandleShootCooldown());
    }

    private IEnumerator HandleShootCooldown() {
        canShoot = false;
        yield return new WaitForSeconds(gunCooldown);
        canShoot = true;
    }

    public void GetAmmo() {
        gunAmmo++;
    }

    private void SettingFacing() {
        Vector2 lookDir = mousePos - PlayerBehaviour.Player.rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        float yScale = 0;

        if(angle > -90 && angle < 90) {
            yScale = 1;
        } else if(angle > 90 || angle < -90) {
            yScale = -1;
        }

        gun.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        gun.localScale = new Vector3(gun.localScale.x, yScale, gun.localScale.z);
    }
}
