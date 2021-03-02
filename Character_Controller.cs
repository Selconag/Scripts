using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Character_Controller : MonoBehaviour
{
    public Joystick joy;

    public float Speed;
    public float Sensitivity;
    public float CamMaxAngle;
    public float BallOffset;

    [Range(100, 2000)]
    public float ThrowForce;
    public float ShootCooldown;

    [Range(0f,1f)]
    public float PowerStart;

    [Range(0.05f, 0.5f)]
    public float PowerIncrease;

    [Range(0f, 1f)]
    public float TouchIgnoreTreshold;

    public Rigidbody rig;

    public Camera FpsCam;

    public GameObject Ball;

    public int currentScore;
    public int currentPosIndex;
    public int MaxBallNumber;

    public Text ScoreText;
    public Text BallAmountText;

    public Image PowerBar;

    public List<int> ScoreForPoints;

    private int BallNumber;

    private Vector3 dir;
    private Vector3 velocity;

    private GameObject currentBall;

    private bool control;
    private bool UIclick;
    private bool fireClick;

    [SerializeField]
    private List<Transform> Points;

    private void Awake()
    {
        transform.position = Points[0].position;
        currentPosIndex = 0;

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        if (MaxBallNumber > 0)
        {
            BallNumber = MaxBallNumber;
            InstantiateBall();
        }

        PowerBar.fillAmount = 0;
    }

    void Update()
    {
        CharacterRotation();
        CameraControl();
        if(currentBall != null && control) BallControl();

        UpdateUI();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        //float xMov = Input.GetAxisRaw("Horizontal");
        //float zMov = Input.GetAxisRaw("Vertical");

        float xMov = joy.Horizontal;
        float zMov = joy.Vertical;

        dir = new Vector3(xMov, 0f, zMov);
        dir.Normalize();
        dir = transform.TransformDirection(dir);

        velocity = dir * Speed * Time.deltaTime;
        velocity.y = rig.velocity.y;

        rig.velocity = velocity;

    }

    void CharacterRotation()
    {
        if (Input.touchCount <= 0 || UIclick) return;
        //float MouseX = Input.GetAxis("Mouse X");

        Touch tc = Input.GetTouch(0);
        float MouseX = Mathf.Clamp(tc.deltaPosition.x / 20, -1f, 1f);

        if (MouseX <= TouchIgnoreTreshold && MouseX >= -TouchIgnoreTreshold) return;

        float xRotate = MouseX * Sensitivity * Time.deltaTime;
        Quaternion t_adj = Quaternion.AngleAxis(xRotate, Vector3.up);
        Quaternion t_delta = transform.localRotation * t_adj;
        transform.localRotation = t_delta;
    }

    void CameraControl()
    {
        if (Input.touchCount <= 0 || UIclick) return;

        //float MouseY = Input.GetAxis("Mouse Y");

        Touch tc = Input.GetTouch(0);
        float MouseY = Mathf.Clamp(tc.deltaPosition.y / 20, -1f, 1f);

        if (MouseY <= TouchIgnoreTreshold && MouseY >= -TouchIgnoreTreshold) return;

        float yRotate = MouseY * Sensitivity * Time.deltaTime;
        Quaternion t_adj2 = Quaternion.AngleAxis(yRotate, -Vector3.right);
        Quaternion t_delta2 = FpsCam.transform.localRotation * t_adj2;

        if (Quaternion.Angle(Quaternion.identity, t_delta2) < CamMaxAngle)
        {
            FpsCam.transform.localRotation = t_delta2;
        }
    }

    void BallControl()
    {
        currentBall.transform.position = FpsCam.transform.position + FpsCam.transform.forward * BallOffset;

        /*if (Input.GetMouseButtonUp(0))
        {
            ShootOperation(PowerBar.fillAmount);
        }

        if (Input.GetMouseButton(0))
        {
            //Image img = PowerBar.GetComponentInChildren<Image>();

            if (PowerBar.fillAmount < 1f) PowerBar.fillAmount += PowerIncrease * Time.deltaTime;
            else ShootOperation(PowerBar.fillAmount);
        }*/

        if(fireClick) PowerBarOperation();
    }

    public void ShootFunc()
    {
        if (currentBall != null && control) ShootOperation(PowerBar.fillAmount);
    }

    public void PowerBarOperation()
    {
        if (currentBall != null && control)
        {
            if (fireClick)
            {
                if (PowerBar.fillAmount < 1f) PowerBar.fillAmount += PowerIncrease * Time.deltaTime;
                else ShootOperation(PowerBar.fillAmount);
            }
            else
            {
                ShootOperation(PowerBar.fillAmount);
            }
        }
    }

    public void UIEnter()
    {
        UIclick = true;
    }

    public void UIExit()
    {
        UIclick = false;
    }

    public void FireButtonDown()
    {
        fireClick = true;
    }

    public void FireButtonUp()
    {
        fireClick = false;
    }

    void ShootOperation(float powerMultiplier)
    {
        control = false;

        currentBall.GetComponent<Rigidbody>().useGravity = true;

        BallNumber--;

        currentBall.GetComponent<Rigidbody>().AddForce(FpsCam.transform.forward * ThrowForce * powerMultiplier);

        Destroy(currentBall, 10f);

        currentBall = null;

        StartCoroutine("Delay", ShootCooldown);
    }

    void InstantiateBall()
    {
        if (BallNumber > 0)
        {
            Vector3 pos = FpsCam.transform.position + FpsCam.transform.forward * BallOffset;

            GameObject newBall = Instantiate(Ball, pos, Quaternion.identity);
            currentBall = newBall;

            newBall.GetComponent<Rigidbody>().useGravity = false;

            control = true;
        }
    }

    public void RandPos()
    {
        int index = Random.Range(0, 2);

        transform.position = Points[index].position;
    }

    void UpdateUI()
    {
        ScoreText.text = "Score: " + currentScore;
        BallAmountText.text = "Ball: " + BallNumber;
    }

    IEnumerator Delay(float delayTime)
    {
        PowerBar.fillAmount = 0;

        yield return new WaitForSeconds(delayTime);

        InstantiateBall();
    }

}
