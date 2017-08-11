using UnityEngine;
using UnityEngine.Events;
using System;

public class Damageable : MonoBehaviour
{
    public OnDamageTaken onDamageTaken;
    public OnDeath onDeath;

    private float hp;
    private float maxHP;

    public float HP { get { return hp; } set { hp = value; } }
    public float MaxHP { get { return maxHP; } set { maxHP = value; } }

    public void TakeDamage(GameObject source, float amount, bool crit)
    {
        float realDamage = amount;

        Debug.Log("Base damage : " + realDamage);

        if (onDamageTaken != null)
            onDamageTaken.Invoke(new OnDamageTakenArgs(new Ref<float>(() => realDamage, x => { realDamage = x; }), crit));

        hp -= realDamage;
        Debug.Log("hp = " + hp);
        if (hp <= 0)
        {
            Die(source);
            Debug.Log("Dead");
        }
        Debug.Log("Real damages : " + realDamage);
    }

    public void Die(GameObject source)
    {
        if (onDeath != null)
            onDeath.Invoke(new OnDeathArgs(source));
    }
}

[System.Serializable]
public class OnDamageTaken : UnityEvent<OnDamageTakenArgs> { }

[System.Serializable]
public class OnDamageTakenArgs
{
    public Ref<float> damageAmount = null;
    public bool wasCrit;
    //ajouter type d'attaque (feu,glace)

    public OnDamageTakenArgs(Ref<float> da, bool wc)
    {
        damageAmount = da;
        wasCrit = wc;
    }
}

[System.Serializable]
public class OnDeath : UnityEvent<OnDeathArgs> { }

[System.Serializable]
public class OnDeathArgs
{
    GameObject source;

    public OnDeathArgs(GameObject src)
    {
        source = src;
    }
}

