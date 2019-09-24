using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHighlight : MonoBehaviour {

    [SerializeField] private Camera camera;
    [SerializeField] private GameObject rotatingAround;

    private bool currentlyHighlighting;
    private int baseDistance;

    [SerializeField] [Range(0, 5)] private float growTo;

    void Start() {
        currentlyHighlighting = false;
        camera = GameObject.Find("Camera").GetComponent<Camera>();

        baseDistance = 10;
    }

	public void StartHighlight() {
	    currentlyHighlighting = true;
        rotatingAround.transform.localScale = new Vector3(1,1,1);
	}

    void Update() {
        if (currentlyHighlighting) {
            
            rotatingAround.transform.LookAt(camera.transform, Vector3.forward);
            //rotatingAround.transform.Rotate(rotatingAround.transform, Space.Self);

            rotatingAround.transform.Rotate(0, 0, 180);

            //TODO scale to camera distance
            Vector3 scale = new Vector3(growTo, growTo, growTo);

            float distance = Vector3.Distance(rotatingAround.transform.position, camera.transform.position);

            float ratio = (distance / baseDistance) / 2;
            //scale *= ratio;


            //if (scale.x <= growTo) {    
            //    scale.x = scale.y = scale.z += Mathf.Lerp(scale.x, growTo, 25);
            //}
            //Debug.Log(scale.x);

            rotatingAround.transform.localScale = Vector3.Lerp(rotatingAround.transform.localScale, scale, Time.deltaTime*5 );
        }
    }

    public void StopHighlight() {
        currentlyHighlighting = false;
    }
}
