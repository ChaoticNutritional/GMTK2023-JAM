using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEntering : MonoBehaviour, IMouseable
{
    // Start is called before the first frame update

    // reference the camera controller to check if isSelected is true;

        // Start is called before the first frame update

    // reference the camera controller to check if isSelected is true;

    // HOVER MATERIA

    private Material myMat;
    private Color originalColor;
    private bool touched = false;

    void Awake()
    {
        myMat = this.GetComponent<MeshRenderer>().material;
        originalColor = myMat.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        myMat.color = Color.green;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myMat.color = originalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Teehee");
    }

    public void MouseOver()
    {
        
    }

    public void MouseLeftClick()
    {
        
    }
}



