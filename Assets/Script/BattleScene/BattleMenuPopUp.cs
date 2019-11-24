using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleMenuPopUp : MonoBehaviour
{

    public GameObject battleBg;

    public void ReturnBattle()

    {
        //シーケンス宣言して、Appendで順番に処理を書いていく、秒数の合計はDestroyの処理時間に合わせる
        Sequence sequence = DOTween.Sequence();
        sequence.Append(battleBg.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).SetEase(Ease.Linear));
        sequence.Append(battleBg.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.25f).SetEase(Ease.Linear));
        sequence.Join(battleBg.GetComponent<CanvasGroup>().DOFade(0, 0.25f).SetEase(Ease.Linear));


        //backGround.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 1.0f);
        //第二引数にfloat型で破壊されるまでの待ち時間を指定できる
        Destroy(gameObject, 0.35f);
    }


    public void ReturnStage()
    {
        StartCoroutine(SceneStateManager.instance.MoveScene(SCENE_TYPE.BATTLE));
    }

}
