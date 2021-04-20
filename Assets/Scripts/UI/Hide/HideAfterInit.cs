using System.Collections;
using UnityEngine;

public abstract class HideAfterInit : MonoBehaviour, IHideAfterInit {
    [SerializeField] protected bool hideAfterInit;
    
    public bool IsInit { get; set; }

    public abstract void Init();

    public IEnumerator Hide() {
        yield return new WaitUntil(() => IsInit);
        gameObject.SetActive(false);
    }

    protected void Awake() {
        Init();
        if (hideAfterInit) StartCoroutine(Hide());
    }
}