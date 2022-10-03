using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] GunMode[] gunModes;
    public GunMode currentGunMode;
    public List<GunMode> guns;
    public int gunModeState = 0;
    public bool onRange;

    #region read & write
    private bool canShoot {
        get => PlayerBehaviour.Player.canShoot;
        set => PlayerBehaviour.Player.canShoot = value;
    }

    private int ammo {
        get => PlayerBehaviour.Player.ammo;
        set => PlayerBehaviour.Player.ammo = value;
    }

    private float currentGunHeat {
        get => PlayerBehaviour.Player.currentGunHeat;
        set => PlayerBehaviour.Player.currentGunHeat = value;
    }
    #endregion

    #region readonly
    private Vector2 mousePos => PlayerBehaviour.Player.mousePos;

    private GameObject[] projectillePrefabs => PlayerBehaviour.Player.projectillePrefabs;
    private Transform gunMuzzle => PlayerBehaviour.Player.gunMuzzle;
    private float gunRangeDistance => PlayerBehaviour.Player.gunRangeDistance;
    private LayerMask wallLayer => PlayerBehaviour.Player.wallLayer;
    private Transform gun => PlayerBehaviour.Player.gun;

    private float projectilleSpeed => PlayerBehaviour.Player.projectilleSpeed;
    private Transform projectillesHolder => PlayerBehaviour.Player.projectillesHolder;

    private float gunCooldown => PlayerBehaviour.Player.gunCooldown;

    private bool canUseGun => PlayerBehaviour.Player.canUseGun;

    private float maxGunHeat => PlayerBehaviour.Player.maxGunHeat;

    private float heatMultiplier => PlayerBehaviour.Player.heatMultiplier;

    private float coolMultipler => PlayerBehaviour.Player.coolMultipler;
    #endregion

    private void Start() {
        foreach (var gunMode in gunModes) {
            guns.Add(gunMode);
        }

        currentGunMode = guns[0];

    }

    private void Update() {
        if (PlayerBehaviour.Player.pressingShootButton) {
            WarmingUp();
        }
        else {
            Colling();
        }
    }

    private void FixedUpdate() {
        checkIfCanShoot();
    }

    public void Shoot() {
        if(canShoot && ammo - currentGunMode.ammoCost > 0 && onRange && currentGunHeat >= currentGunMode.heatPerProjectille) {
            createProjectille();
            CallCooldownCoroutine();
        }
    }

    private void checkIfCanShoot() {
       if(currentGunMode.name == "sticky") {
            onRange = Physics2D.Raycast(gunMuzzle.position, gunMuzzle.right, gunRangeDistance, wallLayer);
       } else {
            onRange = true;
        }
    }

    #region create projectille
    public void createProjectille() {
            GameObject projectille = Instantiate(currentGunMode.projectille, gunMuzzle.position, gun.rotation, projectillesHolder);
            ammo -= currentGunMode.ammoCost;
    }
    #endregion

    #region cooldown coroutine
    public void CallCooldownCoroutine() {
        StartCoroutine(HandleShootCooldown());
    }

    private IEnumerator HandleShootCooldown() {
        canShoot = false;
        yield return new WaitForSeconds(gunCooldown);
        canShoot = true;
    }
    #endregion   
    
    #region warming system
    private void WarmingUp() {
        if (currentGunHeat < maxGunHeat) {
            currentGunHeat += Time.deltaTime * heatMultiplier;
        }
    }

    private void Colling() {
        if (currentGunHeat > 0) {
            currentGunHeat -= Time.deltaTime * coolMultipler;
        }
    }
    #endregion

    #region switch weapon
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
        //print(guns[gunModeState].gunSprite);
        PlayerBehaviour.Player.gunSprite.sprite = guns[gunModeState].gunSprite;
    }
    #endregion

    #region ammo
    public void GetAmmo() {
        ammo++;
    }
    #endregion

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(gunMuzzle.position, gunMuzzle.right * gunRangeDistance);

    }
}
