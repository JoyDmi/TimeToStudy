using UnityEngine;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour
{
    public Camera[] cameras; // ������ �����
    public Button[] buttons; // ������ ������
    public Button startButton; // ������ "������"
    public int shuttleCameraIndex = 12; // ������ ������ ������

    private Camera activeCamera; // ������� �������� ������

    void Start()
    {
        // ��������� ������ ������ ��� ��������
        SetActiveCamera(0);

        // ���������� ��������� � ������ "������"
        startButton.onClick.AddListener(() => SetActiveCamera(shuttleCameraIndex));

        // ���������� ���������� � �������
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // ������ ������� ��� ������ ������
            buttons[i].onClick.AddListener(() => SwitchCamera(index));
        }
    }

    void SwitchCamera(int index)
    {
        // ���������� AudioListener � ������� �������� ������
        activeCamera.GetComponent<AudioListener>().enabled = false;

        // ��������� AudioListener � ��������� ������
        cameras[index].GetComponent<AudioListener>().enabled = true;

        // ��������� ��������� ������ ��� ��������
        SetActiveCamera(index);
    }

    void SetActiveCamera(int index)
    {
        // ��������� ��� ������
        foreach (Camera cam in cameras)
        {
            cam.enabled = false;
        }

        // �������� ��������� ������
        activeCamera = cameras[index];
        activeCamera.enabled = true;
    }
    public void OffAudioListener()
    {
        if (activeCamera != null)
        {
            AudioListener audioListener = activeCamera.GetComponent<AudioListener>();
            if (audioListener != null)
            {
                audioListener.enabled = false;
            }
        }
    }

}
