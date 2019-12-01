using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


/// <summary>
/// バトルシーンの管理クラス
/// </summary>
public class BattleManager : MonoBehaviour
{

    

    [Header("リザルトポップアップのPrefab")]
    public ResultPopUp resultPopUpPrefab;

    [Header("リザルトポップアップの生成位置")]
    public Transform canvasTran;

    public KinStateManager kinStateManager;
    public bool isWin;

    
    public bool isGameUp; //バトル終了確認用



    public GameData.BattleKinData nakamaData;
    public GameData.BattleKinData enemyData;

    public float attackPower;
    public int maxHp;


    [Header("不利属性のときの修正値")]
    public float resistRate;

    [Header("有利属性のときの修正値")]
    public float weakRate;

    

   




    void Awake()
    {
        StartCoroutine(TransitionManager.instance.FadeIn());

      


        //バトルに参加した仲間と敵のキンのデータを取得する
        nakamaData = GameData.instance.nakamaDates;
        enemyData = GameData.instance.enemyDatas;

        kinStateManager.SetUpEnemyKinData();
        SetUpAtackPowerAndHp();

    }


    /// <summary>
    /// プレイヤーの攻撃力とMaxHpを仲間キンと敵キンの属性に合わせて設定
    /// </summary>
    public void SetUpAtackPowerAndHp()
    {
        //攻撃力への倍率の基礎値
        float rate = 1.0f;

        //属性による攻撃力への修正値を求める
        switch (nakamaData.kinType)
        {
            //Dirty >> Neutral >> Clean >> Dirty

            //仲間のキン属性
            case KIN_TYPE.DIRTY:
                switch (enemyData.kinType)
                {
                    //敵のキン属性で分岐
                    case KIN_TYPE.DIRTY: rate = 1.0f; break;
                    case KIN_TYPE.NEUTRAL: rate = weakRate; break;
                    case KIN_TYPE.CLEAN: rate = resistRate; break;
                }
                break;

            case KIN_TYPE.NEUTRAL:
                switch (enemyData.kinType)
                {
                    case KIN_TYPE.DIRTY: rate = resistRate; break;
                    case KIN_TYPE.NEUTRAL: rate = 1.0f; break;
                    case KIN_TYPE.CLEAN: rate = weakRate; break;

                }
                break;

            case KIN_TYPE.CLEAN:
                switch (enemyData.kinType)
                {
                    case KIN_TYPE.DIRTY: rate = weakRate; break;
                    case KIN_TYPE.NEUTRAL: rate = resistRate; break;
                    case KIN_TYPE.CLEAN: rate = weakRate; break;
                }
                break;
        }

        //最終的な攻撃力とmaxHpは倍率をかけて整数にした値
        attackPower = Mathf.CeilToInt(attackPower * rate);
        maxHp = Mathf.CeilToInt(maxHp * rate);
    }






 
    void Update()
    {
        if(isGameUp == true)
        {
            //負け判定
            isWin = false;
            //ゲーム終了処理の呼び出し
            GameUp();

        }

    }

    

/// <summary>
/// ゲーム終了処理
/// リザルト用ポップアップを生成
/// </summary>
public void GameUp()
{
    //リザルトポップアップを生成する
    ResultPopUp resultPopUp = Instantiate(resultPopUpPrefab, canvasTran, false);

    //勝敗、アイコンや倒した回数などを設定する
    resultPopUp.SetUp(kinStateManager, isWin);

}


}
