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

    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> raycastHitList = new List<ARRaycastHit>();
    public GameObject battleKinObj;

    private float tempScale;


    void Awake()
    {

        //変数を用意しておいて、GameDataから必要な情報をもらう
        kinStates = GameData.battleKinStates;

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
                if (data.kinNum == kinStates.rundomNum)
                {
                    setKinPrefab = kinModelPrefabs[data.kinNum];
                }
            }
        }

        //ARRayCastを取得し、AR空間にRayを飛ばして当たり判定を取れるようにしておく
        raycastManager = GetComponent<ARRaycastManager>();

        

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
                    battleKinObj.GetComponent<ShotManager>().kinStateManager = this;
                    //モデルのサイズを取得しておく
                    tempScale = battleKinObj.transform.localScale.x;
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
