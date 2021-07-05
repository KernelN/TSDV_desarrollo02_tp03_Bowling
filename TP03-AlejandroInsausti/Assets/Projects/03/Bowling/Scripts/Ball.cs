using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    //INSPECTOR vars
    //misc vars
    [SerializeField] GameObject hud;
    [SerializeField] GameObject gameOver;
    [SerializeField] Transform floor;
    [SerializeField] float launchSpeedMod;
    [SerializeField] float numDecreaser;
    //ball counter vars
    [SerializeField] TextMeshProUGUI ballCounter;
    [SerializeField] short ballsLeft;
    //pos vars
    [SerializeField] float posLimit;
    [SerializeField] float posLimitMod;
    [SerializeField] float posSpeedMod;
    //power vars
    [SerializeField] float powerSpeedMod;
    [SerializeField] float power;
    [SerializeField] Slider powerBar;

    //INTERN vars
    Rigidbody rb;
    bool inGame;

    //PROPIETES
    float Power
    {
        get { return power; }
        set { power = value < 1 ? value : power; }
    }

    //METHODS
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        posLimit = (floor.localScale.x / 2) * posLimitMod;
        UpdateText();
        GenerateBall();
    }

    void Update()
    {
        if (inGame) { return; }

            if (Mathf.Abs(transform.localPosition.x) < posLimit)
            {
                //Move and launch ball
                MoveBall();
                UpdatePower();
                if (Input.GetKeyDown("space")) { LaunchBall(); }
            }
            else { transform.localPosition *= numDecreaser; }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Deleter")
        {
            Respawn();
        }
    }

    void GenerateBall()
    {
        inGame = false;
        transform.localPosition = new Vector3(0, -1, 0);
        rb.velocity *= 0;
        rb.angularVelocity *= 0;
        Power = 0;
    }

    void LaunchBall()
    {
        inGame = true;
        rb.AddForce(0, 0, (Power + 1) * launchSpeedMod);//adds 1 so the  ball launches even at the bottom of the bar
    }

    void MoveBall()
    {
        transform.localPosition += new Vector3(Input.GetAxis("Horizontal") * posSpeedMod * Time.deltaTime, 0, 0);
    }

    void UpdatePower()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            Power += Input.GetAxis("Vertical") * Time.deltaTime * powerSpeedMod;
        }
        else if (Power > 0.01f)
        {
            Power *= numDecreaser;
        }
        powerBar.value = Power;
    }

    void Respawn()
    {
        ballsLeft--;
        UpdateText();
        if (ballsLeft > 0)
        {
            GenerateBall();
        }
        else
        {
            ballsLeft = 3;
            UpdateText();
            GenerateBall();
            hud.SetActive(false);
            gameOver.SetActive(true);
        }
    }

    void UpdateText()
    {
        ballCounter.SetText("Balls Left\n" + ballsLeft.ToString("D2"));
    }
}
