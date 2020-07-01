using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLocation : MonoBehaviour
{
    public GameObject panelHUD;
    public GameObject magnifyingGlass;

    [HideInInspector] public Vector3 oldMagnifyingGlassPosition;

    public Text nameSheet;
    public Text description;
    public Image aspect;

    // Start is called before the first frame update
    void Start()
    {
        panelHUD.SetActive(false);
        oldMagnifyingGlassPosition = magnifyingGlass.transform.position;
    }

    public void OpenMissionPanel()
    {
        panelHUD.SetActive(true);
        _MGR_MissionManager.Instance.AddArcheologistToHUD();
    }
}
