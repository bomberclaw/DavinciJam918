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
    CORPORATION
}

public class Map : MonoBehaviour {

    public GameObject house;
    public GameObject market;
    public GameObject policeDepartment;
    public GameObject buildings;
    public GameObject hospiotalMorgue;
    public GameObject corporation;

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
    }

    public void GoToLocation(GameObject location)
    {
        location.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
