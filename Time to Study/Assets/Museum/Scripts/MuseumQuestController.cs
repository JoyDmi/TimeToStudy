using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MuseumQuestController : MonoBehaviour
{
    [SerializeField] GameObject[] Bottons; // ������, �� ������� ����� ������ � ������� ��������
    [SerializeField] GameObject BoolPanelOne; // ������ ��������� ��� �� ��������� 1 �������
    [SerializeField] GameObject TextOne; // ����� ������� �������
    [SerializeField] GameObject TeleportObject; // ������ ���������
    [SerializeField] GameObject BoolPanelTwo; // ������ ��������� ��� �� ��������� 2 �������
    [SerializeField] GameObject TextTwo; // ����� ������� �������


    private Teleport teleport;

    void Start()
    {
        teleport = TeleportObject.GetComponent<Teleport>();
        UpdatePlayedCount();
    }

    void Update()
    {
        UpdatePlayedCount();

        // ���������, ��� �� ������ � ������� ����� IsPlayed ������ true
        if (Bottons.All(botton => botton.GetComponent<PlayAudioBotton>().isPlayed))
        {
            Image clr = BoolPanelOne.GetComponent<Image>();
            clr.color = Color.green;
            TeleportObject.SetActive(true);
        }

        // ��������� ��������� teleportTrigger
        if (teleport != null && teleport.IsTeleportTriggered())
        {
            UpdateQuestTwo();
        }
    }

    void UpdatePlayedCount()
    {
        int playedCount = Bottons.Count(botton => botton.GetComponent<PlayAudioBotton>().isPlayed);
        TextOne.GetComponent<TextMeshProUGUI>().text = "�������������� ���������� � ������ �������, ����� ��������������� " + playedCount + "/11";
    }

    void UpdateQuestTwo()
    {
        TextTwo.GetComponent<TextMeshProUGUI>().text = "����� � ���� T-90";
        Image clr2 = BoolPanelTwo.GetComponent<Image>();
        clr2.color = Color.green;
    }
}
