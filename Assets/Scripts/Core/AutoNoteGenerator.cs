using UnityEngine;
using System.Collections.Generic;

public class AutoNoteGenerator : MonoBehaviour
{
    public AudioClip music;
    public float interval = 0.2f;
    public float sensitivity = 0.1f;
    public int laneCount = 3;
    public float forcedLastNoteTime = -1f;

    public List<NoteData> GenerateNotes(float approachTime)
    {
        List<NoteData> notes = new();
        if (music == null)
        {
            Debug.LogError("❌ 음악이 없음!");
            return notes;
        }

        Debug.Log("🎯 최신 GenerateNotes 실행됨");

        int sampleRate = music.frequency;
        float[] samples = new float[music.samples];
        music.GetData(samples, 0);

        int stepSize = Mathf.FloorToInt(sampleRate * interval);
        float visualEndLimit = music.length - approachTime - 0.05f;
        int maxIndex = Mathf.FloorToInt(visualEndLimit * sampleRate);

        for (int i = 0; i < maxIndex - stepSize; i += stepSize)
        {
            float currentTime = (float)i / sampleRate;
            float sum = 0f;
            for (int j = 0; j < stepSize; j++)
                sum += Mathf.Abs(samples[i + j]);
            float average = sum / stepSize;

            if (average > sensitivity)
            {
                notes.Add(new NoteData
                {
                    time = currentTime,
                    lane = Random.Range(0, laneCount),
                    type = "tap"
                });
            }
        }

        notes.RemoveAll(n => n.time > visualEndLimit);

        float lastTimeLimit = forcedLastNoteTime > 0f
      ? Mathf.Min(forcedLastNoteTime, visualEndLimit)
      : visualEndLimit;

        notes.RemoveAll(n => n.time > lastTimeLimit);

        if (notes.Count == 0 || Mathf.Abs(lastTimeLimit - notes[^1].time) > 0.3f)
        {
            notes.Add(new NoteData
            {
                time = lastTimeLimit,
                lane = Random.Range(0, laneCount),
                type = "tap"
            });
        }


        Debug.Log($"🟢 마지막 노트 시간: {notes[^1].time:F3}초");
        return notes;
    }
}