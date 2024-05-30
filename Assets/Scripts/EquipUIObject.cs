using System;
using UnityEngine;
using UnityEngine.UI;

public class EquipUIObject : MonoBehaviour
{
    public bool isSelected = false;
    public int goldValue = 10;
    public InventoryBehaviour.EquipPointer currentEquipPointer = InventoryBehaviour.EquipPointer.None;
    public GameObject selectedIcon;
    public Sprite idleSprite;
    public AnimatorOverrideController animOverride;

    private void Start() 
    {
        GetComponent<Button>().onClick.AddListener(() => OnClickEquipUI());
        if(isSelected) SetAnimationOvByPointer();  
    }

    private void Update() 
    {
        selectedIcon.SetActive(isSelected);    
    }
    
    public void OnClickEquipUI()
    {
        SetAnimationOvByPointer();
        SetSelectedIconActive();
    }

    private void SetAnimationOvByPointer()
    {
        PlayerBehaviour _player = FindObjectOfType<PlayerBehaviour>();
        switch (currentEquipPointer)
        {
            case InventoryBehaviour.EquipPointer.None:
            {
                Debug.Log("No pointer set. Reset Object");
                return;
            }
            case InventoryBehaviour.EquipPointer.Hair:
            {
                _player._hairAnimator.GetComponent<SpriteRenderer>().sprite = idleSprite;
                _player._hairAnimator.runtimeAnimatorController = animOverride;
                break;
            }
            case InventoryBehaviour.EquipPointer.Clothes:
            {
                _player._clothesAnimator.GetComponent<SpriteRenderer>().sprite = idleSprite;
                _player._clothesAnimator.runtimeAnimatorController = animOverride;
                break;
            }
        }
    }

    private void SetSelectedIconActive()
    {
        isSelected = true;
        GetComponentInParent<InventoryBehaviour>().ActivateSelectedIconOnUI(currentEquipPointer, gameObject.name);
    }

}
