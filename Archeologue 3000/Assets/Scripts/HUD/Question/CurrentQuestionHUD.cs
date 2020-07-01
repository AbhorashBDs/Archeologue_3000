using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentQuestionHUD : MonoBehaviour
{
    public Question question;
    public Text NPC;
    public Text descript;

    void Start()
    {
    }

    public void MakeQuestionFitWithHUD(Question question_tempo)
    {
        question = question_tempo;

        NPC.text = question.residentAskingQuestion.name + " : ";
        descript.text = question.description;
    }
}
