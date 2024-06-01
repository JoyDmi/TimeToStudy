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
    [SerializeField] Button[] planetButtons; // Массив кнопок планет
    [SerializeField] GameObject TextOne;
    [SerializeField] GameObject TextTwo;
    [SerializeField] GameObject TextThree;
    [SerializeField] Button play2QuestButton;
    [SerializeField] Button finishButton;    // Кнопка Завершить
    [SerializeField] Button retryButton;     // Кнопка Заново
    [SerializeField] GameObject QuizPanel;
    [SerializeField] GameObject ShuttleEvent;
    [SerializeField] Camera MainCamera;

    
    private bool buttonClicked = false;
    private bool[] planetButtonClicked;
    private int completedQuestsOne = 0;
    private AsteroidEvent asteroidEvent; // Добавляем переменную для хранения ссылки на AsteroidEvent
    private QuizController quizController; // Добавляем переменную для хранения ссылки на QuizController

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

        // Инициализация массива planetButtonClicked
        planetButtonClicked = new bool[planetButtons.Length]; // Инициализируем массив
        for (int i = 0; i < planetButtons.Length; i++)
        {
            int index = i; // Сохраняем индекс в локальной переменной, чтобы избежать проблем с замыканием
            planetButtons[i].onClick.AddListener(() => OnPlanetButtonClicked(index));
        }

        // Получаем ссылку на AsteroidEvent
        asteroidEvent = FindObjectOfType<AsteroidEvent>();
        // Получаем ссылку на QuizController
        quizController = FindObjectOfType<QuizController>();

        // Изначально скрываем кнопки завершения и повторного прохождения
        finishButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
    }

    public void StartSecondQuest()
    {
        // Получаем ссылку на AsteroidEvent
        AsteroidEvent asteroidEvent = FindObjectOfType<AsteroidEvent>();

        // Проверяем, не равен ли asteroidEvent null
        if (asteroidEvent != null)
        {
            // Если asteroidEvent не равен null, активируем второй квест
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

        // Обновляем счетчик уничтоженных астероидов
        if (asteroidEvent != null)
        {
            int destroyedAsteroids = asteroidEvent.GetDestroyedAsteroidsCount();
            TextTwo.GetComponent<TextMeshProUGUI>().text = "Уничтожено астероидов: " + destroyedAsteroids + "/10";

            // Меняем цвет QuestBoolThree на зеленый, если уничтожено 10 астероидов
            if (destroyedAsteroids >= 10)
            {
                Image clr = QuestBoolTwo.GetComponent<Image>();
                TextTwo.GetComponent<TextMeshProUGUI>().faceColor = Color.green;
                clr.color = Color.green;
                ShuttleEvent.SetActive(false);

                

                // Включаем камеру
                MainCamera.enabled = true;
                MainCamera.GetComponent<AudioListener>().enabled = true;
                // Активируем третью панель квестов
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
        completedQuestsOne = 0; // Сбрасываем счетчик перед подсчетом
        foreach (bool clicked in planetButtonClicked)
        {
            if (clicked) completedQuestsOne++;
        }

        if (completedQuestsOne == planetButtons.Length)
        {
            
            QuestTwo.SetActive(true);
            TextOne.GetComponent<TextMeshProUGUI>().text = "Всего " + completedQuestsOne + "/" + planetButtons.Length;
            Image clr = QuestBoolOne.GetComponent<Image>();
            clr.color = Color.green;
            TextOne.GetComponent<TextMeshProUGUI>().faceColor = Color.green;
            StartSecondQuest();
        }
        else
        {
            TextOne.GetComponent<TextMeshProUGUI>().text = "Всего " + completedQuestsOne + "/" + planetButtons.Length;
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
            TextThree.GetComponent<TextMeshProUGUI>().text = "Правильно: " + quizController.Score + "/10";

            // Проверяем, сколько правильных ответов
            if (quizController.Score >= 6)
            {
                // Если правильно 6 и больше, показываем кнопку завершить
                finishButton.gameObject.SetActive(true);
                retryButton.gameObject.SetActive(false);
            }
            else if (quizController.Score > 0)
            {
                // Если правильно меньше 6, но больше 0, показываем кнопку заново
                finishButton.gameObject.SetActive(false);
                retryButton.gameObject.SetActive(true);
            }
            else if (quizController.AttemptsLeft == 0)
            {
                // Если осталось 0 попыток, показываем кнопку заново
                finishButton.gameObject.SetActive(false);
                retryButton.gameObject.SetActive(true);
            }
            else
            {
                // Если еще есть попытки и правильных ответов меньше 6, скрываем обе кнопки
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
        // Сбрасываем состояние третьего квеста
        ResetThirdQuest();
        // Затем сбрасываем состояние викторины
        quizController.ResetQuiz();
        retryButton.gameObject.SetActive(false);
    }

    void ResetThirdQuest()
    {
        // Сбрасываем текст и цвет панели третьего квеста
        TextThree.GetComponent<TextMeshProUGUI>().text = "";
        TextThree.GetComponent<TextMeshProUGUI>().faceColor = Color.white;
        Image clrThree = QuestBoolThree.GetComponent<Image>();
        clrThree.color = Color.white;
    }

}
