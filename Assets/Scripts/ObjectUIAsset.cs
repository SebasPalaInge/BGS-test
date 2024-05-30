using UnityEngine;

[CreateAssetMenu(fileName = "ObjectUI", menuName = "ObjectUI")]
public class ObjectUIAsset : ScriptableObject
{
    public int id = 0;
    public int goldValue = 10;
    public Sprite idleSprite;
    public AnimatorOverrideController animOverride;
    public InventoryBehaviour.EquipPointer currentEquipPointer = InventoryBehaviour.EquipPointer.None;
}
