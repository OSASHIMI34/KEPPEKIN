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
        SceneStateManager.instance.UpdateGage();

    }

    public void Win()
    {
        //先頭に勝つたびダーティが-30
        //最終的に0以下になると経験値が30増える
        currentDirtyPoint -= 30f;

        if (currentDirtyPoint <= 0)
        {
            currentDirtyPoint = 0;
            SceneStateManager.exp += 50;
            SceneStateManager.instance.UpdateGage();

            currentDirtyPoint = maxDirtyPoint;

            //経験値が100以上になるとランクが１上がる
            if (SceneStateManager.exp >= 100)
            {
                SceneStateManager.exp = 0;
                SceneStateManager.instance.UpdateGage();
                SceneStateManager.rank += 1;

                Debug.Log(SceneStateManager.rank);

            }
        }


        dirtyGage.fillAmount = currentDirtyPoint / maxDirtyPoint;
        
    }

    public void Lose()
    {
       
    }
   
}
