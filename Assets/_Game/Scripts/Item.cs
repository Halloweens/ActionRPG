using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Usable))]
public class Item : MonoBehaviour
{
	public enum ItemType : int
	{
		Equipment = 0,
		Weapon,
		Key,
		Consommable
	}

	public ItemType EnumItemType { get { return enumItemType; } set { enumItemType = value; } }
	private ItemType enumItemType;

	public uint Weight { get { return weight; } set { weight = value; } }
	private uint weight;

	public string ItemName { get { return itemName; } set { itemName = value; } }
	private string itemName;

    private bool isUsed = false;

	void Start ()
	{
        GetComponent<Usable>().onUsable.AddListener(OnUseableCallback);
    }
	
    private void OnUseableCallback(OnUsableArg arg)
    {
        if (!isUsed)
        {
            Debug.Log("Open Chest");
            GetComponent<Renderer>().material.color = Color.red;
            isUsed = true;
        }
    }


    void Update ()
	{
	
	}
}

public class ItemComparer : System.Collections.Generic.IEqualityComparer<Item>
{
	public bool Equals(Item item1, Item item2)
	{
		if (item1.EnumItemType == item2.EnumItemType && item1.Weight == item2.Weight && item1.ItemName == item2.ItemName)
			return true;
		return false;
	}

	public int GetHashCode(Item item)
	{
		string code = item.EnumItemType.ToString() + "|" + item.Weight.ToString() + "|" + item.ItemName.ToString();
		return code.GetHashCode();
	}
}
