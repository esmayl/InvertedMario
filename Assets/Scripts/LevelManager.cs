using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    GameObject[] enemies;
    GameObject player;

    Vector3 playerSpawnPoint;
    Vector3[] enemySpawnPoints;

    void Awake()
    {
        if (instance == null)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemySpawnPoints = new Vector3[enemies.Length];
            
            int i = 0;

            foreach (GameObject enemy in enemies)
            {
                enemySpawnPoints[i] = enemy.transform.position;
                i++;
            }

            player = GameObject.FindGameObjectWithTag("Player");
            playerSpawnPoint = player.transform.position;

            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

    }

    public void ReloadLevel()
    {
        //int i = 0;
        //foreach (GameObject enemy in enemies)
        //{
        //    enemy.transform.position = enemySpawnPoints[i];
        //    enemy.SetActive(true);
        //    i++;
        //}

        //player.transform.position = playerSpawnPoint;
        //player.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
