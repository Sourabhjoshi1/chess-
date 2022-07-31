using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    [SerializeField] QuizManager quizManager;
    [SerializeField] private Text questionText;
    [SerializeField] private Image questionImage;
    [SerializeField] private List<Button> options;
    [SerializeField] private Button[] buttonsArray;
    [SerializeField] private Color NormalCol;

    private Question question;
    [HideInInspector] public bool answered;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }
    }

    public void SetQuestion(Question question)
    {
        foreach(Button button in buttonsArray)
        {
            button.image.color = new Color32(55, 66, 84, 255);
        }

        this.question = question;

        switch (question.questionType)
        {
            case QuestionType.TEXT:
                questionImage.transform.parent.gameObject.SetActive(false);
                break;
            case QuestionType.IMAGE:
                questionImage.transform.parent.gameObject.SetActive(true);
                questionImage.sprite = question.questionImg;
                break;
        }

        questionText.text = question.questionInfo;
        List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);

        for(int i = 0; i < options.Count; i++)
        {
            options[i].GetComponentInChildren<Text>().text = answerList[i];
            options[i].name = answerList[i];
        }
        
        answered = false;
    }

    private void OnClick(Button btn)
    {
        string correctChoice = quizManager.selectedQuestion.correctAns;

        if (!answered)
        {
            bool v = quizManager.Answer(btn.name);
            if (v)
            {
                btn.image.color = Color.green;
            }
            else
            {
                btn.image.color = Color.red;

                for (int i = 0; i < options.Count; i++)
                {
                    if(options[i].name == correctChoice)
                    {
                        options[i].image.color = Color.green;
                    }
                }
            }
        }
    }  
}
