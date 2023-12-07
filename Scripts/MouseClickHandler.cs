using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class MouseClickHandler : MonoBehaviour
{
    FarmManager fm;

    SpriteRenderer mouseIcon;
    public bool isMouseHold = false;

    private void Start() {
        fm = FindObjectOfType<FarmManager>();
        mouseIcon = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    private void Update() {
        if (fm.isPlanting || fm.isSelecting) {
            if (Input.GetMouseButtonDown(0)) {
                isMouseHold = true;
            }

            if (Input.GetMouseButtonUp(0)) {
                isMouseHold = false;
            }
        }

        if (fm.isPlanting) {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mouseIcon.sprite = fm.selectPlant.plant.icon;
            mouseIcon.gameObject.SetActive(true);
            mouseIcon.gameObject.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
        } else {
            mouseIcon.gameObject.SetActive(false);
        }
    }
}
