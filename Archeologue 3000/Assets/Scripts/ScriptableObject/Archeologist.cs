using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Archeologist", menuName = "Archeologist")]
public class Archeologist : ScriptableObject
{
    [System.Serializable]
    public struct ReportLines
    {
        public string relativeName;

        [TextArea(3, 20)]
        public string reportSentence;
    }

    public new string name;
    public string role, description, sex;
    public int id;
    public enum AffinityState { NEGATIF, NEUTRAL, POSITIF }
    public List<AffinityState> archeologistAffinity;
    public List<AffinityState> locationAffinity;
    public Sprite sheetAspect;
    public Sprite face;
    public List<ReportLines> listReportLines;

    
}
