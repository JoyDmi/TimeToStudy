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
        "Какая планета известна своим Великим красным пятном - огромным штормом, который бушует уже сотни лет?",//1
        "Какая планета имеет самую высокую гору и самый глубокий каньон в нашей солнечной системе?",//2
        "Какая планета обладает уникальной гидросферой, включающей в себя океаны, моря, реки и ледники?",//3
        "Какая планета имеет сложную орбиту и пять спутников, самый большой из которых — Харон?",//4
        "Какая планета имеет такую низкую плотность, что, теоретически, могла бы плавать в воде?",//5
        "Какая планета имеет тонкую атмосферу, которая расширяется, когда она приближается к Солнцу, и замерзает, когда удаляется?",//6
        "Какая планета имеет плотную атмосферу, состоящую в основном из углекислого газа, с облаками серной кислоты?",//7
        "Какая планета обладает уникальным “хвостом” из атомов натрия, созданным под воздействием солнечного ветра?",//8
        "Какая планета имеет тонкую атмосферу, что приводит к большим перепадам температур?",//9
        "Какая планета имеет плотную атмосферу, состоящую в основном из водорода и гелия, с примесями метана и аммиака?",//10
        "Какая планета имеет ось вращения, наклоненную почти на 90 градусов, что приводит к экстремальным сезонным изменениям?",//11
        "Какая планета имеет атмосферу, которая содержит водород, гелий и следы метана, придающего ей синий цвет?",//12
        "Какая планета получает очень мало тепла от Солнца, что делает ее одной из самых холодных планет?",//13
        "Какая планета была открыта в 1930 году в ходе поисков планеты X?",//14
        "Какая планета имеет сердцевидное ледяное пятно на поверхности?",//15
        "Какой из объектов имеет массу равную 1,9889*10^30 кг, диаметр равный 1392,7 10^3 км и притяжение равное 274 м/с²?",//16
        "Какая планета имеет массу равную 4,87*10^24 кг, диаметр равный 12104 км и притяжение равное 8,9 м/с²?",//17
        "Какая планета имеет массу равную 0,333*10^24 кг, диаметр равный 4879 км и притяжение равное 3,7 м/с²?",//18
        "Какая планета имеет массу равную 0,97*10^24 кг, диаметр равный 12756 км и притяжение равное 9,8 м/с²?",//19
        "Какая планета имеет самые быстрые ветры в солнечной системе?",//20
        "Какая планета имеет великолепные кольца, состоящие из миллиардов частиц льда и космической пыли, вращающихся вокруг планеты?",//21
        "Какой из объектов имеет плазменную структуру с ядром, где происходят термоядерные реакции?",//22
        "Какой из объектов проходит через 11-летние циклы активности, во время которых меняется количество солнечных пятен и вспышек?",//23
        "Какое небесное тело испытывает мощные вспышки, которые могут влиять на другие небесные тела?",//24
        "Свет до какой планеты достигает за 8 минут?",//25
        "Какая планета имеет поверхность, покрытую красным песком и пылью, содержащей оксид железа?",//26
        "Какая планета имеет ось вращения, наклоненную почти на 90 градусов, что приводит к экстремальным сезонным изменениям?",//27 
        "Какая планета третья по дальности от Солнца?",//28
        "Какая планета имеет самые быстрые ветры в солнечной системе?"//29
};

        options = new string[][]
        {
            new string[] { "Меркурий", "Венера", "Юпитер", "Сатурн" },//1
            new string[] { "Земля", "Марс", "Уран", "Нептун" },//2
            new string[] { "Земля", "Венера", "Марс", "Юпитер" },//3
            new string[] { "Уран", "Нептун", "Плутон", "Сатурн" },//4
            new string[] { "Меркурий", "Венера", "Земля", "Сатурн" },//5
            new string[] { "Марс", "Юпитер", "Сатурн", "Плутон" },//6
            new string[] { "Меркурий", "Венера", "Земля", "Марс" },//7
            new string[] { "Меркурий", "Венера", "Земля", "Марс" },//8
            new string[] { "Земля", "Марс", "Юпитер", "Сатурн" },//9
            new string[] { "Юпитер", "Сатурн", "Уран", "Нептун" },//10
            new string[] { "Сатурн", "Уран", "Нептун", "Плутон" },//11
            new string[] { "Уран", "Нептун", "Плутон", "Сатурн" },//12
            new string[] { "Нептун", "Плутон", "Сатурн", "Уран" },//13
            new string[] { "Уран", "Нептун", "Плутон", "Сатурн" },//14
            new string[] { "Нептун", "Плутон", "Сатурн", "Уран" },//15
            new string[] { "Меркурий", "Венера", "Земля", "Солнце" },//16
            new string[] { "Меркурий", "Венера", "Земля", "Марс" },//17
            new string[] { "Меркурий", "Венера", "Земля", "Марс" },//18
            new string[] { "Меркурий", "Венера", "Земля", "Марс" },//19
            new string[] { "Юпитер", "Сатурн", "Уран", "Нептун" },//20
            new string[] { "Юпитер", "Сатурн", "Уран", "Нептун" },//21
            new string[] { "Земля", "Марс", "Юпитер", "Солнце" },//22
            new string[] { "Земля", "Марс", "Юпитер", "Солнце" },//23
            new string[] { "Земля", "Марс", "Юпитер", "Солнце"  },//24
            new string[] { "Земля", "Марс", "Юпитер", "Меркурий" },//25
            new string[] { "Меркурий", "Венера", "Земля", "Марс" },//26
            new string[] { "Меркурий", "Венера", "Юпитер", "Уран" },//27 
            new string[] { "Меркурий", "Венера", "Земля", "Марс"  },//28
            new string[] { "Юпитер", "Сатурн", "Уран", "Нептун" }//29
        };

        answers = new int[]
        {
            2, // 1 Юпитер
            1, // 2 Марс
            0, // 3 Земля
            2, // 4 Плутон
            3, // 5 Сатурн
            3, // 6 Плутон
            1, // 7 Венера
            0, // 8 Меркурий
            1, // 9 Марс
            0, // 10 Юпитер
            1, // 11 Уран
            1, // 12 Нептун
            0, // 13 Нептун
            2, // 14 Плутон
            1, // 15 Плутон
            3, // 16 Солнце
            1, // 17 Венера
            0, // 18 Меркурий
            2, // 19 Земля
            3, // 20 Нептун
            1, // 21 Сатурн
            3, // 22 Солнце
            3, // 23 Солнце
            3, // 24 Солнце
            0, // 25 Земля
            3, // 26 Марс
            3, // 27 Уран 
            2, // 28 Земля
            3 // 29 Нептун
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