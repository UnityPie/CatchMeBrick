using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{   
    public AudioSource RunSound;
    public AudioSource Jump;
    GameObject GameSettings;
    MenuScript MS;
    float Dist;
    public float gravity = -22f;
    public float groundDistance = 0.4f;
    bool Lock = false;
    bool Lock2 = false;
    bool IsGrounded;
    float horizontal;
    float vertical;
    public CharacterController characterController;
    Vector3 velocity; 
    Vector3 move;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Camera mainCamera;
    public GameObject MainCam;
    public GameObject BattleCam;
    public TrajectoryRenderer Trajectory;
    public LineRenderer LR;
    public GameObject[] PrefabCollections;
    GameObject NewGrenade;
    Rigidbody rb;
    Vector3 Speed;
    public Transform ShotPos;
    public CreatBomb CB;
    Vector3 RayCastPoint;
    public GameObject UnitModel;
    public Collider[] overlappedColliders;

    void Start()
    {
        GameSettings = GameObject.FindGameObjectWithTag("GameSettings");
        MS = GameSettings.GetComponent<MenuScript>();
    }
    void InvisibleWall()
    {
        RaycastHit hit;
        if(Physics.Linecast(mainCamera.transform.position, transform.position, out hit))
        {
            if(hit.collider.tag == "Objects")
            {
                GameObject Mat = hit.collider.gameObject;
                MeshRenderer R = Mat.GetComponent<MeshRenderer>();
                R.material = MS.Invisible[0];
            }
        }
    }
    void VisibleWall()
    {
        overlappedColliders = Physics.OverlapSphere(transform.position, 999999999f);
        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            if(overlappedColliders[i].tag == "Objects")
            {
                MeshRenderer R = overlappedColliders[i].GetComponent<MeshRenderer>();
                R.material = MS.Visible[0];
            }
        }
    }
    private void FixedUpdate()
    {
        //Проверяем на земле ли персонаж
        if(IsGrounded)
        {
            move = transform.right * horizontal + transform.forward * vertical;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        //Реализуем движение часть 2
        if(!Input.GetKey(KeyCode.Mouse0))
        {
            characterController.Move(move * MenuScript.speed * Time.deltaTime);
        }
        
        //Проверка земли сферой
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(IsGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Прыжок
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(IsGrounded)
            {
                velocity.y = Mathf.Sqrt(MenuScript.jumpHight * -2 * gravity);
                if(MenuScript.SoundsOnOff)
                {
                    Jump.Play();
                }
            }
        }
    }
    void Update()
    {   
        if(IsGrounded)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        else
        {
            RunSound.Pause();
            horizontal = 0;
            vertical = 0;
        }
        if(horizontal != 0 || vertical != 0)
        {
            if(!RunSound.isPlaying && IsGrounded)
            {
                if(MenuScript.SoundsOnOff && Time.timeScale != 0)
                {
                    RunSound.Play();
                }
            }
        }
        if(horizontal == 0 && vertical == 0)
        {
            RunSound.Pause();
        }
        if(!IsGrounded)
        {
            RunSound.Pause();
        }
        if(Time.timeScale == 0)
        {
            RunSound.Stop();
        }        
        if(!Input.GetKey(KeyCode.Mouse0) && Time.timeScale != 0)
        {
            LR.enabled = false;
            UnitModel.SetActive(false);
            MainCam.SetActive(true);
            BattleCam.SetActive(false);
            Lock = false;
            if(!Lock2)
            {
                VisibleWall();
                Lock2 = true;
            }
        }
        //Прицеливаемся
        if(Input.GetKey(KeyCode.Mouse0) && !MenuScript.Esc && Time.timeScale != 0)
        {
            if(RunSound.isPlaying)
            {
                RunSound.Stop();
            }
            Lock2 = false;
            if(!Lock)
            {
                InvisibleWall();
                Lock = true;
            }
            UnitModel.SetActive(true);
            MainCam.SetActive(false);
            BattleCam.SetActive(true);
            LR.enabled = enabled;
            Ray ray2 = mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray2, out RaycastHit raycastHit))
            {
                RayCastPoint = raycastHit.point;
            }
            float enter;
            new Plane(Vector3.up,transform.position).Raycast(ray2,out enter);
            Vector3 mouseInWorld = ray2.GetPoint(enter);
            Speed = (RayCastPoint - transform.position) / 4f;
            Trajectory.ShowTrajectory(transform.position, Speed);
        }
        //Запускаем гранату по траектории
        if(Input.GetKeyUp(KeyCode.Mouse0) && !MenuScript.Esc && Time.timeScale != 0)
        {
            UnitModel.SetActive(false);
            LR.enabled = false;
            MainCam.SetActive(true);
            BattleCam.SetActive(false);
            if(CB.Cells == 0)
            {
                if(MS.RedBombCount > 0)
                {
                    NewGrenade = Instantiate(PrefabCollections[CB.Cells],ShotPos.transform.position,Quaternion.identity);
                    rb = NewGrenade.GetComponent<Rigidbody>();
                    rb.AddForce(Speed,ForceMode.Impulse);
                    MS.RedBombCount--;
                    MS.RedText.text = MS.RedBombCount.ToString();
                    MS.WarningText.SetActive(false);
                }
                else
                {
                    MS.WarningText.SetActive(true);
                }
            }
            if(CB.Cells == 1)
            {
                if(MS.GreenBombCount > 0)
                {
                    NewGrenade = Instantiate(PrefabCollections[CB.Cells],ShotPos.transform.position,Quaternion.identity);
                    rb = NewGrenade.GetComponent<Rigidbody>();
                    rb.AddForce(Speed,ForceMode.Impulse);
                    MS.GreenBombCount--;
                    MS.GreenText.text = MS.GreenBombCount.ToString();
                    MS.WarningText.SetActive(false);
                }
                else
                {
                    MS.WarningText.SetActive(true);
                }

            }
            if(CB.Cells == 2)
            {
                if(MS.YellowBombCount > 0)
                {
                    NewGrenade = Instantiate(PrefabCollections[CB.Cells],ShotPos.transform.position,Quaternion.identity);
                    rb = NewGrenade.GetComponent<Rigidbody>();
                    rb.AddForce(Speed,ForceMode.Impulse);
                    MS.YellowBombCount--;
                    MS.YellowText.text = MS.YellowBombCount.ToString();
                    MS.WarningText.SetActive(false);
                }
                else
                {
                    MS.WarningText.SetActive(true);
                }
            }
        }
    }
}
