using UnityEngine;
using UnityEngine.UI;

public class Heli : MonoBehaviour
{
    public float speed;

    public GameObject fire0;
    public GameObject fire1;
    private int _lives = 2;
    public int lives
    {
        get { return _lives; }
        set
        {
            _lives = value;
            fire1.SetActive(_lives < 2);
            fire0.SetActive(_lives < 1);
        }
    }

    public Vector3 defaultPos;
    public Text soldierText;
    public Text livesText;
    public Text scoreText;
    public int carryCount = 0;
    public int carryLimit = 3;
    public int score = 0;

    public Vector3[] spawnLocations;
    public GameObject repairKit;
    public GameObject soldier;

    public GameObject gameOverScreen;
    public Text finalScore;
    
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2( Input.GetAxis("Horizontal") * Time.deltaTime * speed, Input.GetAxis("Vertical") * Time.deltaTime * 300));
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Obstacle":
                livesText.text = "Lives: " + --lives;
            
                transform.position = defaultPos;
                rb.velocity = Vector2.zero;
                if (lives < 0)
                {
                    finalScore.text = "Score \n" + score;
                    gameOverScreen.SetActive(true);
                }
                if(other.name == "Bullet(Clone)") Destroy(other.gameObject);
                break;
            
            case "Soldier" when carryCount < carryLimit:
                carryCount++;
                Destroy(other.gameObject);
                soldierText.text = "Soldiers: " + carryCount + "/" + carryLimit;
                break;
            
            case "Repair Kit" when lives < 3:
                livesText.text = "Lives: " + ++lives;

                Destroy(other.gameObject);
                break;
            
            case "Hospital":
                score += carryCount;
                carryCount = 0;
                scoreText.text = "Score: " + score;
                soldierText.text = "Soldiers: " + carryCount + "/" + carryLimit;

                if (GameObject.FindGameObjectsWithTag("Soldier").Length == 0)
                {
                    foreach (var pos in spawnLocations)
                    {
                        Instantiate(Random.value > 0.2 ? soldier : repairKit, pos, Quaternion.identity);
                    }
                }
                break;
        }
    }
    
}
