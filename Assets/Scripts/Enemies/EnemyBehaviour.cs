using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Enemy _enemy;
    private int layer;
    public Animator anim;
    public int damage;

    private void Start() {
        anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = _enemy.animController;
        damage = _enemy.damage;
        gameObject.layer = _enemy.layer;

    }
}
