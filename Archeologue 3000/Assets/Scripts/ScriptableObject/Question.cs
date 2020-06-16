using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Question")]
public class Question : ScriptableObject
{
    [System.Serializable]
    public struct Dialogue
    {
        public Resident NPCResident;

        [TextArea(3, 20)]
        public string dialogue;
    }

    public Resident residentAskingQuestion;
    public new string name;
    public Queue<Dialogue> dialogues;
    public string description;
    public List<Item> items;
    //list for the Queue
    public List<Dialogue> dialoguesList;

    void OnEnable()
    {
        dialogues = new Queue<Dialogue>(dialoguesList);
    }
}
