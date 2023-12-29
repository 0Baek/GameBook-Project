using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies; 
    private float[] arrPosX = { 3.28f, 3.1f, 3f};
    
    // Start is called before the first frame update
    void Start()
    {
       foreach (float posX in arrPosX)
        {
            int index = Random.Range(0, 3);
            SpawnEnemy(posX, index);
        }
       
    }

    void SpawnEnemy(float posX,int index)
    {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);
        Instantiate(enemies[index], spawnPos, Quaternion.identity);
    }
}
