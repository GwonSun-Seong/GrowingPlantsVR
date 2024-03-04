using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public FadeScreen fadeScreen;
    public int sceneIndex;

    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }
    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeduration);

        //SceneManager.LoadScene(sceneIndex);
    }
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			GoToScene(sceneIndex); 
		}
	}
}
