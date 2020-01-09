using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour {
	public enum Lane { Top, Middle, Bottom }

	public static MatchManager Instance;
	[HideInInspector] public List<Champion> champions;
	[HideInInspector] public DateTime matchStartTime;
	public Player localPlayer;
	public GameObject mapObject;
	public Collider floorCollider;
	public Collider structureBaseCollider;
	public Collider riverBedCollider;
	public Collider wallCollider;
	public TMP_Text kdaText;
	public TMP_Text blueTeamKillsText;
	public TMP_Text redTeamKillsText;
	public TMP_Text matchTimerText;
	public Image passiveIcon;
	public Image primaryIcon;
	public Image secondaryIcon;
	public Image tertiaryIcon;
	public Image ultimateIcon;
	public TMP_Text currencyText;
	public Image championIcon;
	public Image trinketIcon;
	public Image firstItemIcon;
	public Image secondItemIcon;
	public Image thirdItemIcon;
	public Image fourthItemIcon;
	public Image fifthItemIcon;
	public Image sixthItemIcon;
	public Sprite cooldownSprite;
	public Sprite missingSprite;
	public LayerMask mapLayerMask;
	public LayerMask entityLayerMask;
	public LayerMask abilityLayerMask;
	public LayerMask allGameLayerMasks;
	public Inhibitor blueTopInhibitor;
	public Inhibitor blueMiddleInhibitor;
	public Inhibitor blueBottomInhibitor;
	public Inhibitor redTopInhibitor;
	public Inhibitor redMiddleInhibitor;
	public Inhibitor redBottomInhibitor;
	public Vector2 blueTopNexusWaypoint;
	public Vector2 blueTopWaypoint;
	public Vector2 topWaypoint;
	public Vector2 redTopWaypoint;
	public Vector2 redTopNexusWaypoint;
	public Vector2 blueMidWaypoint;
	public Vector2 redMidWaypoint;
	public Vector2 blueBottomNexusWaypoint;
	public Vector2 blueBottomWaypoint;
	public Vector2 bottomWaypoint;
	public Vector2 redBottomWaypoint;
	public Vector2 redBottomNexusWaypoint;
	public Vector2 blueSpawnWaypointTop;
	public Vector2 blueSpawnWaypointTopMiddle;
	public Vector2 blueSpawnWaypointMiddle;
	public Vector2 blueSpawnWaypointBottomMiddle;
	public Vector2 blueSpawnWaypointBottom;
	public Vector2 redSpawnWaypointTop;
	public Vector2 redSpawnWaypointTopMiddle;
	public Vector2 redSpawnWaypointMiddle;
	public Vector2 redSpawnWaypointBottomMiddle;
	public Vector2 redSpawnWaypointBottom;
	public AnimatorController idleAnimation;
	public AnimatorController runAnimation;
	private Vector2[] blueTopPath;
	private Vector2[] blueMiddlePath;
	private Vector2[] blueBottomPath;
	private Vector2[] redTopPath;
	private Vector2[] redMiddlePath;
	private Vector2[] redBottomPath;

	private void Awake() {
		if (!MatchManager.Instance) { MatchManager.Instance = this; } else if (MatchManager.Instance != this) { UnityEngine.Object.Destroy(this); }
	}

	private void Start() {
		this.champions = new List<Champion>();
		this.matchStartTime = DateTime.Now;
		this.blueTopPath = new[] {this.blueTopWaypoint, this.topWaypoint, this.redTopWaypoint, this.redTopNexusWaypoint, this.redBottomNexusWaypoint};
		this.blueMiddlePath = new[] {this.blueMidWaypoint, this.redMidWaypoint, this.redTopNexusWaypoint, this.redBottomNexusWaypoint};
		this.blueBottomPath = new[] {this.blueBottomWaypoint, this.bottomWaypoint, this.redBottomWaypoint, this.redBottomNexusWaypoint, this.redTopNexusWaypoint};
		this.redTopPath = new[] {this.redTopWaypoint, this.topWaypoint, this.blueTopWaypoint, this.blueTopNexusWaypoint, this.blueBottomNexusWaypoint};
		this.redMiddlePath = new[] {this.redMidWaypoint, this.blueMidWaypoint, this.blueTopNexusWaypoint, this.blueBottomNexusWaypoint};
		this.redBottomPath = new[] {this.redBottomWaypoint, this.bottomWaypoint, this.blueBottomWaypoint, this.blueBottomNexusWaypoint, this.blueTopNexusWaypoint};

		this.StartCoroutine(this.SpawnMinionTick());
	}

	private System.Collections.IEnumerator SpawnMinionTick() {
		while (true) {
			yield return new WaitForSeconds(30);
			// TODO
		}
	}

	private void Update() {
		if (!this.localPlayer.champion) { return; }

		this.championIcon.sprite = this.localPlayer.champion.icon;

		this.kdaText.text = this.localPlayer.champion.kills + " / " + this.localPlayer.champion.deaths + " / " + this.localPlayer.champion.deaths;

		int redTeamKills = 0;
		int blueTeamKills = 0;
		foreach (Champion champion in this.champions) {
			switch (champion.team) {
				case Entity.Team.Blue:
					blueTeamKills += champion.kills;
					break;
				case Entity.Team.Red:
					redTeamKills += champion.kills;
					break;
				case Entity.Team.Neutral:
					// LMAO somebody died to a creep.
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		this.blueTeamKillsText.text = "" + blueTeamKills;
		this.redTeamKillsText.text = "" + redTeamKills;

		TimeSpan matchTime = DateTime.Now - this.matchStartTime;
		this.matchTimerText.text = (int) matchTime.TotalMinutes + ":" + (int) (matchTime.TotalSeconds % 60);
		this.passiveIcon.sprite = this.localPlayer.champion.passiveAbility.CurrentCooldown > 0 ? this.cooldownSprite : this.localPlayer.champion.passiveAbility.Icon;
		this.primaryIcon.sprite = this.localPlayer.champion.primaryAbility.CurrentCooldown > 0 ? this.cooldownSprite : this.localPlayer.champion.primaryAbility.Icon;
		this.secondaryIcon.sprite = this.localPlayer.champion.secondaryAbility.CurrentCooldown > 0 ? this.cooldownSprite : this.localPlayer.champion.secondaryAbility.Icon;
		this.tertiaryIcon.sprite = this.localPlayer.champion.tertiaryAbility.CurrentCooldown > 0 ? this.cooldownSprite : this.localPlayer.champion.tertiaryAbility.Icon;
		this.ultimateIcon.sprite = this.localPlayer.champion.ultimateAbility.CurrentCooldown > 0 ? this.cooldownSprite : this.localPlayer.champion.ultimateAbility.Icon;
		this.currencyText.text = "" + this.localPlayer.champion.currency;
		this.trinketIcon.sprite = this.localPlayer.champion.trinket == null ? this.missingSprite : this.localPlayer.champion.trinket.icon;

		Item[] items = this.localPlayer.champion.items.ToArray();
		if (items.Length > 0) { this.firstItemIcon.sprite = items[0].icon; }

		if (items.Length > 1) { this.secondItemIcon.sprite = items[1].icon; }

		if (items.Length > 2) { this.thirdItemIcon.sprite = items[2].icon; }

		if (items.Length > 3) { this.fourthItemIcon.sprite = items[3].icon; }

		if (items.Length > 4) { this.fifthItemIcon.sprite = items[4].icon; }

		if (items.Length > 5) { this.sixthItemIcon.sprite = items[5].icon; }
	}
}