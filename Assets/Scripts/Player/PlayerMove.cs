using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float Speed = 40f;
    public SpriteRenderer weaponSpriteRenderer;
    private Animator playerAnimator;
    public Animator weaponanimetor;
    private Rigidbody2D playerRb;
    private Vector2 moveDir;
    public Transform hand;
    int UpView = 0;
    int DownView = 2;
    public Transform Firepoint;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        
    }

    private void Update ()
    {

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(moveX, moveY).normalized;
       
        playerAnimator.SetFloat("Speed", moveDir.sqrMagnitude);
        
      
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - hand.position;

        
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        hand.rotation = Quaternion.Euler(0, 0, angle);

        if (angle > 20 && angle < 160)
        {
            weaponanimetor.SetTrigger("LookUp");
            playerAnimator.SetTrigger("LookUp");
            Firepoint.localPosition = new Vector2(0.397f, 0f);
        }
        else if (angle < -50 && angle > -120)
        {
            weaponanimetor.SetTrigger("LookDown");
            playerAnimator.SetTrigger("LookDown");
            Firepoint.localPosition = new Vector2(0.397f, 0f);
        }
        else if(angle > -50 && angle < 50)
        {
            weaponanimetor.SetTrigger("LookRight");
            playerAnimator.SetTrigger("LookRight");
            Firepoint.localPosition = new Vector2(0.397f, 0.07f);
        }
        else if(angle > 120)
        {
            weaponanimetor.SetTrigger("LookLeft");
            playerAnimator.SetTrigger("LookLeft");
            Firepoint.localPosition = new Vector2(0.397f, -0.167f );
        }
        else if (angle < -120)
        {
            weaponanimetor.SetTrigger("LookLeft");
            playerAnimator.SetTrigger("LookLeft");
            Firepoint.localPosition = new Vector2(0.397f, -0.167f);
        }
        if (angle > 20 && angle < 160)
        {
            weaponSpriteRenderer.sortingOrder = UpView;  
        }
        else
        {
            weaponSpriteRenderer.sortingOrder = DownView;  
        }

    }

    private void FixedUpdate()
    {
        playerRb.velocity = new Vector2(moveDir.x * Speed, moveDir.y * Speed);  
        
       
    }
}
