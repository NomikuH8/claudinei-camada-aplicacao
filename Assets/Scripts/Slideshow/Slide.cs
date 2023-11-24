using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

enum enterAnimation {
	grow, pass_right
}

enum exitAnimation {
	shrink, pass_left
}

public class Slide : MonoBehaviour
{
	[SerializeField]
	private bool interactiveSlide = false;
	[SerializeField]
	private int interactiveTextStart = 1;
	[SerializeField]
	private float activeTextCooldown = .5f;
	[SerializeField]
	private float textAppear = .02f;
	[SerializeField]
	private float imageAppear = .02f;
	[SerializeField]
	private enterAnimation enterAnim;
	[SerializeField]
	private exitAnimation exitAnim;

	private Image[] images;
	private TextMeshProUGUI[] textObjs;
	private string[] texts;

	private int textCursor = 0;
	private int cursor = 0;

	void Awake()
	{
		images = transform.GetComponentsInChildren<Image>();
		for (int i = 0; i < images.Length; i++)
		{
			images[i].gameObject.SetActive(false);
		}

		textObjs = transform.GetComponentsInChildren<TextMeshProUGUI>();
		texts = new string[textObjs.Length];
		for (int i = 0; i < textObjs.Length; i++)
		{
			texts[i] = textObjs[i].text;
			textObjs[i].text = "";
		}
	}


	void OnEnable()
	{
		GetComponent<Animator>().SetBool("enter_grow", enterAnim == enterAnimation.grow);
		GetComponent<Animator>().SetBool("exit_shrink", exitAnim == exitAnimation.shrink);
		GetComponent<Animator>().SetBool("enter_pass_right", enterAnim == enterAnimation.pass_right);
		GetComponent<Animator>().SetBool("exit_pass_left", exitAnim == exitAnimation.pass_left);

		if (!interactiveSlide)
			for (int i = 0; i < textObjs.Length; i++)
				textObjs[i].text = "";

		if (!interactiveSlide)
			StartCoroutine(AppearText());
		else {
			for (int i = interactiveTextStart - 1; i >= 0; i--)
				textObjs[i].text = texts[i];

			textCursor = interactiveTextStart;
			StartCoroutine(AppearOnlyImages());
		}
	}

	void Update() {
		if (interactiveSlide)
			TypeText();
	}

	void DisableSlide()
	{
		gameObject.SetActive(false);
	}

	void TypeText() {
		if (Input.anyKeyDown)
		{
			if (texts[textCursor] != "") {
				try
				{
					textObjs[textCursor].text += texts[textCursor][cursor];
					cursor++;
				}
				catch (System.IndexOutOfRangeException)
				{
					texts[textCursor] = "";
					if (textCursor < texts.Length - 1)
						textCursor++;
					cursor = 0;
				}
			}
		}
	}

	IEnumerator AppearText() {
		yield return new WaitForSeconds(activeTextCooldown);
		for (int i = 0; i < images.Length; i++)
		{
			images[i].gameObject.SetActive(true);
			yield return new WaitForSeconds(imageAppear);
		}
		for (int i = 0; i < textObjs.Length; i++)
		{
			for (int j = 0; j < texts[i].Length; j++) {
				textObjs[i].text += texts[i][j];
				yield return new WaitForSeconds(textAppear);
			}
		}
	}

	IEnumerator AppearOnlyImages() {
		yield return new WaitForSeconds(activeTextCooldown);
		for (int i = 0; i < images.Length; i++)
		{
			images[i].gameObject.SetActive(true);
			yield return new WaitForSeconds(imageAppear);
		}
	}
}
