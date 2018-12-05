using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBoxFunctionality : MonoBehaviour {

    Vector3 mousePos1;
    Vector3 mousePos2;

    FriendlyMovement friendlyMovementScript;

    [SerializeField] Camera mainCamera;

    public FriendlyMovement [] selectablePlayers;

    private void Awake()
    {
        selectablePlayers = GameObject.FindObjectsOfType<FriendlyMovement>();
    }

    private void Start()
    {
        friendlyMovementScript = GameObject.FindObjectOfType<FriendlyMovement>().GetComponent<FriendlyMovement>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos1 = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            mousePos2 = mainCamera.ScreenToViewportPoint(Input.mousePosition);
            if (mousePos1 != mousePos2)
            {
                SelectObjects();
            }
        }
    }

    void SelectObjects()
    {
        List<FriendlyMovement> remObjects = new List<FriendlyMovement>();

        if (Input.GetKey(KeyCode.LeftControl) == false)
        {
            friendlyMovementScript.soldierSelected = false;
        }

        float boxWidth = mousePos2.x - mousePos1.x;
        float boxHeight = mousePos2.y - mousePos1.y;

        Rect selectRect = new Rect(mousePos1.x, mousePos1.y, boxWidth, boxHeight);

        foreach (FriendlyMovement selectedPlayers in selectablePlayers)
        {
            if (selectedPlayers != null)
            {
                if (selectRect.Contains(mainCamera.WorldToViewportPoint(selectedPlayers.transform.position), true))
                {
                    print("Over soldier");
            //        selectablePlayers.Add(selectedPlayers);
                    friendlyMovementScript.soldierSelected = true;
                }
            }
            else
            {
                remObjects.Add(selectedPlayers);
            }
        }

        //if (remObjects.Count > 0)
        //{
        //    foreach (FriendlyMovement rem in remObjects)
        //    {
        //        selectablePlayers.Remove(rem);
        //    }
        //    remObjects.Clear();
        //}
    }
}
