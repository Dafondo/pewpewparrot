using UnityEngine;
using System.Collections;

public class AttackDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 1.5f);
    }
}
