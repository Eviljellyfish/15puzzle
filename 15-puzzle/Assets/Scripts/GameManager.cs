﻿using System.Collections;
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
        
        _camera = Camera.main;
        initiateBoard(board);
    }

    void Update()
    {

    }

    public void initiateBoard(int[,] board) {
        for (int i=0; i<board.GetLength(0); i++) {
            for (int j=0; j<board.GetLength(1); j++) {
                if (i*board.GetLength(0)+j == board.Length-1)
                    break;
                int index = i*board.GetLength(0)+j;
                Debug.Log("index="+index+"i="+i+" j="+j);
                Tiles[index].GetComponent<Tile>().initialize(index+1, j, i);
            }
        }
    }
}