using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class _MGR_MissionManager : MonoBehaviour
{
    private static _MGR_MissionManager p_instance = null;
    public static _MGR_MissionManager Instance { get { return p_instance; } }
    #region HUD
    public Image LocationAspect;

    public GameObject archeologistContainer;
    public GameObject archeologistPrefab;

    private List<Archeologist> displayedArcheologists;
    public List<Archeologist> pickedArcheologists;
    public Location currentLocation;

    [SerializeField] private GameObject mapHUD;
    [SerializeField] private GameObject archeologistHUD;

    [SerializeField] private GameObject sendMissionButton;
    [SerializeField] private GameObject notEnoughArcheologistPanel;

    //The current archeologue pointed
    [SerializeField] private Text arcName;
    [SerializeField] private Text arcJob;
    [SerializeField] private Image arcFace;
    [SerializeField] private Text arcDescription;

    //The object which contains picked archeologist
    public  GameObject pictureContainer;
    [SerializeField] private GameObject picturePrefab;
    [SerializeField] private Text week;

    //The magnifying glass, to reset its position
    [SerializeField] private ChooseLocation magnGlass;
    #endregion

    #region mission
    public int nbrArcheologistByMission;

    [HideInInspector] public bool missionSend = false;

    [SerializeField] private int[] positifValue = new int[4];
    [SerializeField] private int[] negatifValue = new int[4];

    [SerializeField] private int positifLocationValue;
    [SerializeField] private int negatifLocationValue;

    [SerializeField] private int nbrItemMaxByMission;
    public List<Item> itemBroughtBack;
    #endregion
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
        displayedArcheologists = new List<Archeologist>();
    }

    #region HUD
    public void AddArcheologistToHUD()
    {
        foreach (Archeologist arc in _MGR_GameManager.Instance.currentArcheologists)
        {
            bool arcIsAlreadyIn = false;
            foreach (Archeologist archeo in displayedArcheologists)
            {
                if (arc.name == archeo.name)
                {
                    arcIsAlreadyIn = true;
                }
            }
            if (arcIsAlreadyIn == false)
            {
                GameObject go;
                SetArcheologistHUD(arc);
                go = Instantiate(archeologistPrefab);
                go.GetComponentInChildren<ArcheologistToPick>().archeologist = arc;
                go.transform.SetParent(archeologistContainer.transform);
                displayedArcheologists.Add(arc);
            }
        }
    }

    public void ClearPickedArcheologist()
    {
        foreach (ArcheologistToUnequip arc in pictureContainer.transform.GetComponentsInChildren<ArcheologistToUnequip>())
        {
            arc.UnPick();
        }
        pickedArcheologists.Clear();
        sendMissionButton.SetActive(true);
    }

    void SetArcheologistHUD(Archeologist arc)
    {
        foreach (Image image in archeologistPrefab.GetComponentsInChildren<Image>())
        {
            if (image.gameObject != archeologistPrefab)
            {
                image.sprite = arc.face;
            }
        }
    }

    public void SetLocation(Location loc)
    {
        LocationAspect.sprite = loc.aspect;
        currentLocation = loc;
        week.text = "Semaine : " + _MGR_GameManager.Instance.weekCounter + " : " + currentLocation.name;
    }

    public void ActualizeTheArcheologistPointed(Archeologist arc)
    {
        arcName.text = arc.name;
        arcJob.text = arc.role;
        arcFace.sprite = arc.face;
        arcDescription.text = arc.description;
    }

    public void AddPickedArcheologist(Archeologist arc)
    {
        GameObject GO;
        GO = Instantiate(picturePrefab);
        foreach (Image image in GO.GetComponentsInChildren<Image>())
        {
            if (image.gameObject != GO)
            {
                image.sprite = arc.face;
            }
        }
        GO.GetComponentInChildren<ArcheologistToUnequip>().archeologist = arc;
        GO.GetComponentInChildren<Text>().text = arc.name;
        GO.transform.SetParent(pictureContainer.transform);
    }

    public void RemovePickedArcheologist(Archeologist arc)
    {
        foreach (Transform TR in pictureContainer.transform)
        {
            if (TR.gameObject.GetComponentInChildren<Text>().text == arc.name)
            {
                Destroy(TR.gameObject);
            }
        }
    }
    #endregion

    #region mission
    public void SendMission()
    {
        itemBroughtBack = new List<Item>();
        if (pickedArcheologists.Count == nbrArcheologistByMission && missionSend == false && currentLocation != null)
        {
            missionSend = true;
            sendMissionButton.SetActive(false);
            int missionAffinityValue = AffinityValueCalcul();
            currentLocation.items = currentLocation.items.OrderByDescending(a => a.affinityValue).ToList();
            foreach (Item item in currentLocation.items)
            {
                if (item.affinityValue <= missionAffinityValue)
                {
                    bool allArcRGood = true;
                    foreach (Archeologist arc in item.archeologistList)
                    {
                        if (!pickedArcheologists.Contains(arc))
                        {
                            allArcRGood = false;
                        }

                    }
                    if (allArcRGood == true)
                    {
                        if (itemBroughtBack.Count < nbrItemMaxByMission)
                        {
                            if (!_MGR_ItemManager.Instance.allItemRetrieved.Contains(item))
                            {
                                itemBroughtBack.Add(item);  
                            }
                        }
                    }
                }
            }
            foreach (Item item in itemBroughtBack)
            {
                Debug.Log(item.name);
            }
            archeologistHUD.SetActive(false);
            mapHUD.SetActive(false);

        }
        else notEnoughArcheologistPanel.SetActive(true);
       
    }

    private int AffinityValueCalcul()
    {
        int value =50;
        int positif = 0;
        int negatif = 0;

        for (int i = 0; i < pickedArcheologists.Count - 1; i++)
        {
            for (int j = pickedArcheologists.Count - 1; j> 0+i; j--)
            {
                if (pickedArcheologists[i].archeologistAffinity[pickedArcheologists[j].id] == Archeologist.AffinityState.POSITIF)
                {
                    positif += 1;
                }
                else if (pickedArcheologists[i].archeologistAffinity[pickedArcheologists[j].id] == Archeologist.AffinityState.NEGATIF)
                {
                    negatif += 1;
                }
            }
        }

        for (int i = 0; i < positifValue.Length; i++)
        {
            if (positif == i)
            {
                value += positifValue[i];
            }
            if (negatif == i)
            {
                value -= negatifValue[i];
            }
        }

        for (int i = 0; i < pickedArcheologists.Count; i++)
        {
            if (pickedArcheologists[i].locationAffinity[currentLocation.id] == Archeologist.AffinityState.POSITIF)
            {
                value += positifLocationValue;
            }
            else if (pickedArcheologists[i].locationAffinity[currentLocation.id] == Archeologist.AffinityState.NEGATIF)
            {
                value -= negatifLocationValue;
            }
        }
            return value;
    }

    public void ActualizeItem()
    {
        foreach (Item item in itemBroughtBack)
        {
            _MGR_ItemManager.Instance.currentGotItems.Add(item);
            _MGR_ItemManager.Instance.AddItemsInHUD(item);
            _MGR_ItemManager.Instance.allItemRetrieved.Add(item);
        }
    }

    public void ComeBackToTheOriginalSettings()
    {
        currentLocation = null;
        magnGlass.magnifyingGlass.transform.position = magnGlass.oldMagnifyingGlassPosition;
    }
    #endregion
}
