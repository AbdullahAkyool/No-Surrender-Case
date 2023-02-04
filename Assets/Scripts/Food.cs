using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public bool isCollect = false;
    private SpawnSystem spawnSystem;
    public void Collect()      //Food nesnesinin oyuncu ile etkileşime girmesi durumunda çalışacak olan metod
    {
        if (isCollect == false)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;     //mehrenderer ve collider özelliği kapatılır ve nesne alınmış olarak gözükerek hedef olmaktan çıkar
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            isCollect = true;
            StartCoroutine(OpenFood());
        }
    }

    IEnumerator OpenFood()      //Collect olan Food nesnesinin belirlenen sürenin ardından tekrar etkileşime hazır hale gelmesi
    {
        yield return new WaitForSeconds(5f);
        isCollect = false;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
