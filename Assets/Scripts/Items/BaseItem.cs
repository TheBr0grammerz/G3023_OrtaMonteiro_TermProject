using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Rarity
{
    Common = 0,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

public abstract class BaseItem : MonoBehaviour, IInteractable
{

    public ItemData itemData;

    void Start()
    {
        if (GetComponent<SpriteRenderer>())
        {
            GetComponent<SpriteRenderer>().sprite = itemData.itemSprite;
        }
    }

    void Update()
    {

    }

    // Item Functionality
    public virtual void OnPickup( /*PlayerInventory inventory*/)
    {
        //TODO: add inventory as parameter and then add item to inventory before destroy

        // Default pickup behavior, can be overridden by derived classes
        //inventory.AddItem(this);

        StartCoroutine(InteractPanelRoutine());

        GetComponent<SpriteRenderer>().enabled = false;
    }

    // Optional: Item Specific Behavior
    public virtual void UseItem()
    {
        // Placeholder for item-specific actions
        Debug.Log("Item used: " + itemData.itemName);
    }

    IEnumerator InteractPanelRoutine()
    {
        GameObject interactPanel = GameObject.Find("InteractPanel");

        GameObject panel = interactPanel.transform.GetChild(0).gameObject;
        
        panel.gameObject.SetActive(true);
        panel.transform.GetChild(6).GetComponent<Image>().sprite = itemData.itemSprite;
        panel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemData.itemName;
        panel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = itemData.itemRarity.ToString();
        panel.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = itemData.itemValue.ToString();

        yield return new WaitForSeconds(3.0f);

        panel.gameObject.SetActive(false);

        Destroy(gameObject);
    }

    public void Interact(Player player)
    {
        OnPickup();
    }

    public string GetPrompt()
    {
        return $"Press E to pick up {itemData.itemName}";
    }
}