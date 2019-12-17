using UnityEngine;
using static Entity;

public class Player : UnityEngine.MonoBehaviour {
	public Champion champion;
	public UnityEngine.Camera playerCamera;
	[UnityEngine.HideInInspector] public UnityEngine.Vector3 mousePosition;
	public Team team;
	public int uniqueUserId;

	private void Start() {
		this.uniqueUserId = (int) (Tools.Random() * 99999);
	}

	private void Update() {
		if (!champion) { return; }

		UnityEngine.Physics.Raycast(this.playerCamera.ScreenPointToRay(UnityEngine.Input.mousePosition), out UnityEngine.RaycastHit raycastHit);
		this.mousePosition = raycastHit.point;
		this.PositionCamera();
	}

	private void PositionCamera() {
		this.playerCamera.transform.position = new Vector3(this.champion.transform.position.x, this.champion.transform.position.y + 50, this.champion.transform.position.z - 35); // For camera angle (60, 0, 0).
																																												  // TODO: Smart Camera
	}
}