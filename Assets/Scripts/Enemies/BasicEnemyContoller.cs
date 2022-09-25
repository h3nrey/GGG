using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyContoller : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float sinWaveFrequency;
    private Rigidbody2D rb;
    private Vector2 startPos;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        startPos = (Vector2)transform.position;
    }
    private void FixedUpdate() {
        //ping pong to enemies = Vector2(initial pos + Mathf.pingPong(time.time * the speed of enemy, the max distance that the enemy can travel))
        transform.position = new Vector2( startPos.x + Mathf.PingPong(Time.time * walkSpeed, 5), rb.velocity.y) ;
    }

    private void Update() {
        transform.GetChild(0).position = new Vector2(transform.GetChild(0).position.x, transform.position.y + Mathf.Sin(Time.time * -1) * 1);
    }

}
