using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameScene_SM
{
    INITIALIZING,
    WAITING_FOR_QUESTION,
    WRITING_ANSWER,
    ON_ANSWER_FINISH,
    ON_INTRO
}

public class GameScene : MonoBehaviour {

    public GameScenes sceneName;
    public GameObject goToMapButton;
    public GameObject[] questionButtons;
    public GameObject dialogBox;

    public Question[] questions;

    public string[] introText;

    public GameScene_SM currentState = GameScene_SM.INITIALIZING;

    private int questionAsked = -1;

    public Text partialAnswer;
    private string fullAnswer = "";
    private int answerCharacterIndex = 0;
    public float writeSpeed = 0.25f;
    private float t = 0;

    private Question firstButtonQuestion = null;
    private Question secondButtonQuestion = null;
    private Question thirdButtonQuestion = null;

    private bool unableToGoBack = false;

    private int introTextIndex = 0;
    public GameObject nextButton;

    void OnEnable()
    {
        GameManager.Instance.SceneChanged(sceneName);
        ResetButtons();
        questionAsked = -1;
        answerCharacterIndex = 0;   
        partialAnswer.text = "";
        fullAnswer = "";
        if (GameManager.Instance.isIntro && introText.Length > 0)
        {
            fullAnswer = introText[0];
            dialogBox.SetActive(true);
            currentState = GameScene_SM.ON_INTRO;
        }
        else
        {
            currentState = GameScene_SM.INITIALIZING;
        }
    }

    // Use this for initialization
    void Update()
    {
        switch (currentState)
        {
            case GameScene_SM.INITIALIZING:

                LoadButtons();

                if(partialAnswer.text == "")
                    dialogBox.SetActive(false);

                if (!unableToGoBack && !GameManager.Instance.isIntro)
                    EnableGoToMapButton(true);
                else
                {
                    GameManager.Instance.isIntro = false;
                    unableToGoBack = false;
                }

                currentState = GameScene_SM.WAITING_FOR_QUESTION;
                break;
            case GameScene_SM.WAITING_FOR_QUESTION:
                if(questionAsked != -1)
                {
                    EnableGoToMapButton(false);

                    ResetButtons();

                    dialogBox.SetActive(true);

                    currentState = GameScene_SM.WRITING_ANSWER;
                }
                break;
            case GameScene_SM.WRITING_ANSWER:
                if(partialAnswer.text.Length == fullAnswer.Length)
                {
                    currentState = GameScene_SM.ON_ANSWER_FINISH;
                }
                else
                {
                    t += Time.deltaTime * writeSpeed;

                    if(t >= 1) { 
                        partialAnswer.text += fullAnswer[answerCharacterIndex];
                        answerCharacterIndex++;
                        t -= 1;
                    }
                }
                break;
            case GameScene_SM.ON_ANSWER_FINISH:
                questionAsked = -1;
                fullAnswer = "";
                nextButton.SetActive(true);
                //currentState = GameScene_SM.INITIALIZING;
                break;
            case GameScene_SM.ON_INTRO:
                if (partialAnswer.text.Length == fullAnswer.Length)
                {
                    nextButton.SetActive(true);
                }
                else
                {
                    t += Time.deltaTime * writeSpeed;

                    if (t >= 1)
                    {
                        partialAnswer.text += fullAnswer[answerCharacterIndex];
                        answerCharacterIndex++;
                        t -= 1;
                    }
                }
                break;
        }
    }

    public void EnableGoToMapButton(bool state)
    {
        goToMapButton.SetActive(state);
    }

