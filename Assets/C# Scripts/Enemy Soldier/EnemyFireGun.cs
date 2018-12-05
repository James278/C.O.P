using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireGun : MonoBehaviour {

    [SerializeField] ParticleSystem enemyBulletsPrefab;

    public void FireBullets(bool isActive)
    {
        var emissionModule = enemyBulletsPrefab.emission;
        emissionModule.enabled = isActive;
    }

}
