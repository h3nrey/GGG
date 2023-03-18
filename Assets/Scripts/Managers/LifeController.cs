using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utils;
public class LifeController : MonoBehaviour
{
    public int life;
    public int currentLife;

    [SerializeField] private UnityEvent OnLivesOver;

    private void Enable() {
        currentLife = life;
    }

    private void Update() {
        if (currentLife == 0) currentLife = life;
    }

    public void TakeDamage(int damage) {
        print("take damage");
        if(currentLife > 0) {
            currentLife -= damage;

            if (currentLife == 0) OnLivesOver?.Invoke();
        }
    }

    public void DestroyOnLivesOver() {
        Destroy(this.gameObject);
    }
}
