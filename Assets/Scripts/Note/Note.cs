using UnityEngine;

public class Note : MonoBehaviour
{
    public float targetTime;
    public float speed = 800f;

    private MusicConductor conductor;
    private RectTransform rect;
    private float targetY;
    private float targetX;
    public int laneIndex;

    private const float latencyBias = -0.045f; // 오디오 재생 지연 보정값

    public void Initialize(float time, float targetY, float targetX, int lane)
    {
        this.targetTime = time;
        this.targetY = targetY;
        this.targetX = targetX;
        this.laneIndex = lane;

        conductor = FindObjectOfType<MusicConductor>();
        rect = GetComponent<RectTransform>();

        double currentTime = conductor.GetSongTime(); // double 유지
        double remainingTime = targetTime - currentTime; // targetTime도 float이니 double로 캐스팅
        double distance = rect.anchoredPosition.y - targetY;
        double expectedTimeByDistance = distance / speed;

        Debug.Log($"[Note {lane}] 생성시점: {currentTime:F3}s / 목표시간: {targetTime:F3}s / 남은시간: {remainingTime:F3}s / 거리: {distance:F1} / 계산된 이동시간: {expectedTimeByDistance:F3}s / 시간차이: {expectedTimeByDistance - remainingTime:F4}s");
    }

    void LateUpdate()
    {
        float delta = (float)(targetTime - conductor.GetSongTime() + latencyBias);
        rect.anchoredPosition = new Vector2(targetX, targetY + delta * speed);

        // 디버그
        if (Mathf.Abs(delta) < 0.05f)
        {
            Debug.Log($"🎯 target={targetTime:F3}, song={conductor.GetSongTime():F3}, Δ={delta:F3}, Y={rect.anchoredPosition.y:F1}");
        }
    }

    public float GetTimeDiff()
    {
        return Mathf.Abs((float)(targetTime - conductor.GetSongTime()));
    }
}
