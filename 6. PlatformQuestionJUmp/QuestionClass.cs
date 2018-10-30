using System.Collections;
using UnityEngine;
[System.Serializable]
public class QuestionClass {

    [SerializeField]
    private string Question;
    [SerializeField]
    private string[] Answers = new string[3];
    [SerializeField]
    private int CorrectAnswerIndex;


    public string GetQuestion()
    {
        return Question;
    }
    public string GetAnswer(int Ind)
    {
        return Answers[Ind];
    }

    public int GetCorrectAnswer()
    {
        return CorrectAnswerIndex;
    }

    public void MixAnswers()
    {
        bool doneAnswer = false;
        string[] tempNewAnswers = new string[] { "", "", "" };
        for (int i = 0; i < Answers.Length; i++)
        {
            int rand = Random.Range(0, Answers.Length);
            while (!tempNewAnswers[rand].Equals(""))
            {
                rand = Random.Range(0, Answers.Length);

            }
            tempNewAnswers[rand] = Answers[i];
            if (!doneAnswer && i == CorrectAnswerIndex)
            {
                doneAnswer = true;
                CorrectAnswerIndex = rand;
            }
        }
        Answers = tempNewAnswers;
    }
}
