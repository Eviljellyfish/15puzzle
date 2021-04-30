using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Tiles;
    private Camera _camera;
    private int[,] board;
    void Start()
    {
        board = new int[4,4];
        Debug.Log(board.Rank);
        _camera = Camera.main;
        initiateBoard(board);
    }

    void Update()
    {

    }

    public void initiateBoard(int[,] board) {
        for (int i=0; i<board.Length; i++) {
            for (int j=0; j<board.Length; j++) {
                int index = i+j*board.Length;
                Tiles[i+j].GetComponent<Tile>().initialize(index+1, i, j);
            }
        }
    }
}