using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Judgement { Perfect, Good, Miss }

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("Score Values")]
    public int perfectScore = 100;
    public int goodScore = 70;

    [Header("UI Elements")]
    public Text scoreText;
    public Text comboText;
    public Text accuracyText;

    private int score = 0;
    private int combo = 0;
    private int maxCombo = 0;

    private int totalNotes = 0;
    private int hitNotes = 0;
    private int perfectCount = 0;
    private int goodCount = 0;
    private int missCount = 0;

    void Awake()
    {
        Instance = this;
    }

    public void ApplyJudgement(Judgement judgement)
    {
        totalNotes++;

        switch (judgement)
        {
            case Judgement.Perfect:
                score += perfectScore;
                combo++;
                hitNotes++;
                perfectCount++;
                break;

            case Judgement.Good:
                score += goodScore;
                combo++;
                hitNotes++;
                goodCount++;
                break;

            case Judgement.Miss:
                combo = 0;
                missCount++;
                break;
        }

        maxCombo = Mathf.Max(combo, maxCombo);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText) scoreText.text = $"점수 : {score}";
        if (comboText) comboText.text = $"콤보 : {(combo > 0 ? combo.ToString() : "X")}";

        if (accuracyText)
        {
            float accuracy = totalNotes > 0 ? (float)hitNotes / totalNotes * 100f : 100f;
            accuracyText.text = $"정확도 : {accuracy:F2}%";
        }
    }

    // 최종 결과용 getter
    public int GetScore() => score;
    public int GetMaxCombo() => maxCombo;
    public float GetAccuracy() => totalNotes > 0 ? (float)hitNotes / totalNotes * 100f : 100f;
    public int GetPerfectCount() => perfectCount;
    public int GetGoodCount() => goodCount;
    public int GetMissCount() => missCount;
}
