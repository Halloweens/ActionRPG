using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	[SerializeField] private Button buttonPrefab;
	[SerializeField] private Transform filterContent;
	[SerializeField] private Transform itemListContent;

	[SerializeField] private InventoryLowerBar lowerBar;
	[SerializeField] private ItemCharacteristics itemCharaPanel;

	public uint MaxWeight { get { return maxWeight; } set { maxWeight = value; } }
	private uint curWeight = 0;
	[SerializeField] private uint maxWeight;

	public uint Gold { get { return gold; } set { gold = value; } }
	private uint gold = 0;

	private Dictionary<Item, uint> items = new Dictionary<Item, uint>(new ItemComparer());

	void Start ()
	{
		gameObject.SetActive(false);
	}

	public void DisplayInventory()
	{
		gameObject.SetActive(true);

		Button button = Instantiate(buttonPrefab);
		button.transform.SetParent(filterContent);
		button.GetComponentInChildren<Text>().text = "All";
		button.onClick.AddListener(delegate { DisplayAll(); });
		DisplayAll();

		List<Item.ItemType> typeAlreadyHere = new List<Item.ItemType>();

		foreach (Item item in items.Keys)
		{
			if (!typeAlreadyHere.Contains(item.EnumItemType))
			{
				AddButtonFilter(item);

				typeAlreadyHere.Add(item.EnumItemType);
			}
		}
		lowerBar.ChangeWeightLabel(curWeight, maxWeight);
		lowerBar.ChangeGoldLabel(gold);
	}
	public void HideInventory()
	{
		ClearItemListContent();
		ClearFilterListContent();

		gameObject.SetActive(false);
	}

	private void AddButtonFilter(Item item)
	{
		Button otherButton = Instantiate(buttonPrefab);
		otherButton.transform.SetParent(filterContent);
		otherButton.GetComponentInChildren<Text>().text = item.EnumItemType.ToString();
		otherButton.onClick.AddListener(delegate { DisplayItems(item.EnumItemType); });
	}
	private void AddButtonItem(Item item, uint nb_item)
	{
		Button button = Instantiate(buttonPrefab);
		button.transform.SetParent(itemListContent);
		button.GetComponentInChildren<Text>().text = nb_item > 1 ? item.ItemName + " (" + nb_item + ")" : item.ItemName;
		button.onClick.AddListener(delegate { DisplayItemCharacteristics(item); });
	}

	private void DisplayAll()
	{
		ClearItemListContent();

		DisplayListOfItems(items);
	}
	private void DisplayItems(Item.ItemType type)
	{
		ClearItemListContent();

		Dictionary<Item, uint> itemsToDisplay = GetItemsByType(type);

		DisplayListOfItems(itemsToDisplay);
	}
	private void DisplayListOfItems(Dictionary<Item, uint> itemsToDisplay)
	{

		foreach (Item item in itemsToDisplay.Keys)
		{
			AddButtonItem(item, itemsToDisplay[item]);
		}
	}

	private void DisplayItemCharacteristics(Item item)
	{
		itemCharaPanel.Display(item);
	}

	private Button FindButtonInItemListWithText(string text)
	{
		for (int idx = 0; idx < itemListContent.childCount; ++idx)
		{
			string buttonText = itemListContent.GetChild(idx).GetComponentInChildren<Text>().text;

			if (buttonText.IndexOfAny("(".ToCharArray()) != -1)
				buttonText = buttonText.Substring(0, buttonText.IndexOfAny("(".ToCharArray()) - 1);

			if (buttonText == text)
				return itemListContent.GetChild(idx).GetComponent<Button>();
		}

		return null;
	}

	private void ClearItemListContent()
	{
		for (int idx = itemListContent.childCount - 1; idx >= 0; --idx)
			Destroy(itemListContent.GetChild(idx).gameObject);
	}
	private void ClearFilterListContent()
	{
		for (int idx = filterContent.childCount - 1; idx >= 0; --idx)
			Destroy(filterContent.GetChild(idx).gameObject);
	}

	public Dictionary<Item, uint> GetItemsByType(Item.ItemType type)
	{
		Dictionary<Item, uint> itemsOfType = new Dictionary<Item, uint>(new ItemComparer());

		foreach (Item item in items.Keys)
			if (item.EnumItemType == type)
				itemsOfType.Add(item, items[item]);

		return itemsOfType;
	}

	public void AddItem(Item itemToAdd)
	{
		if (curWeight < maxWeight)
		{
			if (items.ContainsKey(itemToAdd))
				++items[itemToAdd];
			else
				items.Add(itemToAdd, 1);

			curWeight += itemToAdd.Weight;
			lowerBar.ChangeWeightLabel(curWeight, maxWeight);
		}
	}
	public void RemoveItem(Item itemToRemove)
	{
		if (!items.ContainsKey(itemToRemove))
			return;

		if (items[itemToRemove] > 1)
		{
			--items[itemToRemove];
			Button button = FindButtonInItemListWithText(itemToRemove.ItemName);
			if (button)
				button.GetComponentInChildren<Text>().text = items[itemToRemove] > 1 ? itemToRemove.ItemName + " (" + items[itemToRemove] + ")"  : itemToRemove.ItemName;
		}
		else
		{
			items.Remove(itemToRemove);
			Button buttonToDestroy = FindButtonInItemListWithText(itemToRemove.ItemName);
			if (buttonToDestroy)
				Destroy(buttonToDestroy.gameObject);
		}

		curWeight -= itemToRemove.Weight;

		lowerBar.ChangeWeightLabel(curWeight, maxWeight);
	}
}
