using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using DG.Tweening;

public class KinStateManager : MonoBehaviour
{

    public string bulletTypeName;
    public KinStates kinStates;

    public float maxHp;
    public float currnetHp;

    public GameObject[] kinModelPrefabs;
    public GameObject setKinPrefab;
    public Camera arCamera;
    public bool isDebugOn;

    public ARRaycastManager raycastManager;
    private List<ARRaycastHit> raycastHitList = new List<ARRaycastHit>();
    public GameObject battleKinObj;

    private float tempScale;

    public GameData.BattleKinData loadEnemyData = new GameData.BattleKinData(); //初期化してる

    [Header("BattleManagerへの紐付け")]
    public BattleManager battleManager;

    private bool isKinCreate;
    private float timer;

    [Header("Timerの生成位置")]
    public Transform timerTran;

    public BattleTimer battleTimerPrefab;






    public void SetUpEnemyKinData()
    {

        //変数を用意しておいて、GameDataから必要な情報をもらう
        loadEnemyData = GameData.instance.enemyDatas; //enemydatasにはStagePopUpクラスのsaveenemykindata入っとる


        if (isDebugOn)
        {
            setKinPrefab = kinModelPrefabs[0];
            maxHp = 100; //(kinStates.maxHp)
            currnetHp = maxHp;
        }
        else
        {
            //インスタンスするモデルを設定する
            foreach(KinData.KinDataList data in GameData.instance.kindata.kinDataList)
            {
                //KinDataに登録されているキンの名前とバトルのキンの名前を照合する
                if (data.kinNum == loadEnemyData.kinNum)
                {
                    setKinPrefab = kinModelPrefabs[data.kinNum];
                }
            }
        }

        //ARRayCastを取得し、AR空間にRayを飛ばして当たり判定を取れるようにしておく
        //raycastManager = GetComponent<ARRaycastManager>();

        

    }


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase != TouchPhase.Ended)
            {
                return;
            }

            if (battleKinObj == null)
            {
                if (raycastManager.Raycast(touch.position, raycastHitList, TrackableType.All))
                {
                    Debug.Log("Raycast成功");
                    Vector3 posY = new Vector3(raycastHitList[0].pose.position.x, raycastHitList[0].pose.position.y + 0.8f, raycastHitList[0].pose.position.z + 1f);
                 

                    battleKinObj = Instantiate(setKinPrefab, posY, raycastHitList[0].pose.rotation);
                    battleKinObj.GetComponent<ShotManager>().SetUp(this);
                    //モデルのサイズを取得しておく
                    tempScale = battleKinObj.transform.localScale.x;

                    isKinCreate = true;
                    
                }
                else
                {
                    Debug.Log("RayCast失敗");
                }
            }

        }

        //objが入ったら下の処理が動く
        if (battleKinObj != null)
        {
            battleKinObj.transform.LookAt(arCamera.transform);
        }

        //キンが出たフラグがたち、１フレームごとにカウントが足されて3秒経ったら残り時間を表示する
        if (isKinCreate == true)
        {
            timer += Time.deltaTime;
            if (timer >= 3.0f)
            {
                isKinCreate = false;　//生成されるのは一回だけ
               BattleTimer battleTimer = Instantiate(battleTimerPrefab, timerTran, false);
                //BattleTimerクラスにもBattleManagerクラスをもたせたいのでこのクラスが持っているBattleManagerの
                //紐付けをそのまま渡してあげる
                battleTimer.battleManager = battleManager;
            }
        }

    }



    /// <summary>
    /// キンへのダメージと縮小処理
    /// </summary>
    public void ProcDamage(float damage)
    {
        currnetHp -= damage;
        float shrinkScale = (currnetHp - 0) / (maxHp - 0);
        

        //battleKinObj.transform.localScale = new Vector3(shrinkScale * battleKinObj.transform.localScale.x, shrinkScale * battleKinObj.transform.localScale.y, shrinkScale * battleKinObj.transform.localScale.z);

        //DOTweenアニメーション

        battleKinObj.transform.DOScale(tempScale * shrinkScale, 0.5f);

    }
}
