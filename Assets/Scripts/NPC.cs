using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private string _dialog;

    public void Display()
    {
        throw new System.NotImplementedException();
    }

    public string GetItemDescription()
    {
        throw new System.NotImplementedException();
    }

    public Sprite GetItemIcon()
    {
        throw new System.NotImplementedException();
    }

    public string GetItemName()
    {
        throw new System.NotImplementedException();
    }

    public void Interact(Player player)
    {
        throw new System.NotImplementedException();
    }
}
