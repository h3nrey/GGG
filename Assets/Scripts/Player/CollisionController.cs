using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        GameObject obj = other.gameObject;
        string objTag = obj.tag;

        if(objTag == "ammo") {
            PlayerBehaviour.Player._gunController.GetAmmo();
            Destroy(obj);
        }
    }
}
