using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{

    [SerializeField] 
    private CanvasGroup _abilityButtons;

    [SerializeField] 
    private GameObject _projectile;

    // EVENTS //

    public delegate void StartCombatEvent();
    public static event StartCombatEvent OnStartCombat;

    public delegate void StartEnemyTurnEvent();
    public static event StartEnemyTurnEvent OnStartEnemyTurn;


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

    public void SetProjectileImage(Sprite sprite)
    {
        _projectile.GetComponent<Image>().sprite = sprite;
    }

    private void StartEnemyTurn()
    {
        if (OnStartEnemyTurn != null)
            OnStartEnemyTurn();
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

        if (EncounterSystem.Instance._currentState.currentTurnShip == EncounterSystem.Instance.Player) // Player turn
        {

        }
        else // Enemy turn
        {
            StartEnemyTurn();
        }
    }


    // Attack Anims
    public void OnStartPlayerAttackAnim()
    {
        SetButtonPanelActive(false);
    }

    public void OnEndPlayerAttackAnim()
    {
        //EnableTextBox(1);
        SetButtonPanelActive(true);

        // todo: move this to after dialog boxes closes
        StartEnemyTurn();
    }

    public void OnStartEnemyAttackAnim()
    {
        SetButtonPanelActive(false);
    }

    public void OnEndEnemyAttackAnim()
    {
        //EnableTextBox(1);
        SetButtonPanelActive(true);
    }


}
