using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject PlanetList;
    [SerializeField] GameObject PlanetInformation;
    [SerializeField] GameObject AudioSource;
    [SerializeField] GameObject ButtonShowPlanetList;
    [SerializeField] GameObject ButtonHidePlanetList;
    [SerializeField] GameObject ButtonShowPlanetInformation;
    [SerializeField] GameObject ButtonHidePlanetInformation;
    [SerializeField] GameObject[] ActivateOnStartAwake;
    [SerializeField] GameObject StartPanel;
    [SerializeField] AudioClip[] AudiosPlanet;
    [SerializeField] Button ButtonStartGame;
    [SerializeField] Button[] buttons; // ������ ������
    [SerializeField] Sprite[] images; // ������ �����������
    public List<GameObject> SpritesToToggle; // ������ �������� ��� ���������/����������
    private Image panelImage; // ����������� ������
    private AudioSource audioPanel; // ��������� � AudioSource

    void Start()
    {
        Time.timeScale = 0f;
        PlanetList.SetActive(false); // show list planet
        PlanetInformation.SetActive(false); // show  planet information
        GameObject[] activeObjects = GameObject.FindGameObjectsWithTag("SpritePlanet");
        SpritesToToggle = new List<GameObject>(activeObjects);
        panelImage = PlanetInformation.GetComponent<Image>(); // �������� ��������� ����������� ������
        audioPanel = AudioSource.GetComponent<AudioSource>();
        // ��������� ������ ������ �������, ������� ����� ������ ����������� ������
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // ��������� ������ � ��������� ����������, ����� �������� ������� � ����������
            buttons[i].onClick.AddListener(() => ChangePanelIndex(index));
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!StartPanel.activeInHierarchy)
            {
                if (PlanetList.activeSelf && PlanetInformation.activeSelf)
                {
                    Hide();
                }
                else
                {
                    Show();
                }
            }
        }


        ButtonStartGame.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        Time.timeScale = 1f;
        audioPanel = AudioSource.GetComponent<AudioSource>();

    }

    public void EnableObjects()
    {
        foreach (GameObject obj in SpritesToToggle)
        {
            obj.SetActive(true);
        }
    }

    public void DisableObjects()
    {
        foreach (GameObject obj in SpritesToToggle)
        {
            obj.SetActive(false);
        }
    }

    public void Hide()
    {
        PlanetList.SetActive(false); // OFF planet list
        ButtonShowPlanetList.SetActive(true);
        ButtonHidePlanetList.SetActive(false);

        PlanetInformation.SetActive(false); // OFF planet information
        ButtonShowPlanetInformation.SetActive(true);
        ButtonHidePlanetInformation.SetActive(false);
    }

    public void Show()
    {
        PlanetList.SetActive(true); // show planet list
        ButtonShowPlanetList.SetActive(false);
        ButtonHidePlanetList.SetActive(true);

        PlanetInformation.SetActive(true); // show planet information
        ButtonShowPlanetInformation.SetActive(false);
        ButtonHidePlanetInformation.SetActive(true);
    }

    public void ActivatePanels()
    {
        for (int j = 0; j < ActivateOnStartAwake.Length; j++)
        {
            //int index = j; // ��������� ������ � ��������� ����������, ����� �������� ������� � ����������
            ActivateOnStartAwake[j].SetActive(true);
        }
    }

    // ������� ��� ��������� ����������� ������
    public void ChangePanelIndex(int index)
    {
        if (!PlanetInformation.activeSelf)
        {
            PlanetInformation.SetActive(true); // �������� ������, ���� ��� �� �������
        }
        panelImage.sprite = images[index]; // ������ �����������
        audioPanel.clip = AudiosPlanet[index];
    }
}
    