using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	public Image fill;
	public float health;
	//public Player player;
	// Start is called before the first frame update
	void Start() {
		//fill.xMin = background.xMin + 10;
	}

	// Update is called once per frame
	void Update() {
		UpdateHealthBar(Mathf.Clamp(health, -1f, 1f));
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
	}
}
