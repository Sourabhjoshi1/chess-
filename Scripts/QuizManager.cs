using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizUI quizUI;
    [SerializeField] private QuizData quizData;
    [SerializeField] private Score _score;

    private List<Question> questions;
    public bool[] unansweredQuestions;
    [HideInInspector] public Question selectedQuestion;
    int val;

    // Start is called before the first frame update
    void Start()
    {
        questions = quizData.questions;
        SelectQuestion();
    }

    void SelectQuestion()
    {
        if(unansweredQuestions.Last() == true)
        {
            bool searchBool = true;
            val = System.Array.IndexOf(unansweredQuestions, searchBool);
            selectedQuestion = questions[val];
            quizUI.SetQuestion(selectedQuestion);
        }

        else if (unansweredQuestions.Last() == false)
        {
            SceneManager.LoadScene(1);
        }
    }

    public bool Answer(string answered)
    {
        bool correctAns = false;
        if(answered == selectedQuestion.correctAns)
        {
            //Correct
            correctAns = true;
            _score.scoreValue += 10;
            unansweredQuestions[val] = false;           
        }
        else
        {
            //Incorrect
            unansweredQuestions[val] = false;          
        }
        Invoke("SelectQuestion", 0.4f);
        return correctAns;
    }
}

[System.Serializable]
public class Question
{
    public string questionInfo;
    public QuestionType questionType;
    public Sprite questionImg;
    public List<string> options;
    public string correctAns;
}

[System.Serializable]
public enum QuestionType
{
    TEXT,
    IMAGE
}