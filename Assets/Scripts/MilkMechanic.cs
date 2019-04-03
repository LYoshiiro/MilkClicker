using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MilkMechanic : MonoBehaviour {
// GameObjects
	[SerializeField] private ParticleSystem pMilkParticle;
	[SerializeField] private List<TextMeshProUGUI> ltMilkText;

// Variables
/// <Summary> Milk Litre Variable. </Summary>
	private float fMilkLitre;
/// <Summary> Milk Per Click Variable. </Summary>
	private float fMilkClick;
/// <Summary> Milk Collected Per Second Variable. </Summary>
	private float fMilkCPS;
/// <Summary> Milk Collected From CPS Variable. </Summary>
	private float fMilkCollected;
/// <Summary> Timer for Passive Increment Variable. </Summary>
	private float fTimer;

// Functions
/* Primary Functions */
	private void Start() {
	// Set Default Variables
		fMilkLitre		= 0.0f;
		fMilkClick		= 1.0f;
		fMilkCPS		= 0.0f;
		fMilkCollected	= 0.0f;
		fTimer			= 0.0f;
	}

	private void Update() {
	// Update Timer Value
		fTimer += Time.deltaTime;

	// Passive Update Per Second
		if (fTimer >= 1.0f) {
		// Passively Increase Milk Collected
			PassiveIncrement();
			fTimer = 0.0f;
		}
	}

	private void FixedUpdate() {
	// Check if the List of TMP exists
		if (ltMilkText != null) {
		// Show Milk Total Litres
			if      (fMilkLitre == 0.0f) { ltMilkText.ToArray()[0].text = "Empty!"; }
			else if (fMilkLitre == 1.0f) { ltMilkText.ToArray()[0].text = fMilkLitre.ToString() + " litre"; }
			else    { ltMilkText.ToArray()[0].text = Mathf.RoundToInt(fMilkLitre).ToString() + " litres"; }

		// Show Milk Collected Per Second
			if (fMilkCPS != 0.0f) { ltMilkText.ToArray()[1].text = "Per Second: " + fMilkCPS.ToString("0.0"); }
			else				  { ltMilkText.ToArray()[1].text = "Per Second: " + 0; }
		}
	}

/* Secondary Functions */
/// <Summary> Milk Click Function. </Summary>
	public void MilkClick() {
	// Add Milk to Count
		fMilkLitre += fMilkClick;
	// Emit Milk Particle
		if (pMilkParticle != null)
			pMilkParticle.Emit(1);
	}

/// <Summary> Increase Milk Click Value Function. [fValue: Increase Value] </Summary>
	public void IncreaseClick(float fValue) {
	// Increase Milk Click Value
		fMilkClick += fValue;
	}

/// <Summary> Increase Milk Collected Per Second Value Function. [fValue: Increase Value] </Summary>
	public void IncreaseCPS(float fValue) {
	// Increase Milk Collected Per Second Value
		fMilkCPS += fValue;
	}

/// <Summary> Increase Milk Litre Passively Function. </Summary>
	public void PassiveIncrement() {
	// Increase Milk Litre Passively
		fMilkLitre += fMilkCPS;
	// Check Milk Collected State
		if (fMilkCollected >= fMilkClick) {
		// Get Emit Amount
			int iEmit = (int)(fMilkCollected / fMilkClick);
		// Emit Milk Particle
			if (pMilkParticle != null)
				pMilkParticle.Emit(Mathf.Clamp(iEmit, 0, 5));
			// Reset Collected Value
			fMilkCollected = 0.0f;
		}
	// Increase Milk Collected
		fMilkCollected += fMilkCPS;
	}

/// <Summary> Reduce Milk Litre When Upgrade is bought Function. [fValue: Cost Value] </Summary>
	public bool CostDeduction(float fValue) {
	// Decrease Milk Litre
		if (fMilkLitre >= (int)fValue) {
			fMilkLitre -= (int)fValue;
			return true;
		}

		return false;
	}
}
