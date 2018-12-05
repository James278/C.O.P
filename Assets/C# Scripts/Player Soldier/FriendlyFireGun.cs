using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyFireGun : MonoBehaviour {

    [SerializeField] ParticleSystem friendlyBulletsPrefab;

    public void FireBullets(bool isActive)
    {
        var emissionModule = friendlyBulletsPrefab.emission;
        emissionModule.enabled = isActive;
    }
}
