using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PeaGlueBehaviour : MonoBehaviour
{
    private Rigidbody2D projectilleRb;
    [SerializeField] private float projectilleSpeed;
    private Vector2 projectilleDirection;

    private void Start() {
        projectilleRb = GetComponent<Rigidbody2D>();
        projectilleDirection = PlayerBehaviour.Player.lookDir;
    }

    void FixedUpdate() {
        projectilleRb.velocity = projectilleDirection * projectilleSpeed * Time.fixedDeltaTime;
    }
}
