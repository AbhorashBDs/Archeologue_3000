using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _MGR_QuestionsManager : MonoBehaviour
{
    private static _MGR_QuestionsManager p_instance = null;
    public static _MGR_QuestionsManager Instance { get { return p_instance; } }

    public List<Question> currentQuestions;

    //the real HUD which contains questions where the player can answers
    public GameObject HUDContainer;
    public GameObject questionPrefab;

    //CQ for CurrentQuestion for the CurrentQuestion panel
    public GameObject CQHUDContainer;
    public GameObject CQquestionPrefab;
    void Awake()
    {
        //Check if instance already exists
        if (p_instance == null)
            //if not, set instance to this
            p_instance = this;
        //If instance already exists and it's not this:
        else if (p_instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }


    public void AddQuestion(Question question)
    {
        GameObject go;
        go = Instantiate(questionPrefab);
        go.GetComponent<QuestionHUD>().MakeQuestionFitWithHUD(question);
        go.transform.SetParent(HUDContainer.transform);

        go = Instantiate(CQquestionPrefab);
        go.GetComponent<CurrentQuestionHUD>().MakeQuestionFitWithHUD(question);
        go.transform.SetParent(CQHUDContainer.transform);
    }
}
