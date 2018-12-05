using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReaction : MonoBehaviour {

    [SerializeField] float attackRange = 10;

    [SerializeField] Transform targetPlayer;

    bool lockedOn = false;

    EnemyFireGun gun;

    FriendlyMovement friendly;

	// Use this for initialization
	void Start () {

        gun = GetComponentInChildren<EnemyFireGun>();
		
	}

    private void Update()
    {
        SetTargetFriendly();
        ToggleFiring();
    }

    void SetTargetFriendly()
    {
        var friendlysInScene = FindObjectsOfType<FriendlyMovement>();
        if (friendlysInScene.Length <= 0)
        {
            return;
        }
        else
        {
            Transform closestFriendly = friendlysInScene[0].transform;

            foreach (FriendlyMovement testFriendly in friendlysInScene)
            {
                closestFriendly = GetClosest(closestFriendly, testFriendly.transform);
            }

            if (lockedOn == false)
            {
                targetPlayer = closestFriendly;
            }

        }
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distToA = Vector3.Distance(transform.position, transformA.transform.position);
        var distToB = Vector3.Distance(transform.position, transformB.transform.position);

        if (distToA < distToB)
        {
            return transformA;
        }
        return transformB;
    }

    private void ToggleFiring()
    {
        if (targetPlayer)
        {
            if (Vector3.Distance(targetPlayer.position, transform.position) <= attackRange)
            {                
                ShootFriendly();               
            }
            else
            {
                StopShooting();
            }
        }
        else
        {
            StopShooting();
        }
    }

    private void ShootFriendly()
    {
        lockedOn = true;
        transform.LookAt(targetPlayer);
        gun.FireBullets(true);
        //      transform.position = friendlyTransform.position + new Vector3(10f, 0f, 0f);
    }

    private void StopShooting()
    {
        gun.FireBullets(false);
        lockedOn = false;
    }
}
