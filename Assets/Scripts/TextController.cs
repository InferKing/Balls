using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class TextController : MonoBehaviour
{
    [SerializeField] private TMP_Text textScore;
    [SerializeField] private TMP_Text textWinLose;
    [SerializeField] private TMP_Text bestScoreText;
    private List<string> winText = new List<string>() {"Wow!","Amazing!","Wonderful..","Can i have your phone number, please?",
        "Good","Nice!","Winner here!","Master..."};
    private List<string> loseText = new List<string>() { "My grandma can better!", "Are you 3 years old?", "Noob ^_^", "Dummy",
        "I can better c:", "Is that all you can do?", "I expected more!", "Delete the game, please", "Why are you playing in me? Please stop!"};
    

    public static Action<string> OnTextUpdated;
    public static Action<bool> OnWinLoseTextUpdated;

    private void Awake()
    {
        OnTextUpdated = SetScoreText;
        OnWinLoseTextUpdated = SetWinLoseText;
    }
    private void SetScoreText(string s)
    {
        textScore.text = s;
        if (int.Parse(bestScoreText.text) < int.Parse(s))
        {
            bestScoreText.text = s;
        }
    }

    private void SetWinLoseText(bool isWinner)
    {
        if (isWinner)
        {
            textWinLose.text = winText[UnityEngine.Random.Range(0, winText.Count)];
        }
        else
        {
            textWinLose.text = loseText[UnityEngine.Random.Range(0, loseText.Count)];
        }
    }
}
