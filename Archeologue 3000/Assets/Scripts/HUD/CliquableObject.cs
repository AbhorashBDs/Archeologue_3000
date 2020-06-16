using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CliquableObject : MonoBehaviour
{
    public GameObject panelHUD;

    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }

    public void OpenPanel()
    {
        panelHUD.SetActive(true);
    }
}
