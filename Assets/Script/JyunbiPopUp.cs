using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JyunbiPopUp : MonoBehaviour
{
    
    public StagePopUp stagePopUpPrefab;
    //public GameObject stagePopUpPrefab;
    public Transform canvasTransform;


    public void CreatePopUp(string kinName)
    {
        //ゲームオブジェクト型でインスタンシエイトすると欲しい情報をわざわざGetcomponentしないといけなくなるが
        //StagePopUp stagePopUp =
        //GameObject test = Instantiate(stagePopUpPrefab, canvasTransform, false);
        //StagePopUp stagePopUp = test.GetComponent<StagePopUp>();
        //stagePopUp.kinImage = ;

        //クラスでクローンするとそのままクラスに入れられる、欲しい情報を直でもらえる
        //第一引数には欲しいクラスがついているプレファブを指定する。prefabの型と欲しいクラスを合わせる。
        //クラスをprefabに指定してもちゃんとゲームオブジェクトができる。
        StagePopUp stagePopUp = Instantiate(stagePopUpPrefab, canvasTransform, false);
        //kinImageはpulicで宣言されているのでいじれる
        stagePopUp.kinImage.sprite = Resources.Load<Sprite>("Image/" + kinName);

    }
}
