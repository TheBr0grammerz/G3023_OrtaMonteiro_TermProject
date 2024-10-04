using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("SpaceScene");
    }
    public void ContinueButtonClicked()
    {
        SceneManager.LoadScene("SpaceScene");
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
