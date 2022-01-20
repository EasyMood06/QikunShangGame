using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinNum : MonoBehaviour
{
    Player player;
    int coin_nums = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        coin_nums = player.GetCoinNum();
        gameObject.GetComponent<Text>().text = coin_nums.ToString();
    }
}
