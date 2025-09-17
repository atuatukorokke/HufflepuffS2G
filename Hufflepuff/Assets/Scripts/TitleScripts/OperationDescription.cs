using UnityEngine;

[RequireComponent(typeof(Animator))]
public class OperationDescription : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Animator gameAanimator;
    [SerializeField] private Animator optionAnimator;
    private bool optionCheck;

    private AudioSource aidio;
    [SerializeField] private AudioClip ButtonTouch;

    private void Start()
    {
        aidio = GetComponent<AudioSource>();
        gameAanimator = GetComponent<Animator>();
        panel.SetActive(false);
        optionCheck = true;
    }
    public void ShowDescription()
    {
        panel.SetActive(true);
    }
    public void GaemAnim()
    {
        aidio.PlayOneShot(ButtonTouch);
        gameAanimator.SetBool("OptionOpen", true);
    }
    public void OptionOpen()
    {
        aidio.PlayOneShot(ButtonTouch);
        optionAnimator.SetBool("Open", optionCheck);
        optionCheck = !optionCheck;
    }

}
