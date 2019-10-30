using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class EnemyManager : MonoBehaviour
{

    [Header("生成するキンの数")]
　　 public int enemys; //３に指定する

    [Header("キンのデータベース(スクリプタブルオブジェクト)")]
    public KinData kindata;

    [Header("生成したキンを管理する(入れておく)配列")]
    public Silhouette[] silhouettesImageBox;

    [Header("生成するキンのプレファブ。クラスから生成")]
    public Silhouette silhouettePrefab;

    [Header("生成するキンの初期位置")]
    public Transform mainStageTransform;


    [Header("生成する位置の幅の最小値")]
    public float minX;
    [Header("生成する位置の幅の最大値")]
    public float maxX;

    [Header("生成する位置の高さの最小値")]
    public float minY;
    [Header("生成する位置の高さの最大値")]
    public float maxY;

    private Vector3 initTran; //生成したキンの位置の調整用


    void Awake()
    {
        StartCoroutine(CreateKinMaskImage());

    }

    private IEnumerator CreateKinMaskImage()
    {
        //SilhouetteImageBox(キンを入れる配列のパーテーション)をいくつ作るか決めないといけないのでまず
        //生成するenemyの数とSilhouetteImageboxの数を一致させる
        //new 初期化
        silhouettesImageBox = new Silhouette[enemys];

        //ランダムな値を代入しておくListを作成し、初期化
        List<int> randomNumbers = new List<int>();

        for (int i = 0; i < kindata.kinDataList.Count; i++)
        {
            //iの値が入っているint型のリストをi個分作っておく
            //List<int>の中身は順番に{0,1,2,3,4,5,6,7,8,9}になっている
            randomNumbers.Add(i);
        }

        //ランダムな値を取得し保存しておくListを作成し、初期化
        List<int> results = new List<int>();

        //while分は(条件)になるまで処理を繰り返す　＝ここでは、enemyの値が0になるまでループする
        while (enemys > 0)
        {
            //kindata.KindataList.Countはスクリプタブルオブジェクトの要素数(Size)をとってきてくれる
            //0〜10の数字をランダムで
          　int random = Random.Range(0, randomNumbers.Count);


            //randomNumbersのrandom番目の中身を見て、その中身をenemyNumに入れる
            //randomNumbersの中身は0-9の値が入っているので、上で取った値と同じ値が取れる
            int enemyNum = randomNumbers[random];

            //ランダムな値を記録するためにListに追加
            results.Add(enemyNum);　//ランダムな値を追加

            //重複してランダムの値を取らないように使った値はListから除いておく
            randomNumbers.Remove(enemyNum); //重複回避用に引数を修正
            Debug.Log(enemyNum);　//重複回避用のデバッグ表示用に引数を修正

            //ランダムな値をとり終わったので、enemysをデクリメントする
            enemys--;
        }

          
        
        //lengthは要素数の最大値をとってきてくれる、countのようなもの
        //配列...length, リスト...countが最大値
        for (int i =0; i < results.Count; i++)
        {
            //クラスを使ってキンのオブジェクト(シルエット)をインスタンシエイトする。
            //生成位置はCanvas(mainStageTransform)内にする。
            Silhouette silhouetteObj = Instantiate(silhouettePrefab, mainStageTransform, false);

            //全て同じ位置に生成されてしまうので、ランダムな位置に生成するため調整を加える
            initTran.x = Random.Range(minX, maxX);
            initTran.y = (-400 + (300 * i) + Random.Range(minY, maxY));

            //最終的な位置を決める
            silhouetteObj.transform.localPosition = initTran;

        
            //inの右側は型とかクラスの変数。探したいものがあるリスト。10こあるやつ
            //inの左側は探し物を一つずつ確認する変数。右と左の型が一緒じゃないとダメ。
            //右と左が合致したら中の処理に入ってくれる、入らないうちは0から一つずつ確認していってくれる
            foreach(KinData.KinDataList data in kindata.kinDataList)
            {
                if (data.kinNum == results[i])
                {
                    //合致したデータの持つ値(名前、属性、イメージ)を生成したキンのデータに入れる
                    silhouetteObj.rundomNum = data.kinNum;
                    silhouetteObj.kinName = data.kinName;
                    silhouetteObj.type = data.kinType;
                    silhouetteObj.kinMaskImage.sprite = Resources.Load<Sprite>("Image/" + data.kinName);

        //キンをリストに追加する
        silhouettesImageBox[i] = silhouetteObj;

        //生成したキンに拡大+縮小のアニメを再生させる
        Sequence seq = DOTween.Sequence();

        seq.Append(silhouetteObj.transform.DOScale(new Vector3(1.5f, 1.5f), 0.7f));
        seq.Append(silhouetteObj.transform.DOScale(new Vector3(1.0f, 1.0f), 0.3f));

        //1秒待つ
        yield return new WaitForSeconds(1.0f);

        //生成したキンのSiihouetteクラスの持つ、IdleAnimeImageメソッドを呼び出す
        StartCoroutine(silhouetteObj.IdleAnimeImage()); //コルーチン呼び出し

       

                }
            }

        }
    }

}

