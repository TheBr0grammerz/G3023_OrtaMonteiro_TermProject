using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITextManager : MonoBehaviour
{
    [SerializeField]
    private String[] _AllTextToDisplay;

    [SerializeField]
    private String _currentText = "";

    [SerializeField]
    private TextMeshProUGUI _textBox;

    [SerializeField] 
    private int _currentLineIndex;

    [SerializeField] 
    private bool _displayText;

    [SerializeField]
    private float _delay = 0.1f;


    public void SetAllText(string text)
    {
        _AllTextToDisplay = text.Split('\n');
    }

    public void DisplayText(bool displayText)
    {
        _displayText = displayText;
    }


    void Start()
    {
        _currentLineIndex = 0;
        _displayText = false;
        _textBox.gameObject.SetActive(false);
    }


    void Update()
    {
        if (_displayText)
        {
            
            //activate text if not already
            if (!_textBox.gameObject.activeInHierarchy)
            {
                _textBox.gameObject.SetActive(true);
                _textBox.text = _AllTextToDisplay[_currentLineIndex];
                //StartCoroutine("PlayTextAnim");
            }

            // continue to next line on mouse click
            if (Input.GetMouseButtonUp(0))
            {
                _currentLineIndex++;

                if (_currentLineIndex < _AllTextToDisplay.Length)
                    _textBox.text = _AllTextToDisplay[_currentLineIndex];

               //StartCoroutine("PlayTextAnim");
            }
            // reset index and stop displaying when end is reached
            else if (_currentLineIndex >= _AllTextToDisplay.Length) 
            {
                _displayText = false;
                _textBox.gameObject.SetActive(false);
                _currentLineIndex = 0;
            }
        }
    }

    IEnumerator PlayTextAnim()
    {
        for (int i = 0; i < _AllTextToDisplay[_currentLineIndex].Length; i++)
        {
            _currentText = _AllTextToDisplay[_currentLineIndex].Substring(0, i);
            this.GetComponent<Text>().text = _currentText;
            yield return new WaitForSeconds(_delay);
        }
    }
}
