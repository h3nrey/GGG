using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ScriptableObject
{
    public string enemyName;

    public int life;
    public LifeController lifeController;

    //layers
    public int wallLayer;

    public int damage;
    public int layer;
    public RuntimeAnimatorController animController;

    
}
