using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trophy",menuName = "Trophy/ New Trophy")]
public class TrophySO : ScriptableObject
{
    [Header("Trophy Details")]
    public string trophyName;
    [TextArea] public string trophyDescription;

    [Header("Unlock Criteria")]
    public int requiredScore;

    internal Trophy ToTrophy()
    {
        return new Trophy
        {
            trophyName = trophyName,
            trophyDescription = FormatDescription(),
            requiredScore = requiredScore,
        };
    }

    private string FormatDescription()
    {

        string formattedDescription = trophyDescription;
        formattedDescription = formattedDescription.Replace("{Score}", requiredScore.ToString());
        return formattedDescription;
    }
}