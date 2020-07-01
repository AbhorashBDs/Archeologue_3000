using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _MGR_ReportManager : MonoBehaviour
{
    private static _MGR_ReportManager p_instance = null;
    public static _MGR_ReportManager Instance { get { return p_instance; } }

    [SerializeField] private Text week;
    [SerializeField] private Text infoLocArc;

    [SerializeField] private GameObject lineContainer;
    [SerializeField] private GameObject linePrefab;


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

    public void CreateReports(Location loc, List<Archeologist> l_arc)
    {
        List<Archeologist> temp_list = new List<Archeologist>();
        temp_list.Add(l_arc[0]);
        week.text = "Semaine " + _MGR_GameManager.Instance.weekCounter;
        foreach (Archeologist arc in temp_list)
        {
            infoLocArc.text = loc.name + " - " + arc.name + "\n" + arc.role;
            GameObject GO;
            foreach (var line in arc.listReportLines)
            {
                if (line.relativeName == loc.name)
                {
                    linePrefab.GetComponent<Text>().text = line.reportSentence;
                }
            }
            GO = Instantiate(linePrefab);
            GO.transform.SetParent(lineContainer.transform);
            foreach (Archeologist arch in l_arc)
            {
                foreach (var line in arc.listReportLines)
                {
                    if (line.relativeName == arch.name)
                    {
                        linePrefab.GetComponent<Text>().text = line.reportSentence;
                        GO = Instantiate(linePrefab);
                        GO.transform.SetParent(lineContainer.transform);
                    }
                }
            }
            bool itemAvailable = true;
            foreach (Item item in loc.items)
            {
                if (!_MGR_ItemManager.Instance.allItemRetrieved.Contains(item))
                {
                    if (item.archeologistList.Contains(arc))
                    {
                        itemAvailable = false;
                    }
                }
            }
            if (itemAvailable == true)
            {
                foreach (var line in arc.listReportLines)
                {
                    if (line.relativeName == "ItemAvailable")
                    {
                        linePrefab.GetComponent<Text>().text = line.reportSentence;
                    }
                }
            }
            else
            {
                foreach (var line in arc.listReportLines)
                {
                    if (line.relativeName == "ItemNotAvailable")
                    {
                        linePrefab.GetComponent<Text>().text = line.reportSentence;
                    }
                }
            }
            GO = Instantiate(linePrefab);
            GO.transform.SetParent(lineContainer.transform);
        }
    }

}
