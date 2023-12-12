using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class MouseClickHandler : MonoBehaviour
{
    FarmManager fm;

    public bool isMouseHold = false;

    private void Start() {
        fm = FindObjectOfType<FarmManager>();
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
    }
}
