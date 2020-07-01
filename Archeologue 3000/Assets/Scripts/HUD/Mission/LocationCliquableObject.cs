using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Security.Cryptography;

public class LocationCliquableObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Location location;

    private ChooseLocation chooseLoc;
    void Start()
    {   
        chooseLoc = transform.parent.GetComponent<ChooseLocation>();       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_MGR_MissionManager.Instance.missionSend)
        {
            chooseLoc.magnifyingGlass.transform.position = transform.position;
            //chooseLoc.nameSheet.gameObject.SetActive(true);
            //chooseLoc.description.gameObject.SetActive(true);
            //chooseLoc.aspect.gameObject.SetActive(true);
            chooseLoc.nameSheet.text = location.name;
            chooseLoc.description.text = location.description;
            chooseLoc.aspect.sprite = location.aspect;
            _MGR_MissionManager.Instance.SetLocation(location);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //chooseLoc.magnifyingGlass.transform.position = chooseLoc.oldMagnifyingGlassPosition;
        //chooseLoc.nameSheet.gameObject.SetActive(false);
        //chooseLoc.description.gameObject.SetActive(false);
        //chooseLoc.aspect.gameObject.SetActive(false);
    }

    
}
