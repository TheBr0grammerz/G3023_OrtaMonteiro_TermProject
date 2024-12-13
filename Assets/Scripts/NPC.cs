using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] [TextArea(1, 10)] private string _dialog;

    public string GetPrompt()
    {
        return "Press E to talk";
    }

    public void Interact(Player player)
    {
        UITextManager.SetAllText(_dialog);
        UITextManager.DisplayText(true);
    }
}
