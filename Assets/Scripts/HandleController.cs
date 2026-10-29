using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.Burst.Intrinsics.X86;

public class HandleController : MonoBehaviour
{
	public static HandleController instance;

	[SerializeField] GameObject mainCamera;
	[SerializeField] GameObject focusPointCamera;
	[SerializeField] GameObject sharpner;
	[SerializeField] GameObject normalPen;
	[SerializeField] GameObject redPen;

	[SerializeField] AudioClip sePen1;
	[SerializeField] AudioClip sePen2;
	[SerializeField] AudioClip sePen3;

	[SerializeField] AudioClip seRota1;
	[SerializeField] AudioClip seRota2;

	[SerializeField] float rotateS;
	[SerializeField] float shakingRange;
	float rotate = 0;
	float randRotateS;
	float randStopCount = 0;

	public float penCMax = 1000;
	public float penCount = 0;

	[SerializeField]int shakingCountMax;
	[SerializeField]int cameraCountMax;
	[SerializeField]int seRange;

	int shakingCount;
	int cameraCount;
	int randCamera;

	[SerializeField]int testPenMode;

	bool penRotateReady = false;    // penCMax > penCount && penCount < 1
	void Start()
    {
		shakingCount = shakingCountMax;
		cameraCount = cameraCountMax;
    }

    // Update is called once per frame
    void Update()
    {
		if(instance == null)instance= this;

		if (penCount == 0 && Keyboard.current.sKey.wasPressedThisFrame) // ƒyƒ“‚ð—pˆÓ‚·‚é
		{
			if (testPenMode == 1)
			{
				GameObject clonePen = Instantiate(normalPen, sharpner.transform);
				clonePen.transform.position = new Vector3(-6, 2, 0);
				clonePen.transform.eulerAngles = new Vector3(0, -90, 0);
			}
			else if (testPenMode == 2)
			{
				GameObject clonePen = Instantiate(redPen, sharpner.transform);
				clonePen.transform.position = new Vector3(-6, 2, 0);
				clonePen.transform.eulerAngles = new Vector3(0, -90, 0);
			}
			else
			{
				if (Random.Range(0, 4) > 0)
				{
					GameObject clonePen = Instantiate(normalPen, sharpner.transform);
					clonePen.transform.position = new Vector3(-6, 2, 0);
					clonePen.transform.eulerAngles = new Vector3(0, -90, 0);
				}
				else
				{
					GameObject clonePen = Instantiate(redPen, sharpner.transform);
					clonePen.transform.position = new Vector3(-6, 2, 0);
					clonePen.transform.eulerAngles = new Vector3(0, -90, 0);
				}
			}
			rotate = 0;
			penCount = 0.1f;
		}

		if (randStopCount<0){	// ‰ñ“]‘¬“x‚ðƒ‰ƒ“ƒ_ƒ€‚É•ÏX
			randRotateS = (rotateS / Random.Range(30, 500));
			randStopCount = Random.Range(25,55);
			if (Random.Range(0, 5) == 0)randRotateS = 0;	// ‚½‚Ü‚ÉŽ~‚ß‚é
		}

		if (Keyboard.current.wKey.IsPressed()){ // ‰ñ“]‚·‚éˆ—@ðŒ‚Ítest
			if (penRotateReady == false)randRotateS = rotateS / 50; // ƒyƒ“‚ªí‚èŠ®—¹‚µ‚Ä‚¢‚½ê‡•’Ê‚É‰ñ‚é
			rotate += randRotateS;	// Šp“x‚ð‘‚â‚·
			randStopCount -= randRotateS;	// ‰ñ“]‘¬“x‚ª•Ï‚í‚é‚Ü‚Å‚Ì’l‚ði‚ß‚é
			if (randRotateS == 0) randStopCount -= 0.5f;    // ‰ñ“]‘¬“x‚ª0‚ÌŽž‚É‚ài‚ß‚é
			if (penRotateReady==true)
			{
				shakingCount--; // ƒKƒ^ƒKƒ^ƒJƒEƒ“ƒg
				cameraCount--;  // ‰æŠp•ÏXƒJƒEƒ“ƒg
				penCount += 0.1f;   // ƒyƒ“í‚èƒJƒEƒ“ƒg
				if (Random.Range(0, seRange) == 1) AudioSource.PlayClipAtPoint(sePen1, transform.position);
				if (Random.Range(0, seRange) == 1) AudioSource.PlayClipAtPoint(sePen2, transform.position);
				if (Random.Range(0, seRange) == 1) AudioSource.PlayClipAtPoint(sePen3, transform.position);
			}
			else
			{
				if (Random.Range(0, seRange) == 1) AudioSource.PlayClipAtPoint(seRota1, transform.position);
				if (Random.Range(0, seRange) == 1) AudioSource.PlayClipAtPoint(seRota2, transform.position);
			}
		}

		if (penCMax > penCount && penCount > 1) penRotateReady = true;
		else penRotateReady = false;

		if (penRotateReady == false) 
		{
			if (penCount == 0) mainCamera.transform.position = new Vector3(-1.7f, 3, 1.2f);
			if (penCount > 0.9999 && penCount < 1) mainCamera.transform.position = new Vector3(-4.2f, 3, -0.4f);
			if (penCount >= penCMax) mainCamera.transform.position = new Vector3(-4.2f, 3, -0.4f);
			mainCamera.transform.LookAt(focusPointCamera.transform.position);
		}
		if (penRotateReady == false && penCount != 0)
		{
			penCount += 0.001f;
		}

		if (shakingCount <= 0)  // ƒKƒ^ƒKƒ^
		{
			if (randRotateS != 0 || penCMax > penCount || penCount < 1) sharpner.transform.eulerAngles = new Vector3(Random.Range(-90 - shakingRange, -90 + shakingRange), Random.Range(-shakingRange, shakingRange), Random.Range(-shakingRange, shakingRange));
			shakingCount = shakingCountMax;
		}

		if (cameraCount <= 0)	// ‰æŠp•ÏX
		{
			int tmpC = randCamera;

			do{randCamera = (Random.Range(0, 4));
			}while (randCamera == tmpC);	// ‘O‚Æˆá‚¤Šp“x‚É‚·‚é‚½‚ß‚ÌðŒ

			if (randCamera == 0) mainCamera.transform.position = new Vector3(2.5f, Random.Range(2f, 4f), 3);
			if (randCamera == 1) mainCamera.transform.position = new Vector3(-4, Random.Range(2f, 4f), 2.5f);
			if (randCamera == 2) mainCamera.transform.position = new Vector3(-4.2f, Random.Range(2f, 4f), -0.4f);
			if (randCamera == 3) mainCamera.transform.position = new Vector3(2.2f, Random.Range(2f, 4f), -1.8f);
			//if (randCamera == 4) mainCamera.transform.position = new Vector3(-1.7f, Random.Range(2f, 4f), 1.2f);
			mainCamera.transform.LookAt(focusPointCamera.transform.position);

			cameraCount = cameraCountMax;
		}

		if (rotate > 360) rotate -= 360;

		transform.eulerAngles = new Vector3(rotate, 0,0);	// ‰ñ“]‚ð”½‰f
    }
}
