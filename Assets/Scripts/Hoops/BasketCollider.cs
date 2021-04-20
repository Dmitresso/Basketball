using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BasketCollider : MonoBehaviour {
    private Basket basket;

    private void Awake() {
        basket = GetComponentInParent<Basket>();
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag(Tags.Items.Ball) && gameObject.tag.Equals(Tags.Env.FirstBasketCollider)) basket.first = true;
        if (other.CompareTag(Tags.Items.Ball) && gameObject.tag.Equals(Tags.Env.SecondBasketCollider) && basket.first) basket.second = true;

        if (basket.first && basket.second) basket.Goal();
    }
}