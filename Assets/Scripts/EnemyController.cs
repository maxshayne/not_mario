using UnityEngine;

public class EnemyController : MonoBehaviour
{


    private Transform thisTransform;
    private Animator animator;

    public bool IsRight;
    
    private float moveSpeed;
    private float moveDirection;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        moveDirection = IsRight ? 1f : -1f;        
        thisTransform = this.transform;
        moveSpeed = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        //двигаем врага по платформе, периодически меняя направление движения
        thisTransform.Translate(Vector2.right * moveDirection * moveSpeed * Time.deltaTime);
        if (moveDirection > 0 && !IsRight)
            Turn();
        else if (moveDirection < 0 && IsRight)
            Turn();
    }

    /// <summary>
    /// Метод разворова юнита собственной персоной
    /// </summary>
    void Turn()
    {        
        IsRight = !IsRight;
        moveDirection = IsRight ? 1 : -1;
        Vector3 scale = thisTransform.localScale;
        scale.x *= -1;
        thisTransform.localScale = scale;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(string.Format("enemy triggered the collider {0}", collider.name));
        switch (collider.tag)
        {
            case "Floor":
                TurnOff();
                break;
            case "Wall":
                Turn();
                break;
            case "Player":
                animator.SetTrigger("Attack");
                break;
        }
    }

    void TurnOff()
    {
        this.enabled = false;
    }
}