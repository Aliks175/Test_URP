using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerCharecterV1_3 _playerCharecter;
    [SerializeField] private Pause _pause;
    private bool _paused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pause()
    {
        _paused = !_paused;
        if (_paused)
        {
            _playerCharecter.EnablePauseMenuControls();
        }
        else
        {
            _playerCharecter.EnableGameplayControls();

        }
        _pause.ControlTime(_paused);
        //Развилка на вкл выкл тут сохранено значение буул
    }
}
