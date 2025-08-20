using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
    public JudgeLine[] judgeLines;
    public RectTransform referenceRect; // 보통 Canvas

    // 레인 기준 좌표 (예: -300, 0, +300)
    public float[] laneXPositions = new float[3] { -300f, 0f, 300f };

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 screenPos = Input.mousePosition;

            // ✅ 1. 화면 터치 → Canvas 로컬 좌표로 변환
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                referenceRect,
                screenPos,
                null,
                out Vector2 localPos
            );

            // ✅ 2. 레인 결정: 가장 가까운 X 위치 찾기
            int lane = 0;
            float minDist = Mathf.Abs(localPos.x - laneXPositions[0]);

            for (int i = 1; i < 3; i++)
            {
                float dist = Mathf.Abs(localPos.x - laneXPositions[i]);
                if (dist < minDist)
                {
                    minDist = dist;
                    lane = i;
                }
            }


            if (judgeLines.Length <= lane || judgeLines[lane] == null) return;

            RectTransform judgeRect = judgeLines[lane].GetComponent<RectTransform>();
            float judgeY = judgeRect.anchoredPosition.y;

            if (Mathf.Abs(localPos.y - judgeY) > 100f) return;

            Note[] notes = FindObjectsOfType<Note>();
            foreach (var note in notes)
            {
                if (note.laneIndex != lane) continue;

                RectTransform noteRect = note.GetComponent<RectTransform>();
                float noteY = noteRect.anchoredPosition.y - noteRect.rect.height * 0.5f;
                if (Mathf.Abs(noteY - judgeY) < 100f)
                {
                    judgeLines[lane].TryJudge(note);
                    break;
                }
            }
        }
    }
}
