using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] GunMode[] gunModes;
    public GunMode currentGunMode;
    public List<GunMode> guns;
    public int gunModeState = 0;

    #region read & write
    private bool canShoot {
        get => PlayerBehaviour.Player.canShoot;
        set => PlayerBehaviour.Player.canShoot = value;
    }
    

    private int gunAmmo {
        get => PlayerBehaviour.Player.gunAmmo;
        set => PlayerBehaviour.Player.gunAmmo = value;
    }
    #endregion

    #region readonly
    private Vector2 mousePos => PlayerBehaviour.Player.mousePos;

    private GameObject[] projectillePrefabs => PlayerBehaviour.Player.projectillePrefabs;
    private Transform gunMuzzle => PlayerBehaviour.Player.gunMuzzle;
    private Transform gun => PlayerBehaviour.Player.gun;

    private float projectilleSpeed => PlayerBehaviour.Player.projectilleSpeed;

    private float gunCooldown => PlayerBehaviour.Player.gunCooldown;
    #endregion

    private void Start() {
        //GunMode gun = new GunMode(gunMode.ToString(), projectillePrefabs[0], 10, 1, 0);
        foreach (var gunMode in gunModes) {
            guns.Add(gunMode);
        }

        currentGunMode = guns[0];

    }

    private void Update() {
        SettingFacing();
    }

    public void Shoot() {
        createProjectille();
        CallCooldownCoroutine();
    }

    public void createProjectille() {
        if (canShoot && gunAmmo > 0 && PlayerBehaviour.Player.currentGunHeat >= currentGunMode.heatPerProjectille ) {
            GameObject projectille = Instantiate(currentGunMode.projectille, gunMuzzle.position, gun.rotation);
            gunAmmo--;

        }
    }

    public void SwtichWeapon() {
        int stateChanger = (int)PlayerBehaviour.Player.mouseScroll.y;

        if(stateChanger > 0) {
            if(gunModeState  < guns.Count - 1) {
                gunModeState++;
            } else if(gunModeState == guns.Count - 1) {
                gunModeState = 0;
            }
        } else if (stateChanger < 0) {
            if (gunModeState > 0) {
                gunModeState--;
            } else if(gunModeState == 0) {
                gunModeState = guns.Count - 1;
            }
        }
        currentGunMode = guns[gunModeState];
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
