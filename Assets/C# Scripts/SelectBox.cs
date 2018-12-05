using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBox : MonoBehaviour {

    Vector3 startPos;
    Vector3 endPos;

    [SerializeField] Camera mainCamera;

    [SerializeField] RectTransform selectBoxImage;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FindStartPos();
        }
        if (Input.GetMouseButtonUp(0))
        {        
            selectBoxImage.gameObject.SetActive(false);
        }
        if (Input.GetMouseButton(0))
        {
            CreateSelectionBox();
        }
    }

    private void FindStartPos()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            startPos = hit.point;
        }
    }

    private void CreateSelectionBox()
    {
        if (!selectBoxImage.gameObject.activeInHierarchy)
        {
            selectBoxImage.gameObject.SetActive(true);
        }

        endPos = Input.mousePosition;

        Vector3 boxStart = mainCamera.WorldToScreenPoint(startPos);
        boxStart.z = 0f;

        Vector3 centre = (boxStart + endPos) / 2f;

        selectBoxImage.position = centre;

        float sizeX = Mathf.Abs(boxStart.x - endPos.x);
        float sizeY = Mathf.Abs(boxStart.y - endPos.y);

        selectBoxImage.sizeDelta = new Vector2(sizeX, sizeY);
    }
}
