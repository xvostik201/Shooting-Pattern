using System.Collections;
using TMPro;
using UnityEngine;

public class Target : MonoBehaviour, IDamagable
{
    [SerializeField] private TMP_Text _currentBulletText;
    [SerializeField] private float _resetTime = 5f;
    private int _currentBullet = 0;
    
    private Coroutine _currentCoroutine;
    
    
    public void TakeDamage(float damage)
    {
        if(_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        
        _currentCoroutine = StartCoroutine(ResetTimer());
        
        _currentBullet++;
        _currentBulletText.text = _currentBullet.ToString();
    }

    private IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(_resetTime);
        _currentBullet = 0;
        _currentBulletText.text = _currentBullet.ToString();
    }
}
