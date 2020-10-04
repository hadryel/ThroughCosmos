using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator Animator;
    Rigidbody2D Rb2d;
    Transform Target;
    Vector2 TargetPosition;
    Vector2 CurrentDirection = Vector2.zero;
    Vector2 CurrentTargetDirection = Vector2.zero;

    public float CurrentSpeed = 2f;
    public float MovementSpeed = 2f;
    public float AttackSpeed = 4.5f;

    public float RoamRadius = 1;
    public float RoamInterval = 3f;
    public float MaximumRoamDuration = 3f;
    float CurrentRoamDuration = 0f;

    float DeaggroRange = 6f;
    public float AttackRange = 2f;
    public bool DealingDamage = false;
    public float AttackDamage = 1f;
    float AttackDelay = 0.5f;

    public float TimeBonus = 3f;
    public AlienSpawner Home;
    public int AlienType = 0;

    public GameObject OrangeProjectile;

    public AudioClip Attacksound;
    public AudioClip IdleSound;
    public AudioClip DyingSound;

    void Start()
    {
        SetupPerType();

        Animator = GetComponentInChildren<Animator>();
        Rb2d = GetComponent<Rigidbody2D>();

        StartCoroutine(LookingForDestiny());
    }

    void SetupPerType()
    {
        switch (AlienType)
        {
            case 0:
                AttackRange = 2f;
                AttackDamage = 1f;
                AttackDelay = 0.5f;
                DeaggroRange = 6f;
                TimeBonus = 2f;
                break;
            case 1:
                AttackRange = 5f;
                AttackDamage = 2f;
                AttackDelay = 0.75f;
                DeaggroRange = 8f;
                TimeBonus = 4f;
                break;
            case 2:
                AttackRange = 3f;
                AttackDamage = 3f;
                AttackDelay = 1f;
                break;
        }
    }
    public void OnDisable()
    {
        Rb2d.velocity = Vector2.zero;
        GetComponent<BoxCollider2D>().enabled = false;
        StopAllCoroutines();
    }

    private void Update()
    {
        Rb2d.velocity = CurrentDirection * CurrentSpeed;
    }
    IEnumerator LookingForDestiny()
    {
        //PlaySound(IdleSound);

        TargetPosition = (Vector2)transform.position + new Vector2(Random.Range(-RoamRadius, RoamRadius), Random.Range(-RoamRadius, RoamRadius));

        CurrentDirection = (TargetPosition - (Vector2)transform.position).normalized;

        Animator.SetBool("Walking", CurrentDirection.magnitude > 0);

        CurrentRoamDuration = 0f;

        var rotation = (TargetPosition.x > transform.position.x) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        Animator.gameObject.transform.rotation = rotation;

        yield return StartCoroutine(UpdateMovement());
    }

    IEnumerator ChaseTarget()
    {
        TargetPosition = (Vector2)Target.position;

        CurrentDirection = (TargetPosition - (Vector2)transform.position).normalized;

        Animator.SetBool("Walking", CurrentDirection.magnitude > 0);

        CurrentRoamDuration = 0f;

        var rotation = (TargetPosition.x > transform.position.x) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        Animator.gameObject.transform.rotation = rotation;

        yield return StartCoroutine(UpdateChase());
    }

    public IEnumerator UpdateMovement()
    {
        do
        {
            CurrentRoamDuration += Time.deltaTime;
            yield return null;
        } while ((TargetPosition - (Vector2)transform.position).magnitude >= 0.1f && CurrentRoamDuration < MaximumRoamDuration);

        CurrentDirection = Vector2.zero;

        Animator.SetBool("Walking", CurrentDirection.magnitude > 0);
        StartCoroutine(IdleWait());
    }

    public IEnumerator UpdateChase()
    {
        do
        {
            TargetPosition = (Vector2)Target.position;
            var targetDistance = TargetPosition - (Vector2)transform.position;
            CurrentDirection = (targetDistance).normalized;

            if (GetComponent<Enemy>().IsDamaged())
            {
                if (targetDistance.magnitude >= DeaggroRange * 3)
                {
                    StopAggro();
                    yield break;
                }
            }
            else
            {
                if (targetDistance.magnitude >= DeaggroRange)
                {
                    StopAggro();
                    yield break;
                }
            }

            var rotation = (TargetPosition.x > transform.position.x) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
            Animator.gameObject.transform.rotation = rotation;

            yield return null;
        } while ((TargetPosition - (Vector2)transform.position).magnitude >= AttackRange);

        CurrentDirection = Vector2.zero;
        Animator.SetBool("Walking", false);

        StartCoroutine(Attacking());
    }

    IEnumerator Attacking()
    {
        CurrentSpeed = AttackSpeed;

        Animator.SetTrigger("AttackStarted");
        Animator.SetTrigger("AttackRunning");
        Animator.Play("AttackCharging");

        TargetPosition = (Vector2)Target.position;

        yield return new WaitWhile(() => { return Animator.GetBool("AttackStarted") && Animator.GetBool("AttackRunning"); });

        switch (AlienType)
        {
            case 0:
                RedAlienAttack();
                break;
            case 1:
                OrangeAlienAttack();
                break;
            case 2:
                GreenAlienAttack();
                break;
        }

        yield return new WaitWhile(() => { return Animator.GetBool("AttackRunning"); });

        DealingDamage = false;
        CurrentSpeed = MovementSpeed;
        CurrentDirection = Vector2.zero;

        yield return new WaitForSeconds(AttackDelay);

        StartCoroutine(ChaseTarget());
    }

    void RedAlienAttack()
    {
        PlaySound(Attacksound);
        CurrentDirection = (TargetPosition - (Vector2)transform.position).normalized;
        DealingDamage = true;
    }

    void OrangeAlienAttack()
    {
        PlaySound(Attacksound);

        CurrentTargetDirection = (TargetPosition - (Vector2)transform.position).normalized;

        Transform projectileOrigin = GetComponentInChildren<ProjectileOrigin>().transform;
        GameObject projectileGO = GameObject.Instantiate(OrangeProjectile, projectileOrigin.position, Quaternion.identity);
        var bullet = projectileGO.GetComponent<EnemyBullet>();
        bullet.Direction = ((Vector2)TargetPosition - (Vector2)projectileOrigin.position);
        //bullet.Direction = WeaponAim.transform.right;
        bullet.Source = transform;
    }

    void GreenAlienAttack()
    {

    }

    public void StopAggro()
    {
        Target = null;

        CurrentDirection = Vector2.zero;
        Animator.SetBool("Walking", false);

        //GetComponentInChildren<AggroSensor>().gameObject.GetComponent<CircleCollider2D>().enabled = true;
        GetComponentInChildren<AggroSensor>(true).gameObject.SetActive(true);
        StartCoroutine(IdleWait());
    }

    IEnumerator IdleWait()
    {
        float WaitingTime = 5f;
        do
        {
            WaitingTime -= Time.deltaTime;
            yield return null;
        } while (WaitingTime > 0);

        StartCoroutine(LookingForDestiny());
    }

    public void Damage(float amount, Transform source)
    {
        // Refactor
        Enemy e = GetComponent<Enemy>();
        e.Life -= amount;


        if (e.Life <= 0)
        {
            Target = null;
            CurrentDirection = Vector2.zero;

            StopAllCoroutines();

            PlaySound(DyingSound);

            //Animator.SetTrigger("Dying");
            Animator.Play("Dying");
            enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            TimeManager.AddTime(TimeBonus);

            if (Home != null)
                Home.MemberKilled(AlienType);

            return;
            // Make item drop, maybe add a animation for fading
            //Destroy(gameObject);
        }

        if (Target == null)
        {
            Target = source;
        }

        StopAllCoroutines();
        StartCoroutine(ChaseTarget());
    }

    public void Aggro(Transform target)
    {
        Target = target;

        StopAllCoroutines();
        StartCoroutine(ChaseTarget());
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (DealingDamage)
        {
            PlayerController p = collision.gameObject.GetComponent<PlayerController>();

            if (p != null)
            {
                p.Damage(AttackDamage, gameObject);
                DealingDamage = false;
            }
        }
    }

    void PlaySound(AudioClip audioClip)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();

    }
}
