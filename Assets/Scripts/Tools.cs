using System;
using System.Collections;
using UnityEngine;

public class Tools {
	public IEnumerator DoAfterTime(double delay, Action action) {
		yield return new WaitForSeconds((float) delay);
		action();
	}
}
