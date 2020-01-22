public class Menu : Photon.Pun.MonoBehaviourPunCallbacks {
	public UnityEngine.GameObject mainMenu;
	public UnityEngine.GameObject playMenu;
	public UnityEngine.GameObject championSelectMenu;
	public UnityEngine.GameObject mainPlayButton;
	public UnityEngine.GameObject mainExitButton;
	public UnityEngine.GameObject playQueueButton;
	public UnityEngine.GameObject playPracticeButton;
	public UnityEngine.GameObject playBackButton;
	public TMPro.TMP_Text queueText;
	public UnityEngine.GameObject championSelectChefNorrisSelectButton;
	public UnityEngine.GameObject championSelectAlliskatorSelectButton;
	public UnityEngine.GameObject championSelectJaydenSelectButton;
	public UnityEngine.GameObject championSelectVespaSelectButton;
	public UnityEngine.GameObject championSelectAtaraxiaSelectButton;
	public UnityEngine.GameObject championSelectCaptainArmstrongSelectButton;
	public UnityEngine.GameObject championSelectCryptoSelectButton;
	public UnityEngine.GameObject championSelectRitokongSelectButton;
	public UnityEngine.GameObject championSelectBearFactSelectButton;
	public UnityEngine.GameObject championSelectPenumbraSelectButton;
	public UnityEngine.GameObject championSelectShionSelectButton;
	public UnityEngine.GameObject championSelectSlambowskiSelectButton;
	public UnityEngine.GameObject championSelectJamesSelectButton;

	[UnityEngine.HideInInspector] public Player localPlayer;
	[UnityEngine.HideInInspector] public string championModelPath;
	[UnityEngine.HideInInspector] public int joinOrder;

	private bool starting;

	private Photon.Realtime.RoomOptions RoomOptions = new Photon.Realtime.RoomOptions {IsOpen = true, IsVisible = true, CleanupCacheOnLeave = false, MaxPlayers = 1}; // TODO: Ten players.

	private void Start() {
		this.starting = false;
		if (UnityEngine.Application.internetReachability != UnityEngine.NetworkReachability.NotReachable) { Photon.Pun.PhotonNetwork.ConnectUsingSettings(); }
	}

	public override void OnConnectedToMaster() { Photon.Pun.PhotonNetwork.AutomaticallySyncScene = true; }

	public void MainPlayButtonAction() {
		this.mainMenu.SetActive(false);
		this.playMenu.SetActive(true);
	}

	public void MainExitButtonAction() { UnityEngine.Application.Quit(); }

	public void PlayQueueButton() { Photon.Pun.PhotonNetwork.JoinOrCreateRoom("Default", this.RoomOptions, Photon.Pun.PhotonNetwork.CurrentLobby); }

	public void PlayPracticeButton() {
		this.playMenu.SetActive(false);
		this.championSelectMenu.SetActive(true);
	}

	public void PlayBackButton() {
		this.mainMenu.SetActive(true);
		this.playMenu.SetActive(false);
	}

	public void ChampionSelectAction(string championInstantiatePath) { this.championModelPath = championInstantiatePath; }

	private void Update() {
		if (Photon.Pun.PhotonNetwork.InRoom) {
			if (Photon.Pun.PhotonNetwork.CurrentRoom.PlayerCount == Photon.Pun.PhotonNetwork.CurrentRoom.MaxPlayers && !this.starting) {
				this.starting = true;
				this.mainMenu.SetActive(false);
				this.playMenu.SetActive(false);
				this.championSelectMenu.SetActive(true);
				if (Photon.Pun.PhotonNetwork.IsMasterClient) { this.StartCoroutine(this.HostLoadMainScene()); }
			} else { this.queueText.text = "In Queue: " + Photon.Pun.PhotonNetwork.CurrentRoom.PlayerCount + " / " + Photon.Pun.PhotonNetwork.CurrentRoom.MaxPlayers; }
		}
	}

	public System.Collections.IEnumerator HostLoadMainScene() {
		UnityEngine.Debug.Log("Test 1");
		yield return new UnityEngine.WaitForSeconds(15);
		UnityEngine.Debug.Log("Test 2");
		if (Photon.Pun.PhotonNetwork.InRoom) { Photon.Pun.PhotonNetwork.LoadLevel("Main"); } else { UnityEngine.SceneManagement.SceneManager.LoadScene("Main"); }

		UnityEngine.Debug.Log("Test 3");
	}

	public override void OnJoinedRoom() {
		base.OnJoinedRoom();
		this.localPlayer = Tools.Instantiate("Prefabs/Player", UnityEngine.Vector3.zero).GetComponent<Player>();
	}
}