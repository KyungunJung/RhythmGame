using UnityEngine;
using UnityEngine.UI;

public class JudgementEffectManager : MonoBehaviour
{
    public GameObject effectPrefab;
    public Transform spawnParent;
    public static JudgementEffectManager Instance;
    void Awake()
    {
        Instance = this;
    }
    public void ShowJudgement(Judgement judgement)
    {
        GameObject obj = Instantiate(effectPrefab, spawnParent);
        JudgementEffect effect = obj.GetComponent<JudgementEffect>();
        Text text = obj.GetComponent<Text>();

        switch (judgement)
        {
            case Judgement.Perfect:
                text.text = "PERFECT";
                text.color = Color.yellow;
                break;
            case Judgement.Good:
                text.text = "GOOD";
                text.color = Color.cyan;
                break;
            case Judgement.Miss:
                text.text = "MISS";
                text.color = Color.red;
                break;
        }
    }
}
