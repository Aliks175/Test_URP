using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    
    public void ControlTime(bool timeStop)
    {
        if (timeStop)
        {
            Time.timeScale = 0f;

        }
        else 
        {
            Time.timeScale = 1f;
        
        }
        _pausePanel.SetActive(timeStop);
    }
}
