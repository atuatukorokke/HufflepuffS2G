using UnityEngine;

[RequireComponent(typeof(Animator))]
public class OperationDescription : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Animator gameAanimator;
    [SerializeField] private Animator optionAnimator;
    private bool optionCheck;

    private void Start()
    {
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
        gameAanimator.SetBool("OptionOpen", true);
    }
    public void OptionOpen()
    {
        optionAnimator.SetBool("Open", optionCheck);
        optionCheck = !optionCheck;
    }

}
