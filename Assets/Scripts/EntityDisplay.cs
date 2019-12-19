using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityDisplay : MonoBehaviour {
	[HideInInspector] public Entity entity;
	public TMP_Text healthText;
	public TMP_Text shieldText;
	public TMP_Text physicalShieldText;
	public TMP_Text magicalShieldText;
	public TMP_Text resourceText;

	public static EntityDisplay CreateEntityDisplay(Entity entity) {
		EntityDisplay display = Instantiate(UnityEngine.Resources.Load<GameObject>("Prefabs/EntityDisplay"), new Vector3(0, 1, 0), Quaternion.identity).GetComponent<EntityDisplay>();
		display.entity = entity;
		display.transform.parent = entity.transform;
		display.transform.localPosition = new Vector3(0, 1, 0);
		display.transform.localScale = new Vector3(1, 1, 1);
		return display;
	}

	void Update() { this.transform.LookAt(MatchManager.Instance.localPlayer.playerCamera.transform); }
}
