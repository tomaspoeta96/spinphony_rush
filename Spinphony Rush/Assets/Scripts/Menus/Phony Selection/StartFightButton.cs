using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartFightButton : MonoBehaviour
{
    public Button readyButton1, readyButton2, readyButton3, readyButton4;
    public GameObject tarjeta1, tarjeta2, tarjeta3, tarjeta4;
    public Button startFightButton;
    public GameObject fightConfiguration;
    private bool canStart;

    // Start is called before the first frame update
    void Start()
    {
        tarjeta1.active = true;
        canStart = false;
        startFightButton.onClick.AddListener(openFightConfig);
    }

    // Update is called once per frame
    void Update()
    {
        readyCheck();
        if (canStart) startFightButton.interactable = true;
        else startFightButton.interactable = false;
    }

    private void openFightConfig()
    {
        fightConfiguration.SetActive(true);
    }

    private void readyCheck()
    {
        canStart = false;
        if ((tarjeta1.active == false || readyButton1.interactable == false)
           && (tarjeta2.active == false || readyButton2.interactable == false)
           && (tarjeta3.active == false || readyButton3.interactable == false)
           && (tarjeta4.active == false || readyButton4.interactable == false)
           && (readyButton2.interactable == false
           || readyButton3.interactable == false
           || readyButton4.interactable == false))
        {
            canStart = true;
        }
    }

    public int numPlayers()
    {
        List<GameObject> players = new List<GameObject>();
        if (tarjeta1.active == true) players.Add(tarjeta1);
        if (tarjeta2.active == true) players.Add(tarjeta2);
        if (tarjeta3.active == true) players.Add(tarjeta3);
        if (tarjeta4.active == true) players.Add(tarjeta4);
        return players.Count;
    }

}
