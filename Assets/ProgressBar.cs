using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public float cap;
    public bool locked;
    private float progress;

    private void Start()
    {
        this.locked = false;
    }

    public float Progress
    {
        set
        {
            if (value <= 0)
            {
                progress = 0;
            }
            else if (!locked)
            {
                if (value >= cap)
                {
                    progress = cap;
                    locked = true;
                }
                else
                {
                    progress = value;
                }
            }
        }
        get { return progress;  }
    }
}
