using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.SceneManagement.SceneManager;

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
        var op1 = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("SpaceScene");
        var op2 = SceneManager.LoadSceneAsync("Battle", LoadSceneMode.Additive);
        StartCoroutine(StartGameRoutine(op2));
    }

    private IEnumerator StartGameRoutine(AsyncOperation op2)
    {
        yield return new WaitForSeconds(1);
        EncounterSystem.Instance.Initialize();
        //op2.completed += (x) => 
        //{

        //    // TODO: new game routine (ex: player name = entered name)
        //};
    }

    public void ContinueButtonClicked()
    {
        var op1 = SceneManager.LoadSceneAsync("SpaceScene");
        op1.completed += (x) =>
        {

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
