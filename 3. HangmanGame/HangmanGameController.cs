using UnityEngine;

public class HangmanGameController : MonoBehaviour
{
    public Animation boyAnimation;
    public string[] boyAnims;

    [SerializeField]
	private int tries;
	private GameObject[] parts;


	public bool isDead
	{
		get { return tries <= -1; }
	}
	// Use this for initialization
	void Start()
	{
		reset();
	}

	public void punish()
	{
        tries--;
        if (!isDead)
        {
            boyAnimation.CrossFade(boyAnims[tries+ 1], 0.2f);
        }
       

    }

	public void reset()
	{
		//if (parts == null)
			//return;

		//tries = parts.Length - 1;
		//foreach (GameObject g in parts)
		//{
		//	g.SetActive(false);
		//}
	}

}