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
    public List<GameObject> CollisionGOList;
    public List<Collision2D> CollisionList;
    public int ListSize;
    public bool Grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        SR = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        CollisionGOList = new List<GameObject>();
        CollisionList = new List<Collision2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ListSize = CollisionList.Count;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey("d") && CanGoLeftOrRight("Right") && rb.velocity.x < 7)
        {
            Direction = "Right";
            SR.flipX = false;
            anim.SetBool("IsRunning", true);
            //IgnoreFrictionLeftOrRight(Direction);
            rb.AddForce(new Vector2(SpeedMovementHorizontal, 0) * Time.fixedDeltaTime * 100);
        } else if (Input.GetKey("q") && CanGoLeftOrRight("Left") && rb.velocity.x > -7)
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
        if (Input.GetKeyDown("z") && Grounded || Input.GetKeyDown("space") && Grounded)
        {
            Grounded = false;
            rb.AddForce(new Vector2(0, SpeedMovementVertical) * Time.fixedDeltaTime * 1000);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.contacts[0].normal);
        CollisionList.Add(collision);
        if (collision.contacts[0].normal.y >= 0 && collision.gameObject.tag == "Ground")
        {
            Grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Collision2D colTemp = new Collision2D();
        foreach (Collision2D elt in CollisionList)
        {
            if (elt.gameObject.name == collision.gameObject.name)
                colTemp = elt;
        }
        CollisionList.Remove(colTemp);
    }

    private bool CanGoLeftOrRight(string Dir)
    {
        if (Dir == "Right")
        {
            foreach (Collision2D elt in CollisionList)
            {
                if (elt.contacts[0].normal.x < -0.71)
                    return false;
            }
        }
        if (Dir == "Left")
        {
            foreach (Collision2D elt in CollisionList)
            {
                if (elt.contacts[0].normal.x > 0.71)
                    return false;
            }
        }
        return true;
    }

    private bool IgnoreFrictionLeftOrRight(string Dir)
    {
        if (Dir == "Right")
        {
            foreach (Collision2D elt in CollisionList)
            {
                if (elt.contacts[0].normal.y > 0)
                    //gameObject.GetComponent<BoxCollider2D>().friction = 0;
                    return true;
            }
        }
        if (Dir == "Left")
        {
            foreach (Collision2D elt in CollisionList)
            {
                if (elt.contacts[0].normal.y > 0)
                    //rb.GetComponent<PhysicsMaterial2D>().friction = 0;
                    return true;
            }
        }
        return false;
    }
}

