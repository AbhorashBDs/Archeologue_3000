using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionHUD : MonoBehaviour
{
    public Question question;
    public Text NPC;
    public Text descript;
    public GameObject slotManager;

    public GameObject itemSlotPrefab;

    public void MakeQuestionFitWithHUD(Question question_tempo)
    {
        question = question_tempo;

        NPC.text = question.residentAskingQuestion.name;
        descript.text = question.description;

        foreach (Item item in question.items)
        {
            GameObject go;
            go = Instantiate(itemSlotPrefab);
            go.transform.SetParent(slotManager.transform);
        }
    }
}
