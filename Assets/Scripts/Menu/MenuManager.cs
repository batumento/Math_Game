using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject startBtn, exitBtn; 
    // Start is called before the first frame update
    void Start()
    {
        FadeOut();
    }
    //Butonlar�n hafif�e var olmas� i�in yaz�lan Metot/Fonksiyon
    void FadeOut()
    {
        startBtn.GetComponent<CanvasGroup>().DOFade(1, 0.8f);
        exitBtn.GetComponent<CanvasGroup>().DOFade(1, 0.8f).SetDelay(0.5f);
    }
    //Oyundan ��kmam�z i�in yaz�lan Metot/Fonksiyon
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("��k�� �al��t�");
    }
    //GameLevel Sahnemize ge�memiz i�in yaz�lan Metot/Fonksiyon
    public void StartGame()
    {
        SceneManager.LoadScene("GameLevelScene");
    }
}
