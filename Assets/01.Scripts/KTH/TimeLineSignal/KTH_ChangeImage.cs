using UnityEngine;
using UnityEngine.UI;

public class KTH_ChangeImage : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite[] backGrond;

    private int i;
    
    public void ChangeImage()
    {
        sr.sprite=backGrond[i];
        i++;
    }
}
