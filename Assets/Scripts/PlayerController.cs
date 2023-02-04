using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Food food;

    public bool isGround;
    public bool isDeath = false;

    private float punchPower = 10f;

    private CurrentPlayerControl currentPlayerControl;
    private CameraFollow cameraFollow;
    
    public TMP_Text playerScore;


    private void Start()  
    {
        food = FindObjectOfType<Food>();
        rb = GetComponent<Rigidbody>();
        currentPlayerControl = FindObjectOfType<CurrentPlayerControl>();
        cameraFollow = FindObjectOfType<CameraFollow>();
    }

    void Update()
    {
        Move();
    }

    private void Move()  //karakter hareketi için velocity kullanıldı
    {
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity); //karakterin rotasyonunun velocity yönüne doğru güncellenmesi
        }
    }

    void SetScale()  //trigger'da kullanılmak üzere DoTween ile objenin scale'inin güncellenmesi
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
            }
        }

        if (other.gameObject.CompareTag("Player") && other.gameObject != gameObject)    //rakip objeler ile etkileşimde scale'e göre objelerin DoMove metodu ile hareketi
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

    private void OnTriggerExit(Collider other)      //karakterin zemin ile etkileşiminin kesilmesi sonucunda oyuncu listesinin güncellenmesi ve kamera hareketi 
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = false;
            currentPlayerControl.Players.Remove(gameObject);
            GetComponent<Rigidbody>().useGravity = true;
            Destroy(gameObject, 1.5f);
            isDeath = true;
            
            playerScore.text = "YOU ARE " + currentPlayerControl.Players.Count + " !!!";

            
            cameraFollow.target = null;
            //cameraFollow.target = currentPlayerControl.Players[^1].transform;
            Camera.main.transform.DOMove(new Vector3(0, 75, -90), 4f);
        }
    }
}