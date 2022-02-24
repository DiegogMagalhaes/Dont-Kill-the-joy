using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    //Serializable Variables
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallMutiplier;
    [SerializeField] private float lowJumpMutiplier;

    [SerializeField] private Vector2 rightOffSet, leftOffSet, bottomOffSet;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject Dust;
    const float overlapCircleRadius = 0.25f; // constante para definir a radiante do circulo usado para verificar se ha colisão com o chao


    // Condition Variable
    private bool WasJump;
    private bool onGround;
    private bool CanMove;

    //Player Variables
    private Rigidbody2D playerRb;
    private float GravityDefault;

    //InputsVariable
    private bool pressjump;
    private float axisX;


    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        if (playerRb == null) Debug.LogError("ERROR: player RigidBody2D was don't found!");

        GravityDefault = playerRb.gravityScale;

        //InputsVariableSet

        pressjump = false;
        axisX = 0f;
        CanMove = false;
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            Vector2 direction = new Vector2(axisX, 0);
            Walk(direction);

            if (pressjump) Jump();

        }
    }

    private void Update()
    {
        if (CanMove)
        {
            onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffSet, overlapCircleRadius, groundLayer);

            MovimentInput();
            JumpModifier();

            //if (onGround && WasJump)
            //{
            //    WasJump = false;
            //    if(GameObject.Find("JumpDust_0") == null)InstantiteDust();
           // }
        }
    }

    private void MovimentInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround) pressjump = true;
        axisX = Input.GetAxis("Horizontal");
    }

    private void Walk(Vector2 direction)   
    {
        playerRb.velocity = new Vector2(direction.x * speed, playerRb.velocity.y);
        if (direction.x > 0) gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else if(direction.x <0) gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

    private void Jump()
    {
        playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        pressjump = false;
        //StartCoroutine(WasjumpOn());
    }

    IEnumerator WasjumpOn()
    {
        yield return new WaitForSeconds(0.2f);
        WasJump = true;
    }

    private void JumpModifier()
    {
        if (playerRb.velocity.y < 0)
        {
            playerRb.gravityScale = fallMutiplier;
        }

        else if (playerRb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            playerRb.gravityScale = lowJumpMutiplier;

        }

        else
            playerRb.gravityScale = GravityDefault;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffSet, overlapCircleRadius);
    }

    public void SwitchCanMove(bool can)
    {
        CanMove = can;
        if (!can) playerRb.velocity = new Vector2(0, 0);
    }

    private void InstantiteDust()
    {
        GameObject temp = Instantiate(Dust);
        temp.transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);

        temp = Instantiate(Dust);
        temp.GetComponent<SpriteRenderer>().flipX = true;
        temp.transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
    }
}
