using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public Transform attackPrefab;

	[System.Serializable]
	public class EnemyStats {
        public int maxHealth;

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

	public EnemyStats stats = new EnemyStats();

    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    void Start()
    {
        stats.Init();
        if(statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }

        InvokeRepeating("AOEAttack", 1f, 1f);
    }

	public void DamageEnemy (int damage) {
		stats.curHealth -= damage;
		if (stats.curHealth <= 0) {
			GameMaster.KillEnemy (this);
		}
        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    void AOEAttack()
    {
        Transform clone = Instantiate(attackPrefab, transform.position, Quaternion.identity) as Transform;
    }


}
