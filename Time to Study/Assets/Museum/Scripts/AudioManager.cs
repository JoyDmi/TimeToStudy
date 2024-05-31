using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioSource currentAudioSource;

    public static void PlayAudio(AudioSource newAudioSource)
    {
        // ���� ��� ���� ����������������� ����, ���������� ���
        if (currentAudioSource != null && currentAudioSource.isPlaying)
        {
            currentAudioSource.Stop();
        }

        // ���������� ����� ���� ��� ������� � ������������� ���
        currentAudioSource = newAudioSource;
        currentAudioSource.Play();
    }
}
