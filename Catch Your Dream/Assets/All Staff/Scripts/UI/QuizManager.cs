using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class QuizManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Text answerText1;
    [SerializeField] private Text answerText2;
    [SerializeField] private Text answerText3;

    [Header("Toggles")]
    [SerializeField] private Toggle firstToggle;
    [SerializeField] private Toggle secondToggle;
    [SerializeField] private Toggle thirdToggle;

    [Header("All necessary buttons")]
    [SerializeField] private Button nextQuestion;
    [SerializeField] private Button submitAnswer;

    [SerializeField] Image panel;

    private Dictionary<int, QuestionManager> questionMap;
    private static int questionIndex;
    private int currentStars;

    private QuestionManager questionManager;

    private void Start()
    {
        // Initialize static UI elements
        QuestionManager.InitializeUIQuizElements(questionText, answerText1, answerText2, answerText3, firstToggle, secondToggle, thirdToggle);

        questionMap = new Dictionary<int, QuestionManager>
        {
            { 0, new QuestionManager("Who was the first person who STEP onto the Moon?", "Yuri Gagarin on 12 April, 1961", "Neil Armstrong on 21 July, 1969", "Buzz Aldrin on July 21, 1969", secondToggle) },
            { 1, new QuestionManager("How long does it take for Pluto to complete one orbit around the Sun?", "1 year", "100 years", "248 years", thirdToggle) },
            {2, new QuestionManager("When was Pluto discovered?", "1930", "1920", "1961", firstToggle) },
            {3, new QuestionManager("Who was the first person to travel to space?","Yuri Gagarin on 12 April 1961", "Neil Armstrong on 21 July, 1969", "Buzz Aldrin on uly 21, 1969" , firstToggle )},
            {4, new QuestionManager("How long does a day on Mercury last in Earth days?", "50 Earth days", "59 Earth days", "59 Mercury days", secondToggle)},
            {5, new QuestionManager("What is located at the center of our solar system?", "Earth", "The Sun", "Nothing is correct", secondToggle) },
            {6, new QuestionManager("What is the position of Earth in relation to the Sun?", "Earth is the third planet", "Earth is the last planet", "Earth is the first planet", firstToggle)},
            {7, new QuestionManager("Which is the largest planet in our solar system?", "All planets are equal", "Earth", "Jupiter", thirdToggle )},
            {8, new QuestionManager("What is the approximate area of Jupiter?", "6,142E10 km2", "6,142E10 cm2", "6,142E10 m2", firstToggle)},
            {9, new QuestionManager("What galaxy contains our solar system?", "Andromeda", "Black Eye Galaxy", "Milky Way", thirdToggle)},
            {10, new QuestionManager("Why is Mars called the Red Planet?", "It's a legend", "Because of its red environment", "Because of its temperature", secondToggle)},
            {11, new QuestionManager("What percentage of the solar system's mass does the Sun account for?", "1%", "80%", "Nothing is correct", thirdToggle)},
            {12, new QuestionManager("What are comets made of?","Ice, dust, and rock", "Sand, dust, and rock", "Water, dust, and rock", firstToggle)},
            {13, new QuestionManager("Which galaxy is the closest spiral galaxy to the Milky Way?", "Andromeda Galaxy", "Comet Galaxy", "Both", firstToggle)},
            {14, new QuestionManager("Why can't anything escape from a black hole?", "Because black holes are very deep", "Because of the strong gravity that not even light can escape", "Becuase of the disorientation", secondToggle)},
            {15, new QuestionManager("Why is space completely silent?", "Because there is no atmosphere to carry sound", "Because there is no creatures", "It's not silent", firstToggle)},
            {16, new QuestionManager("How old is our solar system?", "About 8 billion years old", "About 4.6 billion years old", "About 5 billion years old", secondToggle)},
            {17, new QuestionManager("Which planet rotates in the opposite direction to most other planets?", "Earth", "Venus", "Jupiter", secondToggle)},
            {18, new QuestionManager("What direction does the Sun rise on Venus?", "The Sun rises in the west and sets in the east", "The Sun rises in the east and sets in the east", "The Sun rises in the east and sets in the west", firstToggle)},
            {19, new QuestionManager("What is the maximum temperature on Venus?", "Up to 465 degrees Celsius", "Up to 1000 degrees Celsius", "Up to 365 degrees Celsius", firstToggle) },
            {20, new QuestionManager("How many galaxies are there in the observable universe?", "No more than 100 billion", "100 billion", "More than 100 billion", thirdToggle)},
            {21, new QuestionManager("Which planet has a day longer than its year?", "Jupiter", "Mars", "Venus", thirdToggle)},
            {22, new QuestionManager("Which planet has the strongest winds in the solar system?", "Venus", "Neptune", "Earth", secondToggle)},
            {23, new QuestionManager("What is the closest star to Earth other than the Sun?", "Proxima Centauri", "Ross 128", "Luyten's Star", firstToggle)},
            {24, new QuestionManager("Why does Uranus spin sideways?", "Because of its weight", "Because of the position to the sun", "Because of titanic collision", thirdToggle)},
            {25, new QuestionManager("Have spacecraft visited every planet in our solar system?", "No, not all", "Yes, all", "None is visited", secondToggle)},
            {26, new QuestionManager("How long does it take for light from the Sun to reach Earth?", "About 8 minutes and 20 seconds", "About 20 minutes", "About 20 minutes and 8 seconds", firstToggle)},
            {27, new QuestionManager("What is the temperature on the surface of the Sun?", "About 5,500 degrees Celsius", "About 465 degrees Celsius", "About 6,500 degrees Celsius", firstToggle) },
            {28, new QuestionManager("How much can astronauts' height increase in space?", "Up to 3 inches", "Up to 1 inches", "Up to 2 inches", thirdToggle)},
            {29, new QuestionManager("What are moonquakes?", "Quakes on the Moon, similar to earthquakes on Earth", "Explosions on the Moon", "Quakes on the Moon, different to earthquakes on Earth", firstToggle)},
            {30, new QuestionManager("What is the surface gravity on Mars compared to Earth's?", "About 50% of Earth's", "About 38% of Earth's", "About 10% of Earth's", secondToggle)},
            {31, new QuestionManager("Where is the asteroid belt located?", "Between Venus and Jupiter", "Between Mars and Venus", "Between Mars and Jupiter", thirdToggle)},
            {32, new QuestionManager("Has liquid been discovered on Mars?", "No", "Yes", "It wasn't discovered on other planets than Earth", secondToggle)},
            {33, new QuestionManager("How long would it take to fly a plane to Pluto?", "800 years", "800 days", "800 months", firstToggle)},
            {34, new QuestionManager("Why Astronauts' height can be increased?", "Due to the lack of gravity", "Due to their costumes", "Due to their meals", firstToggle)},
            {35, new QuestionManager("When did Neil Armstrong step onto the Moon?", "21 July 1960", "21 July 1979", "21 July 1969", thirdToggle)},
            {36, new QuestionManager("Does the Moon have quakes?", "Yes, called moonquakes", "No", "It wasn't discovered", firstToggle)},
            {37, new QuestionManager("What collision causes Uranus spin sideways?", "It's galactic collision", "It's titanic collision", "There were no collisions", secondToggle)},
            {38, new QuestionManager("In which direction does the Sun rise on Venus?", "In the east", "In the west", "It differs each time", secondToggle)},
            {39, new QuestionManager("On what date did Yuri Gagarin travel to space?", "12 April 1969", "12 April 1951", "12 April 1961", thirdToggle)}
        };

        SceneFlow.Instance.LoadChoice();
        questionIndex = SceneFlow.Instance.indexOfLastQuestion;

        DisplayQuizQuestion();

        // Add Listeners to Toggles and Button
        //Set only one toggle to active, when click (radio button)
        firstToggle.onValueChanged.AddListener(delegate { questionManager.SetTogglesOnOff(firstToggle, secondToggle, thirdToggle); });
        secondToggle.onValueChanged.AddListener(delegate { questionManager.SetTogglesOnOff(secondToggle, firstToggle, thirdToggle); });
        thirdToggle.onValueChanged.AddListener(delegate { questionManager.SetTogglesOnOff(thirdToggle, firstToggle, secondToggle); });

        nextQuestion.onClick.AddListener(delegate { ReturnIndexOfQuestion(1); });

        submitAnswer.onClick.AddListener(delegate {CheckAnswerBasedOnToggle(); });
    }
    private void Update()
    {
        CheckIfCorrectToggleIsEnabled();
    }

    //Method which NextButton Listen to
    private void ReturnIndexOfQuestion(int valueToAdd)
    {
        questionIndex += valueToAdd;

        SceneFlow.Instance.indexOfLastQuestion = questionIndex;
       
        SceneFlow.Instance.SaveChoice();
      
        nextQuestion.gameObject.SetActive(false);
        firstToggle.enabled = true;
        secondToggle.enabled = true;
        thirdToggle.enabled = true;
        submitAnswer.gameObject.SetActive(true);

        //Display quiz question
        DisplayQuizQuestion();

        panel.color = new Color(255f/255f, 255f/255f, 255f/255f, 140f/255f);

        if (questionMap.ContainsKey(questionIndex))
        {
            questionMap[questionIndex].DisplayQuiz();
        }
        else
        {
            Debug.Log("No more Questions");
        }
    }

    private void DisplayQuizQuestion()
    {
        questionManager = questionMap[questionIndex];
        questionManager.DisplayQuiz();
    }

    public void CheckIfCorrectToggleIsEnabled()
    {
        if (nextQuestion.gameObject.activeSelf)
        {
           
            firstToggle.enabled = false;
            secondToggle.enabled = false;
            thirdToggle.enabled = false;
            submitAnswer.gameObject.SetActive(false );
        }
    }

    private void SetStarsScore(int starAmount)
    {
        currentStars += starAmount;

        SceneFlow.Instance.colledctedStars += starAmount;
        
        SceneFlow.Instance.SaveChoice();
        
    }
    private void CheckAnswerBasedOnToggle()
    {
        SceneFlow.Instance.SaveChoice();

        if (questionMap[questionIndex].ReturnStatusOfCorrectToggle()) {

            SetStarsScore(5);
            nextQuestion.gameObject.SetActive(true);
            panel.color = new Color(157f/255f, 253f/255f, 148f/255f, 140/255f);

        }
        else
        {
            Debug.Log("No money");

            //Normalize color, since Unity uses values from 0 to 1
            panel.color = new Color(225f / 255f, 0f / 255f, 0f / 255f, 140f / 255f);
        }

    }
}

