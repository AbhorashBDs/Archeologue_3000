using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _MGR_GameManager : MonoBehaviour
{
    private static _MGR_GameManager p_instance = null;
    public static _MGR_GameManager Instance { get { return p_instance; } }

    private int weekCounter;
    private Queue<string> dates;
    private string currentDate;

    public Question questionNull;
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

    void EndWeek()
    {
        foreach (Transform GO in _MGR_QuestionsManager.Instance.HUDContainer.transform)
        {
            List<Item> l_item = new List<Item>();
            foreach (Transform ISlot in GO.gameObject.GetComponent<QuestionHUD>().slotManager.transform)
            {
                //Ici il faudra mettre l'item
                //l_item.Add(ISlot.gameObject.
            }

            if (GO.gameObject.GetComponent<QuestionHUD>().question.items == l_item)
            {
                Destroy(GO.gameObject);
            }
        }
    }
}
