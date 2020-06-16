using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resident", menuName = "Resident")]
public class Resident : ScriptableObject
{
    public new string name;
    public Sprite face;
    public int rewardId;
    public Queue<Question> questionsSeries;
    //the list which will contain the questions, and then we'll transfer it in the Queue
    public List<Question> questionToTransfer;

    void OnEnable()
    {
        questionsSeries = new Queue<Question>(questionToTransfer);
    }
}
