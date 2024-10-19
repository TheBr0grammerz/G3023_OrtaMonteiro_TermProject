using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedText : MonoBehaviour
{
    [SerializeField] 
    private float _delay = 0.1f;

    [SerializeField]
    private String _currentText = "";
    
    
    public String fullText = "";

    

    void Start()
    {
        StartCoroutine("PlayTextAnim");
    }
    
    IEnumerator PlayTextAnim()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            _currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = _currentText;
            yield return new WaitForSeconds(_delay);
        }
            
        
    }

    
}
