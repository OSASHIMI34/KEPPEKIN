using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StagePopUp : MonoBehaviour
{
    public GameObject backGround;
    public Image kinImage;
    public Image typeImage;
    public GameObject kinName;

    public void ClosePopUp()

    {
        //シーケンス宣言して、Appendで順番に処理を書いていく、秒数の合計はDestroyの処理時間に合わせる
        Sequence sequence = DOTween.Sequence();
        sequence.Append(backGround.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).SetEase(Ease.Linear));
        sequence.Append(backGround.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.25f).SetEase(Ease.Linear));
                sequence.Join(backGround.GetComponent<CanvasGroup>().DOFade(0, 0.25f).SetEase(Ease.Linear));


        //backGround.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 1.0f);
        //第二引数にfloat型で破壊されるまでの待ち時間を指定できる
        Destroy(gameObject, 0.35f);
    }

}
