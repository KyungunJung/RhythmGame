using UnityEngine;

public class MusicConductor : MonoBehaviour
{
    public AudioSource audioSource;
    private double songStartTime;

    public double GetSongTime()
    {
        return AudioSettings.dspTime - songStartTime;
    }

    public void PlayMusic()
    {
        songStartTime = AudioSettings.dspTime;
        audioSource.PlayScheduled(songStartTime); // ✅ 오디오 DSP 타임에 정확히 맞춰 재생
    }
}