using UnityEngine;

public class JudgeLine : MonoBehaviour
{
    public float perfectDistance = 20f; // �� �پ��� �� (�ȼ� ����)
    public float goodDistance = 60f;    // ��¦ ��/�Ʒ�

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
            ScoreManager.Instance.ApplyJudgement(Judgement.Perfect); // �Ǵ� Good, Miss
            JudgementEffectManager.Instance.ShowJudgement(Judgement.Perfect);

            Destroy(note.gameObject);
        }
        else if (diff <= goodDistance)
        {
            Debug.Log("Good!");
            ScoreManager.Instance.ApplyJudgement(Judgement.Good); // �Ǵ� Good, Miss
            JudgementEffectManager.Instance.ShowJudgement(Judgement.Good);

            Destroy(note.gameObject);
        }
        else
        {
            ScoreManager.Instance.ApplyJudgement(Judgement.Miss); // �Ǵ� Good, Miss
            JudgementEffectManager.Instance.ShowJudgement(Judgement.Miss);

            Debug.Log("Miss! (�ʹ� �־��)");
            // ������ ����
        }
    }
}
