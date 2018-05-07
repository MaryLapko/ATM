using System;
using System.Xml.Serialization;

namespace FlowersGirl.Models
{
    [XmlInclude(typeof(Rose))]
    [XmlInclude(typeof(Iris))]
    [XmlInclude(typeof(Tulip))]
    [XmlInclude(typeof(Lily))]
    [XmlInclude(typeof(Chamomile))]
    [Serializable]
    public class Flower : IComparable<Flower>
    {

        [XmlElement("price")]
        public virtual int Price { get; set; }

        [XmlElement("currency")]
        public virtual Currency Currency { get; set; }

        [XmlElement("name")]
        public virtual string Name { get; set; }

        [XmlElement("colour")]
        public virtual Colour Colour { get; set; }

        // The constructor used for construct object with json
        public Flower()
        {
        }

        public Flower(int price, Colour colour, Currency currency)
        {
            if (price <= 0)
            {
                throw new ArgumentException("Flower's cost must be positive");
            }

            if (Colour.None.Equals(colour))
            {
                throw new ArgumentException("Flower's colour must be defined");
            }

            if (Currency.None.Equals(currency))
            {
                throw new ArgumentException("Flower's price currency must be defined");
            }

            Price = price;
            Colour = colour;
            Currency = currency;
        }

        public static Flower CreateFlower(string name)
        {
            Flower flower;

            switch (name)
            {
                case "Rose":
                    flower = new Rose();
                    break;
                case "Iris":
                    flower = new Iris();
                    break;
                case "Tulip":
                    flower = new Tulip();
                    break;
                case "Lily":
                    flower = new Lily();
                    break;
                case "Chamomile":
                    flower = new Chamomile();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(string.Format("Invalid name of flower: {0}", name));
            }

            return flower;
        }

        public override string ToString()
        {
            return string.Format("Flower {0} with price {1}{2}, colour: {3}", Name, Price, Currency, Colour);
        }

        public int CompareTo(Flower other)
        {
            if (this.Price > other.Price)
            {
                return 1;
            }
            else if (this.Price < other.Price)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    [Serializable]
    public class Rose : Flower
    {
        public Rose()
        {
            Name = "Rose";
        }

        public Rose(int price, Colour colour) : base(price, colour, Currency.BYN)
        {
            Name = "Rose";
        }

        public Rose(int price, Colour colour, Currency currency) : base(price, colour, currency)
        {
            Name = "Rose";
        }
    }

    [Serializable]
    public class Iris : Flower
    {
        public Iris()
        {
            Name = "Iris";
        }

        public Iris(int price, Colour colour) : base(price, colour, Currency.BYN)
        {
            Name = "Iris";
        }

        public Iris(int price, Colour colour, Currency currency) : base(price, colour, currency)
        {
            Name = "Iris";
        }
    }

    [Serializable]
    public class Lily : Flower
    {
        public Lily()
        {
            Name = "Lily";
        }

        public Lily(int price, Colour colour) : base(price, colour, Currency.BYN)
        {
            Name = "Lily";
        }

        public Lily(int price, Colour colour, Currency currency) : base(price, colour, currency)
        {
            Name = "Lily";
        }
    }

    [Serializable]
    public class Tulip : Flower
    {
        public Tulip()
        {
            Name = "Tulip";
        }

        public Tulip(int price, Colour colour) : base(price, colour, Currency.BYN)
        {
            Name = "Tulip";
        }

        public Tulip(int price, Colour colour, Currency currency) : base(price, colour, currency)
        {
            Name = "Tulip";
        }
    }

    [Serializable]
    public class Chamomile : Flower
    {
        public Chamomile()
        {
            Name = "Chamomile";
        }

        public Chamomile(int price, Colour colour) : base(price, colour, Currency.BYN)
        {
            Name = "Chamomile";
        }

        public Chamomile(int price, Colour colour, Currency currency) : base(price, colour, currency)
        {
            Name = "Chamomile";
        }
    }
}