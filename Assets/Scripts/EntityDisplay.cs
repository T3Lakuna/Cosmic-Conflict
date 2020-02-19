using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

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
		if (display.entity.entityHeight <= 5) { display.transform.localPosition = new Vector3(0, 2, 0); } else { display.transform.localPosition = new Vector3(0, 0.5f, 0); }
		return display;
	}

	private void Update() {
		this.transform.rotation = Quaternion.identity;
	}
}
