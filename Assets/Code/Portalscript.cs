using UnityEngine;
using System.Collections;

public class Portalscript : MonoBehaviour {

    public GameObject player;
    public Camera POV;
    private Quaternion offset;
	private Rigidbody rb;


    void Start ()
    {
		rb = GetComponent<Rigidbody>();
		Debug.Log("test");
    }
    
    void LateUpdate ()
    {
        POV.transform.rotation = player.transform.rotation;
    }
	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Entra");
		if (other.gameObject.CompareTag ("Player"))
        {
			Debug.Log("Colliider");
			other.gameObject.transform.position = POV.transform.position;
        }
	}
}
