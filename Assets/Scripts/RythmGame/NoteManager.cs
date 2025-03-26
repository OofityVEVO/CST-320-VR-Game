using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI accText;
    [SerializeField] TextMeshProUGUI hitText;
    [SerializeField] TextMeshProUGUI missText;
    public float scrollSpeed = 1f;

    public int playerScore = 1;
    public int hits = 0;
    public int misses = 0;
    public float accuracy = 100.00f;

    public int currNotes = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        CalcAccuracy();

        scoreText.text = $"{playerScore}";
        accText.text = $"{accuracy}%";
        hitText.text = $"{hits}";
        missText.text = $"{misses}";
    }

    private void CalcAccuracy()
    {
        if (currNotes != 0)
        {
            accuracy = Mathf.Round(((float)hits / currNotes) * 100f * 100f) / 100f;
        }
    }

    public void AddScore(int score)
    {
        playerScore += score;
    }

    public void AddHit()
    {
        hits++;
        currNotes++;
    }

    public void AddMiss() 
    {
        misses++;
        currNotes++;
    }

    public float GetScrollSpeed()
    {
        return (float)(scrollSpeed * 0.05);
    }
}
