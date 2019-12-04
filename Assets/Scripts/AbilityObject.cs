using UnityEngine;

public class AbilityObject : MonoBehaviour {
	public Ability ability;

	private void OnCollisionEnter(Collision collision) {
		Entity entity = collision.gameObject.GetComponent<Entity>();

		if (!entity) { return; }

		entity.AddToRecap(this.ability);
		this.ability.Action();
	}
}