using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using TMPro;

public class Enemy : MonoBehaviour
{
    private GameObject[] Foods;

    private Food food;
    private CurrentPlayerControl currentPlayerControl;

    private Transform targetTransform;
    private float nearestDistance = 50f;
    private float punchPower = 10f;
    private Rigidbody rb;

    public bool hasTarget;
    public bool isGround;

    public TextMeshPro playerName;

    private void Start()
    {
        hasTarget = false;
        food = FindObjectOfType<Food>();
        rb = GetComponent<Rigidbody>();

        currentPlayerControl = FindObjectOfType<CurrentPlayerControl>();

        playerName.text = gameObject.name;
    }

    void Update()
    {
        SearchFood();
    }

    void SearchFood()       //tag'ı Food olan objeler bir diziye atılır ve dizi elemanları sırasıyla kontrol edildikten sonra ai karaktere en yakın olan Food objesi hedef alınır
    {
        Foods = GameObject.FindGameObjectsWithTag("Food");

        // if (hasTarget == false)
        // {
        //     GameObject targetFood = Foods[UnityEngine.Random.Range(0, Foods.Length)];
        //     Transform targetPos = targetFood.transform;
        //     Vector3 direction = (targetPos.position - transform.position).normalized;
        //     transform.LookAt(targetTransform);
        //     transform.position += direction * 5f * Time.deltaTime;
        //     hasTarget = true;
        // }


        foreach (var obj in Foods)
        {
            if (obj.GetComponent<Food>().isCollect == false)
            {
                float distance = Vector3.Distance(transform.position, obj.transform.position);

                if (distance < nearestDistance || hasTarget == false)
                {
                    nearestDistance = distance;
                    targetTransform = obj.transform;
                    Vector3 direction = (targetTransform.position - transform.position).normalized;
                    transform.LookAt(targetTransform);
                    transform.position += direction * 5f * Time.deltaTime;
                    hasTarget = true;

                    //nav.SetDestination(targetTransform.position);
                }
            }
        }
    }

    void SetScale()     //Food objesi ile etkileşimde ai karakterin scale'inin güncellenmesi
    {
        transform.DOScale(
            new Vector3(transform.localScale.x + .5f, transform.localScale.y + .5f, transform.localScale.z + .5f), .5f);
        if (transform.localScale.x >= 6)
        {
            transform.localScale = new Vector3(6, 6, 6);
        }

        punchPower += 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            if (other.gameObject.TryGetComponent<Food>(out Food food))
            {
                food.GetComponent<Food>().Collect();
                SetScale();
                hasTarget = false;
            }
        }

        if (other.gameObject.CompareTag("Player") && other.gameObject != gameObject)
        {
            if (transform.localScale.x > other.transform.localScale.x)
            {
                other.transform.DOMove(transform.position + transform.forward * punchPower, 1.5f);
                //collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 3f);
            }
            else if (transform.localScale.x < other.transform.localScale.x)
            {
                transform.DOMove(transform.position + transform.forward * -punchPower, 1.5f);
            }
            else if (transform.localScale.x == other.transform.localScale.x)
            {
                transform.DOMove(transform.position + transform.forward * -10f, 1.5f);
                other.transform.DOMove(transform.position + transform.forward * 10f, 1.5f);
            }
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void OnTriggerExit(Collider other)  //ai karakterin zemin ile etkileşimi kesilmesi durumunda gerçekleşecek fizik olayları ve oyuncu listesinin güncellenmesi
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = false;
            currentPlayerControl.Players.Remove(gameObject);
            GetComponent<Rigidbody>().useGravity = true;
            Destroy(gameObject, 1.5f);
        }
    }
}