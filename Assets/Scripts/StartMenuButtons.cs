using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class StartMenuButtons : MonoBehaviour
{

    [SerializeField] 
    private Canvas _mainMenu, _creditsMenu;

    void Start()
    {
        _mainMenu.gameObject.SetActive(true);
        _creditsMenu.gameObject.SetActive(false);
    }

    public void StartButtonClicked()
    {
        var op1 = SceneManager.LoadSceneAsync("SpaceScene");
        op1.completed += (x) => 
        {
            SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
            EncounterSystem.Instance.Initialize();

            // TODO: new game routine (ex: player name = entered name)
        };
    }
    public void ContinueButtonClicked()
    {
        var op1 = SceneManager.LoadSceneAsync("SpaceScene");
        op1.completed += (x) => {

            SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
            EncounterSystem.Instance.Initialize();
            PlayerData data = SaveSystem.LoadPlayer();
            EncounterSystem.Instance.LoadPlayer(data);
        };
    }
    public void CreditsButtonClicked()
    {
        _mainMenu.gameObject.SetActive(false);
        _creditsMenu.gameObject.SetActive(true);
    }
    public void ExitButtonClicked()
    {
        Application.Quit();
    }
    public void ReturnButtonClicked()
    {
        _mainMenu.gameObject.SetActive(true);
        _creditsMenu.gameObject.SetActive(false);
    }
}
