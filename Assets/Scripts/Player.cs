using UnityEngine;
using static Entity;

public class Player : UnityEngine.MonoBehaviour, Photon.Pun.IPunObservable {
	public static Player localPlayer;
	[HideInInspector] public Champion champion;
	[HideInInspector] public UnityEngine.Camera playerCamera;
	[HideInInspector] public Team team;
	[HideInInspector] public int uniqueUserId;

	private void Awake() {
		if (!Player.localPlayer) { Player.localPlayer = this; } else {
			if (Player.localPlayer != this) { Tools.Destroy(this.gameObject); }
		}
	}

	private void Start() { this.uniqueUserId = (int) (Tools.Random() * 99999); }

	private void Update() {
		if (!MatchManager.Instance) { return; }

		if (!this.playerCamera) {
			this.playerCamera = Camera.main;
			Debug.Log("First frame in main scene.");
		}

		if (!champion) { return; }

		this.PositionCamera();
	}

	public RaycastHit RaycastOnLayer(LayerMask layerMask) {
		UnityEngine.Physics.Raycast(this.playerCamera.ScreenPointToRay(UnityEngine.Input.mousePosition), out RaycastHit raycastHit, float.MaxValue, layerMask);
		return raycastHit;
	}

	private void PositionCamera() { this.playerCamera.transform.position = new Vector3(this.champion.transform.position.x, this.champion.transform.position.y + 50, this.champion.transform.position.z - 35); } // For camera angle (60, 0, 0). TODO: Smart Camera

	public void OnPhotonSerializeView(Photon.Pun.PhotonStream stream, Photon.Pun.PhotonMessageInfo info) { }
}