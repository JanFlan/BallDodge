using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

	public void ReloadScene(){
		
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
