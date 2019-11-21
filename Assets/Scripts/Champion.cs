public abstract class Champion : ComplexEntity {
	public Player player;

	private void Update() {
		this.ComplexEntityUpdate();
	}
}