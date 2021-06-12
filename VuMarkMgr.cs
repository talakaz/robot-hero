using UnityEngine;


[AddComponentMenu("")]
public class VuMarkMgr : MonoBehaviour {

	public VuMarkHandler vuMark = null;

	private void Start() {
		
		GameObject obj = GameObject.Instantiate(vuMark.gameObject) as GameObject;
		obj.transform.SetParent(transform, false);
		#if ! UNITY_EDITOR
//		GameObject obj = GameObject.Instantiate(vuMark.gameObject) as GameObject;
//		obj.transform.SetParent(transform, false);
		#endif
	}
}
