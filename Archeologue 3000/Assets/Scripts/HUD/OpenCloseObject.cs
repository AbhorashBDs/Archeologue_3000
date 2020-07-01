using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseObject : MonoBehaviour
{
    public GameObject GO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CloseIt()
    {
        GO.SetActive(false);
    }

    public void OpenIt()
    {
        GO.SetActive(true);
    }
}
