using UnityEngine;

public class AbilityObject : MonoBehaviour {
	public Ability collisionAbility;
	public Ability updateAbility;
	public bool destroyOnHit;
	public Vector3 target;
	public int movementSpeed;

	private void Update() {
		this.transform.position = Vector3.MoveTowards(this.transform.position, this.target, this.movementSpeed * Time.deltaTime);
		this.updateAbility.Action();
	}

	private void OnCollisionEnter(Collision collision) {
		Entity collisionEntity = collision.gameObject.GetComponent<Entity>();
		if (!collisionEntity) { return; }

		collisionEntity.AddToRecap(this.collisionAbility);
		this.collisionAbility.Action();

		if (this.destroyOnHit) { Tools.Destroy(this.gameObject); }
	}
}