    public void Answer(Question q)
    {
        questionAsked = q.id;
        answerCharacterIndex = 0;
        partialAnswer.text = "";
        fullAnswer = q.answerText;

        if (q.disableGoToMap)
            unableToGoBack = true;

        switch (q.itemToUnlock)
        {
            case Items.None:
                break;
            case Items.ItemOne:
                GameManager.Instance.hasItemOne = true;
                break;
            case Items.ItemTwo:
                GameManager.Instance.hasItemTwo = true;
                break;
            case Items.ItemThree:
                GameManager.Instance.hasItemThree = true;
                break;
            case Items.ItemFour:
                GameManager.Instance.hasItemFour = true;
                break;
            case Items.ItemFive:
                GameManager.Instance.hasItemFive = true;
                break;
            case Items.ItemSix:
                GameManager.Instance.hasItemSix = true;
                break;
            case Items.ItemSeven:
                GameManager.Instance.hasItemSeven = true;
                break;
            default:
                break;
        }

        if(q.enableScenes.Length > 0)
        {
            for (int i = 0; i < q.enableScenes.Length; i++)
            {
                switch (q.enableScenes[i])
                {
                    case GameScenes.HOUSE:
                        GameManager.Instance.isHouseEnabled = true;
                        break;
                    case GameScenes.MARKET:
                        GameManager.Instance.isMarketEnabled = true;
                        break;
                    case GameScenes.POLICE_DEPARTMENT:
                        GameManager.Instance.isPoliceDepartmentEnabled = true;
                        break;
                    case GameScenes.BUILDINGS:
                        GameManager.Instance.isBuildingsEnabled = true;
                        break;
                    case GameScenes.HOSPITAL_MORGUE:
                        GameManager.Instance.isHospitalMorgueEnabled = true;
                        break;
                    case GameScenes.CORPORATION:
                        GameManager.Instance.isCorporationEnabled = true;
                        break;
                }
            }
        }

        if (q.disableScenes.Length > 0)
        {
            for (int i = 0; i < q.disableScenes.Length; i++)
            {
                switch (q.disableScenes[i])
                {
                    case GameScenes.HOUSE:
                        GameManager.Instance.isHouseEnabled = false;
                        break;
                    case GameScenes.MARKET:
                        GameManager.Instance.isMarketEnabled = false;
                        break;
                    case GameScenes.POLICE_DEPARTMENT:
                        GameManager.Instance.isPoliceDepartmentEnabled = false;
                        break;
                    case GameScenes.BUILDINGS:
                        GameManager.Instance.isBuildingsEnabled = false;
                        break;
                    case GameScenes.HOSPITAL_MORGUE:
                        GameManager.Instance.isHospitalMorgueEnabled = false;
                        break;
                    case GameScenes.CORPORATION:
                        GameManager.Instance.isCorporationEnabled = false;
                        break;
                }
            }
        }

        if (q.disableQuestionIds.Length > 0)
        {
            for (int i = 0; i < q.disableQuestionIds.Length; i++)
            {
                GameManager.Instance.AddDisabledQuestion(q.disableQuestionIds[i]);
            }
        }

        GameManager.Instance.AddAnsweredQuestion(q.id);
        CameraShake.Instance.shakeDuration = q.cameraShakeDuration;
        CameraShake.Instance.shakeAmount = q.cameraShakeAmount;
    }

    private void LoadButtons()
    {
        for (int i = 0; i < questions.Length; i++)
        {
            if (GameManager.Instance.ValidateQuestion(questions[i]))
            {

                if (firstButtonQuestion == null)
                {
                    firstButtonQuestion = questions[i];
                    questionButtons[0].GetComponentInChildren<Button>().onClick.AddListener(() => { Answer(firstButtonQuestion); });
                    questionButtons[0].GetComponentInChildren<Text>().text = questions[i].questionText;
                    questionButtons[0].SetActive(true);
                }
                else if (secondButtonQuestion == null)
                {
                    secondButtonQuestion = questions[i];
                    questionButtons[1].GetComponentInChildren<Button>().onClick.AddListener(() => { Answer(secondButtonQuestion); });
                    questionButtons[1].GetComponentInChildren<Text>().text = questions[i].questionText;
                    questionButtons[1].SetActive(true);
                }
                else
                {
                    thirdButtonQuestion = questions[i];
                    questionButtons[2].GetComponentInChildren<Button>().onClick.AddListener(() => { Answer(thirdButtonQuestion); });
                    questionButtons[2].GetComponentInChildren<Text>().text = questions[i].questionText;
                    questionButtons[2].SetActive(true);
                }

            }
        }
    }

    private void ResetButtons()
    {
        for (int i = 0; i < questionButtons.Length; i++)
        {
            firstButtonQuestion = null;
            secondButtonQuestion = null;
            thirdButtonQuestion = null;
            questionButtons[i].SetActive(false);
            questionButtons[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            questionButtons[i].GetComponentInChildren<Text>().text = "";
        }
    }

    public void Next()
    {
        nextButton.SetActive(false);

        if(GameManager.Instance.isIntro) { 
            introTextIndex++;   
            if(introTextIndex >= introText.Length)
            {
                answerCharacterIndex = 0;
                partialAnswer.text = "";
                fullAnswer = "";
                GameManager.Instance.isIntro = false;
                GameManager.Instance.ReturnToMap();
            }
            else
            {
                answerCharacterIndex = 0;
                partialAnswer.text = "";
                fullAnswer = introText[introTextIndex];
            }
        }
        else
        {
            partialAnswer.text = "";
            currentState = GameScene_SM.INITIALIZING;
        }

    }

}
