using UnityEngine;

public class AbilityObject : MonoBehaviour {
	Ability ability;
	private void OnCollisionEnter(Collision collision) {
		Entity entity = collision.gameObject.GetComponent<Entity>();
		if (entity) {
			entity.ApplyAbility(ability);
		}
	}
}
