using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryLowerBar : MonoBehaviour
{
	[SerializeField] private Text damagesLabel;
	[SerializeField] private Text modifierLabel;
	[SerializeField] private Text weightLabel;
	[SerializeField] private Text goldLabel;

	public void ChangeDamages(int damages)
	{
		damagesLabel.text = "Damages : " + damages;
	}

	public void ChangeModifierLabel(int modifier)
	{
		modifierLabel.color = modifier > 0 ? Color.green : Color.red;

		modifierLabel.text = "( " + modifier + " )";
	}

	public void ChangeWeightLabel(uint curWeight, uint maxWeight)
	{
		weightLabel.text = "Weight : " + curWeight + " / " + maxWeight;
	}

	public void ChangeGoldLabel(uint gold)
	{
		goldLabel.text = "Gold : " + gold;
	}
}
