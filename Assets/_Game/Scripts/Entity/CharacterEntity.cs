using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof (Damageable))]
public class CharacterEntity : MonoBehaviour {

    [SerializeField]
    private string entityName = "Jack";
    bool isDestroyed = false;

    public Charact charac;
    public Arsenal arsenal;

    void Start ()
    {
        GetComponent<Damageable>().MaxHP = charac.CalcMaxHp();
        GetComponent<Damageable>().HP = GetComponent<Damageable>().MaxHP;

        GetComponent<Damageable>().onDamageTaken.AddListener(OnDamageTakenCallback);
        GetComponent<Damageable>().onDeath.AddListener(OnDeathCallback);
	}

    private void OnDamageTakenCallback(OnDamageTakenArgs args)
    {
        Debug.Log("Resist ! : damage / 2");

        args.damageAmount.Value /= 2.0f;

        GetComponent<Renderer>().material.color = Color.yellow;
    }

    private void OnDeathCallback(OnDeathArgs args)
    {
        //print message like "World Killed you!!" with args.source.getName()
        GetComponent<Renderer>().material.color = Color.red;
        //Destroy(gameObject);
    }

    void DestroyEntity()
    {
        //possibility to add anim before
        Destroy(gameObject);
    }
}
