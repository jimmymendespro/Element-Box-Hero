using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dash : MonoBehaviour
{
    [SerializeField] bool dashUnlocked = false;
    [SerializeField] float dashSpeed = 8f;
    [SerializeField] float dashTime = 0.15f;
    [SerializeField] AudioClip dashSFX;
    [SerializeField] AudioClip dashHitSFX;
    [SerializeField] [Range(0,100)] int dashPower = 100;

    [SerializeField] GameObject dashHit;

    public bool DashUnlocked { get { return dashUnlocked; } }
    public int DashPower { get { return dashPower; } set { dashPower = value; } }

    UIUpdater uiUpdater;

    bool fire2Clicked = false;
    public bool Fire2Clicked { get { return fire2Clicked; } set { fire2Clicked = value; } }
    bool dashFullyUsed = false;
    public bool DashFullyUsed { get { return dashFullyUsed; } set { dashFullyUsed = value; } }

    Vector3 dashDirection;

    AudioSource audioSource;

    void Start()
    {
        uiUpdater = FindObjectOfType<UIUpdater>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        DashInput();
    }

    void FixedUpdate() 
    {
        DashFixed();
    }

    void DashInput()
    {
        if(dashUnlocked)
        {
            if(Input.GetButtonDown("Fire2"))
            {
                if(dashPower > 0 && !dashFullyUsed)
                {
                    fire2Clicked = true;
                    dashDirection = Vector3.zero;
                    if(Input.GetKey(KeyCode.Z))
                    {
                        dashDirection += Vector3.forward;
                    }
                    if(Input.GetKey(KeyCode.S))
                    {
                        dashDirection += Vector3.back;
                    }
                    if(Input.GetKey(KeyCode.A))
                    {
                        dashDirection += Vector3.left;
                    }
                    if(Input.GetKey(KeyCode.E))
                    {
                        dashDirection += Vector3.right;
                    }
                }
            }
        }
    }

    void DashFixed()
    {
        if(fire2Clicked && dashDirection != Vector3.zero)
        {
            StartCoroutine("DashRoutine");
            audioSource.clip = dashSFX;
            audioSource.volume = 1f;
            audioSource.Play();
            SubstractDashPoints();
            UpdateDashUI();
            dashDirection = Vector3.zero;
        }
    }

    IEnumerator DashRoutine()
    {
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        float initialSpeed = playerMovement.MovementSpeed;
        playerMovement.MovementSpeed *= dashSpeed;

        RaycastHit obstacleCheck;

        float startTime = Time.time;
        while(Time.time < startTime + dashTime)
        {
            Physics.Raycast(transform.position, transform.forward, out obstacleCheck);
            if(obstacleCheck.collider != null)
            {
                if(obstacleCheck.distance < 2)
                {
                    if(obstacleCheck.collider.tag == "Enemy")
                    {
                        GameObject hitEffect = Instantiate(dashHit, obstacleCheck.collider.transform.parent.transform.position, Quaternion.identity);
                        StartCoroutine("ProjectEnemy", obstacleCheck.collider.transform.parent.gameObject);
                        AudioSource enemyAudioSource = obstacleCheck.collider.transform.parent.GetComponent<AudioSource>();
                        enemyAudioSource.clip = dashHitSFX;
                        enemyAudioSource.Play();
                        obstacleCheck.collider.transform.parent.GetComponent<EnemyHealth>().DamageTaken(15);
                    }
                    break;
                }
            }
            yield return new WaitForFixedUpdate();
        }
        playerMovement.MovementSpeed = initialSpeed;
    }

    IEnumerator ProjectEnemy(GameObject enemy)
    {
        float movementPercent = 0f;
        Vector3 enemyInitialPosition = enemy.transform.position;
        Vector3 destination = transform.forward * 8 + enemy.transform.position;
        while(movementPercent < 1)
        {
            movementPercent += Time.fixedDeltaTime * 4f;
            if(enemy != null)
            {
                enemy.transform.position = Vector3.Lerp(enemyInitialPosition, destination, movementPercent);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void SubstractDashPoints()
    {
        if (dashPower - 50 > 0)
        {
            dashPower -= 50;
        }
        else
        {
            dashPower = 0;
            dashFullyUsed = true;
        }
    }

    private void UpdateDashUI()
    {
        if (!dashFullyUsed)
        {
            uiUpdater.UpdateDashPowerUI(dashUnlocked, dashPower);
        }
        else
        {
            uiUpdater.SetDashToBlack();
        }
    }
}
