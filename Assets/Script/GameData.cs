using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームデータを保存・管理するクラス
/// </summary>
public class GameData : MonoBehaviour
{
    //シングルトン
    public static GameData instance;

    //TODO ここにバトルシーンなどに引き継ぎたいデータを保存する変数を用意する
    //まずSceneManagerクラスから変数を移す
    public static int rank;
    public static int exp;
    public static int chochiku;

    [Header("キンのデータベース(スクリプタブルオブジェクト)")]
    public KinData kindata;

    public static KinStates battleKinStates; //バトルするキンのデータ(その都度変更される)
    public static KinStates nakamaKinStates; //バトルに連れていく仲間のキンのデータ(その都度変更される)

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }


}
