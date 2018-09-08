using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Items
{
    None,
    ItemOne,
    ItemTwo,
    ItemThree,
    ItemFour,
    ItemFive,
    ItemSix,
    ItemSeven
}

[Serializable]
public class Question {

    public int id = 0;
    public string questionText = "";
    public string answerText = "";

    public Items[] requiredItem;
    public int[] requiredQuestionIds;
    public int[] disableQuestionIds;

    public GameScenes[] enableScenes;
    public GameScenes[] disableScenes;

    public Items itemToUnlock;

    public bool disableGoToMap;

}
