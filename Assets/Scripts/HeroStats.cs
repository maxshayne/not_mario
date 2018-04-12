using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroStats : MonoBehaviour
{
    public int LifeCount = 1;

    public Text Label;

    private Collider2D heroCollider;

    private MovementController moveController;

    private Animator animator;

    // Use this for initialization
    void Start ()
	{
        Label.text = LifeCount.ToString();
        heroCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        moveController = GetComponent<MovementController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(string.Format("enter the collider {0}", collider.name));
        switch (collider.tag)
        {
            case "Enemy":
                //проверяем местонахождение игрока. Если над врагом - убиваем его, иначе получаем урон
                var heroPosition = transform.position;
                var enemyPosition = collider.transform.position;
                if (heroPosition.y - enemyPosition.y > 1f)
                    HitAnEnemy(collider.gameObject);
                else
                    LoseLife();
                break;
            case "Floor":
            case "Item":
                GameOver();
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            GameOver();
        }
    }

    void LoseLife()
    {
        //регистрируем потерю жизни и отображаем текущее количество в UI
        Debug.Log("Enemy hits you!");
        LifeCount--;
        Label.text = LifeCount.ToString();
        animator.SetTrigger("Hurt");
        if (LifeCount <= 0)
        {
            Death();
        }
    }

    void HitAnEnemy(GameObject enemy)
    {        
        //если прыгнули на противника - убиваем его и отпрыгиваем от него куда подальше
        //а сам враг проваливается под землю
        Debug.Log("You hit enemy!");
        moveController.Jump();
        var col = enemy.transform.parent.GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void Death()
    {
        heroCollider.isTrigger = true;
    }

    void GameOver()
    {        
        Debug.Log("Lol. You Died");        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
    }
}
