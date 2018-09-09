using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameScenes
{
    HOUSE,
    MARKET,
    POLICE_DEPARTMENT,
    BUILDINGS,
    HOSPITAL_MORGUE,
    CORPORATION,
    MAP,
    MENU,
    WON,
    LOSS
}

public class Map : MonoBehaviour {

    public GameObject house;
    public GameObject market;
    public GameObject policeDepartment;
    public GameObject buildings;
    public GameObject hospiotalMorgue;
    public GameObject corporation;

    public GameObject[] itemSlots;

    void OnEnable()
    {
        if (GameManager.Instance.isHouseEnabled)
            house.SetActive(true);
        else
            house.SetActive(false);

        if (GameManager.Instance.isMarketEnabled)
            market.SetActive(true);
        else
            market.SetActive(false);

        if (GameManager.Instance.isPoliceDepartmentEnabled)
            policeDepartment.SetActive(true);
        else
            policeDepartment.SetActive(false);

        if (GameManager.Instance.isBuildingsEnabled)
            buildings.SetActive(true);
        else
            buildings.SetActive(false);

        if (GameManager.Instance.isHospitalMorgueEnabled)
            hospiotalMorgue.SetActive(true);
        else
            hospiotalMorgue.SetActive(false);

        if (GameManager.Instance.isCorporationEnabled)
            corporation.SetActive(true);
        else
            corporation.SetActive(false);

        if (GameManager.Instance.hasItemOne)
            itemSlots[0].SetActive(true);
        else
            itemSlots[0].SetActive(false);

        if (GameManager.Instance.hasItemTwo)
            itemSlots[1].SetActive(true);
        else
            itemSlots[1].SetActive(false);

        if (GameManager.Instance.hasItemThree)
            itemSlots[2].SetActive(true);
        else
            itemSlots[2].SetActive(false);

        if (GameManager.Instance.hasItemFour)
            itemSlots[3].SetActive(true);
        else
            itemSlots[3].SetActive(false);

        if (GameManager.Instance.hasItemFive)
            itemSlots[4].SetActive(true);
        else
            itemSlots[4].SetActive(false);

        if (GameManager.Instance.hasItemSix)
            itemSlots[5].SetActive(true);
        else
            itemSlots[5].SetActive(false);

        if (GameManager.Instance.hasItemSeven)
            itemSlots[6].SetActive(true);
        else
            itemSlots[6].SetActive(false);
    }

    public void GoToLocation(GameObject location)
    {
        location.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
