using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SlideController : MonoBehaviour
{
	public bool isWorking;
	public bool isTransitioning;
	public Transform slideWrapper;
	public GameObject[] slides;

	[SerializeField]
	private Transform toolbar;
	[SerializeField]
	private float toolbarActivationHeight;

	[SerializeField]
	private int currentSlide = 0;
	private int nextSlide;
	private int lastSlide = -1;
	
	private Camera cam;

	void Awake()
	{
		cam = GetComponent<Camera>();

		slides = new GameObject[slideWrapper.childCount];
		for (int i = 0; i < slideWrapper.childCount; i++)
		{
			slides[i] = slideWrapper.GetChild(i).gameObject;
			slides[i].SetActive(false);
		}

		toolbar.Find("slideSlider").GetComponent<Slider>().maxValue = slides.Length - 1;
		toolbar.Find("slideSlider").GetComponent<Slider>().value = currentSlide;
		toolbar.Find("slideTxt").GetComponent<TextMeshProUGUI>().text = "Slide: " + (currentSlide + 1) + "/" + slides.Length;
	}

	void Start() {
		slides[currentSlide].SetActive(true);
		slides[currentSlide].gameObject.SetActive(true);
		slides[currentSlide].GetComponent<Animator>().SetTrigger("enter");
	}

	void Update()
	{
		if (isWorking)
			Run();
	}

	void Run()
	{
		CheckInput();
	}

	public void UpdateSlide(int amount)
	{
		if (currentSlide == 0 && amount < 0)
			return;

		if (currentSlide == slides.Length - 1 && amount > 0)
			return;

		slides[currentSlide].GetComponent<Animator>().SetTrigger("exit");
		lastSlide = currentSlide;
		currentSlide += amount;

		slides[currentSlide].SetActive(true);
		slides[currentSlide].gameObject.SetActive(true);
		slides[currentSlide].GetComponent<Animator>().SetTrigger("enter");
		toolbar.Find("slideTxt").GetComponent<TextMeshProUGUI>().text = "Slide: " + (currentSlide + 1) + "/" + slides.Length;
	}

	public void UpdateSlider(int amount)
	{
		toolbar.Find("slideSlider").GetComponent<Slider>().value = currentSlide + amount;
	}

	void UpdateSlideRaw(int idx)
	{
		slides[currentSlide].GetComponent<Animator>().SetTrigger("exit");
		lastSlide = currentSlide;
		currentSlide = idx;

		slides[currentSlide].SetActive(true);
		slides[currentSlide].gameObject.SetActive(true);
		slides[currentSlide].GetComponent<Animator>().SetTrigger("enter");
	}

	public void SliderChanged()
	{
		int value = (int)toolbar.Find("slideSlider").GetComponent<Slider>().value;
		toolbar.Find("slideTxt").GetComponent<TextMeshProUGUI>().text = "Slide: " + (value + 1) + "/" + slides.Length;
		UpdateSlideRaw(value);
	}

	void CheckInput()
	{
		if (Input.GetButtonDown("right") && currentSlide != slides.Length - 1)
			UpdateSlider(1);
		if (Input.GetButtonDown("left") && currentSlide != 0)
			UpdateSlider(-1);
		
		Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
		toolbar.gameObject.SetActive(cam.ScreenToWorldPoint(mousePos).y > toolbarActivationHeight);
	}

	public void FullscreenBtn()
	{
		Screen.fullScreen = !Screen.fullScreen;
	}

	public void ExitSlideshow()
	{
		SceneManager.LoadScene("Menu");
	}
}
