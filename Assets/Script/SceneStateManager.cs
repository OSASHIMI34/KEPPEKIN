using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateManager : MonoBehaviour
{

    //シングルトン...この作り方をするとシーン中に一個しか存在しない
    //staticはシーンをまたげる
    public static SceneStateManager instance;

    private void Awake()
    {
        //nullは初めてゲームが実行された場合のこと
        if(instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject) >> シーン遷移してもgameObjectを壊すなという命令
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //すでに一個あったら２個目は作らない、破棄する
            Destroy(gameObject);
        }
    }

    public void MoveStage()
    {
        SceneManager.LoadScene("Stage_1");
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        //SceneManager.LoadScene(1..., 2...一緒に他のシーンを読み込む場合に使う);
        //Additive...第一引数にしたものが一番上になるようにレイヤー構造にしてくれる

    }

    public void MoveHome()
    {
        SceneManager.LoadScene("Home");
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }



}
