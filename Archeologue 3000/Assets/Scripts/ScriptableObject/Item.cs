using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public int id;
    public new string name;
    public string description;
    public Sprite aspect;
    public List<Archeologist> archeologistList;
    public int affinityValue;
}
