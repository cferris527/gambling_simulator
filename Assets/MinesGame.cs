using System;
using System.Collections.Generic;
using MathNet.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MinesGame : MonoBehaviour
{
    public Material greenMaterial, transparent, boxCover;
    public Button mine1Btn, mine3Btn, mine5Btn, gameStartBtn, cashOutBtn, BetHalfBtn, BetTwiceBtn, BetMaxBtn;
    public TextMeshProUGUI numMines, balanceAmount, betAmount;

    private bool gameInProgress, redSpaceRevealed;
    private double balance, bet, amountToWin; 
    private double probWin;
    private int tilesCleared, minesOnBoard;

    // Start is called before the first frame update
    void Start()
    {
        setMines(1);
        gameInProgress = false;
        redSpaceRevealed = false;
        cashOutBtn.gameObject.SetActive(false);
        balance = 5.00;
        bet = 0.01;
        amountToWin = 0;

        probWin = 0;
        tilesCleared = 0;
        minesOnBoard = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            revealMines(true);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            revealMines(false);
        }

        if (balance == 0 && !gameInProgress)
        {
            SceneManager.LoadScene("Game Over");
        }
    }

    public bool isGameInProgress()
    {
        return gameInProgress;
    }

    public bool isRedSpaceRevealed()
    {
        return redSpaceRevealed;
    }

    public void setRedSpaceRevealed(bool b)
    {
        redSpaceRevealed = b;
    }

    // Places a specified number of mines on the board
    public void setMines(int amount)
    {
        if (gameInProgress) { return; }
        minesOnBoard = amount;
        generateMines();
        setNumMinesText();
    }

    public void increaseTilesCleared()
    {
        tilesCleared++;
    }

    public void betHalf()
    {
        if (gameInProgress) { return; }
        double newAmt = Math.Round((bet / 2),2);
        if (newAmt < 0.01) {return;}

        bet = newAmt;
        betAmount.text = "" + bet + "";
    }

    public void betDouble()
    {
        if (gameInProgress) { return; }
        double newAmt = Math.Round((bet * 2),2);
        if (newAmt > balance) {return;}

        bet = newAmt;
        betAmount.text = "" + bet + "";
    }

    public void betAll()
    {
        if (gameInProgress) { return; }
        bet = balance;
        betAmount.text = "" + bet + "";
    }

    void setCashOutText()
    {
        TextMeshProUGUI cashOutText = cashOutBtn.GetComponentInChildren<TextMeshProUGUI>();

        if (cashOutText != null)
        {
            cashOutText.text = "Cash Out: $" + amountToWin;
        }
    }

    void setBetText()
    {
        if (betAmount != null)
        {
            betAmount.text = "" + bet;
        }
    }

    void setBalanceText()
    {
        if (balanceAmount != null)
        {
            balanceAmount.text = "" + Math.Round((balance), 2);
        }
    }

    void setNumMinesText()
    {
        if (numMines != null)
        {
            numMines.text = "" + minesOnBoard;
        }
    }

    public void startGame()
    {
        gameInProgress = true;
        cashOutBtn.gameObject.SetActive(true);
        gameStartBtn.gameObject.SetActive(false);

        tilesCleared = 0;
        amountToWin = bet;
        balance -= bet;

        setCashOutText();
        setBetText();
        setBalanceText();
    }

    public void endGame()
    {
        gameInProgress = false;
        redSpaceRevealed = false;
        cashOutBtn.gameObject.SetActive(false);
        gameStartBtn.gameObject.SetActive(true);
        generateMines();

        balance = Math.Round((amountToWin + balance), 2);
        verifyBet();
        setBetText();
        setBalanceText();
    }

    // Calculates the amount of money the player will win if they cash out now.
    // Formula is based on the odds of the player winning, amount of mines on the board, and amount of money bet.
    public void calcAmountToWin()
    {
        // if red space has been revealed, chance of winning is 0 as game is no longer winnable
        if (redSpaceRevealed)
        {
            amountToWin = 0;
        }
        else
        {
            // calculate probability of winning
            double frac_top =
            SpecialFunctions.Factorial(25 - minesOnBoard) /
            (SpecialFunctions.Factorial(tilesCleared) * SpecialFunctions.Factorial(25 - minesOnBoard - tilesCleared));

            double frac_bottom =
                SpecialFunctions.Factorial(25) /
                (SpecialFunctions.Factorial(tilesCleared) * SpecialFunctions.Factorial(25 - tilesCleared));

            // avoid negative numbers
            if (frac_top < 0) { frac_top = 0; }
            if (frac_bottom < 0) { frac_bottom = 0; }

            probWin = frac_top / frac_bottom;

            // calculate amount to win
            amountToWin = bet * (.97 * (1 / probWin));
        }

        setCashOutText();
    }

    // Verify if the most recent bet placed can be made again. If not, half it until the player can make the bet.
    void verifyBet()
    {
        while (bet > balance)
        {
            bet = Math.Round((bet/2), 2);
        }    
    }

    // Sets all spaces to non-red (green), and covers all spaces
    void initializeBoard()
    {
        //replace all covers
        CoverBehavior[] inactives;
        inactives = FindObjectsOfType<CoverBehavior>();
        foreach(CoverBehavior cov in inactives) 
        {
            if (cov != null) {
                cov.reActivate();
            }
            else {
                Debug.Log("Null object detected");
            }
        }

        // reset all contents to green
        GameObject[] contents;
        contents = GameObject.FindGameObjectsWithTag("Content");
        foreach(GameObject cont in contents) 
        {
            Renderer rend = cont.GetComponent<Renderer>();
            rend.material = greenMaterial;
        }
    }

    // Generates appropriate number of mines on the board.
    void generateMines()
    {
        initializeBoard();

        GameObject[] contents;
        contents = GameObject.FindGameObjectsWithTag("Content");
        // add all 25 green spaces to List
        List<GameObject> greenSpaces = new List<GameObject>();
        foreach (GameObject obj in contents)
        {
            greenSpaces.Add(obj);
        }

        int ind;
        GameObject cont;
        System.Random r = new System.Random();

        for (int i = 0; i < minesOnBoard; ++i)
        {
            ind = r.Next(0, greenSpaces.Count - 1);
            cont = greenSpaces[ind];

            Renderer mineRenderer = cont.GetComponent<Renderer>();
            mineRenderer.material.color = Color.red;
            greenSpaces.RemoveAt(ind);
            greenSpaces.TrimExcess();
        }
    }

    // Reveals mines (cheating). Used for de-bugging purposes.
    void revealMines(bool cond)
    {
        Material setMaterial;
        if (cond)
        {
            setMaterial = transparent;
        }
        else
        {
            setMaterial = boxCover;
        }

        GameObject[] covers;
        covers = GameObject.FindGameObjectsWithTag("Cover");

        foreach (GameObject obj in covers)
        {
            Renderer rend = obj.GetComponent<Renderer>();
            rend.material = setMaterial;
        }
    }
}
