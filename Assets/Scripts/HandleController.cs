using UnityEngine;
using UnityEngine.InputSystem;

public class HandleController : MonoBehaviour
{
	[SerializeField] GameObject mainCamera;
	[SerializeField] GameObject focusPointCamera;
	[SerializeField] GameObject sharpner;

	[SerializeField] AudioClip se1;
	[SerializeField] AudioClip se2;
	[SerializeField] AudioClip se3;
	[SerializeField] AudioClip se4;
	[SerializeField] AudioClip se5;
	[SerializeField] AudioClip se6;

	[SerializeField] float rotateS;
	[SerializeField] float shakingRange;
	float rotate = 0;
	float randRotateS;
	float randStopCount = 0;

	[SerializeField]int shakingCountMax;
	[SerializeField]int cameraCountMax;
	[SerializeField]int seRange;

	int shakingCount;
	int cameraCount;
	int randCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		shakingCount = shakingCountMax;
		cameraCount = cameraCountMax;
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
			cameraCount--;
			//Debug.Log(transform.eulerAngles);
			//Debug.Log(rotate);

			if (Random.Range(0, seRange) == 1)AudioSource.PlayClipAtPoint(se1, transform.position);
			if (Random.Range(0, seRange) == 1)AudioSource.PlayClipAtPoint(se2, transform.position);
			if (Random.Range(0, seRange) == 1)AudioSource.PlayClipAtPoint(se3, transform.position);
			//if (Random.Range(0, seRange) == 1)AudioSource.PlayClipAtPoint(se4, transform.position);
			//if (Random.Range(0, seRange) == 1)AudioSource.PlayClipAtPoint(se5, transform.position);
			//if (Random.Range(0, seRange) == 1)AudioSource.PlayClipAtPoint(se6, transform.position);
		}

		if (shakingCount <= 0)
		{
			if(randRotateS!=0)sharpner.transform.eulerAngles = new Vector3(Random.Range(-90-shakingRange, -90+shakingRange), Random.Range(-shakingRange, shakingRange), Random.Range(-shakingRange, shakingRange));
			shakingCount = shakingCountMax;
		}

		if (cameraCount <= 0)
		{
			int tmpC = randCamera;

			do{randCamera = (Random.Range(0, 5));
			}while (randCamera == tmpC);

			if (randCamera == 0) mainCamera.transform.position = new Vector3(2.5f, Random.Range(2f, 4f), 3);
			if (randCamera == 1) mainCamera.transform.position = new Vector3(-4, Random.Range(2f, 4f), 2.5f);
			if (randCamera == 2) mainCamera.transform.position = new Vector3(-1.7f, Random.Range(2f, 4f), 1.2f);
			if (randCamera == 3) mainCamera.transform.position = new Vector3(-4.2f, Random.Range(2f, 4f), -0.4f);
			if (randCamera == 4) mainCamera.transform.position = new Vector3(2.2f, Random.Range(2f, 4f), -1.8f);
			mainCamera.transform.LookAt(focusPointCamera.transform.position);

			cameraCount = cameraCountMax;
		}
		if (rotate > 360) rotate -= 360;

		transform.eulerAngles = new Vector3(rotate, 0,0);
    }
}
