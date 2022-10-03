using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="new enemy/woodpecker")]
public class Woodpecker : Enemy
{
    public GameObject Object;
    public float speed;

    //states
    public bool onLookingRoutine;
    public bool aimed;

    //Launch
    public float compressingCooldown;
    public float launchForce;


    public LayerMask PlayerMask;

    //looking routine
    public float lookingRange;
    public float rotateSpeed;


}
