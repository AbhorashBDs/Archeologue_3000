using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcheologistToUnequip : OpenCloseObject
{
    public Archeologist archeologist;
    public void UnPick()
    {
        if (!_MGR_MissionManager.Instance.missionSend)
        {
            _MGR_MissionManager.Instance.RemovePickedArcheologist(archeologist);
            _MGR_MissionManager.Instance.pickedArcheologists.Remove(archeologist);
        }
    }
}
