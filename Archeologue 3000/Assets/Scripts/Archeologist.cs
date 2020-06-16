using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archeologist : MonoBehaviour
{
    private string archeologistName, role, description, sex;
    private int id;
    private enum AffinityState { NEGATIF, NEUTRAL, POSITIF }
    private List<AffinityState> archeologistAffinity;
    private List<AffinityState> locationAffinity;
    private Sprite sheetAspect;
}
