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

    [Header("バトル時間の設定値")]
    public int battleTime; //インスペクターで設定する

    [Header("残り時間の文字が大きくなる時間")]
    public int limitTime;

    [Header("バトル時間の表示")]
    public TMP_Text battleTimeText;

    [Header("リザルトポップアップのPrefab")]
    public ResultPopUp resultPopUpPrefab;

    [Header("リザルトポップアップの生成位置")]
    public Transform canvasTran;

    public KinStateManager kinStateManager;
    public bool isWin;

    private int currentTime; //現在の残り時間
    private float timer; //時間計測用
    private bool isGameUp; //バトル終了確認用



    public GameData.BattleKinData nakamaData;
    public GameData.BattleKinData enemyData;

    public float attackPower;
    public int maxHp;


    [Header("不利属性のときの修正値")]
    public float resistRate;

    [Header("有利属性のときの修正値")]
    public float weakRate;



    
    void Start()
    {
        StartCoroutine(TransitionManager.instance.FadeIn());

        //currentTimeにbattleTimeを設定する
        currentTime = battleTime;


        //バトルに参加した仲間と敵のキンのデータを取得する
        nakamaData = GameData.instance.nakamaDates;
        enemyData = GameData.instance.enemyDatas;

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
        //バトルが終了していないなら
        if (!isGameUp)
        {
            //timerを利用して経過時間を計測
            timer += Time.deltaTime;

            //１秒経過ごとにtimerを0に戻し、currentTimeを減算する
            if (timer >= 1)
            {
                timer = 0;
                currentTime--;

                //時間表示を更新する(ToString("F0"))を使って、小数点は表示しない
                battleTimeText.text = currentTime.ToString("F0");

                //残り時間がlimitTimeになった文字を一瞬大きくする
                if (currentTime <= limitTime)
                {
                    Sequence seq = DOTween.Sequence();
                    seq.Append(battleTimeText.transform.DOScale(1.5f, 0.25f));
                    seq.Append(battleTimeText.transform.DOScale(1.0f, 0.25f));
                }

                if (currentTime <= 0)
                {
                    //バトル終了のフラグを立てる
                    currentTime = 0;
                    battleTimeText.text = currentTime.ToString("F0");
                    isGameUp = true;
                }
            }
        }
        else
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
