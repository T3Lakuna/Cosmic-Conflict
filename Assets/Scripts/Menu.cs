public class Menu : Photon.Pun.MonoBehaviourPunCallbacks {
	public UnityEngine.GameObject mainMenu;
	public UnityEngine.GameObject playMenu;
	public UnityEngine.GameObject championSelectMenu;
	public UnityEngine.GameObject mainPlayButton;
	public UnityEngine.GameObject mainExitButton;
	public UnityEngine.GameObject playQueueButton;
	public UnityEngine.GameObject playPracticeButton;
	public UnityEngine.GameObject playBackButton;
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
	private string _selectedChampionInstantiatePath;
	
	private Photon.Realtime.RoomOptions RoomOptions = new Photon.Realtime.RoomOptions { IsOpen = true, IsVisible = true, CleanupCacheOnLeave = false, MaxPlayers = 10 };
	
	private void Start() {
		if (UnityEngine.Application.internetReachability != UnityEngine.NetworkReachability.NotReachable) { Photon.Pun.PhotonNetwork.ConnectUsingSettings(); }
	}
	
	public override void OnConnectedToMaster() { Photon.Pun.PhotonNetwork.AutomaticallySyncScene = true; }
	
	public void MainPlayButtonAction() {
		this.mainMenu.SetActive(false);
		this.playMenu.SetActive(true);
	}

	public void MainExitButtonAction() {
		UnityEngine.Application.Quit();
	}

	public void PlayQueueButton() {
		// TODO
	}

	public void PlayPracticeButton() {
		this.playMenu.SetActive(false);
		this.championSelectMenu.SetActive(true);
	}

	public void PlayBackButton() {
		this.mainMenu.SetActive(true);
		this.playMenu.SetActive(false);
	}

	public void ChampionSelectAction(string championInstantiatePath) { this._selectedChampionInstantiatePath = championInstantiatePath; }
}