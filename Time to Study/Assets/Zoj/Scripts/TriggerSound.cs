using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    public AudioClip triggerSound; // ����, ������� ����� ����������������
    private AudioSource audioSource; // ��������� AudioSource

    private void Start()
    {
        // �������� ��������� AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on the trigger object.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������, �������� �� ������, �������� � �������, �������
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
