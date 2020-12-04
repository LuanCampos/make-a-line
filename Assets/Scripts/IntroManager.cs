using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    void Start()
    {
		if (GameManager.instance.HasShowIntro())
		{
			Destroy(gameObject);
		}
		
		else
		{
			StartCoroutine(LetIntroPlay());
		}
    }
	
	private IEnumerator LetIntroPlay()
	{
		yield return new WaitForSecondsRealtime(4.2f);
		GameManager.instance.SetHasShowIntro();
		Destroy(gameObject);
	}

}
