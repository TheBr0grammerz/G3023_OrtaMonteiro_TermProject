
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuButtons : MonoBehaviour
{

    [SerializeField] 
    private Canvas _mainMenu, _creditsMenu;

    [SerializeField] private GameObject _licenses;

    void Start()
    {
        _mainMenu.gameObject.SetActive(true);
        _creditsMenu.gameObject.SetActive(false);
    }

    public void StartButtonClicked()
    {
        var op1 = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("SpaceScene");
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

    public void OnLicensesClicked()
    {
        _licenses.SetActive(!_licenses.activeInHierarchy);
    }
}
