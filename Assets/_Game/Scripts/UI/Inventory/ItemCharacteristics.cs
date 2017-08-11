using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemCharacteristics : MonoBehaviour
{
	[SerializeField] private Text itemNameLabel;

	void Start ()
	{
		gameObject.SetActive(false);
	}

	public void Display(Item item)
	{
		gameObject.SetActive(true);

		itemNameLabel.text = item.ItemName.ToUpper();
	}
}
