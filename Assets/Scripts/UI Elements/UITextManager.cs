using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UITextManager : MonoBehaviour
{
    [SerializeField]
    private String[] _AllTextToDisplay;

    [SerializeField]
    private String _currentText = "";

    [SerializeField] 
    private TextMeshProUGUI _gameSceneTextBox;

    [SerializeField]
    private TextMeshProUGUI _battleSceneTextBox;

    private TextMeshProUGUI _activeTextBox;
    private int _activeTextBoxID;

    private TextMeshProUGUI _textMeshPro;

    [SerializeField] 
    private int _currentLineIndex;

    [SerializeField] 
    private bool _displayText;

    private float _delay = 0.05f;

    private bool _canClick;

    public static UITextManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {

        GameObject[] rootGameObjects = SceneManager.GetSceneByName("Battle").GetRootGameObjects();
        foreach (var o in rootGameObjects)
        {
            if (o.CompareTag("BattleUI"))
            {
                var dialogBox = o.transform.Find("BattleSceneDialogBox");
                
                if (dialogBox.CompareTag("DialogBox"))
                {
                    _battleSceneTextBox = dialogBox.GetComponentInChildren<TextMeshProUGUI>();
                    _battleSceneTextBox.gameObject.SetActive(false);
                }
                 
                break;
            }
        }

        rootGameObjects = SceneManager.GetSceneByName("SpaceScene").GetRootGameObjects();
        foreach (var o in rootGameObjects)
        {
            if (o.CompareTag("BattleUI"))
            {
                var dialogBox = o.transform.Find("GameSceneDialogBox");

                if (dialogBox.CompareTag("DialogBox"))
                {
                    _gameSceneTextBox = dialogBox.GetComponentInChildren<TextMeshProUGUI>();
                    _gameSceneTextBox.gameObject.SetActive(false);
                }

                break;
            }
        }

        _currentLineIndex = 0;
        _displayText = false;
        _activeTextBox = _gameSceneTextBox;
        _textMeshPro = _gameSceneTextBox.GetComponentInChildren<TextMeshProUGUI>();

    }

    void Update()
    {
        if (_displayText)
        {
            //// set the active text box
            //if (_activeTextBoxID == 0)
            //{
            //    _activeTextBox = _gameSceneTextBox;
            //}
            //else if (_activeTextBoxID == 1)
            //{
            //    _activeTextBox = _battleSceneTextBox;
            //}

            //activate text if not already
            if (!_activeTextBox.gameObject.activeInHierarchy)
            {
                _activeTextBox.gameObject.SetActive(true);
                _textMeshPro.text = _AllTextToDisplay[_currentLineIndex];
                StartCoroutine("PlayTextAnim");
            }

            // continue to next line on mouse click
            if (Input.GetMouseButtonUp(0) && _canClick)
            {
                _currentLineIndex++;

                if (_currentLineIndex < _AllTextToDisplay.Length)
                {
                    _textMeshPro.text = _AllTextToDisplay[_currentLineIndex];

                    StartCoroutine("PlayTextAnim");
                }
            }
            // reset index and stop displaying when end is reached
            else if (_currentLineIndex >= _AllTextToDisplay.Length) 
            {
                _displayText = false;
                _activeTextBox.gameObject.SetActive(false);
                _currentLineIndex = 0;
            }
        }
    }

    IEnumerator PlayTextAnim()
    {
        _canClick = false;

        for (int i = 0; i < _AllTextToDisplay[_currentLineIndex].Length; i++)
        {
            _currentText = _AllTextToDisplay[_currentLineIndex].Substring(0, i + 1);
            _textMeshPro.text = _currentText;
            yield return new WaitForSeconds(_delay);
        }

        _canClick = true;
    }

    public void SetAllText(string text)
    {
        _AllTextToDisplay = text.Split('\n');
    }

    public void DisplayText(bool displayText, int textBoxID)
    {
        _activeTextBoxID = textBoxID;
        _displayText = displayText;
    }



}
