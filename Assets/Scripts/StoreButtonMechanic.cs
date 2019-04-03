using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreButtonMechanic : MonoBehaviour {
// GameObjects
	[SerializeField] private List<Transform> ltChild;
	private MilkMechanic rMilkMechanic;

// Variables
/// <Summary> Button Name Variable. </Summary>
	private string sName;
/// <Summary> Bought Number Variable. </Summary>
	private float fBought;
/// <Summary> Buying Number Variable. </Summary>
	private float fNumber;
/// <Summary> Buying Cost Variable. </Summary>
	private float fCost;
/// <Summary> Passive Collected Per Second Variable. </Summary>
	private float fCPS;

// Functions
/* Primary Functions */
	private void LateUpdate() {
	// Check if the Cost Amount matches before visually updating it
		if (ltChild.ToArray()[3].GetComponent<TextMeshProUGUI>().text != fCost.ToString("0"))
			ltChild.ToArray()[3].GetComponent<TextMeshProUGUI>().text = fCost.ToString("0");
	// Check if the Bought Amount matches before visually updating it
		if (ltChild.ToArray()[0].GetComponent<TextMeshProUGUI>().text != fBought.ToString("0"))
			ltChild.ToArray()[0].GetComponent<TextMeshProUGUI>().text = fBought.ToString("0");
	}

/* Secondary Functions */
/// <Summary> Set Button Cost Function.
/// <para /> [MilkMechanic: rmilkMechanic]
/// <para /> [string: sname]
/// <para /> [float: fbought]
/// <para /> [float: fnumber]
/// <para /> [float: fcost]
/// <para /> [float: fcps]
/// </Summary>
	public void Initialization(MilkMechanic rmilkMechanic, string sname, float fbought, float fnumber, float fcost, float fcps) {
	// Attach Initialization Values
		rMilkMechanic = rmilkMechanic;

		sName = sname;

		fBought = fbought;
		fNumber = fnumber;
		fCost = fcost;
		fCPS = fcps;

	// Update Prefab Child Objects
		ltChild.ToArray()[5].GetComponent<TextMeshProUGUI>().text = sName;
		ltChild.ToArray()[0].GetComponent<TextMeshProUGUI>().text = fBought.ToString("0");
		ltChild.ToArray()[2].GetComponent<TextMeshProUGUI>().text = fNumber.ToString("0");
		ltChild.ToArray()[3].GetComponent<TextMeshProUGUI>().text = " " + fCost.ToString("0");
	}

/// <Summary> Buy Passive Increment Function. </Summary>
	public void BuyPassive() {
	// Check Cost Deduction first
		if (rMilkMechanic.CostDeduction(fCost)) {
		// Update Cost per scaling
			fCost += fBought + fCPS;
		// Update Bought Number
			fBought += fNumber;
		// Update CPS
			if (rMilkMechanic != null)
				rMilkMechanic.IncreaseCPS(fCPS);
		}
	}

/// <Summary> Get Bought Upgrade Count Function. </Summary>
	public float GetCount() {
		return fBought;
	}
}
