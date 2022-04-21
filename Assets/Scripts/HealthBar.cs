using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	[Tooltip("bar fill image")]
	public Image fill;
	public Image alertImage;
	[Tooltip("current health (-1, 1)")]
	public float health;
	[Tooltip("object for detecting entry to red side. must have collider enabled and have Is Trigger set")]
	public Collider redSide;
	[Tooltip("object for detecting entry to blue side. must have collider enabled and have Is Trigger set")]
	public Collider blueSide;
	[Tooltip("time for bar to go from empty to full in seconds")]
	public float fillTime = 5f;
	[Tooltip("time for bar to go from full to empty in seconds")]
	public float drainTime = 5f;
	public float secondaryFillTime = 5f;
	public float secondaryDrainTime = 2f;
	public float alertFlashFrequency = 2f;
	public float alertAlphaHigh = 1f;
	public float alertAlphaLow = 0f;
	private bool side; //true if blue
	/*private float leftWarnMin = 0.85f;
	private float leftStopMax = 0.9f;
	private float rightStopMin = -0.85f;
	private float rightStopMax = -0.9f;*/
	private const float warnStart = 0.9f;
	private float alertAlpha = 1f;
	private float alertFlashTimer;
	private bool alertFlash = false;
	//public Player player;
	// Start is called before the first frame update
	void Start() {
		//
	}

	// Update is called once per frame
	void Update() {
		if(side) {
			if(health < 0) {
				if(Math.Abs(health) > warnStart) health += (Time.deltaTime / secondaryDrainTime) / 10f;
				else health += Time.deltaTime / drainTime;
			}
			else {
				if(Math.Abs(health) > warnStart) health += (Time.deltaTime / secondaryFillTime) / 10f;
				else health += Time.deltaTime / fillTime;
			}

			if(health > 1) health = 1;
		}
		else {
			if(health > 0) {
				if(Math.Abs(health) > warnStart) health -= (Time.deltaTime / secondaryDrainTime) / 10f;
				else health -= Time.deltaTime / drainTime;
			}
			else {
				if(Math.Abs(health) > warnStart) health -= (Time.deltaTime / secondaryFillTime) / 10f;
				else health -= Time.deltaTime / fillTime;
			}
			/*if(health > 0) health -= Time.deltaTime / drainTime;
			else health -= Time.deltaTime / fillTime;*/

			if(health < -1) health = -1;
		}

		if(alertFlash) {
			alertFlashTimer += Time.deltaTime;
			if(alertFlashTimer > (0.5 / alertFlashFrequency)) {

				if(alertAlpha >= alertAlphaHigh) {
					alertAlpha = alertAlphaLow;
				}
				else if(alertAlpha <= alertAlphaLow) {
					alertAlpha = alertAlphaHigh;
				}
				alertFlashTimer = 0;
			}
		}
		else alertFlashTimer = 0;

		UpdateHealthBar(Mathf.Clamp(health, -1f, 1f));
	}

	void OnTriggerEnter(Collider other) {
		if(other.transform.CompareTag("redSideDetect")) side = false;

		if(other.transform.CompareTag("blueSideDetect")) side = true;
	}

	public void UpdateHealthBar(float health) {
		if(health <= 0) {
			fill.color = new Color(1f, 1-Math.Abs(health), 1-Math.Abs(health), 1f);
			fill.fillOrigin = 0;
			fill.fillAmount = Math.Abs(health);
		}
		else {
			fill.color = new Color(1-Math.Abs(health), 1-Math.Abs(health), 1f, 1f);
			fill.fillOrigin = 1;
			fill.fillAmount = Math.Abs(health);
		}

		if(Math.Abs(health) > warnStart) {
			alertFlash = true;
			if(health > 0) alertImage.color = new Color(0f, 0f, 1f, alertAlpha);
			else alertImage.color = new Color(1f, 0f, 0f, alertAlpha);
		}
		else {
			alertImage.color = new Color(0f, 0f, 0f, 0f);
			alertFlash = false;
		}
	}
}
