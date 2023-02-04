using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentPlayerControl : MonoBehaviour
{
    private GameObject[] PlayersArray;
    public List<GameObject> Players;
    private int index = 1;

    public TMP_Text PlayerCount;
    void Awake()       //Player tag'ına sahip tüm objelerin bir listede tutulması ve liste elemanlarının sırasıyla kontrol edilip izimlerinin güncellenmesi
    {
        PlayersArray = GameObject.FindGameObjectsWithTag("Player");
        Players = new List<GameObject>(PlayersArray);
        
        foreach (var plyr in PlayersArray)
        {
            plyr.gameObject.name = "Player " + index;
            index++;
        }
    }
    void Update()   //oyuncuların değiştirilen isimlerinin üzerlerinde bulunan textmeshpro nesnesine atanması
    {
        PlayerCount.text = Players.Count.ToString();
    }
}
