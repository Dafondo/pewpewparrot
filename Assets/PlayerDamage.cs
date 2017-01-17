using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour {

    private bool invuln = false;
    public float invulTime;

    [System.Serializable]
    public class PlayerStats
    {
        public int maxHealth = 100;

        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Init()
        {
            curHealth = maxHealth;
        }
    }

    public PlayerStats stats = new PlayerStats();

    [SerializeField]
    private StatusIndicator statusIndicator;

    // Use this for initialization
    void Start()
    {
        stats.Init();

        if (statusIndicator == null)
        {
            Debug.LogError("No SI");
        }
        else
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Enemy Attack"))
        {
            Debug.Log("Hit!!!!!!!!");
            DamagePlayer(10);
        }
    }

    public void DamagePlayer(int damage)
    {
        stats.curHealth -= damage;
        if (stats.curHealth <= 0)
        {
            GameMaster.KillPlayer(this);
        }
        Invuln();
        Invoke("ClearInvuln", invulTime);
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }

    void Die()
    {
        Destroy(this);
    }

    void Invuln()
    {
        invuln = true;
    }

    void ClearInvuln()
    {
        invuln = false;
    }
}
