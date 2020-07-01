using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemOpenCloseObject : OpenCloseObject
{
    [HideInInspector] public Item item;
    [HideInInspector] public string locationName;
    [SerializeField] private string sentenceBeforeLocation;

    [SerializeField] private GameObject sheet;
    [SerializeField] private GameObject nameText;

    public void OpenAndMakeItFitPanel()
    {
        _MGR_ItemManager.Instance.ReactivateTheSlot();
        GO.SetActive(true);
        _MGR_ItemManager.Instance.nameItem.text = item.name;
        _MGR_ItemManager.Instance.description.text = item.description;
        _MGR_ItemManager.Instance.location.text = sentenceBeforeLocation + locationName;
        _MGR_ItemManager.Instance.aspect.sprite = item.aspect;
        sheet.SetActive(false);
        nameText.SetActive(false);
    }
}
