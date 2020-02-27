using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovingBoundry : MonoBehaviour
{

	private Rigidbody2D _rB;
	public float _speed = 1;

	[SerializeField] private bool _uD;
	
	private float _additionalSpeed = 0.001f;

	// Use this for initialization
	void Start (){
		
		_rB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (_uD){
			
			_rB.velocity = Vector2.right * _speed;
		}
		else{
			
			_rB.velocity = Vector2.up * _speed;
		}
	}

	
	private void OnTriggerEnter2D(Collider2D other){
		
		if (_uD == false && other.tag.Equals("RLBoundries"))
		{
			
			_speed = -_speed;
			_additionalSpeed = -_additionalSpeed;

			// changing the position on X axis
			var randX = Random.Range(-2.52f, 1.62f);
				
			transform.position = new Vector3(randX, transform.position.y);
				
			// adding to the speed so the game gets diffecuit every x amount of time
			if (Math.Abs(_speed) < 3){
				
				_speed += _additionalSpeed;
			}
			
		}else if (_uD && other.tag.Equals("UDBoundries")){
			
			_speed = -_speed;
			_additionalSpeed = -_additionalSpeed;
				
			// changing the position on Y axis
			var randY = Random.Range(-3.88f, 3.88f);
				
			transform.position = new Vector3(transform.position.x, randY);
				
			// adding to the speed so the game gets diffecuit every x amount of time
			if (Math.Abs(_speed) < 3){
				
				_speed += _additionalSpeed;
			}
		}
	}
}
