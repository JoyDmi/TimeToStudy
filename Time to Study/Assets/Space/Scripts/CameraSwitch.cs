using UnityEngine;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour
{
    public Camera[] cameras; // Массив камер
    public Button[] buttons; // Массив кнопок
    public Button startButton; // Кнопка "Начать"
    public int shuttleCameraIndex = 12; // Индекс камеры шаттла

    private Camera activeCamera; // Текущая активная камера

    void Start()
    {
        // Установка первой камеры как активной
        SetActiveCamera(0);

        // Добавление слушателя к кнопке "Начать"
        startButton.onClick.AddListener(() => SetActiveCamera(shuttleCameraIndex));

        // Добавление слушателей к кнопкам
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Захват индекса для каждой кнопки
            buttons[i].onClick.AddListener(() => SwitchCamera(index));
        }
    }

    void SwitchCamera(int index)
    {
        // Отключение AudioListener у текущей активной камеры
        activeCamera.GetComponent<AudioListener>().enabled = false;

        // Включение AudioListener у выбранной камеры
        cameras[index].GetComponent<AudioListener>().enabled = true;

        // Установка выбранной камеры как активной
        SetActiveCamera(index);
    }

    void SetActiveCamera(int index)
    {
        // Отключаем все камеры
        foreach (Camera cam in cameras)
        {
            cam.enabled = false;
        }

        // Включаем выбранную камеру
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
