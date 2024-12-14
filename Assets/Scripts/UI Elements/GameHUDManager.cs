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
        _player = FindFirstObjectByType<Player>();
        _pauseMenu.SetActive(false);
    }

    public void OnResume()
    {
        _pauseMenu.SetActive(false);
        _HUDCanvas.SetActive(true);
        Time.timeScale = 1;
    }

    public void OnPause()
    {
        _HUDCanvas.SetActive(false);
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnSave()
    {
        SaveSystem.SavePlayer(_player);
        OnResume();
    }

    public void OnLoad()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        EncounterSystem.Instance.LoadPlayer(data);
        _player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        OnResume();
    }

    public void OnQuit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }

}
