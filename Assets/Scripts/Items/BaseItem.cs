using UnityEngine;

public enum Rarity
{
    Common = 0,
    Uncommon,
    Rare,
    Epic,
    Legendary
}
public abstract class BaseItem : MonoBehaviour
{
    public ItemData itemData;

    void Start()
    {
        if (GetComponent<SpriteRenderer>())
        {
            GetComponent<SpriteRenderer>().sprite = itemData.itemSprite;
        }
    }

    // Item Functionality
    public virtual void OnPickup(/*PlayerInventory inventory*/)
    {
        //TODO: add inventory as parameter and then add item to inventory before destroy

        // Default pickup behavior, can be overridden by derived classes
        //inventory.AddItem(this);
        Destroy(gameObject);
    }

    // Optional: Item Specific Behavior
    public virtual void UseItem()
    {
        // Placeholder for item-specific actions
        Debug.Log("Item used: " + itemData.itemName);
    }

}