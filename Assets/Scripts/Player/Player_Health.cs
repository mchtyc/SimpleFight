using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public Player_Master P_Master;
    public Character_Stats P_Stats;

    GameManager_Master GM_Master;

    bool hasSlowedDown = false;
    
    void OnEnable()
    {
        GM_Master = GameObject.Find(GameManager_References.instance.managersName).GetComponent<GameManager_Master>();
    }

    void startRecoverCountdown()
    {
        StopCoroutine(countAndRecover());
        StartCoroutine(countAndRecover());
    }

    IEnumerator countAndRecover() {
        
        yield return new WaitForSeconds(P_Stats.timePastBeforeRecovery);

        while (true)
        {
            P_Stats.currentHP *= 1 + P_Stats.recoveryRate;

            if (P_Stats.currentHP > P_Stats.maxHP)
            {
                P_Stats.currentHP = P_Stats.maxHP;
                P_Master.CallEventHealthChanged();
                StopCoroutine(countAndRecover());
            }

            P_Master.CallEventHealthChanged();

            yield return new WaitForSeconds(P_Stats.recoveryRepeatTime);
        }
    }

    void TakeDamage(Bullet bullet)
    {
        float netDamage = bullet.damage - P_Stats.defense;
        startRecoverCountdown();

        if (netDamage > 0)
        {
            P_Stats.currentHP -= netDamage;
            bullet.E_Master.CallEventXPIncrease((int)netDamage);    // XP ve Power vurulan damage kadar artıyor
            bullet.E_Master.CallEventPowerIncrease((int)netDamage);

            if (P_Stats.currentHP <= 0)
            {
                P_Master.CallEventHealthChanged();
                bullet.E_Master.CallEventXPIncrease((int)(P_Stats.power / 10));    // XP ve Power powerın 10da 1 i kadar artıyor
                bullet.E_Master.CallEventPowerIncrease((int)(P_Stats.power / 10));
                Die();
            }

            P_Master.CallEventHealthChanged();
        }
    }

    public void TakeDamage(float damage)
    {
        float netDamage = damage - P_Stats.defense;
        startRecoverCountdown();

        if (netDamage > 0)
        {
            P_Stats.currentHP -= netDamage;

            if (P_Stats.currentHP <= 0)
            {
                P_Master.CallEventHealthChanged();
                Die();
            }

            P_Master.CallEventHealthChanged();
        }
    }

    void Die()
    {
        GM_Master.CallEventPlayerDied();
    }

    IEnumerator SwampSlower(float slowRate, float slowTime)
    {
        hasSlowedDown = true;
        float defaultSpeed = P_Stats.speed;
        P_Stats.speed *= slowRate;

        yield return new WaitForSeconds(slowTime);

        P_Stats.speed = defaultSpeed;
        hasSlowedDown = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameManager_References.instance.bulletTag))
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();

            if (gameObject.GetInstanceID() != bullet.ID)    // NPC Playerı vurunca
            {
                if (bullet.E_Master != null)
                {
                    TakeDamage(bullet);
                }

                Destroy(other.gameObject);  // Bullet destroyed
            }
        }
        else if (other.CompareTag(GameManager_References.instance.flameThrowerTag))
        {
            TakeDamage(other.gameObject.GetComponentInParent<FlameThrower_Controller>().damage);
        }
        else if (other.CompareTag(GameManager_References.instance.littleBladesTag))
        {
            TakeDamage(other.gameObject.GetComponent<TrapLittleBlades_Controller>().damage);
        }
        else if (other.CompareTag(GameManager_References.instance.circleBladeTag))
        {
            TakeDamage(other.gameObject.GetComponent<CircleBlade_Controller>().damage);
        }
        else if (other.CompareTag(GameManager_References.instance.woodenTag))
        {
            TakeDamage(other.gameObject.GetComponent<Wooden_Controller>().damage);
        }
        else if (other.CompareTag(GameManager_References.instance.cannonTag))
        {
            TakeDamage(other.gameObject.GetComponent<Cannon_Ball_Controller>().damage);
        }
        else if (other.CompareTag(GameManager_References.instance.swampTag) && !hasSlowedDown)
        {
            Swamp_Controller SC = other.gameObject.GetComponent<Swamp_Controller>();
            StartCoroutine(SwampSlower(SC.slowRate, SC.slowTime));
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(GameManager_References.instance.swampTag) && !hasSlowedDown)
        {
            Swamp_Controller SC = other.gameObject.GetComponent<Swamp_Controller>();
            StartCoroutine(SwampSlower(SC.slowRate, SC.slowTime));
        }
    }
}
