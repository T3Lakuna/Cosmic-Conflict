public class Player : UnityEngine.MonoBehaviour {
	[UnityEngine.HideInInspector] public Champion champion;
	public UnityEngine.Camera playerCamera;
	[UnityEngine.HideInInspector] public UnityEngine.Vector3 mousePosition;

	private void Update() {
		UnityEngine.Physics.Raycast(this.playerCamera.ScreenPointToRay(UnityEngine.Input.mousePosition), out UnityEngine.RaycastHit raycastHit);
		this.mousePosition = raycastHit.point;
	}
}