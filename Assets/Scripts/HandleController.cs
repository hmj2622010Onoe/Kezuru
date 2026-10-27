using UnityEngine;
using UnityEngine.InputSystem;

public class HandleController : MonoBehaviour
{
	[SerializeField] GameObject mainCamera;
	[SerializeField] GameObject focusPointCamera;
	[SerializeField] GameObject sharpner;

	[SerializeField] float rotateS;
	[SerializeField] float shakingRange;
	float rotate = 0;
	float randRotateS;
	float randStopCount = 0;

	int shakingCount=50;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		if (randStopCount <= 0){
			randRotateS = (rotateS / Random.Range(30, 500));
			randStopCount = Random.Range(25,55);
			if (Random.Range(0, 5) == 0)randRotateS = 0;
		}

		if (Keyboard.current.wKey.IsPressed()){
			rotate += randRotateS;
			randStopCount -= randRotateS;
			if (randRotateS == 0) randStopCount -= 0.5f;
			shakingCount--;
			Debug.Log(transform.eulerAngles);
			Debug.Log(rotate);
		}

		if (shakingCount <= 0)
		{
			//if(randRotateS!=0)sharpner.transform.eulerAngles = new Vector3(-90 , Random.Range(-shakingRange, shakingRange), Random.Range(-shakingRange, shakingRange));
			shakingCount = 50;
		}
		if (rotate > 720) rotate -= 720;

		transform.eulerAngles = new Vector3(rotate, 0,0);
		transform.eulerAngles = new Vector3(rotate, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
