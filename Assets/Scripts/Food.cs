using UnityEngine;

public class Food : MonoBehaviour
{

	private SpriteRenderer _sRendrer;
	private Collider2D _collider;

	[SerializeField] private GameObject _particles;
	[SerializeField] private GameObject _explosion;
	
	// Use this for initialization
	void Start ()
	{
		_collider = GetComponent<Collider2D>();
		_sRendrer = GetComponent<SpriteRenderer>();
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag.Equals("Player")){
			
			_collider.enabled = false;
			_sRendrer.enabled = false;
			
			Instantiate(_explosion, transform.position, Quaternion.identity);
			Instantiate(_particles, transform.position, Quaternion.identity);
			
			Invoke("MakeMeAlive", 0.5f);
		}
		
	}


	void MakeMeAlive(){ // function to make the food bullet appear in a random place 

		var yPos = Random.Range(-4.8f, 4.8f);
		var xPos = Random.Range(-2.5f, 2.5f);
		
		transform.position = new Vector2(xPos, yPos);
		_collider.enabled = true;
		_sRendrer.enabled = true;
	}
}
