using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _MGR_GameManager : MonoBehaviour
{
    private static _MGR_GameManager p_instance = null;
    public static _MGR_GameManager Instance { get { return p_instance; } }

    [HideInInspector] public int weekCounter;
    private Queue<string> dates;
    private string currentDate;

    private int nbrSlot = 1;
    public Question questionNull;

    [SerializeField] private List<Resident> allResidents;
    private List<Resident> currentResidents;
    [SerializeField] private List<Archeologist> allArcheologists;
    [HideInInspector] public List<Archeologist> currentArcheologists;

    [SerializeField] private List<Location> allLocations;
    [HideInInspector] public List<Location> currentLocations;

    // Start is called before the first frame update
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

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            currentArcheologists.Add(allArcheologists[i]);
        }
        currentResidents = new List<Resident>();
        NewQuestion();
        _MGR_DialogueManager.Instance.NextQuestion();
        weekCounter = 1;
    }


    public Queue<string> QuestionRightWrong()
    {
        Queue<string> answers = new Queue<string>();
        string answer;
        foreach (Transform GO in _MGR_QuestionsManager.Instance.HUDContainer.transform)
        {
            if (QuestionIsRight(GO))
            {
                answer = GO.gameObject.GetComponent<QuestionHUD>().question.residentAskingQuestion.name + " est satisfait de votre réponse.";
                Destroy(GO.gameObject);
                NewQuestion();
            }
            else answer = GO.gameObject.GetComponent<QuestionHUD>().question.residentAskingQuestion.name + " n'est pas satisfait de votre réponse.";
            answers.Enqueue(answer);
        }

        return answers;
    }

    void AddResidentToCurrentResident()
    {
        currentResidents.Add(allResidents[0]);
        _MGR_DialogueManager.Instance.currentResidents.Enqueue(allResidents[0]);
        allResidents.Remove(allResidents[0]);   
    }

    public void NewQuestion()
    {
        bool isLackofResident = true;
        bool isResidentStillActive = true;
        List<Question> questions = _MGR_QuestionsManager.Instance.GetCurrentQuestions(); 
        if (nbrSlot > questions.Count)
        {
            if (currentResidents.Count <= nbrSlot)
            {
                foreach (Resident resident in currentResidents)
                {
                    foreach (Question question in questions)
                    {
                        if (question.residentAskingQuestion == resident)
                        {
                            isResidentStillActive = false;
                        }
                    }
                    if (isResidentStillActive == true)
                    {
                        _MGR_DialogueManager.Instance.currentResidents.Enqueue(resident);
                        isLackofResident = false;
                        
                    }
                }
                if (isLackofResident == true)
                {
                    AddResidentToCurrentResident();                      
                }
            }
            else Debug.Log("ATTENTION LA C'EST PAS BON");
        }
    }

    bool QuestionIsRight(Transform GO)
    {
        bool isRight = true;
        List<Item> l_item = new List<Item>();
        foreach (Transform ISlot in GO.gameObject.GetComponent<QuestionHUD>().slotManager.transform)
        {
            if (ISlot.GetComponent<ItemSlot>().item != null)
            {
                l_item.Add(ISlot.GetComponent<ItemSlot>().item);
            }
        }

        foreach (Item item in GO.gameObject.GetComponent<QuestionHUD>().question.items)
        {
            if (!l_item.Contains(item))
            {
                isRight = false;
            }
        }
        
        return isRight;
    }
}
