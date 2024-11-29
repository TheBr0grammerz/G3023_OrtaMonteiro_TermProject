using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHUDManager : MonoBehaviour
{
    [SerializeField] private GameObject _HUDCanvas;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _dialogBox;
    [SerializeField] private GameObject _promptText;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _pauseMenu.SetActive(false);
    }

    public void OnResume()
    {
        _pauseMenu.SetActive(false);
        _HUDCanvas.SetActive(true);
    }

    public void OnPause()
    {
        _HUDCanvas.SetActive(false);
        _pauseMenu.SetActive(true);
    }

    public void OnSave()
    {
        SaveSystem.SavePlayer(_player);
    }

    public void OnLoad()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        _player.LoadPlayer(data);
    }

    public void OnQuit()
    {
        SceneManager.LoadScene("StartScene");
    }

}
