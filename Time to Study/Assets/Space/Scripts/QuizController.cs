using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class QuizController : MonoBehaviour
{
    [SerializeField] Text questionText;
    [SerializeField] Button nextButton;
    [SerializeField] Button previousButton;
    public Button[] optionButtons;
    private string[] questions;
    private string[][] options;
    private int[] answers;
    private int currentQuestionIndex;
    [SerializeField] List<int> selectedQuestions;
    private int score = 0;
    public bool[] questionAnswered;
    private int[] attempts;

    void Start()
    {
        // Задаем вопросы и ответы
        questions = new string[]
        {
            "Какая планета известна своим Великим красным пятном - огромным штормом, который бушует уже сотни лет?",
            "Какая планета имеет самую высокую гору и самый глубокий каньон в нашей солнечной системе?",
            "Какая планета обладает уникальной гидросферой, включающей в себя океаны, моря, реки и ледники?",
            "Какая планета имеет сложную орбиту и пять спутников, самый большой из которых — Харон?",
            "Какая планета имеет такую низкую плотность, что, теоретически, могла бы плавать в воде?",
            "Какая планета имеет тонкую атмосферу, которая расширяется, когда она приближается к Солнцу, и замерзает, когда удаляется?",
            "Какая планета имеет плотную атмосферу, состоящую в основном из углекислого газа, с облаками серной кислоты?",
            "Какая планета обладает уникальным “хвостом” из атомов натрия, созданным под воздействием солнечного ветра?",
            "Какая планета имеет тонкую атмосферу, что приводит к большим перепадам температур?",
            "Какая планета имеет плотную атмосферу, состоящую в основном из водорода и гелия, с примесями метана и аммиака?",
            "Какая планета имеет ось вращения, наклоненную почти на 90 градусов, что приводит к экстремальным сезонным изменениям?",
            "Какая планета имеет атмосферу, которая содержит водород, гелий и следы метана, придающего ей синий цвет?",
            "Какая планета получает очень мало тепла от Солнца, что делает ее одной из самых холодных планет?",
            "Какая планета была открыта в 1930 году в ходе поисков планеты X?",
            "Какая планета имеет сердцевидное ледяное пятно на поверхности?",
            "Какой из объектов имеет массу равную 1,9889*10^30 кг, диаметр равный 1392,7 10^3 км и притяжение равное 274 м/с²?",
            "Какая планета имеет массу равную 4,87*10^24 кг, диаметр равный 12104 км и притяжение равное 8,9 м/с²?",
            "Какая планета имеет массу равную 0,333*10^24 кг, диаметр равный 4879 км и притяжение равное 3,7 м/с²?",
            "Какая планета имеет массу равную 0,97*10^24 кг, диаметр равный 12756 км и притяжение равное 9,8 м/с²?",
            "Какая планета имеет самые быстрые ветры в солнечной системе?",
            "Какая планета имеет великолепные кольца, состоящие из миллиардов частиц льда и космической пыли, вращающихся вокруг планеты?",
            "Какой из объектов имеет плазменную структуру с ядром, где происходят термоядерные реакции?",
            "Какой из объектов проходит через 11-летние циклы активности, во время которых меняется количество солнечных пятен и вспышек?",
            "Какая планета испытывает мощные вспышки, которые могут влиять на Землю и другие планеты?",
            "Свет до какой планеты достигает за 8 минут?",
            "Какая планета имеет поверхность, покрытую красным песком и пылью, содержащей оксид железа?",
            "Какая планета имеет массу равную 0,333*10^24 кг, диаметр равный 4879 км и притяжение равное 3,7 м/с²?",
            "Какая планета третья по дальности от Солнца?",
            "Какая планета имеет самые быстрые ветры в солнечной системе?"
        };

        options = new string[][]
        {
            new string[] { "Меркурий", "Венера", "Юпитер", "Сатурн" },
            new string[] { "Земля", "Марс", "Уран", "Нептун" },
            new string[] { "Земля", "Венера", "Марс", "Юпитер" },
            new string[] { "Уран", "Нептун", "Плутон", "Сатурн" },
            new string[] { "Меркурий", "Венера", "Земля", "Сатурн" },
            new string[] { "Марс", "Юпитер", "Сатурн", "Плутон" },
            new string[] { "Меркурий", "Венера", "Земля", "Марс" },
            new string[] { "Меркурий", "Венера", "Земля", "Марс" },
            new string[] { "Земля", "Марс", "Юпитер", "Сатурн" },
            new string[] { "Юпитер", "Сатурн", "Уран", "Нептун" },
            new string[] { "Сатурн", "Уран", "Нептун", "Плутон" },
            new string[] { "Уран", "Нептун", "Плутон", "Сатурн" },
            new string[] { "Нептун", "Плутон", "Сатурн", "Уран" },
            new string[] { "Уран", "Нептун", "Плутон", "Сатурн" },
            new string[] { "Нептун", "Плутон", "Сатурн", "Уран" },
            new string[] { "Меркурий", "Венера", "Земля", "Солнце" },
            new string[] { "Меркурий", "Венера", "Земля", "Марс" },
            new string[] { "Меркурий", "Венера", "Земля", "Марс" },
            new string[] { "Меркурий", "Венера", "Земля", "Марс" },
            new string[] { "Юпитер", "Сатурн", "Уран", "Нептун" },
            new string[] { "Юпитер", "Сатурн", "Уран", "Нептун" },
            new string[] { "Земля", "Марс", "Юпитер", "Солнце" },
            new string[] { "Земля", "Марс", "Юпитер", "Солнце" },
            new string[] { "Земля", "Марс", "Юпитер", "Солнце"  },
            new string[] { "Земля", "Марс", "Юпитер", "Солнце" },
            new string[] { "Меркурий", "Венера", "Земля", "Марс" },
            new string[] { "Уран", "Нептун", "Плутон", "Сатурн" },
            new string[] { "Меркурий", "Венера", "Земля", "Марс"  },
            new string[] { "Меркурий", "Венера", "Земля", "Марс" },
            new string[] { "Юпитер", "Сатурн", "Уран", "Нептун" },
        };

        answers = new int[]
        {
            2, // Юпитер
            1, // Марс
            0, // Земля
            2, // Плутон
            3, // Сатурн
            0, // Марс
            1, // Венера
            0, // Меркурий
            1, // Марс
            0, // Юпитер
            1, // Уран
            1, // Нептун
            0, // Нептун
            2, // Плутон
            1, // Плутон
            3, // Солнце
            1, // Венера
            0, // Меркурий
            2, // Земля
            3, // Нептун
            1, // Сатурн
            3, // Солнце
            3, // Солнце
            3, // Солнце
            3, // Солнце
            2, // Земля
            3, // Сатурн
            3, // Марс
            2, // Земля
            3, // Нептун
        };

        // Инициализация массивов для отслеживания ответов и попыток
        questionAnswered = new bool[questions.Length];
        attempts = new int[questions.Length];

        // Настройка кнопок навигации
        nextButton.onClick.AddListener(NextQuestion);
        previousButton.onClick.AddListener(PreviousQuestion);

        // Настройка кнопок вариантов ответов
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i;
            optionButtons[i].onClick.AddListener(() => OnOptionClicked(index));
        }

        // Выбор 10 случайных вопросов
        selectedQuestions = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, questions.Length);
            }
            while (selectedQuestions.Contains(randomIndex));
            selectedQuestions.Add(randomIndex);
        }

        // Начало теста с первого вопроса
        SetQuestion(0);
    }

    void SetQuestion(int selectedIndex)
    {
        if (selectedIndex >= 0 && selectedIndex < selectedQuestions.Count)
        {
            currentQuestionIndex = selectedIndex;
            int questionIndex = selectedQuestions[selectedIndex];
            questionText.text = questions[questionIndex];

            for (int i = 0; i < options[questionIndex].Length; i++)
            {
                optionButtons[i].GetComponentInChildren<Text>().text = options[questionIndex][i];
                optionButtons[i].GetComponent<Image>().color = Color.white;
            }
        }
    }

    void OnOptionClicked(int optionIndex)
    {
        int questionIndex = selectedQuestions[currentQuestionIndex];

        if (!questionAnswered[questionIndex])
        {
            attempts[questionIndex]++;

            if (optionIndex == answers[questionIndex])
            {
                HighlightCorrectAnswer(optionIndex);
                if (attempts[questionIndex] == 1)
                {
                    score++;
                }
                questionAnswered[questionIndex] = true;
            }
            else
            {
                HighlightIncorrectAnswer(optionIndex);
            }
        }
    }

    void HighlightCorrectAnswer(int optionIndex)
    {
        optionButtons[optionIndex].GetComponent<Image>().color = Color.green;
    }

    void HighlightIncorrectAnswer(int optionIndex)
    {
        optionButtons[optionIndex].GetComponent<Image>().color = Color.red;
    }

    void NextQuestion()
    {
        if (currentQuestionIndex < selectedQuestions.Count - 1)
        {
            SetQuestion(currentQuestionIndex + 1);
        }
    }

    void PreviousQuestion()
    {
        if (currentQuestionIndex > 0)
        {
            SetQuestion(currentQuestionIndex - 1);
        }
    }

    public int Score
    {
        get { return score; }
    }

    public int AttemptsLeft
    {
        get { return selectedQuestions.Count - currentQuestionIndex; }
    }

    public void ResetQuiz()
    {
        score = 0;
        currentQuestionIndex = 0;
        questionAnswered = new bool[questions.Length];
        attempts = new int[questions.Length];
        selectedQuestions = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, questions.Length);
            }
            while (selectedQuestions.Contains(randomIndex));
            selectedQuestions.Add(randomIndex);
        }
        SetQuestion(0);
    }
}