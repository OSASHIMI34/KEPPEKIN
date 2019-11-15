using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// キンから弾を生成するクラス
/// </summary>
public class ShotManager : MonoBehaviour
{
    [Header("生成する弾のオブジェクトのクラス")]
    public KinBullet kinBulletPrefab;

    [Header("弾の速度")]
    public float speed;

    [Header("弾を生成するまでの待機時間")]
    public float waitTime;

    private int count = 0;

    void Update()
    {
        count += 1;
        //waittTimeフレームごとにショットする（小さいほど早く打ってくる）
        if (count % waitTime == 0)
        {
            //キンをランダムに回転させる
            float value_x = Random.Range(-40, 40);
            float value_y = Random.Range(-140, -180);
            transform.DORotate(new Vector3(value_x, value_y, 0), 0.5f);
            Kinshot();
        }

    }

    public void Kinshot()
    {
        KinBullet kinBullet = Instantiate(kinBulletPrefab, transform.position, transform.rotation);
        kinBullet.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }

}
