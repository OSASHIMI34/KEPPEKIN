using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDebug : MonoBehaviour
{
    public Image dirtyGage;
    public float maxDirtyPoint;
    public float currentDirtyPoint;

    public GameObject debugPanel;


    private void Start()
    {
        //100/100で始まるのでゲージがフルの状態で始まる
        currentDirtyPoint = maxDirtyPoint;
        //現在値を最大値で割ることで徐々にゲージを減らしていける
        dirtyGage.fillAmount = currentDirtyPoint / maxDirtyPoint;
    }

    public void Win()
    {
        currentDirtyPoint -= 30f;
        dirtyGage.fillAmount = currentDirtyPoint / maxDirtyPoint;
        
    }

    public void Lose()
    {
       
    }
   
}
