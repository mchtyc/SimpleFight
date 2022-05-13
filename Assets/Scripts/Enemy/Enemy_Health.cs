using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public Enemy_Master E_Master;
    public Enemy_AI AI;
    
    GameManager_Master GM_Master;

    bool hasSlowedDown = false;
    
    void OnEnable()
    {
        GM_Master = GameObject.Find("Managers").GetComponent<GameManager_Master>();
    }

    void TakeDamage(Bullet bullet)
    {
        float netDamage = bullet.damage - AI.Stats.defense;

        if (netDamage > 0)
        {
            AI.Stats.currentHP -= netDamage;

            CallEvents(bullet, (int)netDamage);

            if (AI.Stats.currentHP <= 0)
            {
                E_Master.CallEventHealthChanged(AI.Stats.currentHP, AI.Stats.maxHP);
                CallEvents(bullet, (int)(AI.Stats.power / 10));
                Die();
            }
            else
            {
                E_Master.CallEventHealthChanged(AI.Stats.currentHP, AI.Stats.maxHP);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        float netDamage = damage - AI.Stats.defense;

        if (netDamage > 0)
        {
            AI.Stats.currentHP -= netDamage;

            if (AI.Stats.currentHP <= 0)
            {
                E_Master.CallEventHealthChanged(AI.Stats.currentHP, AI.Stats.maxHP);
                Die();
            }
            else
            {
                E_Master.CallEventHealthChanged(AI.Stats.currentHP, AI.Stats.maxHP);
            }
        }
    }

    void Die()
    {        
        E_Master.CallEventOnDie();
        Destroy(gameObject, 0.1f);
        gameObject.SetActive(false);

    }

    IEnumerator SwampSlower(float slowRate, float slowTime)
    {
        hasSlowedDown = true;
        float defaultSpeed = AI.Stats.speed;
        AI.Stats.speed *= slowRate;

        yield return new WaitForSeconds(slowTime);

        AI.Stats.speed = defaultSpeed;
        hasSlowedDown = false;
    }

    void CallEvents(Bullet bullet, int count)
    {
        if (bullet.ID == GameManager_References.player.gameObject.GetInstanceID())  //Player NPC vurmuşsa
        {
            GM_Master.CallEventPlayerShotEnemy(count); // Player vurduğu damage kadar XP ve Power kazanacak
            GM_Master.CallEventPowerIncrease(count);
        }
        else    // NPC NPC vurmuşsa
        {
            if (bullet.E_Master != null)
            {
                bullet.E_Master.CallEventXPIncrease(count); // NPC vurduğu damage kadar XP ve Power kazanacak
                bullet.E_Master.CallEventPowerIncrease(count);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameManager_References.instance.bulletTag))
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();

            if (gameObject.GetInstanceID() != bullet.ID)
            {
                TakeDamage(bullet);

                Destroy(other.gameObject);
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
