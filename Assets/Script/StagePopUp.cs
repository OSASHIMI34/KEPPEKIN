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
    public GameObject levelImagePrefab;
    public GameObject rarelityImagePrefab;

    public Transform levelPlace;
    public Transform rarelityPlace;


    public GameObject debugPanel;
    public BattleDebug battleDebug;

    public Button winButton;
    public Button loseButton;

    /// <summary>
    /// 強さとレアリティの値に応じてイメージを生成する
    /// </summary>
    public void SetUp(int level, int rarelity)
    {
        for (int i = 0; i<level; i++)
        {
            Instantiate(levelImagePrefab, levelPlace, false);
        }

        for (int i =0; i<rarelity; i++)
        {
            Instantiate(rarelityImagePrefab, rarelityPlace, false);
        }

        //BattleDebugを探して紐付けする
        battleDebug = GameObject.FindGameObjectWithTag("Stage").GetComponent<BattleDebug>();

        //ボタンに外からメソッドを登録できるonClickのスクリプト版
        //AddListenerに登録できるメソッドは引数を持ってないメソッドだけ
        //登録したい場合は　AddListener(() => battleDebug.Win(level, rarelity))のようにする(ラムダ式)
        //private void Test () {    //  () => の部分
        //battleDebug.Win(level, rarelity);
        //}
    winButton.onClick.AddListener(() => battleDebug.Win(level, rarelity));
        loseButton.onClick.AddListener(battleDebug.Lose);
    }



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

    public void OnClickBattleButton()
    {
        debugPanel.SetActive(true);

    } 

    public void CloseDebugPanel()
    {
        debugPanel.SetActive(false);
        ClosePopUp();
    }

}
