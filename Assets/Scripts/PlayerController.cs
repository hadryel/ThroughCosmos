using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D Rb2d;
    InputHandler InputHandler;

    float MovementSpeed = 4f;

    // Weapon related
    public GameObject Bullet;
    public Transform WeaponAim;

    // Resource related
    public float ResourcePickUpRange = 2f;
    public float ResourcePickUpSpeed = 1f;
    public int[] Resources;

    Animator Animator;
    public GameObject ActionArm;

    public float Life = 4f;
    public float MaximumLife = 4f;

    public ResourceManager ResourceManager;
    public QuestManager QuestManager;
    public ResultManager ResultManager;

    public int BulletCounter = 5;
    int MaxBullets = 5;

    float LastFire;
    public float FireDelay = 0.5f;
    public float RechargeTime = 1.5f;
    public float RechargeStart;
    public GameObject[] ResourceParticles;

    public AudioClip LaserSound;
    public AudioClip DamageSound;
    public AudioClip DrillNoise;
    void Start()
    {
        RechargeStart = Time.time - RechargeTime;
        LastFire = Time.time - FireDelay;

        Animator = GetComponentInChildren<Animator>();
        Rb2d = GetComponent<Rigidbody2D>();

        InputHandler = GetComponent<InputHandler>();
        InputHandler.FireAction += Fire;
        InputHandler.TargetAction += InteractAction;

        Resources = new int[3]; // Init the array of resources
    }

    void Update()
    {
        UpdateMovement();
    }

    public void UpdateMovement()
    {
        Animator.SetBool("Walking", InputHandler.Direction.magnitude > 0);

        UpdateAim();
        UpdateRotation();

        Rb2d.velocity = InputHandler.Direction * MovementSpeed;
    }

    public void UpdateRotation()
    {
        var rotation = (InputHandler.TargetPosition.x > ActionArm.transform.position.x) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        Animator.gameObject.transform.rotation = rotation;
    }

    public void UpdateAim()
    {
        var dir = ((Vector2)InputHandler.TargetPosition - (Vector2)ActionArm.transform.position).normalized;
        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 85.48f;

        ActionArm.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 180);

        if (dir.x >= 0)
        {
            ActionArm.transform.localRotation = Quaternion.Euler(0f, 0f, rotZ + 180);
        }
        else
        {
            ActionArm.transform.localRotation = Quaternion.Euler(0f, 0f, -rotZ + 180);
        }
    }

    public void Fire()
    {
        if (Time.time - LastFire < FireDelay || Time.time - RechargeStart < RechargeTime)
        {
            return;
        }

        GameObject bulletGO = GameObject.Instantiate(Bullet, WeaponAim.transform.position, Quaternion.identity);
        var bullet = bulletGO.GetComponent<Bullet>();
        //bullet.Direction = ((Vector2)InputHandler.TargetPosition - (Vector2)WeaponAim.transform.position);
        bullet.Direction = WeaponAim.transform.right;
        bullet.Source = transform;

        PlaySound(LaserSound);

        BulletCounter--;

        LastFire = Time.time;

        if (BulletCounter <= 0)
        {
            //Recharging = true;
            RechargeStart = Time.time;
            BulletCounter = MaxBullets;
        }
    }

    void PlaySound(AudioClip audioClip)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void InteractAction()
    {
        if (InputHandler.LastTarget.GetComponent<Resource>() != null && (InputHandler.LastTarget.transform.position - transform.position).magnitude <= ResourcePickUpRange)
        {
            Resource resource = InputHandler.LastTarget.GetComponent<Resource>();
            int collected = resource.Collect();

            if (collected <= 0)
            {
                return;
            }

            Resources[(int)resource.Type] += collected;
            ResourceManager.UpdateResource(resource.Type, Resources[(int)resource.Type]);
            GameObject.Instantiate(ResourceParticles[(int)resource.Type], WeaponAim.transform.position, Quaternion.Euler(-90f, 0, 0));

            PlaySound(DrillNoise);
            Animator.Play("Drilling");
            InputHandler.enabled = false;

            StartCoroutine(CollectResource());
        }
        else if (InputHandler.LastTarget.GetComponent<Rocket>() != null && (InputHandler.LastTarget.transform.position - transform.position).magnitude <= ResourcePickUpRange)
        {
            QuestManager.TryToComplete();
        }
    }

    IEnumerator CollectResource()
    {
        yield return new WaitForSeconds(1f);

        Animator.SetTrigger("DrillFinished");
        InputHandler.enabled = true;
    }

    public void Damage(float amount, GameObject source)
    {
        Life -= amount;

        PlaySound(DamageSound);

        if (Life <= 0)
        {
            GameObject.Find("Player").SetActive(false);
            Time.timeScale = 0;
            ResultManager.SetupDefeat();
        }
    }
}
