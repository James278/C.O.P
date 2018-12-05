using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnterStructure : MonoBehaviour {

    [SerializeField] Transform heavyTowerTransform;

    public FriendlyMovement friendlyMovement;

    [SerializeField] private float groundFromTowerHeight;
    [SerializeField] private float towerRange = 2f;

    public bool enteringTower = false;
    public bool towerEntered = false;

    // Use this for initialization
    void Start () {
        friendlyMovement = GetComponent<FriendlyMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        SetTargetTower();
        CheckIfCloseToTower();
    }


    void SetTargetTower()
    {
        var concreatTowersInScene = FindObjectsOfType<HeavyTower>();

        if (concreatTowersInScene.Length <= 0)
        {
            return;
        }

        Transform closestTower = concreatTowersInScene[0].transform;

        foreach (HeavyTower tower in concreatTowersInScene)
        {
            closestTower = GetClosest(closestTower, tower.transform);
        }

        heavyTowerTransform = closestTower;
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

    void CheckIfCloseToTower()
    {
        if (Vector3.Distance(transform.position, heavyTowerTransform.position) <= towerRange)
        {
            print("Close to Tower");
            if (enteringTower)
            {
                HeavyTower heavyTowerScript = heavyTowerTransform.GetComponent<HeavyTower>();
                
                if (heavyTowerScript.soldierCount <= 3)
                {
                    EnterTower(heavyTowerScript);
                    heavyTowerScript.soldierCount++;
                }
            }
        }
    }

    private void EnterTower(HeavyTower heavyTowerScript)
    {
        SetPlacement(heavyTowerScript);
        towerEntered = true;
        friendlyMovement.canMove = false;
        enteringTower = false;
        print("Tower entered");
    }

    private void SetPlacement(HeavyTower heavyTowerScript)
    {
        if (heavyTowerScript.soldierCount <= 0f)
        {
            transform.position = heavyTowerTransform.position + new Vector3(1.4f, 7.5f, .6f);
        }
        else if (heavyTowerScript.soldierCount <= 1f)
        {
            transform.position = heavyTowerTransform.position + new Vector3(1.4f, 7.5f, -0.9f);
        }
        else if (heavyTowerScript.soldierCount <= 2f)
        {
            transform.position = heavyTowerTransform.position + new Vector3(-1.4f, 7.5f, -1.1f);
        }
        else if (heavyTowerScript.soldierCount <= 3f)
        {
            transform.position = heavyTowerTransform.position + new Vector3(-1.4f, 7.5f, 0.5f);
        }
        else if (heavyTowerScript.soldierCount == 4f)
        {
            transform.position = heavyTowerTransform.position + new Vector3(0f, 7.5f, -.5f);
        }
    }

    public IEnumerator ExitTower() // is called in FriendlyMovement, in SetTargetPos method 
    {
        yield return new WaitForSeconds(.55f);
        SetTransformY(groundFromTowerHeight);
        towerEntered = false;
        heavyTowerTransform.GetComponent<HeavyTower>().soldierCount--;
        print("Tower Exited");
    }

    void SetTransformY(float y)
    {
        transform.position = new Vector3
        (
        transform.position.x,
        y,
        transform.position.z
        );
    }

}
