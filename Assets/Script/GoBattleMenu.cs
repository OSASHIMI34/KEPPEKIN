using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBattleMenu : MonoBehaviour
{
   public void MoveBattleMenu()
    {
        SceneManager.LoadScene("Stage_1");
    }
}
