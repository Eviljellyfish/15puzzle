using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    private int index, posX, posY;
    public void initialize(int index, int posX, int posY) {
        this.index = index;
        this.posX = posX;
        this.posY = posY;
    }

    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log(this.name+" index="+index+" x="+posX+" y="+posY);
    }
}
