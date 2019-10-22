using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int[] enemyBox;
    public int enemys; //３に指定する
    public KinData kindata;
    public Silhouette[] silhouette;


    void Awake()
    {
        //enemyboxをいくつ作るか決めないといけないのでまず
        //シルエットの数とenemyboxの数を一致させる
        //new 初期化
        enemyBox = new int [silhouette.Length];
        for(int i = 0; i < enemys; i++)
        {
            //kindata.kinDataList.Countはスクリプタブルオブジェクトの要素数(Size)をとってきてくれる
            //0〜10の数字をランダムで
            int random = Random.Range(0, kindata.kinDataList.Count);

            enemyBox[i] = random;
        }

        //lengthは要素数の最大値をとってきてくれる、countのようなもの
        //配列...length, リスト...countが最大値
        for (int i =0; i < enemyBox.Length; i++)
        {
            silhouette[i].rundomNum = enemyBox[i];

            //inの右側は型とかクラスの変数。探したいものがあるリスト。10こあるやつ
            //inの左側は探し物を一つずつ確認する変数。右と左の型が一緒じゃないとダメ。
            //右と左が合致したら中の処理に入ってくれる、入らないうちは0から一つずつ確認していってくれる
            foreach(KinData.KinDataList data in kindata.kinDataList)
            {
                if (data.kinNum == silhouette[i].rundomNum)
                {
                    silhouette[i].kinName = data.kinName;
                    silhouette[i].type = data.kinType;
                }
            }

        }
    }


}
