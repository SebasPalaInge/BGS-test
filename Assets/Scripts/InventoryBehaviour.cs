using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class InventoryBehaviour : MonoBehaviour
{
    private bool isOpened = false;
    public int currentGold = 100;
    public List<GameObject> hairEquipUIPrefabs;
    public List<GameObject> clothesEquipUIPrefabs;
    public List<GameObject> hairEquipUIObjects = new List<GameObject>();
    public List<GameObject> clothesEquipUIObjects = new List<GameObject>();
    public GameObject instantiatePanel;
    public GameObject instantiateShopPanel;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI shopGoldText;

    public enum EquipPointer { None, Hair, Clothes };

    private void Awake() 
    {
        InstantiateUIObjects();
    }

    private void Update() 
    {
        goldText.text = "You have: "+currentGold+" gold";
        shopGoldText.text = currentGold.ToString();   
    }

    public void SetParentOfBoughtItem(GameObject gameObj)
    {
        gameObj.transform.SetParent(instantiatePanel.transform);
        foreach (Transform item in instantiatePanel.transform)
        {
            item.GetComponent<EquipUIObject>().isBuyingItem = false;
            item.GetComponent<EquipUIObject>().isSellingItem = false;
            item.GetComponent<EquipUIObject>().InitialConfig();
        }
    }

    public bool SearchForItem(int id)
    {
        foreach (GameObject item in hairEquipUIObjects)
        {
            if(item.GetComponent<EquipUIObject>().objectInfo.id == id)
            {
                return true;
            }
        }
        foreach (GameObject item in clothesEquipUIObjects)
        {
            if(item.GetComponent<EquipUIObject>().objectInfo.id == id)
            {
                return true;
            }
        }
        return false;
    }

    public void InventoryPanelState()
    {
        if(!isOpened)
        {
            isOpened = true;
            transform.DOLocalMoveX(transform.localPosition.x + 365f, 0.2f);
        }
        else
        {
            isOpened = false;
            transform.DOLocalMoveX(transform.localPosition.x - 365f, 0.2f);
        }
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

    public void InstantiateUIObjectsInShop()
    {
        foreach(Transform child in instantiateShopPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (GameObject hairUI in hairEquipUIObjects)
        {
            GameObject newInstance = Instantiate(hairUI, instantiateShopPanel.transform);
            newInstance.GetComponent<EquipUIObject>().isSellingItem = true;
            newInstance.GetComponent<EquipUIObject>().InitialConfig();
        }

        foreach (GameObject clothesUI in clothesEquipUIObjects)
        {
            GameObject newInstance = Instantiate(clothesUI, instantiateShopPanel.transform);
            newInstance.GetComponent<EquipUIObject>().isSellingItem = true;
            newInstance.GetComponent<EquipUIObject>().InitialConfig();
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
