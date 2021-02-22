using System;
using System.Collections.Generic;

namespace TesteTecnico
{
    ///<sumary>
    /// To run this application you need of dotnet 
    /// core sdk and run "dotnet run" in console/VSCode 
    /// or press F5 in Visual Studio.
    ///</sumary>
    class Program
    {
        static void Main(string[] args)
        {
            var emailList = new List<string>()
            {
                "joao@gmail.com",
                "maria@yahoo.com",
                "joaomaria@outlook.com"
            };
            var itemList = new List<Item>()
            {
                new Item(1, 100),
                new Item(2, 242),
                new Item(7, 195),
            };

            var response = CalculateCart(itemList, emailList);
            foreach (var key in response.Keys)
            {
                Console.WriteLine($"Email: {key}, Total: {response[key] / 100.0}");
            }
        }
        public static Dictionary<string, int> CalculateCart(List<Item> itens, List<string> emails)
        { 
            if (emails.Count < 1)
            {
                throw new DivideByZeroException("'emailList' cannot be empty. Provide at least one e-mail for it to work.");
            }

            int totalSum = 0;

            itens.ForEach(item => totalSum += item.Amount * item.Price);

            int valueToPaid = (int)Math.Floor((decimal)totalSum / emails.Count);
            int remainder = totalSum % emails.Count;

            var resultDictionary = new Dictionary<string, int>();

            emails.ForEach(email => resultDictionary.Add(email, valueToPaid));

            while (remainder > 0)
            {
                foreach (var key in resultDictionary.Keys)
                {
                    resultDictionary[key] += remainder-- > 0 ? 1 : 0;
                    if (remainder == 0)
                        break;
                }
            }
            return resultDictionary;
        }

    }
    class Item
    {
        public int Amount { get; private set; }
        public int Price { get; private set; }
        public Item(int amount, int price)
        {
            this.Amount = amount;
            this.Price = price;
        }
    }
}
