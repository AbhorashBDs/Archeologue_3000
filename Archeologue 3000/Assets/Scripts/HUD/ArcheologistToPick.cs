using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArcheologistToPick : OpenCloseObject, IPointerEnterHandler
{
    public Archeologist archeologist;

    public void Pick()
    {
        if (_MGR_MissionManager.Instance.missionSend == false)
        {
            if (!ArcheologistIsAlreadyPicked() && _MGR_MissionManager.Instance.nbrArcheologistByMission > _MGR_MissionManager.Instance.pickedArcheologists.Count)
            {
                _MGR_MissionManager.Instance.AddPickedArcheologist(archeologist);
                _MGR_MissionManager.Instance.pickedArcheologists.Add(archeologist);
            }
        }
    }

    public bool ArcheologistIsAlreadyPicked()
    {
        bool isPicked = false;
        foreach (Transform TR in _MGR_MissionManager.Instance.pictureContainer.transform)
        {
            if (TR.GetComponentInChildren<ArcheologistToUnequip>().archeologist == archeologist)
            {
                isPicked = true;
            }
        }

        return isPicked;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _MGR_MissionManager.Instance.ActualizeTheArcheologistPointed(archeologist);
    }
}
