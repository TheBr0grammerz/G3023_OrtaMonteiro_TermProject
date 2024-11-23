using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    // Item Properties
    public string itemName;
    public Sprite itemSprite;
    public Rarity itemRarity;
    public int itemValue; // represents overall value (trade value) of item

}
