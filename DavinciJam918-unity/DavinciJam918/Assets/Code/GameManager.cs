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
    public bool isHouseEnabled = false;
    [HideInInspector]
    public bool isMarketEnabled = false;
    [HideInInspector]
    public bool isPoliceDepartmentEnabled = false;
    [HideInInspector]
    public bool isBuildingsEnabled = false;
    [HideInInspector]
    public bool isHospitalMorgueEnabled = false;
    [HideInInspector]
    public bool isCorporationEnabled = false;

    private List<int> questionsAsked = new List<int>();

    [HideInInspector]
    public bool firstSceneException = true;

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
    }

    public void ReturnToMap()
    {
        EnableGoToMapButton(false);

        for (int i = 0; i < gameScenes.Length; i++)
        {
            gameScenes[i].SetActive(false);
        }
        gameScenes[0].SetActive(true);
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

        if (q.disableQuestionIds.Length > 0)
        {
            for (int i = 0; i < q.disableQuestionIds.Length; i++)
            {
                AddAnsweredQuestion(q.disableQuestionIds[i]);
            }
        }

        return true;
    }

}
