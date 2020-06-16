using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Location", menuName = "Location")]
public class Location : ScriptableObject
{
    public new string name;
    public int id;
    public Sprite aspect;
    public List<Item> items;
}
