using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendlySoldierHealth : MonoBehaviour {

    [SerializeField] float startHealth = 10;

    float currentHealth;

    [SerializeField] ParticleSystem enemyBullets;

    [SerializeField] Image healthBar;

    private void Start()
    {
        currentHealth = startHealth;
    }

    private void OnParticleCollision(GameObject other) 
    {
        if (enemyBullets)
        {
            DecreaseHealth();
        }
    }

    private void DecreaseHealth()
    {
        currentHealth -= .1f;
        healthBar.fillAmount = currentHealth / startHealth;

        if (healthBar.fillAmount <= .5f)
        {
            healthBar.color = Color.yellow;
        }
        if (healthBar.fillAmount <= .25f)
        {
            healthBar.color = Color.red;
        }
        if (healthBar.fillAmount <= 0)
        {
            // possibly add effects/animation
            Destroy(gameObject);
        }
    }

}