public class QuestionManager
{
    private string mainQuestion;
    private string answer1;
    private string answer2;
    private string answer3;

    private static TextMeshProUGUI questionText;
    private static Text answerText1;
    private static Text answerText2;
    private static Text answerText3;

    private static Toggle firstToggle;
    private static Toggle secondToggle;
    private static Toggle thirdToggle;

    private Toggle correctToggle;

    public QuestionManager(string mainQuestion, string answer1, string answer2, string answer3, Toggle correctToggle)
    {
        this.mainQuestion = mainQuestion;
        this.answer1 = answer1;
        this.answer2 = answer2;
        this.answer3 = answer3;
        this.correctToggle = correctToggle;
    }

    public static void InitializeUIQuizElements(TextMeshProUGUI questionText, Text answerText1, Text answerText2, Text answerText3,
                                                Toggle firstToggle, Toggle secondToggle, Toggle thirdToggle)
    {
        QuestionManager.questionText = questionText;
        QuestionManager.answerText1 = answerText1;
        QuestionManager.answerText2 = answerText2;
        QuestionManager.answerText3 = answerText3;
        QuestionManager.firstToggle = firstToggle;
        QuestionManager.secondToggle = secondToggle;
        QuestionManager.thirdToggle = thirdToggle;
    }

    public void DisplayQuiz()
    {
        questionText.text = mainQuestion;
        answerText1.text = answer1;
        answerText2.text = answer2;
        answerText3.text = answer3;

        // Reset toggles
        firstToggle.isOn = false;
        secondToggle.isOn = false;
        thirdToggle.isOn = false;

        correctToggle.enabled = true;
    }

    public void SetTogglesOnOff(Toggle changedToggle, Toggle second, Toggle third)
    {
        if (changedToggle.isOn)
        {
            second.isOn = false;
            third.isOn = false;
        }
    }

    public bool ReturnStatusOfCorrectToggle()
    {
        
        return correctToggle.isOn;
    }

   
}
