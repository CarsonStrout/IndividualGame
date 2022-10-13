using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    Text text;
    [SerializeField] int totalCoin;
    public static int coinCheck;

    public static int coinAmount;

    void Start()
    {
        coinCheck = totalCoin;
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = coinAmount.ToString() + "/" + totalCoin.ToString();
    }
}
