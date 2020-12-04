using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    void Start()
    {
		if (GameManager.instance.HasShowIntro())
		{
			ShowMenu();
			Destroy(gameObject);
		}
		
		else
		{
			StartCoroutine(LetIntroPlay());
		}
    }
	
	private void ShowMenu()
	{
		GameObject.Find("Menu Panel").GetComponent<Animator>().Play("FadeFromIntro");
	}
	
	private IEnumerator LetIntroPlay()
	{
		yield return new WaitForSecondsRealtime(4.5f);
		GameManager.instance.SetHasShowIntro();
		ShowMenu();
		Destroy(gameObject);
	}

}
