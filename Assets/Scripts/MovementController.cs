using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{


    private Transform thisTransform;
    private Rigidbody2D rigidbody;
    private Animator animator;

    private bool isRight;
    public bool IsGrounded;

    private float groundRadius = 0.2f;

    public Transform GroundCheck;

    public LayerMask WhatIsGround;

    public float Move;
    public float MoveSpeed;

    public float JumpForce;
    public float RealJumpForce = 500f;

    // Use this for initialization
    void Start()
    {
        isRight = true;
        thisTransform = this.transform;
        rigidbody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        MoveSpeed = 4f;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void FixedUpdate()
    {
        //двигаем персонажа, используюя инпуты определенные по умолчанию
        //сработает wasd, стрелки и даже геймпад
        Move = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Math.Abs(Move));

        thisTransform.Translate(Vector2.right * Move * MoveSpeed * Time.deltaTime);
        if (Move > 0 && !isRight)
            Turn();
        else if (Move < 0 && isRight)
            Turn();

        JumpForce = Input.GetAxis("Jump");
        //проверяем, находится ли персонаж на земле, если нет, то прыгать нельзя
        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundRadius, WhatIsGround);
        animator.SetBool("Ground", IsGrounded);
        if (IsGrounded && JumpForce > 0.1f)
        {
           Jump();
        }
    }

    public void Jump()
    {
        //прыжок
        Debug.Log("Jump");
        animator.SetBool("Ground", false);
        rigidbody.AddForce(new Vector2(0, RealJumpForce));
    }

    void Turn()
    {
        //поворачиваем персонажа, когда идем влево/вправо
        isRight = !isRight;
        Vector3 scale = thisTransform.localScale;
        scale.x *= -1;
        thisTransform.localScale = scale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //делаем игрока наследником движущейся платформы, иначе он будет падать с нее во время движения платформы
        if (collision.collider.tag == "MovingPlatform")
        {
            thisTransform.parent = collision.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //выпрыгивая с платформы, делает игрока своим собственным, как кота Матроскина
        if (collision.collider.tag == "MovingPlatform")
        {
            thisTransform.parent = null;
        }
    }
}
