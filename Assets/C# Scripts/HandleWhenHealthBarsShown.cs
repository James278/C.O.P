using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleWhenHealthBarsShown : MonoBehaviour {

    [SerializeField] Camera mainCamera;

    CapsuleCollider capsule;

    Canvas healthBarCanvas;

    bool selected = false;

	// Use this for initialization
	void Start () {

        healthBarCanvas = GetComponentInChildren<Canvas>();
        capsule = GetComponent<CapsuleCollider>();
		
	}
	
	// Update is called once per frame
	void Update () {
        SelectFriendly();
	}

    private void OnMouseOver()
    {
        healthBarCanvas.enabled = true;
    }

    private void OnMouseExit()
    {
        if (selected == false)
        {
            healthBarCanvas.enabled = false;
        }
    }

    void SelectFriendly()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<CapsuleCollider>())
                {
                    selected = false;
                    healthBarCanvas.enabled = false;

                }

                if (hit.collider == capsule)
                {
                    selected = true;
                    healthBarCanvas.enabled = true;
                }

                if (hit.collider is TerrainCollider)
                {
                    selected = false;
                    healthBarCanvas.enabled = false;
                }
            }
        }
    }

}
