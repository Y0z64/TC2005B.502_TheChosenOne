using UnityEngine;
using System.Collections;

public class DemonMovement : MonoBehaviour
{
	private Animator anim;
	int hIdles;
	int hAngry;
	int hAttack;
	int hGrabs;
	int hThumbsUp;

	void Start()
	{
		anim = GetComponent<Animator>();
		hIdles = Animator.StringToHash("Idles");
		hAngry = Animator.StringToHash("Angry");
		hAttack = Animator.StringToHash("Attack");
		hGrabs = Animator.StringToHash("Grabs");
		hThumbsUp = Animator.StringToHash("ThumbsUp");

		StartAnimationSequence();
	}

	void StartAnimationSequence()
	{
		StartCoroutine(PlayAnimationsInSequence());
	}

	IEnumerator PlayAnimationsInSequence()
	{
		while (true)
		{ // Loop indefinitely
		  // Trigger the first animation
			anim.SetBool(hAngry, true);
			yield return new WaitForSeconds(1); // Wait for the animation to play out
			anim.SetBool(hAngry, false);

			// Trigger the second animation
			anim.SetBool(hAttack, true);
			yield return new WaitForSeconds(1); // Adjust time as needed
			anim.SetBool(hAttack, false);

			// Trigger the third animation
			anim.SetBool(hGrabs, true);
			yield return new WaitForSeconds(1); // Adjust time as needed
			anim.SetBool(hGrabs, false);

			// Trigger the fourth animation
			anim.SetBool(hThumbsUp, true);
			yield return new WaitForSeconds(1); // Adjust time as needed
			anim.SetBool(hThumbsUp, false);

			// Optionally, ensure the character returns to idles state before restarting the sequence
			anim.SetBool(hIdles, true);
			yield return new WaitForSeconds(1); // Wait a moment before restarting the sequence
		}
	}
}