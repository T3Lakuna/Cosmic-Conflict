using Photon.Pun;
using System;
using TMPro;

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
	public TMP_Text selectedChampionText;
	public TMP_Text championSelectTimeRemainingText;

	private DateTime championSelectStartTime;
	private int championSelectSeconds;

	private bool starting;
	private bool practiceStart;

	private Photon.Realtime.RoomOptions RoomOptions = new Photon.Realtime.RoomOptions { IsOpen = true, IsVisible = true, CleanupCacheOnLeave = false, MaxPlayers = 2 }; // TODO: Ten players.

	private void Start() {
		this.championSelectSeconds = 15;
		this.starting = false;
		this.practiceStart = false;
		if (UnityEngine.Application.internetReachability != UnityEngine.NetworkReachability.NotReachable) { Photon.Pun.PhotonNetwork.ConnectUsingSettings(); }
	}

	public override void OnConnectedToMaster() {
		Photon.Pun.PhotonNetwork.AutomaticallySyncScene = true;
		this.playQueueButton.SetActive(true);
	}

	public void MainPlayButtonAction() {
		this.mainMenu.SetActive(false);
		this.playMenu.SetActive(true);
	}

	public void MainExitButtonAction() { UnityEngine.Application.Quit(); }

	public void PlayQueueButton() { Photon.Pun.PhotonNetwork.JoinOrCreateRoom("Default", this.RoomOptions, Photon.Pun.PhotonNetwork.CurrentLobby); }

	public void PlayPracticeButton() {
		Player.localPlayer = Tools.Instantiate("Prefabs/Player", UnityEngine.Vector3.zero).GetComponent<Player>();
		this.practiceStart = true;
	}

	public void PlayBackButton() {
		this.mainMenu.SetActive(true);
		this.playMenu.SetActive(false);
	}

	public void ChampionSelectAction(string championInstantiatePath) { Player.localPlayer.championModelPath = championInstantiatePath; }

	private void Update() {
		if (Player.localPlayer && Player.localPlayer.championModelPath != null) { this.selectedChampionText.text = Player.localPlayer.championModelPath; }
		if (this.championSelectStartTime != null) { this.championSelectTimeRemainingText.text = "" + (int) ((TimeSpan.FromSeconds(this.championSelectSeconds) - (DateTime.Now - this.championSelectStartTime)).TotalSeconds); }

		// Start game.
		if (this.practiceStart && !this.starting || PhotonNetwork.InRoom && Photon.Pun.PhotonNetwork.CurrentRoom.PlayerCount == Photon.Pun.PhotonNetwork.CurrentRoom.MaxPlayers && !this.starting) {
			this.starting = true;
			this.mainMenu.SetActive(false);
			this.playMenu.SetActive(false);
			this.championSelectMenu.SetActive(true);
			if (!PhotonNetwork.InRoom || Photon.Pun.PhotonNetwork.IsMasterClient) { this.StartCoroutine(this.HostLoadMainScene()); }
		} else { this.queueText.text = PhotonNetwork.InRoom ? this.queueText.text = "In Queue: " + Photon.Pun.PhotonNetwork.CurrentRoom.PlayerCount + " / " + Photon.Pun.PhotonNetwork.CurrentRoom.MaxPlayers : "Not in Queue"; }
	}

	public System.Collections.IEnumerator HostLoadMainScene() {
		this.championSelectStartTime = DateTime.Now;
		yield return new UnityEngine.WaitForSeconds(this.championSelectSeconds);
		if (Photon.Pun.PhotonNetwork.InRoom) { Photon.Pun.PhotonNetwork.LoadLevel("Main"); } else { UnityEngine.SceneManagement.SceneManager.LoadScene("Main"); }
	}

	public override void OnJoinedRoom() {
		base.OnJoinedRoom();
		Player.localPlayer = Tools.Instantiate("Prefabs/Player", UnityEngine.Vector3.zero).GetComponent<Player>();
		Player.localPlayer.team = PhotonNetwork.CurrentRoom.PlayerCount % 2 == 0 ? Entity.Team.Blue : Entity.Team.Red;
	}
}