using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MuseumQuestController : MonoBehaviour
{
    [SerializeField] GameObject[] Bottons; // Кнопки, на которые можно нажать с помощью коллизии
    [SerializeField] GameObject BoolPanelOne; // Панель выполнено или не выполнено 1 задание
    [SerializeField] GameObject TextOne; // Текст первого задания
    [SerializeField] GameObject TeleportObject; // Объект телепорта
    [SerializeField] GameObject BoolPanelTwo; // Панель выполнено или не выполнено 2 задание
    [SerializeField] GameObject TextTwo; // Текст второго задания


    private Teleport teleport;

    void Start()
    {
        teleport = TeleportObject.GetComponent<Teleport>();
        UpdatePlayedCount();
    }

    void Update()
    {
        UpdatePlayedCount();

        // Проверяем, все ли кнопки в массиве имеют IsPlayed равный true
        if (Bottons.All(botton => botton.GetComponent<PlayAudioBotton>().isPlayed))
        {
            Image clr = BoolPanelOne.GetComponent<Image>();
            clr.color = Color.green;
            TeleportObject.SetActive(true);
        }

        // Проверяем состояние teleportTrigger
        if (teleport != null && teleport.IsTeleportTriggered())
        {
            UpdateQuestTwo();
        }
    }

    void UpdatePlayedCount()
    {
        int playedCount = Bottons.Count(botton => botton.GetComponent<PlayAudioBotton>().isPlayed);
        TextOne.GetComponent<TextMeshProUGUI>().text = "Воиспроизвести информацию о каждой технике, всего воиспроизведено " + playedCount + "/11";
    }

    void UpdateQuestTwo()
    {
        TextTwo.GetComponent<TextMeshProUGUI>().text = "Войти в танк T-90";
        Image clr2 = BoolPanelTwo.GetComponent<Image>();
        clr2.color = Color.green;
    }
}
