using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public GameObject[] gameScenes;

    public GameObject mainMenu;

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

    public void StartGameCicle()
    {
        if(gameScenes.Length > 0)
        {
            gameScenes[1].SetActive(true);
        }
    }

    void ReturnToMainMenu()
    {
        for (int i = 0; i < gameScenes.Length; i++)
        {
            gameScenes[i].SetActive(false);
        }

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
            for (int i = 0; i < q.requiredQuestionIds.Length; i++)
            {
                if (!questionsAsked.Contains(q.requiredQuestionIds[i]))
                    return false;
            }
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
                Debug.Log("You are in your House");
                break;
            case GameScenes.MARKET:
                Debug.Log("You are in the Market");
                break;
            case GameScenes.POLICE_DEPARTMENT:
                Debug.Log("You are in the Police Department");
                break;
            case GameScenes.BUILDINGS:
                Debug.Log("You are in the Buildings Complex");
                break;
            case GameScenes.HOSPITAL_MORGUE:
                Debug.Log("You are in the Hospital");
                break;
            case GameScenes.CORPORATION:
                Debug.Log("You are in the Corporation");
                break;
            case GameScenes.MAP:
                Debug.Log("You are in the Map");
                break;
            case GameScenes.MENU:
                Debug.Log("You are in the Menu");
                break;
        }
    }

}
