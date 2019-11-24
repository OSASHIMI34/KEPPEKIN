using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ResultPopUp : MonoBehaviour
{
    [Header("除菌回数を表示するテキスト")]
    public TMP_Text removeCountTxt;

    [Header("バトルしたキンのアイコン")]
    public Image removeKinIcon;

    [Header("バトルしたキンのなまえ")]
    public string removeKinName;

    [Header("バトルしたキンの属性")]
    public KIN_TYPE removeKinType;

    [Header("勝利イメージ")]
    public Image winImage;

    [Header("敗北イメージ")]
    public Image loseImage;



    /// <summary>
    /// BattleManager.CSから呼ばれる
    /// リザルトポップアップに表示する情報を設定する
    /// </summary>
    public void SetUp(KinStateManager kinState, bool isResult)
    {
        //除菌回数やキンのアイコンなどの情報をKinStateManagerのloadEnemyDataから取得し表示する
        removeCountTxt.text = kinState.loadEnemyData.removeCount.ToString();
        removeKinIcon.sprite = Resources.Load<Sprite>("Image/" + kinState.loadEnemyData.kinName);
        removeKinName = kinState.loadEnemyData.kinName;
        removeKinType = kinState.loadEnemyData.kinType;

        //勝敗によって分岐
        if (isResult)
        {
            //勝利　勝利イメージを表示
            winImage.enabled = true;

            //除菌回数をアニメ付きで加算する
            DOTween.To(() => kinState.loadEnemyData.removeCount, (x) =>
            kinState.loadEnemyData.removeCount = x,
            kinState.loadEnemyData.removeCount++, 1.5f).SetRelative();
        }
        else
        {
            //敗北　敗北イメージ表示
            loseImage.enabled = true;

        }
        }


    public void OnClickCloseButton()
    {
        StartCoroutine(SceneStateManager.instance.MoveScene(SCENE_TYPE.STAGE));
    }

}
