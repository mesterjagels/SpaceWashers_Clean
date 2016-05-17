using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBubble : MonoBehaviour
{
    public Animator animator;
    public TextMesh bubbleScore;


    void OnEnable()
    {
       /* AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Debug.Log(clipInfo.Length);
        Destroy(gameObject, clipInfo[0].clip.length);
        bubbleScore = animator.GetComponent<Text>();*/
    }

    public void SetText(string text)
    {
        bubbleScore.text = text;
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(1f);
        GameObject.Destroy(this.gameObject);
    }

    IEnumerator delayBubbleText()
    {
        yield return new WaitForSeconds(0.5f);
        bubbleScore.GetComponent<Renderer>().enabled = true;
    }

    void Start()
    {
        StartCoroutine(delayBubbleText());
        StartCoroutine(Disappear());
    }
}
