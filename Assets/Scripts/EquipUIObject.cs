using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.AI;

public class EquipUIObject : MonoBehaviour
{
    public bool isSelected = false;
    public bool isBuyingItem = false;
    public bool isSellingItem = false;
    public bool isNone = false;
    public ObjectUIAsset objectInfo;
    public GameObject selectedIcon;
    public GameObject priceInterface;
    public TextMeshProUGUI priceText;

    private InventoryBehaviour _inventory;
    private ShopInventory _shop;

    private void Awake() 
    {
        _inventory = FindObjectOfType<InventoryBehaviour>();    
        _shop = FindObjectOfType<ShopInventory>();
    }

    private void Start() 
    {
        InitialConfig();
    }

    public void InitialConfig()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
        if(!isBuyingItem && !isSellingItem)
        {
            GetComponent<Button>().onClick.AddListener(() => OnClickEquipUI());
            if(isSelected) SetAnimationOvByPointer();  
        }
        else if(isSellingItem && !isBuyingItem)
        {
            GetComponent<Button>().onClick.AddListener(() => SellItem());
        }
        else if(!isSellingItem && isBuyingItem)
        {
            GetComponent<Button>().onClick.AddListener(() => BuyItem());
        }
    }

    private void SellItem()
    {
        if(isSelected)
        {
            Debug.Log("Can't sell equiped items");
        }
        else
        {
            int foundGameObjID = -1;
            switch (objectInfo.currentEquipPointer)
            {
                case InventoryBehaviour.EquipPointer.Hair:
                {
                    for (int i = 0; i < _inventory.hairEquipUIObjects.Count; i++)
                    {
                        if(GetComponent<EquipUIObject>().objectInfo.id == _inventory.hairEquipUIObjects[i].GetComponent<EquipUIObject>().objectInfo.id)
                        {
                            foundGameObjID = GetComponent<EquipUIObject>().objectInfo.id;
                            _shop.hairBuyUIObjects.Add(_inventory.hairEquipUIObjects[i]);
                            _inventory.hairEquipUIObjects.Remove(_inventory.hairEquipUIObjects[i]);
                            break;
                        }
                    }
                    break;
                }
                case InventoryBehaviour.EquipPointer.Clothes:
                {
                    for (int i = 0; i < _inventory.clothesEquipUIObjects.Count; i++)
                    {
                        if(GetComponent<EquipUIObject>().objectInfo.id == _inventory.clothesEquipUIObjects[i].GetComponent<EquipUIObject>().objectInfo.id)
                        {
                            foundGameObjID = GetComponent<EquipUIObject>().objectInfo.id;
                            _shop.clothesBuyUIObjects.Add(_inventory.clothesEquipUIObjects[i]);
                            _inventory.clothesEquipUIObjects.Remove(_inventory.clothesEquipUIObjects[i]);
                            break;
                        }
                    }
                    break;
                }
            }

            _shop.SetParentOfSoldItem(foundGameObjID);
            _inventory.currentGold += objectInfo.goldValue;
            _inventory.InstantiateUIObjectsInShop();
        }
    }

    private void BuyItem()
    {
        if(_inventory.SearchForItem(objectInfo.id))
        {
            Debug.Log("You already have this item");
        }
        else
        {
            if(_inventory.currentGold > objectInfo.goldValue)
            {
                switch (objectInfo.currentEquipPointer)
                {
                    case InventoryBehaviour.EquipPointer.Hair:
                    {
                        _inventory.hairEquipUIObjects.Add(this.gameObject);
                        _shop.hairBuyUIObjects.Remove(this.gameObject);
                        break;
                    }
                    case InventoryBehaviour.EquipPointer.Clothes:
                    {
                        _inventory.clothesEquipUIObjects.Add(this.gameObject);
                        _shop.clothesBuyUIObjects.Remove(this.gameObject);
                        break;
                    }
                }
                
                _inventory.SetParentOfBoughtItem(this.gameObject);
                _inventory.currentGold -= objectInfo.goldValue;
                _inventory.InstantiateUIObjectsInShop();
            }
            else
            {
                Debug.Log("Not enough gold");
            }
        }
    }

    private void Update() 
    {
        if(isBuyingItem || isSellingItem)
        {
            priceInterface.SetActive(true);
            selectedIcon.SetActive(false);
        }
        if(!isBuyingItem && !isSellingItem)
        {
            selectedIcon.SetActive(isSelected);
        }
        priceText.text = objectInfo.goldValue.ToString();    
    }
    
    public void OnClickEquipUI()
    {
        SetAnimationOvByPointer();
        SetSelectedIconActive();
    }

    private void SetAnimationOvByPointer()
    {
        PlayerBehaviour _player = FindObjectOfType<PlayerBehaviour>();
        switch (objectInfo.currentEquipPointer)
        {
            case InventoryBehaviour.EquipPointer.None:
            {
                Debug.Log("No pointer set. Reset Object");
                return;
            }
            case InventoryBehaviour.EquipPointer.Hair:
            {
                if (!isNone)
                {
                    _player._hairAnimator.GetComponent<SpriteRenderer>().enabled = true;
                    _player._hairAnimator.GetComponent<SpriteRenderer>().sprite = objectInfo.idleSprite;
                    _player._hairAnimator.runtimeAnimatorController = objectInfo.animOverride;
                }
                else
                {
                    _player._hairAnimator.GetComponent<SpriteRenderer>().enabled = false;
                }
                break;
            }
            case InventoryBehaviour.EquipPointer.Clothes:
            {
                if (!isNone)
                {
                    _player._clothesAnimator.GetComponent<SpriteRenderer>().enabled = true;
                    _player._clothesAnimator.GetComponent<SpriteRenderer>().sprite = objectInfo.idleSprite;
                    _player._clothesAnimator.runtimeAnimatorController = objectInfo.animOverride;
                }
                else
                {
                    _player._clothesAnimator.GetComponent<SpriteRenderer>().enabled = false;
                }
                break;
            }
        }
    }

    private void SetSelectedIconActive()
    {
        isSelected = true;
        _inventory.ActivateSelectedIconOnUI(objectInfo.currentEquipPointer, gameObject.name);
    }

}
