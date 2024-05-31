using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    public AudioClip triggerSound; // Звук, который будет воспроизводиться
    private AudioSource audioSource; // Компонент AudioSource

    private void Start()
    {
        // Получаем компонент AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on the trigger object.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, является ли объект, входящий в триггер, игроком
        if (other.CompareTag("Player"))
        {
            PlaySound();
        }
    }

    private void PlaySound()
    {
        if (audioSource != null && triggerSound != null)
        {
            audioSource.PlayOneShot(triggerSound);
        }
    }
}
