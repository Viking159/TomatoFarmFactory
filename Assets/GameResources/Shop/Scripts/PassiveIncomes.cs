namespace Features.Shop
{
    using Features.InfoBox;
    using Features.Shop.Data;
    using Features.Store;
    using System.Collections.Generic;
    using UnityEngine;

    public sealed class PassiveIncomes : MonoBehaviour
    {
        [SerializeField]
        private LastTimeActivityController _lastTimeActivityController = default;
        [SerializeField]
        private List<AbsractIncomeController> _incomeControllers = new List<AbsractIncomeController>();
        [SerializeField]
        private InfoBoxContainer _infoBoxContainer = null;
        [SerializeField]
        private MoneyData _moneyData = default;
        [SerializeField]
        private int _maxIncome = 500;
        [SerializeField]
        private string _productId = "no_ads";
        private int _totalIncome = default;
        private int _detlaSeconds = default;

        private void Start()
        {
            InitTime();
            InitIncome();
        }

        private void InitTime() => _detlaSeconds = (int)_lastTimeActivityController.GetSecondsSinceLastSave();

        private void InitIncome()
        {
            _totalIncome = 0;
            foreach (AbsractIncomeController incomeController in _incomeControllers)
            {
                _totalIncome += incomeController.GetIncome(_detlaSeconds);
            }
            if (_totalIncome > 0)
            {
                if (UIAPStore.Instance.IsBought(_productId))
                {
                    _maxIncome *= 2;
                }
                if (_totalIncome > _maxIncome)
                {
                    _totalIncome = _maxIncome;
                }
                _moneyData.SetCoins(_moneyData.Coins + _totalIncome);
                ShowPopup();
            }
        }

        private void ShowPopup()
        {
            if (_infoBoxContainer.InfoBox == null)
            {
                _infoBoxContainer.Init(Instantiate(_infoBoxContainer.InfoBoxPrefab));
            }
            else
            {
                StopListenInfoBox();
            }
            _infoBoxContainer.InfoBox.gameObject.SetActive(true);
            (_infoBoxContainer.InfoBox as IncomeInfoBox).Init(_totalIncome);
            _infoBoxContainer.InfoBox.onBoxClose += CloseInfoBox;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_infoBoxContainer != null && _infoBoxContainer.InfoBoxPrefab is not IncomeInfoBox)
            {
                Debug.LogError($"{nameof(PassiveIncomes)}: infoBox is not IncomeInfoBox");
            }
        }
#endif

        private void CloseInfoBox()
        {
            StopListenInfoBox();
            if (_infoBoxContainer.InfoBox != null)
            {
                _infoBoxContainer.InfoBox.gameObject.SetActive(false);
            }
        }

        private void StopListenInfoBox()
        {
            if (_infoBoxContainer.InfoBox != null)
            {
                _infoBoxContainer.InfoBox.onBoxClose -= CloseInfoBox;
            }
        }

        private void OnDisable()
            => StopListenInfoBox();
    }
}