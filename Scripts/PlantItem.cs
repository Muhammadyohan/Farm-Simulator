using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantItem : MonoBehaviour
{
    public PlantObject plant;
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI priceTxt;
    public TextMeshProUGUI amountTxt;
    string amountTxtChecker;
    public Image icon;
    public Image btnImage;
    public TextMeshProUGUI btnTxt;

    FarmManager fm;

    // Start is called before the first frame update
    void Start() {
        fm = FindObjectOfType<FarmManager>();
        InitializeUI();
    }

    void Update() {
        if (amountTxtChecker != plant.existed.ToString()) {
            amountTxtChecker = plant.existed.ToString();
            amountTxt.text = "Existed: " + amountTxtChecker;
        }
    }

    public void BuyPlant() {
        if (fm.money >= plant.buyPrice) {
            fm.Transaction(-plant.buyPrice);
            plant.existed += 1;
        }
    }

    void InitializeUI() {
        nameTxt.text = plant.plantName;
        priceTxt.text = "$" + plant.buyPrice;
        icon.sprite = plant.icon;
    }

}
