using UnityEngine;

public class CoverBehavior : MonoBehaviour
{
    private Renderer objectRenderer;
    private Renderer targetRenderer;
    private MinesGame minesGame;

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        minesGame = GetComponentInParent<MinesGame>();
    }

    // re-renders the object (makes it visible)
    public void reActivate()
    {
        if (objectRenderer != null) 
        {
            objectRenderer.enabled = true;
        }
    }

    void OnMouseDown()
    {
        // Can't click squares if we haven't started a game, or there has been a red square revealed.
        if (minesGame.isRedSpaceRevealed() || !minesGame.isGameInProgress()) 
        {
            return;
        }

        // locate corresponding content inside this cover
        string coords = gameObject.name.Substring(5);
        GameObject targetCube = GameObject.Find("cube" + coords);
        targetRenderer = targetCube.GetComponent<Renderer>();

        // if red square is revealed, other squares can no longer be clicked
        if (targetRenderer.material.color.Equals(Color.red))
        {
            minesGame.setRedSpaceRevealed(true);
        }
        else 
        {
            minesGame.increaseTilesCleared();
        }
        minesGame.calcAmountToWin();

        // hide the cover when it is clicked
        objectRenderer.enabled = false;
    }
}
