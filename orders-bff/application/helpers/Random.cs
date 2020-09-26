using System;
using System.Collections.Generic;
using application.models;

namespace application.helpers
{
    public static class Random
    {
        private static string[] names = new string[]
           { "Leonardo", "Júlia", "Marina", "Augusto", "João", "Bruna", "Carlos", "André"};

        private static string[] cellphones = new string[]
            { "202-555-0174", "202-555-0171", "202-555-0143", "202-555-0122", "202-555-0171", "202-555-0009" };

        private static string[] emails = new string[]
            { "granboul@comcast.net", "payned@optonline.net", "chunzi@mac.com", "estousemcriatividade@socorro.com" };

        private static string[] items = new string[]
            { "Batata Frita", "Calzone", "Pastel", "Big Mac", "Refrigerante", "Macarrão" };

        private static string[] itemsSuffix = new string[]
            { "Pequena", "Média", "Grande", "Turbinada", "à Moda da Casa", "Secreta" };

        public static System.Random rng = new System.Random(1337);
        public static Client GenerateClient()
        {
            return new Client
            {
                Email = emails[rng.Next(0, emails.Length)],
                Name = names[rng.Next(0, names.Length)],
                Phone = cellphones[rng.Next(0, cellphones.Length)]
            };
        }

        // terminar o bgl
        public static Order GenerateOrder(Client client)
        {
            return new Order
            {
                ClientId = client.Id,
                CreatedAt = DateTime.Now.AddMinutes(rng.Next(0, 80)),
                ConfirmedAt = DateTime.Now.AddMinutes(rng.Next(0, 80)),
                RestaurantId = Guid.NewGuid(),
                Items = GenerateItems()
            };
        }

        public static IEnumerable<Item> GenerateItems()
        {
            var numberOfItems = rng.Next(1, 5);
            for(var i = 0; i < numberOfItems; i++)
            {
                yield return new Item
                {
                    Price = rng.NextDouble() * 10,
                    Quantity = rng.Next(1, 5),
                    Description = items[rng.Next(0, items.Length)] + " " + itemsSuffix[rng.Next(0, itemsSuffix.Length)]
                };
            }
        }
    }
}