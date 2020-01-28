using Photon.Pun;
using UnityEngine;
using static Entity;

public class Player : Photon.Pun.MonoBehaviourPun, Photon.Pun.IPunObservable {
	private const int fuzzyClickRange = 30;

	public static Player localPlayer;
	[HideInInspector] public Champion champion;
	[HideInInspector] public UnityEngine.Camera playerCamera;
	[HideInInspector] public Team team;
	[HideInInspector] public int uniqueUserId;
	[HideInInspector] public string championModelPath;

	private void Awake() {
		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }
		if (!Player.localPlayer) {
			Player.localPlayer = this;
			GameObject.DontDestroyOnLoad(this);
		} else {
			if (Player.localPlayer != this) { Tools.Destroy(this.gameObject); }
		}
	}

	private void Start() {
		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }
		this.championModelPath = "Entities/Champions/Chef Norris/Model"; // Chef Norris is the default champion.
		this.uniqueUserId = (int) (Tools.Random() * 99999);
	}

	private void Update() {
		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }
		if (!MatchManager.Instance) { return; }

		if (!this.playerCamera) { this.playerCamera = Camera.main; }
		if (this.champion == null) { this.champion = Tools.Instantiate(this.championModelPath, Tools.StartingPositionForTeam(this.team)).GetComponent<Champion>(); }

		this.PositionCamera();
	}

	public RaycastHit RaycastOnLayer(LayerMask layerMask) {
		UnityEngine.Physics.Raycast(this.playerCamera.ScreenPointToRay(UnityEngine.Input.mousePosition), out RaycastHit raycastHit, float.MaxValue, layerMask);
		return raycastHit;
	}

	public Entity FuzzyMouseTargetEntity(bool includeEnemies, bool includeAllies, bool includeSelf, bool includeChampions, bool includeStructures, bool includeOtherEntities) {
		UnityEngine.Physics.Raycast(this.playerCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit, float.MaxValue, MatchManager.Instance.mapLayerMask);
		Collider[] hitColliders = Physics.OverlapSphere(raycastHit.point, 10, MatchManager.Instance.entityLayerMask);
		Entity closestEntity = null;
		foreach (Collider hitCollider in hitColliders) {
			if (Vector3.Distance(hitCollider.transform.position, raycastHit.point) > Player.fuzzyClickRange) { continue; } // Necessary for some reason.
			Entity collidedEntity = hitCollider.GetComponent<Entity>();
			if (!includeSelf && collidedEntity == this) { continue; }
			if (!includeEnemies && collidedEntity.team != this.team || !includeAllies && collidedEntity.team == this.team) { continue; }
			Champion collidedChampion = collidedEntity.GetComponent<Champion>();
			Structure collidedStructure = collidedEntity.GetComponent<Structure>();
			if (!includeChampions && collidedChampion || !includeStructures && collidedStructure || !includeOtherEntities && !collidedStructure && !collidedChampion) { continue; }
			if (closestEntity == null) { closestEntity = collidedEntity; }
			if (Vector3.Distance(this.transform.position, closestEntity.transform.position) < Vector3.Distance(this.transform.position, collidedEntity.transform.position)) { continue; }
			closestEntity = collidedEntity;
		}
		return closestEntity;
	}

	private void PositionCamera() { this.playerCamera.transform.position = new Vector3(this.champion.transform.position.x, this.champion.transform.position.y + 50, this.champion.transform.position.z - 35); } // For camera angle (60, 0, 0). TODO: Smart Camera

	public void OnPhotonSerializeView(Photon.Pun.PhotonStream stream, Photon.Pun.PhotonMessageInfo info) { }
}