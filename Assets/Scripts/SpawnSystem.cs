using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public float offset;
    public int horizontalCount;
    public int verticalCount;
    public GameObject prefab;
    public List<GameObject> allSpawns;

    void Start()
    {
        Spawn();
    }

    public void Spawn() //belirlenen aralıkta ve sayıda kare planda Food objesinin Spawn edilmesi
    {
        for (int z = 0; z < verticalCount; z++)
        {
            for (int x = 0; x < horizontalCount; x++)
            {
                Vector3 spawnPos = new Vector3(transform.position.x + x * offset, .5F,
                    transform.position.z + z * offset);
                GameObject food = Instantiate(prefab, spawnPos, Quaternion.identity);
                food.transform.parent = gameObject.transform;
                allSpawns.Add(food);
            }
        }
    }

    private void OnDrawGizmos()  //Scene ekranında Spawn pozisyonunun görülmesi için OnDrawGizmos metodunun kullanılması
    {
        for (int z = 0; z < verticalCount; z++)
        {
            for (int x = 0; x < horizontalCount; x++)
            {
                Vector3 spawnPos = new Vector3(transform.position.x + x * offset, .5F,
                    transform.position.z + z * offset);
                Gizmos.DrawCube(spawnPos, new Vector3(1f, .5F, .5f));
            }
        }
    }
}