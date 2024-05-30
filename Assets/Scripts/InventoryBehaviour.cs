using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBehaviour : MonoBehaviour
{
    public int currentGold = 100;
    public List<GameObject> hairEquipUIPrefabs;
    public List<GameObject> clothesEquipUIPrefabs;
    private List<GameObject> hairEquipUIObjects = new List<GameObject>();
    private List<GameObject> clothesEquipUIObjects = new List<GameObject>();
    public GameObject instantiatePanel;

    public enum EquipPointer { None, Hair, Clothes };

    private void Start() 
    {
        InstantiateUIObjects();
    }

    private void InstantiateUIObjects()
    {
        foreach(Transform child in instantiatePanel.transform)
        {
            Destroy(child.gameObject);
        }

        hairEquipUIObjects.Clear();
        foreach (GameObject hairUI in hairEquipUIPrefabs)
        {
            hairEquipUIObjects.Add(Instantiate(hairUI, instantiatePanel.transform));
        }

        clothesEquipUIObjects.Clear();
        foreach (GameObject clothesUI in clothesEquipUIPrefabs)
        {
            clothesEquipUIObjects.Add(Instantiate(clothesUI, instantiatePanel.transform));
        }
    }

    public void ActivateSelectedIconOnUI(EquipPointer _equipPointer, string gameObjectName)
    {
        switch (_equipPointer)
        {
            case EquipPointer.Hair:
            {
                foreach (GameObject hairUI in hairEquipUIObjects)
                {
                    if(!hairUI.name.Equals(gameObjectName))
                    {
                        hairUI.GetComponent<EquipUIObject>().isSelected = false;
                    }
                }
                break;
            }
            case EquipPointer.Clothes:
            {
                foreach (GameObject clothesUI in clothesEquipUIObjects)
                {
                    if(!clothesUI.name.Equals(gameObjectName))
                    {
                        clothesUI.GetComponent<EquipUIObject>().isSelected = false;
                    }
                }
                break;
            }
        }
    }
}
