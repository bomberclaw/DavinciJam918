using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public GameObject[] gameScenes;

    public GameObject mainMenu;
    public GameObject gameLoss;
    public GameObject gameWon;

    public GameObject goToMapButton;

    [HideInInspector]
    public bool hasItemOne = false;
    [HideInInspector]
    public bool hasItemTwo = false;
    [HideInInspector]
    public bool hasItemThree = false;
    [HideInInspector]
    public bool hasItemFour = false;
    [HideInInspector]
    public bool hasItemFive = false;
    [HideInInspector]
    public bool hasItemSix = false;
    [HideInInspector]
    public bool hasItemSeven = false;

    [HideInInspector]
    public bool isHouseEnabled = true;
    [HideInInspector]
    public bool isMarketEnabled = true;
    [HideInInspector]
    public bool isPoliceDepartmentEnabled = true;
    [HideInInspector]
    public bool isBuildingsEnabled = false;
    [HideInInspector]
    public bool isHospitalMorgueEnabled = false;
    [HideInInspector]
    public bool isCorporationEnabled = false;

    private List<int> questionsAsked = new List<int>();
    private List<int> questionsDisabled = new List<int>();

    [HideInInspector]
    public bool isIntro = true;


    private FMOD.Studio.EventInstance music;
    private FMOD.Studio.ParameterInstance whichMusic;

    private int musicState = 99;

    private FMOD.Studio.PLAYBACK_STATE eventStateChecker;

    private void Start()
    {
        music.getPlaybackState(out eventStateChecker);
        if (eventStateChecker != FMOD.Studio.PLAYBACK_STATE.PLAYING && musicState != 0)
        {
            music = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
            music.getParameter("Music Type", out whichMusic);
            whichMusic.setValue(0f);
            musicState = 0;
            music.start();
        }
    }


    public void StartGameCicle()
    {
        if(gameScenes.Length > 0)
        {
            gameScenes[1].SetActive(true);
        }
    }

    public void ReturnToMainMenu()
    {
        for (int i = 0; i < gameScenes.Length; i++)
        {
            gameScenes[i].SetActive(false);
        }

        hasItemOne = false;
        hasItemTwo = false;
        hasItemThree = false;
        hasItemFour = false;
        hasItemFive = false;
        hasItemSix = false;
        hasItemSeven = false;

        isHouseEnabled = true;
        isMarketEnabled = true;
        isPoliceDepartmentEnabled = true;
        isBuildingsEnabled = false;
        isHospitalMorgueEnabled = false;
        isCorporationEnabled = false;

        questionsAsked.Clear();
        questionsDisabled.Clear();

        isIntro = true;

        mainMenu.GetComponent<CanvasGroup>().alpha = 1;

        gameLoss.SetActive(false);
        gameWon.SetActive(false);
        mainMenu.SetActive(true);

        SceneChanged(GameScenes.MENU);
    }

    public void ReturnToMap()
    {
        EnableGoToMapButton(false);

        for (int i = 0; i < gameScenes.Length; i++)
        {
            gameScenes[i].SetActive(false);
        }
        gameScenes[0].SetActive(true);

        SceneChanged(GameScenes.MAP);
    }

    public void GoToWonScreen()
    {
        EnableGoToMapButton(false);

        for (int i = 0; i < gameScenes.Length; i++)
        {
            gameScenes[i].SetActive(false);
        }

        gameLoss.SetActive(false);
        mainMenu.SetActive(false);
        gameWon.SetActive(true);

        SceneChanged(GameScenes.WON);
    }

    public void GoToLossScreen()
    {
        EnableGoToMapButton(false);

        for (int i = 0; i < gameScenes.Length; i++)
        {
            gameScenes[i].SetActive(false);
        }

        mainMenu.SetActive(false);
        gameWon.SetActive(false);
        gameLoss.SetActive(true);

        SceneChanged(GameScenes.LOSS);
    }

    public void EnableGoToMapButton(bool state)
    {
        goToMapButton.SetActive(state);
    }

    public void AddAnsweredQuestion(int questionId)
    {
        if(!questionsAsked.Contains(questionId))
            questionsAsked.Add(questionId);
    }

    public void AddDisabledQuestion(int questionId)
    {
        if (!questionsDisabled.Contains(questionId))
            questionsDisabled.Add(questionId);
    }

    public List<int> GetAnsweredQuestion()
    {
        return questionsAsked;
    }

    public bool ValidateQuestion(Question q)
    {

        if (questionsAsked.Contains(q.id))
            return false;

        if (q.requiredItem.Length > 0)
        {
            for (int i = 0; i < q.requiredItem.Length; i++)
            {
                switch (q.requiredItem[i])
                {
                    case Items.None:
                        break;
                    case Items.ItemOne:
                        if (!hasItemOne)
                            return false;
                        break;
                    case Items.ItemTwo:
                        if (!hasItemTwo)
                            return false;
                        break;
                    case Items.ItemThree:
                        if (!hasItemThree)
                            return false;
                        break;
                    case Items.ItemFour:
                        if (!hasItemFour)
                            return false;
                        break;
                    case Items.ItemFive:
                        if (!hasItemFive)
                            return false;
                        break;
                    case Items.ItemSix:
                        if (!hasItemSix)
                            return false;
                        break;
                    case Items.ItemSeven:
                        if (!hasItemSeven)
                            return false;
                        break;
                }
            }
        }

        if (q.requiredQuestionIds.Length > 0)
        {
            bool founded = false;

            for (int i = 0; i < q.requiredQuestionIds.Length; i++)
            {
                if (!questionsAsked.Contains(q.requiredQuestionIds[i]))
                    founded = true;
            }

            if (!founded)
                return false;
        }

        if (questionsDisabled.Count > 0)
        {
            for (int i = 0; i < questionsDisabled.Count; i++)
            {
                if (questionsDisabled.Contains(q.id))
                    return false;
            }
        }

        return true;
    }

    public void SceneChanged(GameScenes newScene)
    {
        switch (newScene)
        {
            case GameScenes.HOUSE:
                if (musicState != 0)
                {
                    musicState = 0;
                    whichMusic.setValue(0f);
                }
                Debug.Log("You are in your House");
                break;
            case GameScenes.MARKET:
                if (musicState != 1)
                {
                    musicState = 1;
                    whichMusic.setValue(1f);
                }
                Debug.Log("You are in the Market");
                break;
            case GameScenes.POLICE_DEPARTMENT:
                if (musicState != 2)
                {
                    musicState = 2;
                    whichMusic.setValue(2f);
                }
                Debug.Log("You are in the Police Department");
                break;
            case GameScenes.BUILDINGS:
                if (musicState != 3)
                {
                    musicState = 3;
                    whichMusic.setValue(3f);
                }
                Debug.Log("You are in the Buildings Complex");
                break;
            case GameScenes.HOSPITAL_MORGUE:
                if (musicState != 4)
                {
                    musicState = 4;
                    whichMusic.setValue(4f);
                }
                Debug.Log("You are in the Hospital");
                break;
            case GameScenes.CORPORATION:
                if (musicState != 5)
                {
                    musicState = 5;
                    whichMusic.setValue(5f);
                }
                Debug.Log("You are in the Corporation");
                break;
            case GameScenes.MAP:
                if (musicState != 0)
                {
                    musicState = 0;
                    whichMusic.setValue(0f);
                }
                Debug.Log("You are in the Map");
                break;
            case GameScenes.MENU:
                if (musicState != 0)
                {
                    musicState = 0;
                    whichMusic.setValue(0f);
                }
                Debug.Log("You are in the Menu");
                break;
            case GameScenes.WON:
                if (musicState != 0)
                {
                    musicState = 0;
                    whichMusic.setValue(0f);
                }
                Debug.Log("You Won");
                break;
            case GameScenes.LOSS:
                if (musicState != 0)
                {
                    musicState = 0;
                    whichMusic.setValue(0f);
                }
                Debug.Log("You Loss");
                break;
        }
    }

}
