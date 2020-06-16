using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewsPaper : MonoBehaviour
{
    private Queue<Sprite> content;
    //the List to transfer in the queue
    public List<Sprite> contentList;

    void Start()
    {
        content = new Queue<Sprite>(contentList);
    }
}
