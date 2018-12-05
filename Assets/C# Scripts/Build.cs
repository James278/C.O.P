using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Build : MonoBehaviour
{
    [SerializeField]
    private GameObject currentObject;

    private Vector3 objectPosition;

    private GameObject previewObject;

    private bool buildMode = true;

    public BuildingSelection building { get; set; }

    private void Update()
    {
    //    Debug.Log("Current Prefab: " + previewObject);
    //    Debug.Log("New current Object: " + currentObject);
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            ONOFF();
            if (!buildMode)
            {
                UserInput();
                if (previewObject != null)
                {
                    MoveObjectWithMouse();
                    RotateObject();
                    PlaceObjectDown();
                } 
            }
            if (buildMode == true)
            {
                Destroy(previewObject);
            }
        }
    }

    private void ONOFF()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log(" " + buildMode);
            buildMode = !buildMode;
        }
  
    }

    private void UserInput()
    {

        if (building != null)
        {
            //GameObject newItem = building.NewItem;
            //currentObject = newItem;
            //DeleteOldGameObject();

            if (previewObject == null)
            {
                previewObject = Instantiate(currentObject);
                //Destroy(previewObject);
            }
        }

    }

    private void DeleteOldGameObject()
    {

    }

    private void MoveObjectWithMouse()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {

            previewObject.transform.position = hitInfo.point;

            //previewObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

        }

    }

    private void PlaceObjectDown()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            previewObject = null;
        }
    }

    private void RotateObject()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            previewObject.transform.Rotate(0, 15, 0, Space.World);

            Debug.Log("rotate");
        }
    }

    public void SelectedBuild(BuildingSelection buildingSelected)
    {
        building = buildingSelected;

        currentObject = building.NewItem;  

        Debug.Log("Game Object: " + building.gameObject);
    }
}
