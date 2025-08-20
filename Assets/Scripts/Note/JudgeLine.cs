using UnityEngine;

public class JudgeLine : MonoBehaviour
{
    public float perfectDistance = 20f; // 딱 붙었을 때 (픽셀 기준)
    public float goodDistance = 60f;    // 살짝 위/아래

    public void TryJudge(Note note)
    {
        RectTransform noteRect = note.GetComponent<RectTransform>();
        float noteY = noteRect.anchoredPosition.y - noteRect.rect.height * 0.5f;

        RectTransform judgeRect = GetComponent<RectTransform>();
        float judgeY = judgeRect.anchoredPosition.y;

        float diff = Mathf.Abs(noteY - judgeY);

        if (diff <= perfectDistance)
        {
            Debug.Log("Perfect!");
            ScoreManager.Instance.ApplyJudgement(Judgement.Perfect); // 또는 Good, Miss
            JudgementEffectManager.Instance.ShowJudgement(Judgement.Perfect);

            Destroy(note.gameObject);
        }
        else if (diff <= goodDistance)
        {
            Debug.Log("Good!");
            ScoreManager.Instance.ApplyJudgement(Judgement.Good); // 또는 Good, Miss
            JudgementEffectManager.Instance.ShowJudgement(Judgement.Good);

            Destroy(note.gameObject);
        }
        else
        {
            ScoreManager.Instance.ApplyJudgement(Judgement.Miss); // 또는 Good, Miss
            JudgementEffectManager.Instance.ShowJudgement(Judgement.Miss);

            Debug.Log("Miss! (너무 멀어요)");
            // 지우지 않음
        }
    }
}
