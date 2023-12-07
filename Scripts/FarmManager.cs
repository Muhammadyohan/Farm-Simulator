using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FarmManager : MonoBehaviour {
    // public PlantItem selectPlant;
    public PlantInventory selectPlant;
    public bool isPlanting = false;
    public int money=100;
    public TextMeshProUGUI moneyTxt;
    public TextMeshProUGUI moneyTxt2;

    public Color buyColor = Color.green;
    public Color cancelColor = Color.red;

    public bool isSelecting = false;
    public int selectedTool = 0;

    public Image[] buttonsImg;
    public Sprite normalButton; 
    public Sprite selectedButton; 

    // Start is called before the first frame update
    void Start()
    {
        moneyTxt.text = "$" + money;
        moneyTxt2.text = "$" + money;
    }
    
    public void SelectPlant(PlantInventory newPlant)
    {
        if(selectPlant == newPlant) {
            DeselectAll();
        } else {
            DeselectAll();
            selectPlant = newPlant;
            selectPlant.btnImage.sprite = selectedButton;
            isPlanting = true;
        }
    }

    public void SelectTool(int toolNumber) {
        if (toolNumber == selectedTool) {    
            DeselectAll();
        } else {
            DeselectAll();
            isSelecting = true;
            selectedTool = toolNumber;
            buttonsImg[toolNumber-1].sprite = selectedButton;
        }
    }

    public void DeselectAll() {
        isPlanting = false;
        if (selectPlant != null) {
            selectPlant.btnImage.sprite = normalButton;
            selectPlant = null;
        }
        if (isSelecting) {
            if (selectedTool > 0) {
                buttonsImg[selectedTool-1].sprite = normalButton;
            }
            isSelecting = false;
            selectedTool = 0;
        }
    }

    public void Transaction(int value) {
        money += value;
        moneyTxt.text = "$" + money;
        moneyTxt2.text = "$" + money;
    }
}
