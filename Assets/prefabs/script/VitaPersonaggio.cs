using UnityEngine;

public class VitaPersonaggio: MonoBehaviour
{
    public static VitaPersonaggio instance;

    public int health;
    public int maxHealth = 3;

    public float invincibleDuration = 2f;
    private float invincibleTimer;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ResetHealth();
    }

    private void Update()
    {
        if (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;

            bool visible = Mathf.Floor(invincibleTimer * 5f) % 2 == 0 || invincibleTimer <= 0;

            foreach (GameObject part in PlayerControllerPlatform.instance.modelParts)
                part.SetActive(visible);
        }
    }

    public void DamagePlayer()
    {
        if (invincibleTimer > 0)
            return;

        health--;

        if (health == 0)
        {
            ResetHealth();
            GameManager.Instance.GameOver();
        }
        else
        {
            PlayerControllerPlatform.instance.Knockback();
            invincibleTimer = invincibleDuration;
        }

        //AudioManager.instance.PlayEffect(AudioManager.SFX.TakeDamage);


        HudLife();
        // UpdateUI();
    }

    public void ResetHealth()
    {
        health = maxHealth;


        HudLife();
        // UpdateUI();
    }

    public void HealPlayer()
    {
        health = Mathf.Min(health + 1, maxHealth);

        HudLife();
       // UpdateUI();
    }

    public void HudLife()
    {
        switch (health)
        {
            case 1:hudmanager.instance.life(1);  break;
            case 2:hudmanager.instance.life(2);  break;
            case 3:hudmanager.instance.life(3);  break;

        }
        

    }
    /*
    public void UpdateUI()
    {
        for (int i = 0; i < UIManager.instance.hearts.Length; ++i)
            UIManager.instance.hearts[i].enabled = i < health;
    }*/
}
