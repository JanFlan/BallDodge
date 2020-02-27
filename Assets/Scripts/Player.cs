using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Camera CamColour;
	
	[SerializeField] private Animator _camAnim;
	[SerializeField] private Animator _deathPanelAnim;
	[SerializeField] private Animator _reloadButtonAnim;
	[SerializeField] private Animator _bestScoreAnim;
	private AudioSource _myAudioSource;
	[SerializeField] private AudioSource _music;
	[SerializeField] private AudioClip _foodHitSound;
	[SerializeField] private AudioClip _deathSound;

	[SerializeField] private TextMeshProUGUI _scoreText;
	[SerializeField] private TextMeshProUGUI _bestScore;

	[SerializeField] private List<MonoBehaviour> _scriptsDisabled;

	private int _score;
	// Use this for initialization

	private void Awake(){

		Time.fixedDeltaTime = 0.02f;
		Time.timeScale = 1;
	}

	void Start (){
		
		_myAudioSource = GetComponent<AudioSource>();
		_myAudioSource.clip = _foodHitSound;
		_camAnim.SetTrigger("CamStart");
	}
	
	// Update is called once per frame
	void Update () {
		
		MovingPlayer();
	}


	void MovingPlayer(){
		var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10); // getting mouse pos
		transform.position = Vector3.MoveTowards(transform.position, mousePos, 0.1f); // moving towards mouse pos
	}

	private void OnTriggerEnter2D(Collider2D other){

		if (other.tag.Equals("Food")){
			
			_score += 1;
			
			_scoreText.text = " "+ _score;
			
			_myAudioSource.Play();
		
			var randR = Random.Range(0.0f, 1.0f);
			var randG = Random.Range(0.0f, 1.0f);
			var randB = Random.Range(0.0f, 1.0f);
			
			CamColour.backgroundColor = new Color(randR, randG, randB); // setting a random background colour to the cam
			
			_camAnim.SetTrigger("CamShake"); // to play camera shake animation

		}else if (other.tag.Equals("DEATH")){
			if (_score >= PlayerPrefs.GetInt("_best", _score)){
				
				PlayerPrefs.SetInt("_best", _score); // setting the best score
			}
			
//			print("YOU'RE DEAD");

			if (_myAudioSource != null){
				
				_myAudioSource.clip = _deathSound;
				_myAudioSource.Play();
			}

			// achieving the slow mo effect
			Time.fixedDeltaTime = 0.001f;
			Time.timeScale = 0.16f;

			_deathPanelAnim.SetTrigger("Dead");
			_reloadButtonAnim.SetTrigger("ReloadButtonEntrance");
			_bestScoreAnim.SetTrigger("BestScore");

			_music.spatialBlend = 1; // lowering the volume of the music 

			_myAudioSource = null; // to not play death sound twice

			foreach (var script in _scriptsDisabled){
				
				script.enabled = false;
			}

			_bestScore.text = "Best " + PlayerPrefs.GetInt("_best"); // getting the best score to best score text

			
			
		}
	}
}
