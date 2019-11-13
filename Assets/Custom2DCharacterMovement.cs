using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom2DCharacterMovement : MonoBehaviour
{

    public float SpeedMovementHorizontal = 1;
    public float SpeedMovementVertical = 1;
    private Rigidbody2D rb;
    private Animator anim;
    private string Direction = "";
    private SpriteRenderer SR;
    public List<GameObject> CollisionList;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        SR = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        CollisionList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey("d"))
        {
            SR.flipX = false;
            anim.SetBool("IsRunning", true);
            rb.AddForce(new Vector2(SpeedMovementHorizontal, 0) * Time.fixedDeltaTime * 100);
            Direction = "Right";
        } else if (Input.GetKey("q"))
        {
            SR.flipX = true;
            anim.SetBool("IsRunning", true);
            rb.AddForce(new Vector2(-SpeedMovementHorizontal, 0) * Time.fixedDeltaTime * 100);
            Direction = "Left";
        } else if (Direction == "Right")
        {
            anim.SetBool("IsRunning", false);
            SR.flipX = false;
        } else if (Direction == "Left")
        {
            anim.SetBool("IsRunning", false);
            SR.flipX = true;
        }
        if (Input.GetKey("z") && IsGrounded())
        {
            rb.AddForce(new Vector2(0, SpeedMovementVertical) * Time.fixedDeltaTime * 1000);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionList.Add(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CollisionList.Remove(collision.gameObject);

    }

    private bool IsGrounded()
    {
        foreach (GameObject CollisionGO in CollisionList)
        {
            if (CollisionGO.tag == "Ground" && )
                return true;
        }
        return false;
    }
}

