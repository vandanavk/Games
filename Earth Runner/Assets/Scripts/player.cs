using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Advertisements;

public class player : MonoBehaviour {

	private Rigidbody rbody;
	public float speed =1.0f;
	public Text countText;
	public Text EOGText;
	private int count;
	public cameracontroller cam;
	private bool isGrounded = true;
	public LayerMask WhatISGround;
	private float startTime;
	private float endTime;
	private Vector3 startPos;
	private Vector3 endPos;
	private float swipeDistanceVertical;
	private float swipeDistanceHorizontal;
	private float swipeTime;
	private float maxTime =0.5f;
	private float minSwipeDist = 50f;
	private Text life;
	private int l=3;
	[SerializeField] string gameID = "1244981";

	void setScore() {
		countText.text = "Score: " + count.ToString();
	}

	void Awake()
	{
		Advertisement.Initialize (gameID, true);
	}

	public void ShowAd(string zone = "")
	{
		#if UNITY_EDITOR
		StartCoroutine(WaitForAd ());
		#endif

		if (string.Equals (zone, ""))
			zone = null;

		ShowOptions options = new ShowOptions ();
		options.resultCallback = AdCallbackhandler;

		if (Advertisement.IsReady (zone))
			Advertisement.Show (zone, options);
	}

	void AdCallbackhandler (ShowResult result)
	{
		switch(result)
		{
		case ShowResult.Finished:
			Debug.Log ("Ad Finished. Rewarding player...");
			l = 1;
			if (l > 0) {
				life.text = l.ToString ();
				gameObject.SetActive (true);
				EOGText.text = "";
			}
			break;
		case ShowResult.Skipped:
			Debug.Log ("Ad skipped. Son, I am dissapointed in you");
			break;
		case ShowResult.Failed:
			Debug.Log("I swear this has never happened to me before");
			break;
		}
	}

	IEnumerator WaitForAd()
	{
		float currentTimeScale = Time.timeScale;
		Time.timeScale = 0f;
		yield return null;

		while (Advertisement.isShowing)
			yield return null;

		Time.timeScale = currentTimeScale;
	}

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody> ();
		count = 0;
		setScore ();
		EOGText.text = "";
		life = GameObject.FindGameObjectWithTag ("life").GetComponentInChildren<Text>();
		l = 3;
		life.text = l.ToString();
	}

	void FixedUpdate() {
		Vector3 delta = new Vector3 (Input.GetAxis ("Horz"), Input.GetAxis("Jump"), Input.GetAxis ("Vert"));

		if (Physics.CheckSphere (transform.position, .5f, WhatISGround))
			isGrounded = true;
		else
			isGrounded = false;
		
		if (Input.GetKeyDown ("space") && isGrounded) {
			rbody.AddForce (new Vector3(0,Input.GetAxis("Jump"),0)*3, ForceMode.Impulse);
			isGrounded = false;
		} else {
			rbody.AddForce (delta * speed, ForceMode.Acceleration);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 delta = new Vector3 (0, 0, 1);

		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began) {
				startTime = Time.time;
				startPos = touch.position;
			} else if (touch.phase == TouchPhase.Ended) {
				endTime = Time.time;
				endPos = touch.position;
				swipeDistanceVertical = (new Vector3 (0, endPos.y, 0) - new Vector3 (0, startPos.y, 0)).magnitude;
				if (swipeDistanceVertical > minSwipeDist) {
					float swipeValue = Mathf.Sign (endPos.y - startPos.y);
					if (swipeValue > 0) {
						Debug.Log("jump");//up swipe
						if (Physics.CheckSphere (transform.position, .5f, WhatISGround))
							isGrounded = true;
						else
							isGrounded = false;

						if (isGrounded) {
							rbody.AddForce (new Vector3 (0, 3, 0) * 3, ForceMode.Impulse);
						}
					}
				}

				swipeDistanceHorizontal = (new Vector3 (endPos.x, 0, 0) - new Vector3 (startPos.x, 0, 0)).magnitude;
				if (swipeDistanceHorizontal > minSwipeDist) {
					float swipeValue = Mathf.Sign (endPos.x - startPos.x);
					if (swipeValue > 0) {//right swipe
						Debug.Log ("right");
						//delta += new Vector3 (swipeDistanceHorizontal/10, 0, 0);
						delta += Vector3.right*30f;
					} else if (swipeValue < 0) {//left swipe
						Debug.Log ("right");
						//delta += new Vector3 ((-1)*swipeDistanceHorizontal/10, 0, 0);
						delta += Vector3.left*30f;
					}

				}
			}
		}
		rbody.AddForce (delta * speed, ForceMode.Acceleration);
	}


	void OnTriggerEnter(Collider other)
	{   
		if (other.gameObject.CompareTag ("food")) {
			Destroy (other.gameObject);
			count = count + 1;
		} else if (other.gameObject.CompareTag ("enemy")) {
			l--;
			if (l == 0) {
				gameObject.SetActive (false);
				EOGText.text = "Game Over!!! Score: " + count.ToString ();
				ShowAd ("rewardedVideo");
			}
			life.text = l.ToString ();
			other.gameObject.SetActive(false);
		} else if (other.gameObject.CompareTag ("bonus_food")) {
			Destroy (other.gameObject);
			count = count + 5;
		} 
		setScore ();
	} 
		
}
