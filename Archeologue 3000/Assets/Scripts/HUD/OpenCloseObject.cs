using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseObject : MonoBehaviour
{
    public GameObject goOpened;
    // Start is called before the first frame update
    void Start()
    {
        goOpened.SetActive(false);
    }

    public void CloseIt()
    {
        goOpened.SetActive(false);
    }

    public void OpenIt()
    {
        goOpened.SetActive(true);
    }
}
