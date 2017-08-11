using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Charact {

    public int strength;
    public int constitution;
    public int intelligence;
    public int dexterity;

    #region const
    public int CalcMaxHp()
    {
        return 120 + 10 * constitution;
    }

    public float HpRegen()
    {
        return constitution / 10;
    }

    public int BaseDef()
    {
        return constitution / 2;
    }
    #endregion
    #region strenght
    public int BaseAtk()
    {
        return 3 + strength;
    }

    public int CarryWeight()
    {
        return 300 + 10 * strength;
    }
    #endregion
    #region int
    public int MaxMana()
    {
        return 80 + 10 * intelligence;
    }
    public float MpRegen()
    {
        return intelligence / 10;
    }
    public int SpellPower()
    {
        return 5 + intelligence;
    }
    #endregion
    #region dext
    public float CritRate()
    {
        int rate = 1 + dexterity / 10;
        return rate < 100 ? rate : 100;
    }
    public float AtkSpeed()
    {
        return 1 + dexterity / 100;
    }
    public float Accuracy()
    {
        return 0 + dexterity / 100;
    }
    #endregion

}
