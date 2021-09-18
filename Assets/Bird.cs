using UnityEngine;

public class Bird : MonoBehaviour
{
    private float _timeStagnent;
    private bool _birdWasLaunched;
    private Vector3 _initialPosition;
    
    [SerializeField] private float _gForceMultiplyer = 185;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);

        if (
            _birdWasLaunched &&
            GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1
           )
        {
            _timeStagnent += Time.deltaTime;
        }

        if (
            // out of bounds
            transform.position.y > 10 ||
            transform.position.y < -15 ||
            transform.position.x > 30 ||
            transform.position.x < -20 ||
            // dead on ground
            _timeStagnent > 2
           )
        {
            string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
        }
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        GetComponent<LineRenderer>().enabled = true;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;

        Vector2 directionToInitialPosition = _initialPosition - transform.position;

        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _gForceMultiplyer);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _birdWasLaunched = true;
        GetComponent<LineRenderer>().enabled = false;
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }
}
