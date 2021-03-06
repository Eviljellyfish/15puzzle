using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public delegate void SomeAction();

    public static event SomeAction OnTileMoved;

    public static event SomeAction OnWonCondition;

    public void Start() {
        InitiateBoard();

        //Debug.Log(availableNumbers[0]);

        _camera = Camera.main;
        ResetBoard();
    }

    public void InitiateBoard() {
        board = new int[4,4];
        availableNumbers = new List<int>();
        Tiles = new List<Tile>();

        FillListWithAvailableNumbers(availableNumbers);
    }

    public void FillListWithAvailableNumbers(List<int> list) {
        list.Clear();
        for (int i=0; i<board.Length; i++) {
            list.Add(i);
        }
    }

    public void ResetBoard() {
        removeTiles();
        InitiateBoard();
        initiateShuffledBoard(board);
        if (!CheckSolvability(board)) {
            ResetBoard();
        }
    }

    public void OnTileMove() {
        //Debug.Log("Caught Tile move.");
        OnTileMoved?.Invoke();
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
            OnWonCondition?.Invoke();
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

    public bool CheckSolvability(int[,] board) {
        int sum = 0;
        var array = board.Cast<int>().ToArray();
        for (int i=0; i<array.Length; i++) {
        //     Debug.Log(" index="+i+" row="+(i/board.GetLength(0)+1+" number="+array[i]));
            if (array[i]==0) {
                sum += i/board.GetLength(0)+1;
                continue;
            }
            for(int j=i;  j<array.Length; j++) {
                if (array[j]<array[i] && array[j]!=0) {
                    sum++;
                }
            }
        }
        Debug.Log(sum);
        return (sum%2==0)? true : false;
    }

    public void initiateShuffledBoard(int[,] board) {
        int pos = 0;
        for (int i=0; i<board.GetLength(0); i++) {
            for (int j=0; j<board.GetLength(1); j++) {
                int index = availableNumbers[Random.Range(0, availableNumbers.Count)];
                //Debug.Log(pos);
                foreach(var record in availableNumbers) {
                    //Debug.Log("availableNumbers"+record);
                }
                availableNumbers.Remove(index);
                board[i, j] = index;
                //Debug.Log("index="+index+" i="+i+" j="+j);
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
                //Debug.Log("index="+index+" i="+i+" j="+j);
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

    public void removeTiles() {
        foreach(var Tile in Tiles) {
            Destroy(Tile.gameObject);
        }
    }
}