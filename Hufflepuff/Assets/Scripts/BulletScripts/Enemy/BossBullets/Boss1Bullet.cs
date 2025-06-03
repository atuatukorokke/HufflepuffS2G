using UnityEngine;

public enum State
{
    fast = 0,
    second = 1,
    third = 2,
    four = 3,
    final = 4
}

public class Boss1Bullet : MonoBehaviour
{
    [SerializeField] private State state = State.fast;
    [SerializeField] private float maxHP = 100f;
    [SerializeField] private float currentHP;
    private bool isSpecialBulletActive = false;
    private float specialBulletDuration = 15f;
    private float timer = 0f;

    void Start()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        if (currentHP > 0)
        {
            HandleBulletPattern();
        }
        else
        {
            if (state == State.final)
            {
                SpecialFinalBullet();
            }
            else
            {
                TransitionToNextState();
            }
        }

        if (isSpecialBulletActive)
        {
            timer += Time.deltaTime;
            if (timer >= specialBulletDuration)
            {
                Destroy(gameObject);
            }
        }
    }

    private void HandleBulletPattern()
    {
        switch (state)
        {
            case State.fast:
            case State.second:
            case State.third:
            case State.four:
            case State.final:
                FireNormalBullet();
                break;
        }
    }

    private void FireNormalBullet()
    {
        Debug.Log("’Êí’e–‹”­Ë: " + state);
    }

    private void FireSpecialBullet()
    {
        Debug.Log("“Áê’e–‹”­Ë: " + state);
    }

    private void SpecialFinalBullet()
    {
        if (!isSpecialBulletActive)
        {
            isSpecialBulletActive = true;
            Debug.Log("Finaló‘Ô: “Á•Ê‚È’e–‹‚ğ”­Ë");
        }
    }

    private void TransitionToNextState()
    {
        if (state < State.final)
        {
            state++;
            currentHP = maxHP; // HP‚ğ‰ñ•œ
            Debug.Log("State‚ª•ÏX‚³‚ê‚Ü‚µ‚½: " + state);
        }
    }

    private void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= maxHP * 0.3f)
        {
            FireSpecialBullet();
        }

        if (currentHP <= 0)
        {
            currentHP = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("P_Bullet"))
        {
            TakeDamage(1f); // —á‚Æ‚µ‚Äƒ_ƒ[ƒW—Ê‚ğ10‚Éİ’è
            Destroy(collision.gameObject); // ’e‚ğÁ‚·
        }
    }
}
