using System;
using System.Linq;
using Data.ScriptableObjects;
using Entities.Buildings;
using Extensions;
using UnityEngine;

namespace Managers
{
    public class TradeManager : SingletonMonoBehaviour<TradeManager>
    {
        [Header("References")]
        [SerializeField] private ResourceData resourceData;

        [Header("Info - No Touch")]
        [SerializeField] private ResourceAndAmount[] buyCart;
        [SerializeField] private ResourceAndAmount[] sellCart;
        [SerializeField] private ResourceAndAmount moneyCart;
        public RocketSite currentRocketSite;

        public int GetMoneyInCart() => moneyCart.amount;
        public int GetSellCartSize() => sellCart.Sum(resource => resource.amount);
        public int GetBuyCartSize() => buyCart.Sum(resource => resource.amount);

        private void Start()
        {
            buyCart = new ResourceAndAmount[3];
            buyCart[0] = new ResourceAndAmount { resource = ResourceType.Population };
            buyCart[1] = new ResourceAndAmount { resource = ResourceType.Energy };
            buyCart[2] = new ResourceAndAmount { resource = ResourceType.Food };

            sellCart = new ResourceAndAmount[3];
            sellCart[0] = new ResourceAndAmount { resource = ResourceType.Metal };
            sellCart[1] = new ResourceAndAmount { resource = ResourceType.MetalPremium };
            sellCart[2] = new ResourceAndAmount { resource = ResourceType.Gem };

            moneyCart = new ResourceAndAmount { resource = ResourceType.Money };
        }

        public void ClearTradeData()
        {
            for (var i = 0; i < buyCart.Length; i++) buyCart[i].amount = 0;
            for (var i = 0; i < sellCart.Length; i++) sellCart[i].amount = 0;
            moneyCart.amount = 0;
            currentRocketSite = null;
        }

        private bool CanBuyResource(ResourceType type)
        {
            if (GetBuyCartSize() + 1 > currentRocketSite.GetCapacity()) return false; //We are check first, adding later, so we need to add 1 to size

            var money = ResourceManager.Instance.GetResourceAmount(ResourceType.Money);
            money += moneyCart.amount;

            var cost = resourceData.GetResourceMoneyValue(type);

            if (type == ResourceType.Population) //For population, besides money, check population capacity too
            {
                var currentPopulation = ResourceManager.Instance.GetResourceAmount(ResourceType.Population);
                var currentCapacity = ResourceManager.Instance.GetResourceAmount(ResourceType.PopulationCapacity);
                var remainingCapacity = currentCapacity - currentPopulation;
                if (remainingCapacity < 1) return false;
            }

            return money >= cost;
        }

        private bool CanSellResource(ResourceType type)
        {
            if (GetSellCartSize() + 1 > currentRocketSite.GetCapacity()) return false; //We are check first, adding later, so we need to add 1 to size

            var amount = ResourceManager.Instance.GetResourceAmount(type);
            var amountOnCart = GetResourceAmountOnCart(type, false);

            return amount + 1 >= amountOnCart; //We are check first, adding later, so we need to add 1 to amount
        }

        public bool TryAddToCart(ResourceType type, int amount, bool isBuying)
        {
            var cart = isBuying ? buyCart : sellCart;

            for (var i = 0; i < cart.Length; i++)
            {
                if (cart[i].resource == type)
                {
                    if (isBuying)
                    {
                        if (CanBuyResource(type))
                        {
                            cart[i].amount += amount;
                            moneyCart.amount -= amount * resourceData.GetResourceMoneyValue(type);
                            return true;
                        }

                        else return false;
                    }

                    else
                    {
                        if (CanSellResource(type))
                        {
                            cart[i].amount += amount;
                            moneyCart.amount += amount * resourceData.GetResourceMoneyValue(type);
                            return true;
                        }

                        else return false;
                    }
                }
            }

            var isBuyingText = isBuying ? "buy" : "sell";
            throw new Exception($"Can not {isBuyingText} {type}.");
        }

        public int GetResourceAmountOnCart(ResourceType type, bool isBuying)
        {
            var cart = isBuying ? buyCart : sellCart;

            for (var i = 0; i < cart.Length; i++)
            {
                if (cart[i].resource == type)
                {
                    return cart[i].amount;
                }
            }

            return 0;
        }

        public void BuyCart()
        {
            for (var i = 0; i < buyCart.Length; i++)
            {
                if (buyCart[i].amount == 0) continue;

                ResourceManager.Instance.BuyResource(buyCart[i].resource, buyCart[i].amount);
                buyCart[i].amount = 0;
            }

            moneyCart.amount = 0;
        }

        public void SellCart()
        {
            for (var i = 0; i < sellCart.Length; i++)
            {
                if (sellCart[i].amount == 0) continue;

                ResourceManager.Instance.SellResource(sellCart[i].resource, sellCart[i].amount);
                sellCart[i].amount = 0;
            }

            moneyCart.amount = 0;
        }
    }
}