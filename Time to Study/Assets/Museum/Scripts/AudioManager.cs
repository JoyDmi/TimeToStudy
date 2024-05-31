using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioSource currentAudioSource;

    public static void PlayAudio(AudioSource newAudioSource)
    {
        // Если уже есть воспроизводящийся звук, остановить его
        if (currentAudioSource != null && currentAudioSource.isPlaying)
        {
            currentAudioSource.Stop();
        }

        // Установить новый звук как текущий и воспроизвести его
        currentAudioSource = newAudioSource;
        currentAudioSource.Play();
    }
}
