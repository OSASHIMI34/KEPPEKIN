using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Silhouette : MonoBehaviour
{
    public int rundomNum;
    public string kinName;
    public JyunbiPopUp jyunbi;
    public KIN_TYPE type;

    public void YobidashiCreatePop()
    {
        jyunbi.CreatePopUp(kinName,type);

    }

   
}
