using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UITextManager : MonoBehaviour
{
    [SerializeField]
    private static String[] _AllTextToDisplay;

    [SerializeField]
    private String _currentText = "";

    [SerializeField]
    private GameObject _dialogBox;

    private TextMeshProUGUI _textMeshPro;

    [SerializeField] 
    private int _currentLineIndex;

    [SerializeField] 
    private static bool _displayText;

    private float _delay = 0.02f;

    private bool _canClick;

    public delegate void EndDialogBoxEvent();
    public static event EndDialogBoxEvent OnEndDialogBox;

    //public static UITextManager Instance { get; private set; }


    private void Awake()
    {
        //if (Instance != null && Instance != this)
        //{
        //    Destroy(this.gameObject);
        //    return;
        //}

        //Instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {

        _currentLineIndex = 0;
        _displayText = false;
        _textMeshPro = _dialogBox.GetComponentInChildren<TextMeshProUGUI>();

    }

    void Update()
    {
        if (_displayText)
        {
            //activate text if not already
            if (!_dialogBox.gameObject.activeInHierarchy)
            {
                _dialogBox.gameObject.SetActive(true);
                _textMeshPro.text = _AllTextToDisplay[_currentLineIndex];
                StartCoroutine(PlayTextAnim());
            }

            // continue to next line on mouse click
            if (Input.GetMouseButtonUp(0) && _canClick)
            {
                _currentLineIndex++;

                if (_currentLineIndex < _AllTextToDisplay.Length)
                {
                    _textMeshPro.text = _AllTextToDisplay[_currentLineIndex];

                    StartCoroutine(PlayTextAnim());
                }
            }
            // reset index and stop displaying when end is reached
            else if (_currentLineIndex >= _AllTextToDisplay.Length) 
            {
                _displayText = false;
                _dialogBox.gameObject.SetActive(false);
                _currentLineIndex = 0;

                if (OnEndDialogBox != null)
                    OnEndDialogBox();
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

    public static void SetAllText(string text)
    {
        _AllTextToDisplay = text.Split('\n');
    }

    public static void DisplayText(bool displayText)
    {
        _displayText = displayText;
    }


}
