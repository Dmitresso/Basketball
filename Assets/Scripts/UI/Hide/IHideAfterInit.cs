using System.Collections;

public interface IHideAfterInit {
    bool IsInit { get; set; }
    abstract void Init();
    abstract IEnumerator Hide();
}