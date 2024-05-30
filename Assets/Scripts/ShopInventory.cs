using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShopInventory : MonoBehaviour
{
    public List<GameObject> hairBuyUIprefabs;
    public List<GameObject> clothesBuyUIprefabs;
    public List<GameObject> hairBuyUIObjects = new List<GameObject>();
    public List<GameObject> clothesBuyUIObjects = new List<GameObject>();

    public GameObject fullInterface;
    public GameObject shopForBuyObjectsPanel;
    public InventoryBehaviour inventory;

    private void Start() 
    {
        InstantiatePrefabs();  
    }

    public void OpenShopInterface()
    {
        fullInterface.SetActive(true);
        inventory.InstantiateUIObjectsInShop();
    }

    public void CloseShopInterface()
    {
        fullInterface.SetActive(false);
        FindObjectOfType<PlayerBehaviour>().canMove = true;
        FindObjectOfType<PlayerBehaviour>().isInteracting = false;
    }

    public void SetParentOfSoldItem(int gameObjID)
    {
        GameObject obj = new GameObject();
        for (int i = 0; i < inventory.instantiatePanel.transform.childCount; i++)
        {
            if(inventory.instantiatePanel.transform.GetChild(i).GetComponent<EquipUIObject>().objectInfo.id == gameObjID)
            {
                obj = inventory.instantiatePanel.transform.GetChild(i).gameObject;
                break;
            }
        }
        obj.transform.SetParent(shopForBuyObjectsPanel.transform);
        foreach (Transform item in shopForBuyObjectsPanel.transform)
        {
            item.GetComponent<EquipUIObject>().isBuyingItem = true;
            item.GetComponent<EquipUIObject>().isSellingItem = false;
            item.GetComponent<EquipUIObject>().InitialConfig();
        }
    }

    public void InstantiateUIBuyObjects()
    {
        foreach(Transform child in shopForBuyObjectsPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (GameObject hairUI in hairBuyUIObjects)
        {
            GameObject newInstance = Instantiate(hairUI, shopForBuyObjectsPanel.transform);
            newInstance.GetComponent<EquipUIObject>().isBuyingItem = true;
        }

        foreach (GameObject clothesUI in clothesBuyUIObjects)
        {
            GameObject newInstance = Instantiate(clothesUI, shopForBuyObjectsPanel.transform);
            newInstance.GetComponent<EquipUIObject>().isSellingItem = true;
        }
    }

    private void InstantiatePrefabs()
    {
        foreach(Transform child in shopForBuyObjectsPanel.transform)
        {
            Destroy(child.gameObject);
        }

        hairBuyUIObjects.Clear();
        foreach (GameObject hairUI in hairBuyUIprefabs)
        {
            GameObject newInstance = Instantiate(hairUI, shopForBuyObjectsPanel.transform);
            newInstance.GetComponent<EquipUIObject>().isBuyingItem = true;
            hairBuyUIObjects.Add(newInstance);
        }

        clothesBuyUIObjects.Clear();
        foreach (GameObject clothesUI in clothesBuyUIprefabs)
        {
            GameObject newInstance = Instantiate(clothesUI, shopForBuyObjectsPanel.transform);
            newInstance.GetComponent<EquipUIObject>().isBuyingItem = true;
            clothesBuyUIObjects.Add(newInstance);
        }
    }
}
