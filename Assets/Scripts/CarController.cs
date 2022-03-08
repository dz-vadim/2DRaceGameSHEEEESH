using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class CarController : MonoBehaviour
{

    [SerializeField] WheelJoint2D backWheel;    
    [SerializeField] WheelJoint2D frontWheel;
    JointMotor2D motor;
    public bool moveForward = false;
    public bool moveBackward = false;

    bool isGrounded = true;
    float speed = 0f;
    int fuel = 100;
    [SerializeField] Text fuelText; //using UnityEngine.UI;
    Rigidbody2D rb2d;

        void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        motor.maxMotorTorque = 100500;
        StartCoroutine(nameof(FuelReducer));
    }
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveForward = true;
            moveBackward = false;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveForward = false;
            moveBackward = true;
        }
        else
        {
            moveForward = false;
            moveBackward = false;
        }
        */
        if (frontWheel.GetComponent<Collider2D>().IsTouchingLayers() 
            || backWheel.GetComponent<Collider2D>().IsTouchingLayers())
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        CheckGameOver();
    }
    void FixedUpdate()
    {
        MoveOnGround();
        if (!isGrounded) MoveInAir();
    }

     void MoveOnGround()
     {
        if (moveForward)
        {
            if (frontWheel.attachedRigidbody.angularVelocity > -2000)
            {
                speed += 30;
                motor.motorSpeed = speed;
            }

            backWheel.useMotor = true;
            frontWheel.useMotor = true;
            backWheel.motor = motor;
            frontWheel.motor = motor;
        }
        else if (moveBackward)
        {
            if (backWheel.attachedRigidbody.angularVelocity < 2000)
            {
                speed -= 30;
                motor.motorSpeed = speed;
            }

            backWheel.useMotor = true;
            frontWheel.useMotor = true;
            backWheel.motor = motor;
            frontWheel.motor = motor;
        }
        else
        {
            speed = -frontWheel.attachedRigidbody.angularVelocity;
            backWheel.useMotor = false;
            frontWheel.useMotor = false;
        }
    }

    void MoveInAir()
    {
        backWheel.useMotor = false;
        frontWheel.useMotor = false;
        if (moveForward)
        {
            if(rb2d.angularVelocity < 200)
            {
                rb2d.AddTorque(8f);
            }
        }
        else if (moveBackward)
        {
            if (rb2d.angularVelocity > -200)
            {
                rb2d.AddTorque(-8f);
            }
        }
    }
    void CheckGameOver()
    {
        Vector2 rayDir = transform.up;
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, 
            rayDir, 0.5f);
        Debug.DrawRay(transform.position, rayDir/2, Color.green);

        if (hit.Length > 1)
        {
            GameOver(); 
        }
    }
    void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("DeathZone"))
        {
            GameOver();
        }
        else if (collider.gameObject.CompareTag("Finish"))
        {
            int saveLevel = PlayerPrefs.GetInt("CurrentLevel");
            int playLevel = SceneManager.GetActiveScene().buildIndex;
            if (playLevel >= saveLevel)
            {
                PlayerPrefs.SetInt("CurrentLevel", playLevel + 1);
            }
            SceneManager.LoadScene(0);
        }
        else if (collider.gameObject.CompareTag("Fuel"))
        {
            Destroy(collider.gameObject);
            fuel += 20;
            if (fuel > 100) fuel = 100;
        }
    }
    IEnumerator FuelReducer()
    {
        while (fuel > 0)
        {
            fuel -= 1;
            fuelText.text = fuel.ToString();
            yield return new WaitForSeconds(0.5f);
        }
        GameOver();
    }

}
