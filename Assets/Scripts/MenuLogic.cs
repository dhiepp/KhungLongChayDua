using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLogic : MonoBehaviour
{

   public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

	public void ToggleSound(Text soundTxt)
	{
		if (AudioLogic.Enable)
		{
			AudioLogic.Enable = false;
			soundTxt.text = "Âm thanh: Tắt";
		}
		else
		{
			AudioLogic.Enable = true;
			soundTxt.text = "Âm thanh: Bật";
		}
	}

    public void QuitGame()
    {
        Application.Quit();
    }
}
