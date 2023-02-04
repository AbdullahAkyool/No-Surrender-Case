using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [SerializeField] private float chaseSpeed = 5f;

    private void Start()       //oyunun başlangıcında kamera target'ı olarak PlayerController scriptine sahip objenin atanması
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    private void LateUpdate()   //kamera pozisyonunun karakter pozisyonuna Lerp metodu ile ilerlemesi
    { 
        transform.position = Vector3.Lerp(transform.position, target.position + offset, chaseSpeed * Time.deltaTime);   
    }
}