using System.Collections.Generic;
using UnityEngine;

public class ChartManager : MonoBehaviour
{
    public Transform noteParent;
    public GameObject notePrefab;
    public Transform[] lanePositions;

    private List<NoteData> notes;
    private int spawnIndex = 0;
    private MusicConductor conductor;

    public float approachTime = 2.0f;
    public float speed = 800f;

    void Start()
    {
        //conductor = FindObjectOfType<MusicConductor>();

        //AutoNoteGenerator generator = GetComponent<AutoNoteGenerator>();
        //if (generator == null)
        //{
        //    Debug.LogError("[ChartManager] AutoNoteGenerator가 없음!");
        //    return;
        //}

        //notes = generator.GenerateNotes(approachTime);

        //if (notes == null || notes.Count == 0)
        //{
        //    Debug.LogWarning("[ChartManager] 노트가 생성되지 않았습니다.");
        //}

        //conductor.PlayMusic();
        //Debug.Log($"🎯 마지막 노트 시간: {notes[^1].time:F2}초");
    }

    void Update()
    {
        float songTime = (float)conductor.GetSongTime();

        while (spawnIndex < notes.Count)
        {
            float noteTime = notes[spawnIndex].time;

            // ✅ 과거 시간 노트 무시
            if (noteTime < songTime - 0.1f) // 100ms 오차 허용
            {
                Debug.LogWarning($"[ChartManager] 지나간 노트 무시됨: {noteTime:F3} < {songTime:F3}");
                spawnIndex++;
                continue;
            }

            // ✅ 일반 노트 생성 조건
            if (noteTime <= songTime + approachTime)
            {
                SpawnNote(notes[spawnIndex]);
                spawnIndex++;
            }
            else break;
        }
    }

    void SpawnNote(NoteData data)
    {
        RectTransform laneRect = lanePositions[data.lane].GetComponent<RectTransform>();
        float targetX = laneRect.anchoredPosition.x;
        float targetY = laneRect.anchoredPosition.y;
        float spawnY = targetY + speed * approachTime;

        GameObject obj = Instantiate(notePrefab, noteParent);
        RectTransform noteRect = obj.GetComponent<RectTransform>();
        noteRect.anchoredPosition = new Vector2(targetX, spawnY);

        Note note = obj.GetComponent<Note>();
        note.speed = speed;
        note.Initialize(data.time, targetY, targetX, data.lane);
    }

    public void GameStart(GameObject obj)
    {
        conductor = FindObjectOfType<MusicConductor>();

        AutoNoteGenerator generator = GetComponent<AutoNoteGenerator>();
        if (generator == null)
        {
            Debug.LogError("[ChartManager] AutoNoteGenerator가 없음!");
            return;
        }

        notes = generator.GenerateNotes(approachTime);

        if (notes == null || notes.Count == 0)
        {
            Debug.LogWarning("[ChartManager] 노트가 생성되지 않았습니다.");
        }

        conductor.PlayMusic();
        Debug.Log($"🎯 마지막 노트 시간: {notes[^1].time:F2}초");
        obj.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}