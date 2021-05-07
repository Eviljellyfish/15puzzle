using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Tiles;
    public GameObject TilePrefab;
    public GameObject boardCanvas;
    private Camera _camera; 
    public int[,] board;
    public float startX=600, startY=600, shift=150;
    private List<int> availableNumbers;

    void Start() {
        board = new int[4,4];

        availableNumbers = new List<int>();
        Tiles = new List<GameObject>();
        for (int i=0; i<board.Length; i++) {
            availableNumbers.Add(i);
        }
        Debug.Log(availableNumbers[0]);
        _camera = Camera.main;
        initiateShuffledBoard(board);
    }

    void Update() {

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
                Tiles[index] = Instantiate(TilePrefab);
                Tiles[index].transform.SetParent(boardCanvas.transform);
                Tiles[index].name = "Square ("+(index+1)+")";
                Tiles[index].transform.localPosition = new Vector2(startX+shift*j, startY-shift*i);
                Tiles[index].GetComponent<Tile>().initialize(index+1, j, i);
                board[i, j] = index+1;
            }
        }
    }

    public GameObject generateTile(int index, int i, int j) {
        GameObject go = Instantiate(TilePrefab);
        go.transform.SetParent(boardCanvas.transform);
        go.name = "Square ("+index+")";
        go.transform.localPosition = new Vector2(startX+shift*j, startY-shift*i);
        go.GetComponent<Tile>().initialize(index, j, i);
        return go;
    }

    public int getNumberFromAvailable(List<int> an) {
        int randomNumber = Random.Range(0, an.Count);
        an.Remove(randomNumber);
        return randomNumber;
    }
}