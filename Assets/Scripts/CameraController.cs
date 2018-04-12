using UnityEngine;

public class CameraController : MonoBehaviour
{
    //этот контроллер я честно скопировал с туториала юнити, свою задачу он выполняет
    //а именно, следует за игроком
    //перестает двигаться, когда заканчиваются жизни и персонаж начинает падать вниз

    public GameObject player;       //Public variable to store a reference to the player game object

    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    private bool isFollow;

    private HeroStats stats;

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
        offset.y = 2f;
        stats = player.GetComponent<HeroStats>();
        isFollow = stats.LifeCount > 0;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {        
        isFollow = stats.LifeCount > 0;
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        if (isFollow)
            transform.position = player.transform.position + offset;
    }
}