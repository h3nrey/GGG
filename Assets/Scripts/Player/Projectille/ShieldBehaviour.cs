using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    public int life = 3;
    private int currentLife;
    [SerializeField] private float shieldImpulseForce;

    private void Start() {
        currentLife = life;
        PlayerBehaviour.Player.rb.AddForce(-transform.right * shieldImpulseForce, ForceMode2D.Impulse);
    }
    void TakeDamage(int damage) {
        if(currentLife > 0) {
            currentLife -= damage;

            if(currentLife == 0) {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        GameObject obj = other.gameObject;

        if(obj.tag == "projectille") {
            TakeDamage(obj.GetComponent<DamageController>().damage);
        }
    }
}
