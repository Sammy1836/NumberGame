using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public int sessionScore = 0;

    public Text tryAgainText;

    private Cell selectedCell1;
    private Cell selectedCell2;

    //---------------

    public GameObject cellPrefab; // Assign your Cell prefab here
    public int rows = 9;
    public int columns = 9;
    public float cellSize = 1f; // Adjust as needed
    // public Text textPrefab;
    public int setTartgetSum;
    public Text targetSumText;

    Text textPrefab;

    public int[,] grid = new int[9, 9];

    private int[,] surr = { { 1, 0 }, { 0, 1 }, { 1, 1 } };

    public List<Cell> allCells = new List<Cell>();

    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {

        GenerateGrid();

        sessionScore = 0;
        UpdateScoreText();

        UpdateTargetSum();
    }

    void GenerateGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                GameObject cell = Instantiate(cellPrefab, transform);

                float xPos = col * cellSize;
                float yPos = row * cellSize;
                cell.transform.localPosition = new Vector3(xPos, yPos, 0f);

                int randomNumber = Random.Range(1, 10);
                grid[row, col] = randomNumber;

                Text newText = cell.transform.GetChild(1).GetChild(0).GetComponent<Text>();
                newText.text = randomNumber.ToString();
                newText.transform.localPosition = new Vector3(xPos, yPos, 0f);

            }
        }
    }

    void UpdateTargetSum()
    {
        int targetRow1 = Random.Range(0, 8);
        int targetCol1 = Random.Range(0, 8);

        int x = Random.Range(0, 2);
        int y;
        if (x == 1) { y = 0; }
        else { y = 1; }

        int targetRow2 = targetRow1 + x;
        int targetCol2 = targetCol1 + y;

        setTartgetSum = grid[targetRow1, targetCol1] + grid[targetRow2, targetCol2];

    }

    public void OnCellSelected(Cell cell)
    {
        if (selectedCell1 == null)
        {
            selectedCell1 = cell;
        }
        else if (selectedCell2 == null && cell != selectedCell1)
        {
            selectedCell2 = cell;
            CheckSelectedCells();
        }
    }

    void CheckSelectedCells()
    {
        int sum = selectedCell1.value + selectedCell2.value;
        if (sum == setTartgetSum)
        {
            sessionScore += 10;
            UpdateScoreText();
            ReplaceSelectedCells();
        }
        else
        {

            selectedCell1 = null;
            selectedCell2 = null;

            StartCoroutine(ShowTryAgainMessage());
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + sessionScore.ToString();
    }

    void ReplaceSelectedCells()
    {
        selectedCell1.SetNumber(Random.Range(1, 10));
        selectedCell2.SetNumber(Random.Range(1, 10));
        selectedCell1 = null;
        selectedCell2 = null;

        UpdateTargetSum();
    }

    IEnumerator ShowTryAgainMessage()
    {

        tryAgainText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        tryAgainText.gameObject.SetActive(false);
        print("Try Again!");
    }
}
