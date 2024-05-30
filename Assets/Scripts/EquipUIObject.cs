using UnityEngine;
using UnityEngine.UI;

public class EquipUIObject : MonoBehaviour
{
    public int goldValue = 10;
    public enum EquipPointer { None, Hair, Clothes };
    public EquipPointer currentEquipPointer = EquipPointer.None;
    public Sprite idleSprite;
    public AnimatorOverrideController animOverride;

    private void Start() 
    {
        GetComponent<Button>().onClick.AddListener(() => OnClickEquipUI());    
    }
    
    public void OnClickEquipUI()
    {
        SetAnimationOvByPointer();
    }

    private void SetAnimationOvByPointer()
    {
        PlayerBehaviour _player = FindObjectOfType<PlayerBehaviour>();
        switch (currentEquipPointer)
        {
            case EquipPointer.None:
            {
                Debug.Log("No pointer set. Reset Object");
                return;
            }
            case EquipPointer.Hair:
            {
                _player._hairAnimator.GetComponent<SpriteRenderer>().sprite = idleSprite;
                _player._hairAnimator.runtimeAnimatorController = animOverride;
                break;
            }
            case EquipPointer.Clothes:
            {
                _player._clothesAnimator.GetComponent<SpriteRenderer>().sprite = idleSprite;
                _player._clothesAnimator.runtimeAnimatorController = animOverride;
                break;
            }
        }
    }

}
