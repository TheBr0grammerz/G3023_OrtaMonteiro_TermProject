
using UnityEngine;

public interface IInteractable
{
    void Interact(Player player);
    void Display();
    string GetItemName();
    string GetItemDescription();
    Sprite GetItemIcon();

}
