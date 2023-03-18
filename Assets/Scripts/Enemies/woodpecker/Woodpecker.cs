using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="new enemy/woodpecker")]
public class Woodpecker : Enemy
{
    public GameObject Object;
    public float speed;

    //states
    public bool insideLookingRange;
    public bool aimed;

    //Launch
    public float compressingCooldown;
    public float launchForce;


    public LayerMask PlayerMask;

    //looking routine
    public float lookingRange;
    public float rotateSpeed;

    //normal routine 
    public enum direction {horizontal, vertical}
    public float walkSpeed;
    public float walkSense;
    public Vector2 walkDir;


}
