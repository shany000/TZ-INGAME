using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float gameTime;

    public Text TextTimer;
    public Button RestartButton;

    public static bool isGame = false;

    IEnumerator Timer()
    {
        while(isGame)
        {
            yield return new WaitForSeconds(0.01f);
            gameTime += 0.01f;
            TextTimer.text = "" + Mathf.Round(gameTime);
        }
        RestartButton.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        isGame = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<EnemyController>().Spawn();
        StartCoroutine(Timer());
    }

    public static void GameOver()
    {
        isGame = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}