using System.Collections;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField] private int _åxperience = 15;
    [SerializeField] private GameObject _body;
    private bool isactive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!isactive) return;
        if (other.TryGetComponent<SystemLevel>(out SystemLevel component))
        {
            isactive = false;
            component.SetEx(_åxperience);
            _body.SetActive(false);
            StartCoroutine(WaitUpdate());
        }
    }


    private IEnumerator WaitUpdate()
    {
        yield return new WaitForSeconds(5f);
        _body.SetActive(true);
        isactive = true;
    }
}
