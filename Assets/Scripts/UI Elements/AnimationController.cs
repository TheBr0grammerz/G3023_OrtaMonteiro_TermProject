using UnityEngine;

public class AnimationController : MonoBehaviour
{

    [SerializeField] 
    private CanvasGroup _abilityButtons;


    void Awake()
    {
    }

    void Start()
    {
        EncounterSystem.Instance.onEnterCombat.AddListener(StartEncounter);
        _abilityButtons = GetComponentInChildren<CanvasGroup>();
    }

    private void SetButtonPanelActive(bool status)
    {
        if (_abilityButtons != null)
        {
            _abilityButtons.interactable = status;
        }
    }

    public void EnableTextBox(int textBoxID)
    {
        UITextManager.Instance.DisplayText(true, textBoxID);
    }


    // Encounter Anims
    private void StartEncounter(Ship enemyShip)
    {
        GetComponent<Animator>().SetTrigger("EnterEncounter");
    }

    public void OnExitEncounterAnimComplete()
    {

    }


    //Enter Battle Anim
    public void OnEndEnterBattleAnim()
    {
        EncounterSystem.Instance.inCombat = true;
    }


    // Attack Anims
    public void OnStartPlayerAttackAnim()
    {
        SetButtonPanelActive(false);
    }

    public void OnEndPlayerAttackAnim()
    {
        EnableTextBox(1);
        SetButtonPanelActive(true);
    }

    public void OnStartEnemyAttackAnim()
    {
        SetButtonPanelActive(false);
    }

    public void OnEndEnemyAttackAnim()
    {
        EnableTextBox(1);
    }


}
