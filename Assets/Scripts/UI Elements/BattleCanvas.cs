using UnityEngine;

public class BattleCanvas : MonoBehaviour
{
    private UITextManager _textManager;

    void Start()
    {
        _textManager = FindObjectOfType<UITextManager>();
    }

    public void EnableTextBox()
    {
        _textManager.DisplayText(true);
    }
}
