using UnityEngine;

public class Title : MonoBehaviour {
	public GameObject titleScreen;
	public GameObject heroScreen;
	public GameObject optionScreen;
	public GameObject hero1;

	private void Start() {
		this.titleScreen.gameObject.SetActive(true);
		this.heroScreen.gameObject.SetActive(false);
	}

	public void ToHeroScreen() {
		this.titleScreen.gameObject.SetActive(false);
		this.heroScreen.gameObject.SetActive(true);
	}

	public void ToTitleScreen() {
		this.titleScreen.gameObject.SetActive(true);
		this.heroScreen.gameObject.SetActive(false);
		this.optionScreen.gameObject.SetActive(false);
	}

	public void ToOptions() {
		this.titleScreen.gameObject.SetActive(false);
		this.optionScreen.gameObject.SetActive(true);
	}

	public void BackToHero() {
		this.heroScreen.gameObject.SetActive(true);
		this.hero1.gameObject.SetActive(false);
	}

	public void ToHero1() {
		this.heroScreen.gameObject.SetActive(false);
		this.hero1.gameObject.SetActive(true);
	}

	public void quit()
	{
		Application.Quit();
	}
}