using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreMechanic : MonoBehaviour {
// GameObjects
	[SerializeField] private List<Sprite> lsImage;
	[SerializeField] private MilkMechanic rMilkMechanic;
	[SerializeField] private Transform tPrefab;
	[SerializeField] private Transform tContent;

// Functions
/* Primary Functions */
	private void LateUpdate() {
	// Create Prefab if the Content is Empty
		if (tContent.childCount != lsImage.ToArray().Length) {
			CreatePrefab();
		}
	
	// Check Prefab Bought Count if the Content isn't Empty
		if (tContent.childCount > 0) {
			CountCheck();
		}
	}

/* Secondary Functions */
/// <Summary> Create Buy Button Prefab Function. </Summary>
	private void CreatePrefab() {
	// Create Prefab for Cow Button
		{
			Transform tCow = Instantiate(tPrefab, tContent.position, Quaternion.identity) as Transform;
		// Set Parent GameObject
			tCow.SetParent(tContent);
		// Store Button Instance Initialization
			tCow.GetComponent<StoreButtonMechanic>().Initialization(rMilkMechanic, "Cow", 0, 1, 15, 0.1f);
		// Set Button Sprite
			tCow.GetChild(1).GetComponent<Image>().sprite = lsImage.ToArray()[0];
		// Set GameObject to Active
			tCow.gameObject.SetActive(true);
		}
	
	// Create Prefab for Milk Man Button
		{
			Transform tMilkMan = Instantiate(tPrefab, tContent.position, Quaternion.identity) as Transform;
		// Set Parent GameObject
			tMilkMan.SetParent(tContent);
		// Store Button Instance Initialization
			tMilkMan.GetComponent<StoreButtonMechanic>().Initialization(rMilkMechanic, "Milk Man", 0, 1, 110, 1.0f);
		// Set Button Sprite
			tMilkMan.GetChild(1).GetComponent<Image>().sprite = lsImage.ToArray()[1];
		// Set GameObject to Non-active
			tMilkMan.gameObject.SetActive(false);
		}

	// Create Prefab for Milk Barn Button
		{
			Transform tMilkBarn = Instantiate(tPrefab, tContent.position, Quaternion.identity) as Transform;
		// Set Parent GameObject
			tMilkBarn.SetParent(tContent);
		// Store Button Instance Initialization
			tMilkBarn.GetComponent<StoreButtonMechanic>().Initialization(rMilkMechanic, "Milk Barn", 0, 1, 1215, 7.5f);
		// Set Button Sprite
			tMilkBarn.GetChild(1).GetComponent<Image>().sprite = lsImage.ToArray()[2];
		// Set GameObject to Non-active
			tMilkBarn.gameObject.SetActive(false);
		}
	}

/// <Summary> Check Prefab Bought Count Function. </Summary>
	private void CountCheck() {
	// Int List for child
		List<int> liChild = new List<int>();

	// Get the index for Non-active child
		for (int i = 0; i < tContent.childCount; ++i) {
		// Check for Non-active child
			if (tContent.GetChild(i).gameObject.activeSelf == false)
			// Store Non-active child
				liChild.Add(i);
		}

	// Only proceed if child list isn't empty
		if (liChild.Any()) {
			for (int j = 0; j < liChild.ToArray().Length; ++j) {
				if (tContent.GetChild(liChild.ToArray()[j] - 1).GetComponent<StoreButtonMechanic>().GetCount() >= Mathf.Pow(10, (liChild.ToArray()[j] * 0.5f))) {
					tContent.GetChild(liChild.ToArray()[j]).gameObject.SetActive(true);
					Debug.Log(tContent.GetChild(liChild.ToArray()[j]).name);
				}
			}
		}
	}
}
