using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new gun", menuName="GunMode")]
public class GunMode : ScriptableObject
{
    public new string name;
    public Sprite gunSprite; // probably this will be removed on late versios
    public GameObject projectille;
    public float heatPerProjectille;
    public int ammoCost;
    public int gunAmmo;
}
