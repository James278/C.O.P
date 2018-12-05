using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyMovement : MonoBehaviour {

    public float rotationSpeed = 10f;
    [SerializeField] private float moveSpeed = 5f;

    public Vector3 targetPos = Vector3.zero;
    Vector3 playerMove = Vector3.zero;

    [SerializeField] private Camera mainCamera;

    SelectBoxFunctionality selectBox;

    public CapsuleCollider enemyCapsule;
    CapsuleCollider friendlyCapsule;

    CharacterController characterController;
    CollisionFlags colFlag = CollisionFlags.None;
    FriendlyFireGun gun;

    public ParticleSystem clickParticle;

    public bool soldierSelected;
    public bool canMove = false;

    AttackTargetEnemy attackTargetEnemy;
    HandleEnterStructure handleEnterStructure;

    // Use this for initialization
    void Start () {
        //selectBox = GameObject.FindObjectOfType<SelectBoxFunctionality>();
        //selectBox.GetComponent<SelectBoxFunctionality>().selectablePlayers.Add(this.gameObject.GetComponent<FriendlyMovement>());

        friendlyCapsule = GetComponent<CapsuleCollider>();
        characterController = GetComponent<CharacterController>();
        gun = gameObject.GetComponentInChildren<FriendlyFireGun>();
        attackTargetEnemy = GetComponent<AttackTargetEnemy>();
        handleEnterStructure = GetComponent<HandleEnterStructure>();
    }

    private void Update()
    {
        SelectFriendly();
        Movement();
        characterController.Move(playerMove);
    }

    void SelectFriendly()
    {
        if (Input.GetMouseButtonDown(0))
        {
            soldierSelected = false;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == friendlyCapsule)
                {
                    soldierSelected = true;
                }
            }
        }
    }

    void Movement()
    {
        if (soldierSelected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider is TerrainCollider)
                    {
                        handleEnterStructure.enteringTower = false;
                        Instantiate(clickParticle, hit.point, Quaternion.identity);
                        SetTargetPos(hit);
                    }

                    else if (hit.collider.GetComponent<EnemyReaction>())
                    {
                        if (handleEnterStructure.towerEntered == true)
                        {
                            attackTargetEnemy.enemyTransform = hit.collider.transform;
                            attackTargetEnemy.AttackFromGarrison(hit);
                        }
                        else
                        {
                            attackTargetEnemy.enemyTransform = hit.collider.transform;
                            attackTargetEnemy.MoveToEnemy(hit);
                        }
                    }

                    else if (hit.collider.GetComponent<HeavyTower>())
                    {
                        handleEnterStructure.enteringTower = true;
                        SetTargetPos(hit);
                    }
                }
            }

            else
            {
                playerMove.Set(0f, 0f, 0f);
            }

        }

        if (canMove)
        {
            MoveToWhereClicked();
        }
    }

    

    private void SetTargetPos(RaycastHit hit)
    {
        attackTargetEnemy.attackMode = false;
        targetPos = hit.point;
        canMove = true;
        gun.FireBullets(false);

        if (handleEnterStructure.towerEntered == true)
        {
            StartCoroutine(handleEnterStructure.ExitTower());
        }
    }

    private void MoveToWhereClicked()
    {
        RotateToWhereClicked();

        playerMove = transform.forward * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPos) <= 1.1f)
        {
            canMove = false;
            if (canMove == false)
            {
                playerMove.Set(0f, 0f, 0f);
            }
        }
    }

    private void RotateToWhereClicked()
    {
        Vector3 tempTargetPosition = new Vector3(targetPos.x, transform.position.y, targetPos.z);

        var targetRotation = Quaternion.LookRotation(tempTargetPosition - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
        rotationSpeed * Time.deltaTime);
    }
}
