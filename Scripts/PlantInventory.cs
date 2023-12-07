using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantInventory : MonoBehaviour
{
    public PlantObject plant;
    public TextMeshProUGUI amountTxt;
    public Image icon;
    public Image btnImage;
    FarmManager fm;
    void Start() {
        fm = FindObjectOfType<FarmManager>();
        InitializeUI();
    }

    void Update() {
        if (amountTxt.text != plant.existed.ToString()) {
            amountTxt.text = plant.existed.ToString();
        }
    }

    void InitializeUI() {
        amountTxt.text = plant.existed.ToString();
        icon.sprite = plant.icon;
    }

    public void SelectButton() {
        fm.SelectPlant(this);
    }
}
