using System.Collections;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField] private int _�xperience = 15;
    [SerializeField] private GameObject _body;
    private bool isactive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!isactive) return;
        var playerData = other.gameObject.GetComponent<ICharacterData>();
        if (playerData != null)
        {
            isactive = false;
            playerData.SystemLevel.SetEx(_�xperience);
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
