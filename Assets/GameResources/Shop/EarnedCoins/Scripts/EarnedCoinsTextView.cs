namespace Features.Shop
{
    using System;
    using UnityEngine;
    using Features.Extensions.View;
    using System.Collections.Generic;

    /// <summary>
    /// Earned coins count text view
    /// </summary>
    public class EarnedCoinsTextView : MaskedTextView
    {
        protected const int NOT_FOUND_INDEX = -1;
        protected const int THOUSAHD_VALUE = 1000;
        protected const int MIN_THOUSANDS_COUNT = 1;
        protected const int DIGITS_COUNT = 2;

        [SerializeField]
        protected EarnedCoinsController earnedCoinsController = default;

        protected int thousandsCount = 0;
        protected float resultCount = 0;
        protected string resultCountString = string.Empty;

        protected readonly List<string> coinsPostfix = new List<string>
        { "K", "M", "B" };

        protected virtual void OnEnable()
        {
            SetText();
            earnedCoinsController.onDataInit += SetText;
        }

        protected virtual void SetText() => SetView(GetCoinsCountText());

        protected virtual string GetCoinsCountText()
        {
            thousandsCount = 0;
            resultCount = earnedCoinsController.CoinsCount;
            resultCountString = resultCount.ToString();
            while (resultCount >= THOUSAHD_VALUE && thousandsCount < coinsPostfix.Count)
            {
                resultCount /= THOUSAHD_VALUE;
                thousandsCount++;
            }
            return thousandsCount >= MIN_THOUSANDS_COUNT
                ? resultCount.ToString("0.##").Replace(',', '.') + coinsPostfix[thousandsCount - MIN_THOUSANDS_COUNT]
                : resultCount.ToString();
        }

        protected virtual void OnDisable() => earnedCoinsController.onDataInit -= SetText;
    }
}