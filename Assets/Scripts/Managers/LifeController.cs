using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeController : MonoBehaviour
{
    public int life;
    public int currentLife;

    [SerializeField] private UnityEvent OnLivesOver;

    private void Start() {
        currentLife = life;
    }

    public void TakeDamage(int damage) {
        if(currentLife > 0) {
            currentLife -= damage;

            if (currentLife == 0) OnLivesOver?.Invoke();
        }
    }

    public void DestroyOnLivesOver() {
        Destroy(this.gameObject);
    }
}
