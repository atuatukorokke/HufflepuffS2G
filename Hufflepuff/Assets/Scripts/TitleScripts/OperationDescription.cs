using UnityEngine;

[RequireComponent(typeof(Animator))]
public class OperationDescription : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        panel.SetActive(false);
    }
    public void ShowDescription()
    {
        panel.SetActive(true);
    }
    public void GaemAnim()
    {
        animator.SetBool("OptionOpen", true);
    }
}
