using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMPRotation : MonoBehaviour
{
    private Transform cam;
    private Transform _transform;
    void Start()
    {
        cam = Camera.main.transform;
        _transform = GetComponent<Transform>();
    }
    void LateUpdate()   //TMP objesinin rotasyonu kameraya doğru ayarlanır ve karakter ters yönde olsa dahi ismi düz okunur
    {
        _transform.rotation = cam.rotation;
    }
}
