using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;

public class Controller : Reset
{
    public GameObject player;
    public float playerJumpHeight;
    
    public GameObject ground;

    private Vector3 _playerStartingPos;
    
    public GameObject snake;

    public float snakesPerSecond;
    public float snakesAddition;
    public float snakesAdditionSpeed;
    

    private readonly ArrayList _snakes = new ArrayList();
    private readonly ArrayList _snakesRigidBody = new ArrayList();
    
    private Rigidbody2D _playerRigidBody;
    
    public TextMeshProUGUI scoreBoard;
    public GameObject gameOver;

    private float _score;
    
    public AudioSource sasukeLaugh;
    
    [SerializeField] private Reset[] resets;

    // Start is called before the first frame update
    private void Start()
    {
        _playerRigidBody = player.GetComponent<Rigidbody2D>();
        _playerRigidBody.freezeRotation = true;

        var position = player.transform.position;
        _playerStartingPos = new Vector3(position.x, position.y);
        
        Settings.OnGround = true;
        Settings.GameOver = false;
        
        gameOver.SetActive(false);

        scoreBoard.text = (0.0f).ToString(CultureInfo.InvariantCulture);
        
        Invoke(nameof(AddSnake), 2.0f);
        InvokeRepeating(nameof(AddScore), 1f, 1f);
    }

    private void AddScore()
    {
        if (Settings.GameOver) return;
        
        _score += Settings.Speed;
        scoreBoard.text= _score.ToString(CultureInfo.InvariantCulture);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Settings.GameOver)
        {
            if (!Input.GetMouseButton(0)) return;
            if (resets == null) return;
            foreach (var t in resets)
            {
                t.ResetSettings();
            }
            return;
        }
        
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetMouseButton(0)) && Settings.OnGround)
        {
            _playerRigidBody.velocity = new Vector2(0, playerJumpHeight);
            Settings.OnGround = false;
        }

        foreach (Rigidbody2D snakeBody in _snakesRigidBody)
            snakeBody.velocity = new Vector2(Settings.Speed * -10, 0);
        
        if (_snakes.Count <= 0) return;
        if (!(((GameObject) _snakes[0]).transform.position.x < -15f)) return;
        
        Destroy((GameObject) _snakes[0]);
        _snakes.RemoveAt(0);
        _snakesRigidBody.RemoveAt(0);
    }

    private void AddSnake()
    {
        var newSnake = Instantiate(snake);
        newSnake.transform.Translate(-15f, 0.1f, 0f);
        _snakes.Add(newSnake);
        var snakeBody = newSnake.GetComponent<Rigidbody2D>();
        snakeBody.freezeRotation = true;
        _snakesRigidBody.Add(snakeBody);

        var timer = 1f / (snakesPerSecond + snakesAddition * ((int) (Settings.TimesIncreased / snakesAdditionSpeed)));
        //Debug.Log("Value ->" + timer);
        Invoke(nameof(AddSnake),timer);
    }

    private static void TouchedGround()
    {
        Settings.OnGround = true;
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.Equals(ground))
        {
            TouchedGround();
            Debug.Log("Ground Touched");
        }

        if (!_snakes.Contains(col.gameObject)) return;
        
        Debug.Log("Hit By Snake");
        Settings.GameOver = true;
        Time.timeScale = 0;
        foreach (GameObject s in _snakes)
        {
            var aS = s.GetComponent<AudioSource>();
            aS.Stop();
        }
        sasukeLaugh.Play();
        gameOver.SetActive(true);
    }

    public override void ResetSettings()
    {
        Time.timeScale = 1;
        foreach (GameObject s in _snakes)
        {
            Destroy(s);
        }
        _snakes.Clear();
        _snakesRigidBody.Clear();
        scoreBoard.text = (0.0f).ToString(CultureInfo.InvariantCulture);
        _score = 0f;
        Settings.OnGround = true;
        Settings.GameOver = false;
        player.transform.position = _playerStartingPos;
        
        gameOver.SetActive(false);

        _playerRigidBody.velocity = new Vector2(0f,0f);
    }
}
