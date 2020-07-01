using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _MGR_DialogueManager : MonoBehaviour
{
    private static _MGR_DialogueManager p_instance = null;
    public static _MGR_DialogueManager Instance { get { return p_instance; } }

    //private Queue<Resident> currentResident;
    public Queue<Resident> currentResidents;
    private Resident currentResident;
    public Resident guildChef;
    private Question currentQuestion;

    //HUD
    public Image resident1;
    public Image resident2;
    public Text NPC;
    public Text Dialogue;
    public GameObject DialogueHUD;
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
        currentResidents = new Queue<Resident>();
    }

    void Start()
    {
    }

    void SetHUD(Question question)
    {
        DialogueHUD.SetActive(true);
        Question.Dialogue currentDialogue = question.dialogues.Dequeue();
        resident1.sprite = currentResident.face;
        resident2.sprite = guildChef.face;
        NPC.text = currentDialogue.NPCResident.name;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentDialogue.dialogue));

        IEnumerator TypeSentence (string sentence)
        {
            Dialogue.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                Dialogue.text += letter;
                yield return null;
            }
        }

        if (currentDialogue.NPCResident.name == currentResident.name)
        {
            resident1.color = new Color(1f, 1f, 1f);
            resident2.color = new Color(0.5f, 0.5f, 0.5f);
        }
        else 
        {
            resident2.color = new Color(1f, 1f, 1f);
            resident1.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

    public void NextDialogue()
    {
            if (currentQuestion.dialogues.Count == 0)
            {
                DialogueHUD.SetActive(false);               
                _MGR_QuestionsManager.Instance.AddQuestion(currentQuestion);
                if (currentResidents.Count != 0)
                {
                    NextQuestion();
                }
            _MGR_GameManager.Instance.NewQuestion();
            }
            else SetHUD(currentQuestion);

    }

    public void NextQuestion()
    {
        currentResident = currentResidents.Dequeue();
        currentQuestion = currentResident.questionsSeries.Dequeue();
        NextDialogue();
    }
}

