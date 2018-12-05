using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSelection : MonoBehaviour
{
    [SerializeField]
    private GameObject itemGameObject;

    [SerializeField]
    private int objectPrice;

    public GameObject NewItem
    {
        get { return itemGameObject; }
    }

    public int ObjectPrice
    {
        get { return objectPrice; }
    }
}

