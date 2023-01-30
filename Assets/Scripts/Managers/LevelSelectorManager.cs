using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorManager : MonoBehaviour
{
	[SerializeField] private SceneFader fader;
	[SerializeField] private LoadingManager loading;
	[SerializeField] private Button[] levelButtons;

	void Start()
	{
		int levelReached = PlayerPrefs.GetInt("levelReached", 1);

		for (int i = 0; i < levelButtons.Length; i++)
		{
			if (i + 1 > levelReached)
				levelButtons[i].interactable = false;
		}
	}

	public void Select(string levelName)
	{
		//	fader.FadeTo(levelName);
		loading.LoadScreen(levelName);
	} 
}
