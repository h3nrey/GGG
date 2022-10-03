using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    private LayerMask enemyLayer => PlayerBehaviour.Player.enemyLayer;
    private void OnTriggerEnter2D(Collider2D other) {
        GameObject obj = other.gameObject;
        string objTag = obj.tag;

        if(objTag == "ammo") {
            PlayerBehaviour.Player._gunController.GetAmmo();
            Destroy(obj);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        GameObject obj = other.gameObject;
        string objTag = obj.tag;
        LayerMask objLayer = obj.layer;

        if(enemyLayer.value == objLayer.value) {
            int damage = obj.GetComponent<EnemyBehaviour>().damage;
            PlayerBehaviour.Player._lifeController.TakeDamage(damage);
        }
    }

    private void FixedUpdate() {
        for (int i = 0; i < PlayerBehaviour.Player.projectillesHolder.childCount; i++) {
            Transform child = PlayerBehaviour.Player.projectillesHolder.GetChild(i);

            if(child.gameObject.tag != "shield") {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), child.GetComponent<Collider2D>());
            }
        }
    }
}
