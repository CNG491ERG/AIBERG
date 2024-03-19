using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance;
    private Boss boss;
    private GameManager gameManager;
    private float bossPreviousHP;
    private void Awake() {
        if(Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
        }
    }

    void Start()
    {
        boss = GameObject.Find("Boss").GetComponent<Boss>();
        gameManager = GameManager.Instance;
        bossPreviousHP = boss.Health;
        boss.OnDamageableHurt += OnBossHurt;
        boss.OnDamageableDeath += OnBossDeath;
    }

    void OnDestroy(){
        boss.OnDamageableHurt -= OnBossHurt;
        boss.OnDamageableDeath -= OnBossDeath;
    }

    private void OnBossHurt(object sender, System.EventArgs e){

        float percentageHPLost = ((bossPreviousHP - boss.Health) / boss.MaxHealth) * 100;
            gameManager.Score += (long) (500 * percentageHPLost);
            bossPreviousHP = boss.Health;
    }

    private void OnBossDeath(object sender, System.EventArgs e){
        int stepsRemaining = (int) (gameManager.MaxSteps - gameManager.StepCount);
        gameManager.Score += (10 * stepsRemaining);
        bossPreviousHP = boss.MaxHealth;
    }
}
