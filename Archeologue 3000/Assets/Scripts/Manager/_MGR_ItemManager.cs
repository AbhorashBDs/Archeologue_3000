using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _MGR_ItemManager : MonoBehaviour
{
    private static _MGR_ItemManager p_instance = null;
    public static _MGR_ItemManager Instance { get { return p_instance; } }

    public GameObject itemsContainer;
    public GameObject itemsPrefab;

    public List<Item> currentGotItems;
    public List<Item> allItemRetrieved;

    //GameObject which contains the InventorySlot Prefab
    public GameObject inventorySlotContainer;
    //List of the slot in the container
    private List<GameObject> l_inventorySlotPrefab;

    //All the texts and images, we have to chnage when we click on the item
    public Text nameItem;
    public Text description;
    public Text location;
    public Image aspect;

    //The Panel which will be opened when we click on an object
    public GameObject panelHUD;
    //The Child where all the item stuff is
    public string childName;
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
        currentGotItems = new List<Item>();
        l_inventorySlotPrefab = new List<GameObject>();
        
    }
    private void Start()
    {
        for (int i = 0; i < inventorySlotContainer.transform.childCount; i++)
        {
            l_inventorySlotPrefab.Add(inventorySlotContainer.transform.GetChild(i).gameObject);
        }
        foreach (GameObject GO in l_inventorySlotPrefab)
        {
            GO.transform.Find(childName).GetComponentInChildren<ItemOpenCloseObject>().GO = panelHUD;
        }

    }

    public void AddItemsInHUD(Item item)
    {
        GameObject go;
        go = Instantiate(itemsPrefab);
        go.GetComponentInChildren<DragableItem>().item = item;
        go.GetComponentInChildren<DragableItem>().ItemAspect();
        go.transform.SetParent(itemsContainer.transform);
    }

    //the function which add the item in the inventoryHUD
    public void AddItemsInTheInventory()
    {
        foreach (Item item in currentGotItems)
        {
            //we test for all prefab if it is free
            foreach (GameObject GO in l_inventorySlotPrefab)
            {
                //for this, we need to have a GameObject which cover all the others childs
                if (GO.transform.Find(childName).gameObject.activeSelf == false)
                {
                    GO.transform.Find(childName).gameObject.SetActive(true);
                    GO.transform.Find(childName).gameObject.GetComponentInChildren<Text>().text = item.name;
                    //See if it's take the first child, otherwise i would have to find another solution
                    GO.transform.Find(childName).gameObject.GetComponentInChildren<Image>().sprite = item.aspect;
                    GO.transform.Find(childName).gameObject.GetComponentInChildren<ItemOpenCloseObject>().locationName = _MGR_MissionManager.Instance.currentLocation.name;
                    GO.transform.Find(childName).gameObject.GetComponentInChildren<ItemOpenCloseObject>().item = item;
                    break;
                }
            }
        }
    }

    public void ReactivateTheSlot()
    {
        foreach (GameObject GO in l_inventorySlotPrefab)
        {
            if (GO.transform.Find(childName).gameObject.activeSelf == true)
            {
                foreach (Transform TR in GO.transform.Find(childName).transform)
                {
                    TR.gameObject.SetActive(true);
                }
            }
        }
    }
}
