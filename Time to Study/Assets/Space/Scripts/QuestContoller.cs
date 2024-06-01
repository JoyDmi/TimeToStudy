using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class QuestController : MonoBehaviour
{
    [SerializeField] GameObject QuestsPanel;
    [SerializeField] GameObject QuestOne;
    [SerializeField] GameObject QuestTwo;
    [SerializeField] GameObject QuestThree;
    [SerializeField] GameObject QuestBoolOne;
    [SerializeField] GameObject QuestBoolTwo;
    [SerializeField] GameObject QuestBoolThree;
    [SerializeField] Button ButtonStart;
    [SerializeField] Button[] planetButtons; // ������ ������ ������
    [SerializeField] GameObject TextOne;
    [SerializeField] GameObject TextTwo;
    [SerializeField] GameObject TextThree;
    [SerializeField] Button play2QuestButton;
    [SerializeField] Button finishButton;    // ������ ���������
    [SerializeField] Button retryButton;     // ������ ������
    [SerializeField] GameObject QuizPanel;
    [SerializeField] GameObject ShuttleEvent;
    [SerializeField] Camera MainCamera;

    
    private bool buttonClicked = false;
    private bool[] planetButtonClicked;
    private int completedQuestsOne = 0;
    private AsteroidEvent asteroidEvent; // ��������� ���������� ��� �������� ������ �� AsteroidEvent
    private QuizController quizController; // ��������� ���������� ��� �������� ������ �� QuizController

    void Start()
    {
        QuestsPanel.SetActive(false);
        ButtonStart.onClick.AddListener(OnButtonClicked);

        play2QuestButton.onClick.AddListener(() =>
        {
            StartSecondQuest();
            FindObjectOfType<AsteroidEvent>().StartSpawningAsteroids();
        });

        finishButton.onClick.AddListener(FinishQuiz);
        retryButton.onClick.AddListener(RetryQuiz);

        // ������������� ������� planetButtonClicked
        planetButtonClicked = new bool[planetButtons.Length]; // �������������� ������
        for (int i = 0; i < planetButtons.Length; i++)
        {
            int index = i; // ��������� ������ � ��������� ����������, ����� �������� ������� � ����������
            planetButtons[i].onClick.AddListener(() => OnPlanetButtonClicked(index));
        }

        // �������� ������ �� AsteroidEvent
        asteroidEvent = FindObjectOfType<AsteroidEvent>();
        // �������� ������ �� QuizController
        quizController = FindObjectOfType<QuizController>();

        // ���������� �������� ������ ���������� � ���������� �����������
        finishButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
    }

    public void StartSecondQuest()
    {
        // �������� ������ �� AsteroidEvent
        AsteroidEvent asteroidEvent = FindObjectOfType<AsteroidEvent>();

        // ���������, �� ����� �� asteroidEvent null
        if (asteroidEvent != null)
        {
            // ���� asteroidEvent �� ����� null, ���������� ������ �����
            asteroidEvent.CompleteFirstQuest();
        }
    }

    void Update()
    {
        
        if (buttonClicked)
        {
            QuestsPanel.SetActive(true);
            QuestOne.SetActive(true);
        }

        // ��������� ������� ������������ ����������
        if (asteroidEvent != null)
        {
            int destroyedAsteroids = asteroidEvent.GetDestroyedAsteroidsCount();
            TextTwo.GetComponent<TextMeshProUGUI>().text = "���������� ����������: " + destroyedAsteroids + "/10";

            // ������ ���� QuestBoolThree �� �������, ���� ���������� 10 ����������
            if (destroyedAsteroids >= 10)
            {
                Image clr = QuestBoolTwo.GetComponent<Image>();
                TextTwo.GetComponent<TextMeshProUGUI>().faceColor = Color.green;
                clr.color = Color.green;
                ShuttleEvent.SetActive(false);

                

                // �������� ������
                MainCamera.enabled = true;
                MainCamera.GetComponent<AudioListener>().enabled = true;
                // ���������� ������ ������ �������
                QuestThree.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }


        if (QuizPanel.activeSelf)
        {
            ThirdQuest();
        }

    }
   

    public void OnButtonClicked()
    {
        buttonClicked = true;
    }

    public void OnPlanetButtonClicked(int buttonIndex)
    {
        if (!planetButtonClicked[buttonIndex])
        {
            planetButtonClicked[buttonIndex] = true;
            FirstQuest();
        }
    }

    void FirstQuest()
    {
        completedQuestsOne = 0; // ���������� ������� ����� ���������
        foreach (bool clicked in planetButtonClicked)
        {
            if (clicked) completedQuestsOne++;
        }

        if (completedQuestsOne == planetButtons.Length)
        {
            
            QuestTwo.SetActive(true);
            TextOne.GetComponent<TextMeshProUGUI>().text = "����� " + completedQuestsOne + "/" + planetButtons.Length;
            Image clr = QuestBoolOne.GetComponent<Image>();
            clr.color = Color.green;
            TextOne.GetComponent<TextMeshProUGUI>().faceColor = Color.green;
            StartSecondQuest();
        }
        else
        {
            TextOne.GetComponent<TextMeshProUGUI>().text = "����� " + completedQuestsOne + "/" + planetButtons.Length;
        }
    }
    
    public void EndThirdQuest()
    {
        
        Image clr = QuestBoolThree.GetComponent<Image>();
        clr.color = Color.green;
        TextThree.GetComponent<TextMeshProUGUI>().faceColor = Color.green;
    }

    void ThirdQuest()
    {
        if (QuizPanel.activeSelf)
        {
            TextThree.GetComponent<TextMeshProUGUI>().text = "���������: " + quizController.Score + "/10";

            // ���������, ������� ���������� �������
            if (quizController.Score >= 6)
            {
                // ���� ��������� 6 � ������, ���������� ������ ���������
                finishButton.gameObject.SetActive(true);
                retryButton.gameObject.SetActive(false);
            }
            else if (quizController.Score > 0)
            {
                // ���� ��������� ������ 6, �� ������ 0, ���������� ������ ������
                finishButton.gameObject.SetActive(false);
                retryButton.gameObject.SetActive(true);
            }
            else if (quizController.AttemptsLeft == 0)
            {
                // ���� �������� 0 �������, ���������� ������ ������
                finishButton.gameObject.SetActive(false);
                retryButton.gameObject.SetActive(true);
            }
            else
            {
                // ���� ��� ���� ������� � ���������� ������� ������ 6, �������� ��� ������
                finishButton.gameObject.SetActive(false);
                retryButton.gameObject.SetActive(false);
            }
        }
    }


    void FinishQuiz()
    {
        QuestsPanel.SetActive(false);
    }

    void RetryQuiz()
    {
        // ���������� ��������� �������� ������
        ResetThirdQuest();
        // ����� ���������� ��������� ���������
        quizController.ResetQuiz();
        retryButton.gameObject.SetActive(false);
    }

    void ResetThirdQuest()
    {
        // ���������� ����� � ���� ������ �������� ������
        TextThree.GetComponent<TextMeshProUGUI>().text = "";
        TextThree.GetComponent<TextMeshProUGUI>().faceColor = Color.white;
        Image clrThree = QuestBoolThree.GetComponent<Image>();
        clrThree.color = Color.white;
    }

}
