using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class Rules : MonoBehaviour
{
    public GameObject deathScreen;
    public int wins;
    public int coins;
    public Action onStartOfBattle;
    public DealerMudak dm;
    public Enemy enemy;
    public SpriteRenderer pool;
    private void Start()
    {
        enemy.onKill += Inc;
        coins = PlayerPrefs.GetInt("Money");
    }
    public void Inc()
    {
        wins++;
    }
    public void BattleStart()
    {
        onStartOfBattle?.Invoke();
        for (int i = 0; i < 6; i++)
        {
            dm.DealCard();
        }
    }
    public void OnYourDeath()
    {
        deathScreen.SetActive(true);
        Time.timeScale = 0;
        var i = PlayerPrefs.GetInt("Points");
        PlayerPrefs.SetInt("Points", wins+i);
        PlayerPrefs.Save();
    }
    public void DeathButton()
    {
        SceneManager.LoadScene(0);
    }
}
