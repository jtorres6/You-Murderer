using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseClick : MonoBehaviour {
	public GameObject note;
	public GameObject Sphere;
	public Button phone;
	public GameObject Line;
	public bool canIPut = true; 
	public float separation;
	public float lerpFactor;
	private bool isZoomed = false;
	private Vector3 initialPosition;
	private Vector4 target;
	private GameObject Point1;
	private GameObject Point2;
	private string Point1Name;
	private string Point2Name;
	private GameObject FinalLine;
	private RaycastHit hit;
	private int click_order = 0;
	private LineRenderer line;
	GameObject parent;

	void Start(){
		initialPosition = gameObject.transform.position;
	}


	void Update(){
		if (Input.GetButtonDown ("Fire1") && canIPut) {
			Plane plane = new Plane(Vector3.forward,0);
            
 
 			float dist;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
 			if (plane.Raycast(ray, out dist))
 			{	
   				Vector3 point = ray.GetPoint(dist);
				if(click_order == 0){
					if(Physics.Raycast(ray, out hit)){
            			Point1Name = hit.collider.gameObject.name;
						if(Point1Name == "Sphere(Clone)"){
                          //parent.transform = hit.collider.gameObject.transform.parent;
						  Destroy(hit.collider.gameObject.transform.parent.transform.gameObject);
                          click_order = 0;
						  

						}
						else{
							parent = new GameObject("Name");
							Point1 = Instantiate (Sphere, point, Quaternion.identity);
                          	//Point1.name = "esfera1";
							click_order = 1;
                            Point1.transform.parent = parent.transform;
						}
					}
				}
				else if(click_order == 1){
					
					if(Physics.Raycast(ray, out hit)){
						Point2Name = hit.collider.gameObject.name;
						Point2 = Instantiate (Sphere, point, Quaternion.identity);
                        //Point2.name = "esfera2";
						FinalLine = Instantiate(Line, point, Quaternion.identity);
						Vector3 midpoint = (Point1.transform.position + Point2.transform.position) / 2f;
						midpoint = new Vector3(midpoint.x, midpoint.y, midpoint.z -0.1f);
						GameObject newNote = Instantiate (note, midpoint, note.transform.localRotation);
						newNote.transform.parent = Point1.transform.parent ;
                      	Point2.transform.parent = Point1.transform.parent ;
                      	FinalLine.transform.parent = Point1.transform.parent ;
						// Update position of the two vertex of the Line Renderer
						line = FinalLine.gameObject.GetComponent<LineRenderer>();
						line.SetVertexCount(2);
						line.SetPosition(0, Point1.transform.position);
						line.SetPosition(1, Point2.transform.position);

						FinalLine.transform.SetPositionAndRotation(new Vector3(0, 0, 0),Quaternion.identity);
						click_order = 0;
						print(Point1Name + " verbo " + Point2Name);
					}
				}
			}
			 
		}


		//Zoom:
		if (Input.GetButtonDown ("Fire2")) {
			if (!isZoomed) {
				Plane plane = new Plane (Vector3.forward, 0);
				float dist;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (plane.Raycast (ray, out dist)) {	
					Vector3 point = ray.GetPoint (dist);
					target = new Vector3 (point.x, point.y, point.z - separation);
					isZoomed = true;
				}
			} else {
				isZoomed = false;
			}
		}

		if (isZoomed) {
			phone.gameObject.SetActive (false);
			this.transform.position = Vector3.Lerp (this.transform.position, target, lerpFactor);
		} else {
			phone.gameObject.SetActive (true);
			this.transform.position = Vector3.Lerp (this.transform.position, initialPosition, lerpFactor);
		}
	}
}