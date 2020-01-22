using UnityEngine;
using static Entity;

public class Player : UnityEngine.MonoBehaviour {
	[HideInInspector] public Champion champion;
	[HideInInspector] public UnityEngine.Camera playerCamera;
	[HideInInspector] public Team team;
	[HideInInspector] public int uniqueUserId;

	private void Start() {
		this.uniqueUserId = (int) (Tools.Random() * 99999);
		this.playerCamera = Camera.main;
	}

	private void Update() {
		if (!champion) { return; }

		this.PositionCamera();
	}

	public RaycastHit RaycastOnLayer(LayerMask layerMask) {
		UnityEngine.Physics.Raycast(this.playerCamera.ScreenPointToRay(UnityEngine.Input.mousePosition), out RaycastHit raycastHit, float.MaxValue, layerMask);
		return raycastHit;
	}

	private void PositionCamera() { this.playerCamera.transform.position = new Vector3(this.champion.transform.position.x, this.champion.transform.position.y + 50, this.champion.transform.position.z - 35); } // For camera angle (60, 0, 0). TODO: Smart Camera
}