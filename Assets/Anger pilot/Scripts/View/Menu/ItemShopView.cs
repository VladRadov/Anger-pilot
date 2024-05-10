using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemShopView : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private Text _viewPrice;
    [SerializeField] private Button _purchase;
    [SerializeField] private Button _purchased;
    [SerializeField] private Image _selected;

    [HideInInspector]
    public UnityEvent OnSelectedItemEventHander = new ();
    [HideInInspector]
    public UnityEvent OnPurchasedItemEventHander = new ();

    public virtual void InitializeStateItem() { }

    protected virtual void OnPurchased()
    {
        if (WalletManager.TryPurchase(_price))
        {
            Purchase();
            SetPurchasedState();
            AudioManager.Instance.PlayClickButton();
            OnPurchasedItemEventHander?.Invoke();
        }
        else
            AudioManager.Instance.PlayErrorEntry();
    }

    protected void SetPurchaseState()
        => SetStatePurchase(false, true, false);

    protected void SetPurchasedState()
        => SetStatePurchase(false, false, true);

    protected void SetSelectedState()
        => SetStatePurchase(true, false, false);

    protected virtual void OnSelectedItem()
    {
        AudioManager.Instance.PlayClickButton();
        OnSelectedItemEventHander?.Invoke();
    }

    protected virtual void Purchase() { }

    protected virtual void OnEnable()
        => InitializeStateItem();

    private void SetStatePurchase(bool isSelected, bool isPurchase, bool isPurchased)
    {
        _selected.gameObject.SetActive(isSelected);
        _purchase.enabled = isPurchase;
        _purchased.gameObject.SetActive(isPurchased);
    }

    private void Start()
    {
        _purchase.onClick.AddListener(() => { OnPurchased(); });
        _purchased.onClick.AddListener(() => { OnSelectedItem(); });
        _viewPrice.text = _price.ToString();
    }
}
