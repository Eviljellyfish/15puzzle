using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Tiles;
    public GameObject TilePrefab;
    private Camera _camera; 
    public int[,] board;
    public float startX=-225, startY=225, shift=150;
    void Start()
    {
        board = new int[4,4];
        
        _camera = Camera.main;
        initiateBoard(board);
    }

    void Update()
    {

    }

    public void initiateBoard(int[,] board) {
        for (int i=0; i<board.GetLength(0); i++) {
            for (int j=0; j<board.GetLength(1); j++) {
                if (i*board.GetLength(0)+j == board.Length-1) {
                    board[i, j] = 0;
                    break;
                }
                int index = i*board.GetLength(0)+j;
                Debug.Log("index="+index+" i="+i+" j="+j);
                Tiles[index].GetComponent<Tile>().initialize(index+1, j, i);
                board[i, j] = index+1;
            }
        }
    }

    public GameObject generateTile(Vector3 pos) {
        GameObject go = Instantiate(TilePrefab, pos, new Quaternion());
        return go;
    }
}