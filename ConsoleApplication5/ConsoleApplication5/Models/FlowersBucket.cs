using FlowersGirl.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowersGirl.Models
{
    public class FlowersBucket
    {
        public Flower[] Flowers { get; }

        public int Price { get; }

        public Currency Currency { get; }

        private FlowersBucket(Flower[] flowers, int price, Currency currency)
        {
            Flowers = flowers;
            Price = price;
            Currency = currency;
        }

        public override string ToString()
        {
            StringBuilder StringBuilder = new StringBuilder("Bucket of flowers: " + Environment.NewLine);
            foreach (Flower flower in Flowers)
            {
                StringBuilder.Append(flower.ToString());
                StringBuilder.Append(" ");
                StringBuilder.Append(Environment.NewLine);
            }

            StringBuilder.Append(string.Format("Bucket price: {0}{1}", Price, Currency));

            return StringBuilder.ToString();
        }

        public class Builder
        {
            private List<Flower> Flowers = new List<Flower>();

            private int? MaxPrice = null;

            private Currency Currency = Currency.None;

            public FlowersBucket Build()
            {
                if (Flowers.Count == 0)
                {
                    throw new FlowersBucketCreationException("Flowers can not be empty in flowers bucket");
                }

                var price = CalculatePrice();

                if (MaxPrice != null && price > MaxPrice)
                {
                    throw new FlowersBucketCreationException(string.Format("Max price is: {1}, currency bucket price: {2}", MaxPrice, price));
                }

                if (Currency.None.Equals(Currency))
                {
                    throw new FlowersBucketCreationException("Flower's currency must be defined");
                }

                return new FlowersBucket(Flowers.ToArray(), price, Currency);
            }

            public void AddFlower(Flower flower)
            {
                if (Currency.None == Currency)
                {
                    Currency = flower.Currency;
                }

                validateFlower(flower);

                Flowers.Add(flower);
            }

            public void SetMaxPrice(int price)
            {
                MaxPrice = price;
            }

            private int CalculatePrice()
            {
                int FlowersCost = 0;

                foreach (var flower in Flowers)
                {
                    FlowersCost += flower.Price;
                }

                return FlowersCost;
            }

            private void validateFlower(Flower flower)
            {
                if (flower == null)
                {
                    throw new ArgumentNullException("Flower can not be null");
                }

                if (flower.Price <= 0)
                {
                    throw new FlowerCostException("Flower's cost must be positive");
                }

                if (string.IsNullOrEmpty(flower.Name))
                {
                    throw new ArgumentException("Flower's name must be set");
                }

                var flowerCurrency = flower.Currency;
                
                if (!flowerCurrency.Equals(Currency))
                {
                    throw new FlowersBucketCurrencyException(string.Format("All flowers have to have the same currency: " + 
                        "new flower currency: {0}, bucket currency: {1}"));
                }
            }
        }
    }
}
