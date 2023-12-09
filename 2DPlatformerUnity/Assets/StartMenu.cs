using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public void StartLevel1(){
        SceneManager.LoadScene("ProgStructLevel1");
    }
    public void StartLevel2(){
        SceneManager.LoadScene("BazyDanychLevel2");
    }
    public void StartLevel3(){
        SceneManager.LoadScene("AnalizaMatematycznaLevel3");
    }
    public void StartScene(){
        SceneManager.LoadScene("StartScene");
    }

}
