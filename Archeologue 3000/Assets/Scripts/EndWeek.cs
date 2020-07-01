using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndWeek : MonoBehaviour
{
    Animator animator;
    Queue<string> answers;

    [SerializeField] private GameObject questionInfo;
    [SerializeField] private GameObject archeologistInfo;

    [SerializeField] private GameObject prefabItem;
    [SerializeField] private GameObject itemContainer;

    public GameObject missionNotSend;

    void Start()
    {
        animator = GetComponent<Animator>();
        questionInfo.SetActive(false);
        archeologistInfo.SetActive(false);
       
    }

    public void FadeBegin()
    {
        if (_MGR_MissionManager.Instance.missionSend == true)
        {
            animator.SetTrigger("FadeIn");
            answers = _MGR_GameManager.Instance.QuestionRightWrong();
            _MGR_MissionManager.Instance.missionSend = false;
        }
        else missionNotSend.SetActive(true);
        ChangeHUDArcheologist();
    }

    public void NextHUD()
    {
        if (answers.Count != 0)
        {
            if (questionInfo.activeSelf == false)
            {
                animator.SetTrigger("QuestionFadeIn");
            }
            else animator.SetTrigger("QuestionFadeInOut");
        }
        else if (archeologistInfo.activeSelf == false)
        {
            animator.SetTrigger("QuestionFadeOut");
        }
        else
        {
            animator.SetTrigger("ArcheologistFadeOut");
            //foreach (Transform tr in itemContainer.transform)
            //{
            //    tr.gameObject.GetComponent<Animator>().SetTrigger("FadeOut");
            //}
        }
    }

    public void ChangeQuestionText()
    {
        if (answers.Count != 0)
        {
            questionInfo.GetComponentInChildren<Text>().text = answers.Dequeue();
        }
    }

    public void ChangeHUDArcheologist()
    {
        for(int i=0; i< itemContainer.transform.childCount; i++)
        {
            if (i< _MGR_MissionManager.Instance.itemBroughtBack.Count)
            {
                itemContainer.transform.GetChild(i).gameObject.SetActive(true);
                itemContainer.transform.GetChild(i).GetComponent<Image>().sprite = _MGR_MissionManager.Instance.itemBroughtBack[i].aspect;
                itemContainer.transform.GetChild(i).GetComponentInChildren<Text>().text = _MGR_MissionManager.Instance.itemBroughtBack[i].name;
            }
            else itemContainer.transform.GetChild(i).gameObject.SetActive(false);
        }

    }
    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
        _MGR_ReportManager.Instance.CreateReports(_MGR_MissionManager.Instance.currentLocation, _MGR_MissionManager.Instance.pickedArcheologists);
        _MGR_MissionManager.Instance.ClearPickedArcheologist();
        foreach (Transform tr in itemContainer.transform)
        {
            Destroy(tr.gameObject);
        }       
        _MGR_GameManager.Instance.weekCounter ++;
        _MGR_MissionManager.Instance.ActualizeItem();
        _MGR_ItemManager.Instance.AddItemsInTheInventory();
        _MGR_MissionManager.Instance.ComeBackToTheOriginalSettings();

    }
    public void ArcheologistFadeIn()
    {    
        animator.SetTrigger("ArcheologistFadeIn");
        //foreach (Transform tr in itemContainer.transform)
        //{
        //    tr.gameObject.GetComponent<Animator>().SetTrigger("FadeIn");
        //}
    }
}
