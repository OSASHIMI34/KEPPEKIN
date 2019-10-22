using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="KinData",menuName = "ScritablaObjects/CreateKindata")]
public class KinData : ScriptableObject
{
    public List<KinDataList> kinDataList = new List<KinDataList>();

    [System.Serializable]
    public class KinDataList
    {
        public int kinNum;
        public string kinName;
        public int rarerity;
        public int level;
        public KIN_TYPE kinType;
    }
}