using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : MonoBehaviour
{

    public bool isBought = true;
    bool isPlanted = false;
    bool isCoin = false;
    bool isDry = true;

    SpriteRenderer plant;
    SpriteRenderer plot;
    SpriteRenderer coin;
    public Sprite drySprite;
    public Sprite normalSprite;
    public Sprite unavailableSprite;

    BoxCollider2D plantCollider;

    int plantStage = 0;

    public Color availableColor = Color.green;
    public Color unavailableColor = Color.red;

    PlantObject selectedPlant;
    FarmManager fm;
    MouseClickHandler mch;


    float timer;
    float speed = 1f;
    float coinTimer;

    void Start() {
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        fm = transform.parent.GetComponent<FarmManager>();
        mch = FindObjectOfType<MouseClickHandler>();
        plot = GetComponent<SpriteRenderer>();
        coin = transform.GetChild(1).GetComponent<SpriteRenderer>();
        if (isBought) {
            plot.sprite = drySprite;
        } else {
            plot.sprite = unavailableSprite;
        }
    }

    void Update() {
        if (isPlanted && !isDry) {
            timer -= speed*Time.deltaTime;

            if (timer < 0 && plantStage < selectedPlant.plantStages.Length - 1) {
                timer = selectedPlant.timeBtwStages;
                plantStage++;
                UpdatePlant();
            }
        }

        if (isCoin) {
            coinTimer -= Time.deltaTime;

            if (coinTimer < 0) {
                CoinCollect();
            }
        }
    }

    public void OnMouseDown() {
        if (!isPlanted && fm.isPlanting && isBought && fm.selectPlant.plant.existed > 0) {
            Plant(fm.selectPlant.plant);
        }
        if (fm.isSelecting) {
            switch(fm.selectedTool) {
                case 1:
                    if (isBought) {
                        isDry = false;
                        plot.sprite = normalSprite;
                        if (isPlanted) UpdatePlant();
                    }
                    break;
                case 2:
                    if (fm.money >= 10 && isBought) {
                        fm.Transaction(-10);
                        if (speed < 10) speed += .2f;
                    }
                    break;
                case 3:
                    if (fm.money >= 100 && !isBought) {
                        fm.Transaction(-100);
                        isBought = true;
                        plot.sprite = drySprite;
                    }
                    break;
                case 4:
                    if (isPlanted && plantStage == selectedPlant.plantStages.Length - 1)
                        Harvest();
                    break;
                default:
                    break;
            }
        }

        if (isCoin && !fm.isSelecting) CoinCollect();
    }

    private void OnMouseOver()
    {
        if (fm.isPlanting) {
            if (isPlanted || !isBought || fm.selectPlant.plant.existed == 0) {
                plot.color = unavailableColor;
            } else {
                plot.color = availableColor;
                if (mch.isMouseHold && fm.selectPlant.plant.existed > 0 && !isPlanted) {
                    Plant(fm.selectPlant.plant);
                    plot.color = unavailableColor;
                }
            }
        }
        if (fm.isSelecting) {
            switch (fm.selectedTool) {
                case 1:
                    if (isBought) {
                        plot.color = availableColor;
                        if (mch.isMouseHold) {
                            isDry = false;
                            plot.sprite = normalSprite;
                            if (isPlanted) UpdatePlant();
                        }
                    } else {
                        plot.color = unavailableColor;
                    }
                    break;
                case 2:
                    if (isBought && fm.money >= 10) {
                        plot.color = availableColor;
                    } else {
                        plot.color = unavailableColor;
                    }
                    break;
                case 3:
                    if (!isBought && fm.money >= 100) {
                        plot.color = availableColor;
                    } else {
                        plot.color = unavailableColor;
                    }
                    break;
                case 4:
                    if (isPlanted && plantStage == selectedPlant.plantStages.Length - 1) {
                        plot.color = availableColor;
                        if (mch.isMouseHold) {
                            Harvest();
                            plot.color = unavailableColor;
                        }
                    } else {
                        plot.color = unavailableColor;
                    }
                    break;
                default:
                    plot.color = unavailableColor;
                    break;
            }
        }

        if (isCoin && !fm.isSelecting && mch.isMouseHold) CoinCollect();
    }

    void Harvest() {
        isPlanted = false;
        plant.gameObject.SetActive(false);
        isDry = true;
        plot.sprite = drySprite;
        speed = 1f;
        coin.gameObject.SetActive(true);
        coinTimer = 5f;
        isCoin = true;
    }

    void Plant(PlantObject newPlant) {
        if (isCoin) CoinCollect();
        selectedPlant = newPlant;
        isPlanted = true;

        fm.selectPlant.plant.existed -= 1;
        fm.selectPlant.amountTxt.text = fm.selectPlant.plant.existed.ToString();

        plantStage = 0;
        UpdatePlant();
        timer = selectedPlant.timeBtwStages;
        coin.gameObject.SetActive(false);
        plant.gameObject.SetActive(true);
    }

    void UpdatePlant() {
        if (isDry) {
            plant.sprite = selectedPlant.dryPlanted;
        } else {
            plant.sprite = selectedPlant.plantStages[plantStage];
        }
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0,plant.bounds.size.y/2);
    }

    void CoinCollect() {
        isCoin = false;
        coin.gameObject.SetActive(false);
        fm.Transaction(selectedPlant.sellPrice);
    }
    
    private void OnMouseExit()
    {
        plot.color = Color.white;
    }
}
