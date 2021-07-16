using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool gameover;
    public GameObject gameoverpanel;
    public GameObject startingtext;

    public static bool isGameStarted;
    public static int numberofcoins;

    public Text coinstext;
    void Start()
    {
        gameover=false;
        Time.timeScale = 1;
        isGameStarted = false;
        numberofcoins = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameover){
            Time.timeScale = 0;
            gameoverpanel.SetActive(true);
        }
        coinstext.text = "Coins: " + numberofcoins;

        if (SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(startingtext);
        }
    }
}
