using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{

    [SerializeField] 
    private CanvasGroup _abilityButtons;

    [SerializeField] 
    private GameObject _projectile;

    [SerializeField] 
    private GameObject _playerNameText;

    [SerializeField] 
    private GameObject _enemyNameText;

    // EVENTS //

    public delegate void StartCombatEvent();

    public delegate void StartEnemyTurnEvent();
    public static event StartEnemyTurnEvent OnStartEnemyTurn;


    void Awake()
    {
    }

    void Start()
    {
        EncounterSystem.Instance.onEnterCombat.AddListener(StartEncounter);
        _abilityButtons = GetComponentInChildren<CanvasGroup>();
        //_playerNameText.GetComponent<TextMeshProUGUI>().text = EncounterSystem.Instance.Player.shipName;
        UITextManager.OnEndDialogBox += OnEndDialogBox;
    }

    void OnDestroy()
    {
        UITextManager.OnEndDialogBox -= OnEndDialogBox;
    }

    private void SetButtonPanelActive(bool status)
    {
        if (_abilityButtons != null)
        {
            _abilityButtons.interactable = status;
        }
    }

    public void EnableTextBox()
    {
        UITextManager.DisplayText(true);
    }

    public void SetProjectileImage(Sprite sprite)
    {
        _projectile.GetComponent<Image>().sprite = sprite;
        _projectile.GetComponent<Image>().SetNativeSize();
    }

    private void StartEnemyTurn()
    {
        if (OnStartEnemyTurn != null)
            OnStartEnemyTurn();
    }

    private void OnEndDialogBox()
    {
        SetButtonPanelActive(true);

        if (EncounterSystem.Instance._currentState.currentTurnShip != EncounterSystem.Instance.Player) // Enemy turn
        {
            StartEnemyTurn();
        }
    }


    // Encounter Anims
    private void StartEncounter(Ship enemyShip)
    {
        //GetComponent<Animator>().SetTrigger("EnterEncounter");

    }

    public void OnExitEncounterAnimComplete()
    {

    }


    //Enter Battle Anim
    public void OnEndEnterBattleAnim()
    {
        EncounterSystem.Instance.inCombat = true;

        if (EncounterSystem.Instance._currentState.currentTurnShip != EncounterSystem.Instance.Player) // Enemy turn
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
        EnableTextBox();
    }

    public void OnStartEnemyAttackAnim()
    {
        SetButtonPanelActive(false);
    }

    public void OnEndEnemyAttackAnim()
    {
        EnableTextBox();
    }

}
