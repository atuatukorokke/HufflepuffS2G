using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private int currentTutorialIndex = 0;
    private int maxTutorialIndex;

    public void IndexUpdate()
    {
        currentTutorialIndex++;
    }

    public void IndexDown()
    {
        currentTutorialIndex--;
    }
}
