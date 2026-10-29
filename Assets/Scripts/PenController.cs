using UnityEngine;
using UnityEngine.InputSystem;

public class PenController : MonoBehaviour
{
	[SerializeField] float beforeX;
	[SerializeField] float afterX;
	[SerializeField] bool redPen;

	[SerializeField] AudioClip sePerfect;

	bool penEnd = false;
	float penEndX = 0;
	float penEndY = 0;
	float penRotateX = 0;
	float penRotateZ = 0;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		float pen = HandleController.instance.penCount;
		float penM = HandleController.instance.penCMax;

		if (pen < 1)
		{
			transform.position = new Vector3(beforeX + (pen * 2.2f), 2, 0);
			transform.eulerAngles = new Vector3(0, -90, 0);
		}
		else
		{
			if (Keyboard.current.wKey.IsPressed() && Random.Range(0, 40) == 0)
			{
				transform.position = new Vector3(afterX + (pen / 2000), 2, 0);
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90, Random.Range(0, 0.5f));
			}

			if (Keyboard.current.spaceKey.wasPressedThisFrame && pen >= penM)
			{
				AudioSource.PlayClipAtPoint(sePerfect, transform.position);
				penEnd = true;
			}

			if (penEnd == true)	// ペンの終了動作
			{
				if (penEndX < 2) { penEndX += 0.003f; penRotateZ += 0.1f; }	// 指定の場所まで回転しながら下がる
				if (penRotateX > -90 && penEndX >= 2) { penRotateX -= 0.5f; penRotateZ = 0; }	// 指定の角度まで動く
				if (penRotateX <= -90) penEndY += 0.01f;    // 指定の高さまで上がる
				if (penEndY > 10) { HandleController.instance.penCount = 0; Destroy(gameObject); }	// 消える
				if(redPen==false)transform.position = new Vector3(afterX + (pen / 2000) - penEndX, 2+penEndY, 0);
				else transform.position = new Vector3(afterX + (pen / 2000) - penEndX, 2 + penEndY+penRotateX/70, 0);
				transform.eulerAngles = new Vector3(penRotateX, 90, penRotateZ);
			}
		}
	}
}
