using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrophyUIController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private TMP_Text trophyName;
    [SerializeField] private TMP_Text trophyDescription;

    public delegate IEnumerator OnTrophyUnlocked(Trophy trophy);

    static public  OnTrophyUnlocked OnTrophy;

    void Awake()
    {
        animator = GetComponent<Animator>();

    }

    void OnEnable()
    {
        OnTrophy += PlayAnimation;
    }

    void OnDisable()
    {
        OnTrophy -= PlayAnimation;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator PlayAnimation(Trophy trophy)
    {
        trophyName.text = trophy.trophyName;
        trophyDescription.text = trophy.trophyDescription;
        animator.SetTrigger("Open");

        yield return new WaitForSeconds(5f);
        animator.SetTrigger("Close");

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    
}
