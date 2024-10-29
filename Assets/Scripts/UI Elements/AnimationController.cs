using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private UITextManager _textManager;

    void Awake()
    {
        EncounterSystem.Instance.onEnterCombat.AddListener(StartBattle);
    }

    void Start()
    {
        _textManager = FindObjectOfType<UITextManager>();
    }

    public void EnableTextBox()
    {
        _textManager.DisplayText(true);
    }

    public void StartBattle(Ship enemyShip)
    {

    }
}
