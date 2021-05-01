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
        GameManager gm = Object.FindObjectOfType<GameManager>();
        Vector2 zeroPos = findZero(gm.board);
        Vector2 pos = new Vector2(posX, posY);
        Vector2 diff = zeroPos - pos;
        if (((zeroPos - pos) == new Vector2(-1, 0)) || ((zeroPos - pos) == new Vector2(1, 0)) || ((zeroPos - pos) == new Vector2(0, -1)) || ((zeroPos - pos) == new Vector2(0, +1))) {
            Debug.Log("Can swap!");
            gm.board[(int)zeroPos.y, (int)zeroPos.x] = index;
            gm.board[posX, posY] = 0;
            posX = (int)zeroPos.y;
            posY = (int)zeroPos.x;
            redraw(diff*150);
        }
        else {
            Debug.Log("NO!");
        }
    }

    public void swapPos(int x, int y, int xx, int yy) {
        int tmp = x;
        x = xx;
        xx = x;
        tmp = y;
        y = yy;
        yy = tmp;
    }

    public void redraw(Vector2 diff) {
        // Debug.Log(new Vector3(-225+150*posX, 225-150*posY, 0));
        transform.Translate(diff);
    }

    public Vector2 findZero(int[,] board) {
        for (int i=0; i<board.GetLength(0); i++) {
            for (int j=0; j<board.GetLength(1); j++) {
                if (board[i, j]==0)
                    return new Vector2(i, j);
            }
        }
        return new Vector2(-1, -1);
    }
}
