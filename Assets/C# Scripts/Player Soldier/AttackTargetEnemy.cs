using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTargetEnemy : MonoBehaviour {

    [SerializeField] private float attackRange = 10f;
    float currentTransformY;

    public bool attackMode = false;
    
    public Transform enemyTransform;

    FriendlyFireGun gun;
    FriendlyMovement friendlyMovement;

    // Use this for initialization
    void Start () {
        gun = gameObject.GetComponentInChildren<FriendlyFireGun>();
        friendlyMovement = GetComponent<FriendlyMovement>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (friendlyMovement.canMove == false && attackMode == false)
        {
            SetTargetEnemy();
            ControlFiring();
        }
        ControlFiring();
    }

    void SetTargetEnemy()
    {
        var enemiesInScene = FindObjectsOfType<EnemyReaction>();

        if (enemiesInScene.Length <= 0)
        {
            return;
        }

        Transform closestEnemy = enemiesInScene[0].transform;

        foreach (EnemyReaction enemy in enemiesInScene)
        {
            closestEnemy = GetClosest(closestEnemy, enemy.transform);
        }

        enemyTransform = closestEnemy;

        attackMode = true;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distToA = Vector3.Distance(transform.position, transformA.transform.position);
        var distToB = Vector3.Distance(transform.position, transformB.transform.position);

        if (distToA < distToB)
        {
            return transformA;
        }
        else
        {
            return transformB;
        }
    }

    private void ControlFiring()
    {
        if (enemyTransform)
        {
            if (Vector3.Distance(enemyTransform.position, transform.position) <= attackRange)
            {
                if (attackMode)
                {
                    FireAtEnemy();
                }
                else if (attackMode == false)
                {
                    gun.FireBullets(false);
                }
            }
            else
            {
                gun.FireBullets(false);
            }
        }
        else
        {
            gun.FireBullets(false);
        }
    }

    private void FireAtEnemy()
    {
        RotateToFaceTarget();
        friendlyMovement.canMove = false;
        gun.FireBullets(true);
    }

    public void MoveToEnemy(RaycastHit hit)
    {
        RotateToFaceTarget();
        attackMode = true;
        friendlyMovement.targetPos = hit.point;
        friendlyMovement.canMove = true;
    }

    public void AttackFromGarrison(RaycastHit hit)
    {
        RotateToFaceTarget();
        attackMode = true;
    }

    private void RotateToFaceTarget()
    {
        var lookPos = enemyTransform.position - transform.position;
        lookPos.y = 0f;

        var targetRotation = Quaternion.LookRotation(lookPos);

        transform.rotation = Quaternion.Slerp(transform.rotation,
        targetRotation,
        friendlyMovement.rotationSpeed * Time.deltaTime);
    }
}
