using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject squarePrefabs;
    [SerializeField] private GameObject conclusionPanel;
    [SerializeField] private Transform squaresPanel;
    [SerializeField] private Transform questionPanel;
    [SerializeField] private Text questionText;
    [SerializeField] private Sprite[] squareSprites;
    [SerializeField] private AudioClip trueButtonClip;
    [SerializeField] private AudioClip falseButtonClip;
    private AudioSource audioSource;
    private GameObject trueSquare;
    private GameObject[] squaresArray = new GameObject[25];
    private RemainingHealthManager remainingManager;
    private ScoreManager scoreManager;

    private int bolenDeger;
    private int bolunenDeger;
    private int whichQuestion;
    private int valueChecked;
    private int buttonValue;
    private int remainingRight;
    private bool buttonPressed;
    private string difficultyLvlProblem;
    //Kutucuktaki de�erlerin hepsini listeye atmak i�in burda liste olu�turuyoruz.
    List<int> partitionValuesList = new List<int>();

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        conclusionPanel.GetComponent<RectTransform>().localScale = Vector3.zero;
        remainingRight = 3;
        remainingManager = Object.FindObjectOfType<RemainingHealthManager>();
        remainingManager.RemainingHealthChecked(remainingRight);
        scoreManager = Object.FindObjectOfType<ScoreManager>();
    }
    void Start()
    {
        buttonPressed = false;
        //Bir animasyonla ba�latmak istedi�imden ilk ba�ta yok olmas� i�in bu sat�r yaz�ld�.
        questionPanel.GetComponent<RectTransform>().localScale = Vector3.zero;
        SquareCreate();
    }
    private void SquareCreate()
    {
        for (int i = 0; i < 25; i++)
        {
            GameObject square = Instantiate(squarePrefabs, squaresPanel);
            square.transform.GetChild(1).GetComponent<Image>().sprite = squareSprites[Random.Range(0,squareSprites.Length)];
            //Burda kutuya bas�ld���nda �al��t�r�lcak fonksiyonuda belirtiyoruz olu�turmayla beraber
            square.transform.GetComponent<Button>().onClick.AddListener(()=>ButtonClick());
            squaresArray[i] = square;
        }
        PrintSectionValuesText();
        StartCoroutine(DoFadeRoutine());
    }
    //butona bas�ld���nda �al��t�r�l�cak fonksiyon
    void ButtonClick()
    {
        if (buttonPressed)
        {
            //Butona verilen say� de�erini console ekran�m�zda bast���m�zda g�rebilmemiz i�in yazd���m�z sat�r(�nemli)!!
            buttonValue = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);
            trueSquare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            ValueChecked();
        }
    }

    void ValueChecked()
    {
        if (buttonValue == valueChecked)
        {   audioSource.PlayOneShot(trueButtonClip);
            //Kendin bak ��z ��nk� bunu sen yapt�n !!
            trueSquare.transform.GetChild(1).GetComponent<Image>().enabled = true;
            trueSquare.transform.GetChild(0).GetComponent<Text>().text = "";
            trueSquare.GetComponent<Button>().interactable = false;//burada enabled "Button" componentini devre d��� b�rak�r interactable ise "Button" Componentinin kendi �zelli�i olan butonla etkile�imi kapat�r.

            scoreManager.ScoreIncrease(difficultyLvlProblem);
            partitionValuesList.RemoveAt(whichQuestion);
            if (partitionValuesList.Count > 0)
            {
                QuestionPanelSetActive();
            }
            else
            {
                GameOver();
            }
        }
        else
        {
            audioSource.PlayOneShot(falseButtonClip);
            remainingRight--;
            remainingManager.RemainingHealthChecked(remainingRight);
            if (remainingRight <= 0 )
            {
                GameOver();
            }
        }
    }
    void GameOver()
    {
        buttonPressed = false;
        conclusionPanel.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }
    //Karelerin Olu�mas�n� geciktirerek ortaya g�zel bir animasyon veriyoruz
    IEnumerator DoFadeRoutine()
    {
        foreach (var square in squaresArray)
        {
            square.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
            yield return new WaitForSeconds(0.07f);
        }
        //foreach d�ng�s� yani kutucuklar�m�z�n olu�mas� biter bitmez sorumuz ekrana ��k�yor.
        QuestionPanelSetActive();
    }
    //Kutucuklar�m�za rastgele de�er vermemizi sa�layan metot
    void PrintSectionValuesText() 
    {
        foreach(var square in squaresArray)
        {
            int randomValue = Random.Range(1, 13);
            partitionValuesList.Add(randomValue);
            //Kutucu�umuzun Child'i olan texte ula�mam�z i�in yazd���m�z sat�r� g�r�yorsunuz.
            square.transform.GetChild(0).GetComponent<Text>().text = randomValue.ToString();
        }
    }
    //Sorumuzun ekrana k���kten b�y��e do�ru boyutu artarak belirmesi i�in yazd���m�z metot
    void QuestionPanelSetActive()
    {
        AskQuestion();
        buttonPressed = true;
        questionPanel.GetComponent<RectTransform>().DOScale(1, 0.5f);
    }
    //Soruca��m�z sorunun cevab� hangi kutucuk olucak onunla ilgili yaz�lm�� bir metot
    void AskQuestion()
    {
        bolenDeger = Random.Range(2, 11);
        whichQuestion = Random.Range(0, partitionValuesList.Count);
        valueChecked = partitionValuesList[whichQuestion];
        bolunenDeger = bolenDeger * valueChecked;
        if (bolunenDeger <= 40)
        {
            difficultyLvlProblem = "Easy";
        }
        else if (bolunenDeger <= 60)
        {
            difficultyLvlProblem = "Medium";
        }
        else
        {
            difficultyLvlProblem = "Hard";
        }
        questionText.text = bolunenDeger.ToString()+ " : "+ bolenDeger.ToString();
    }
}
