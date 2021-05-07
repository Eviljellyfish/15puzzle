using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<Tile> Tiles;
    public Tile TilePrefab;
    public GameObject boardCanvas;
    private Camera _camera; 
    public int[,] board;
    public float startX=-225, startY=225, shift=150;
    private List<int> availableNumbers;

    void Start() {
        board = new int[4,4];

        availableNumbers = new List<int>();
        Tiles = new List<Tile>();
        //

        for (int i=0; i<board.Length; i++) {
            availableNumbers.Add(i);
        }
        Debug.Log(availableNumbers[0]);
        _camera = Camera.main;
        initiateShuffledBoard(board);
    }

    void Update() {

    }

    public void OnTileMove() {
        Debug.Log("Caught Tile move.");
        if (checkWinCondition()) {
            for (int i=0; i<board.GetLength(0); i++) {
                for (int j=0; j<board.GetLength(1); j++) {
                    if (i==board.GetLength(0)-1 && j==board.GetLength(1)-1)
                        break;
                    int index = i*board.GetLength(0)+j;
                    Tiles[index].GetComponent<Image>().color = Color.green;
                }
            }
            Debug.Log("You Won!!!");
        }
    }

    public bool checkWinCondition() {
        for (int i=0; i<board.GetLength(0); i++) {
            for (int j=0; j<board.GetLength(1); j++) {
                if (i==board.GetLength(0)-1 && j==board.GetLength(1)-1)
                        break;
                int index = i*board.GetLength(0)+j;
                if (board[i, j] != (index+1)) {
                    return false;
                }
            }
        }
        return true;
    }

    public void initiateShuffledBoard(int[,] board) {
        int pos = 0;
        for (int i=0; i<board.GetLength(0); i++) {
            for (int j=0; j<board.GetLength(1); j++) {
                int index = availableNumbers[Random.Range(0, availableNumbers.Count)];
                Debug.Log(pos);
                foreach(var record in availableNumbers) {
                    Debug.Log("availableNumbers"+record);
                }
                availableNumbers.Remove(index);
                board[i, j] = index;
                Debug.Log("index="+index+" i="+i+" j="+j);
                if (index == 0)
                    continue;
                Tiles.Add(generateTile(index, i, j));
                pos++;
            }
        }
    }

    public void initiateBoardWithCompleateOrder(int[,] board) {
        for (int i=0; i<board.GetLength(0); i++) {
            for (int j=0; j<board.GetLength(1); j++) {
                if (i*board.GetLength(0)+j == board.Length-1) {
                    board[i, j] = 0;
                    break;
                }
                int index = i*board.GetLength(0)+j;
                Debug.Log("index="+index+" i="+i+" j="+j);
                Tiles.Add(generateTile(index+1, i, j));
                board[i, j] = index+1;
                
                //Tiles[index].moveAction += OnTileMove;
            }
        }
    }

    public Tile generateTile(int index, int i, int j) {
        Tile go = Instantiate(TilePrefab);
        go.transform.SetParent(boardCanvas.transform);
        go.name = "Square ("+index+")";
        go.transform.localPosition = new Vector2(startX+shift*j, startY-shift*i);
        go.GetComponent<Tile>().initialize(index, j, i);
        go.moveAction += OnTileMove;
        return go;
    }

    public int getNumberFromAvailable(List<int> an) {
        int randomNumber = Random.Range(0, an.Count);
        an.Remove(randomNumber);
        return randomNumber;
    }
}