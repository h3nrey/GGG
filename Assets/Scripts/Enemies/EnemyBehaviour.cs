using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(LifeController))]
public class EnemyBehaviour : MonoBehaviour
{
    public Enemy _enemy;
    private int layer;
    public Animator anim;
    public int damage;

    //Layers
    public LayerMask wallLayer;

    public UnityEvent WhenTouchWall;

    private void Start() {
        anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = _enemy.animController;
        damage = _enemy.damage;
        gameObject.layer = _enemy.layer;
        wallLayer = _enemy.wallLayer;

        GetComponent<LifeController>().life = _enemy.life;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        GameObject obj = other.gameObject;
        string objTag = obj.tag;
        LayerMask objLayer = obj.layer;

        if(objTag == "projectille") {
            print("hitted");
            int damage = obj.GetComponent<DamageController>().damage;
            GetComponent<LifeController>().TakeDamage(damage);
        }

        print($"obj value: {objLayer.value}, wall layer: {wallLayer}");

        if(objLayer.value == wallLayer.value) {
            print("touch wall");
            WhenTouchWall?.Invoke();
        }
    }
}
