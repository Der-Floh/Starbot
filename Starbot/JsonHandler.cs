using Starbot.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Starbot
{
    public class JsonHandler
    {
        public async Task AddBaby(Baby baby)
        {
            string fileName = @"Suggestion-Files\Baby-Ideas.json";
            List<Baby> _baby = new List<Baby>();
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                _baby = await JsonSerializer.DeserializeAsync<List<Baby>>(openStream);
                /*
                jsonString = File.ReadAllText(fileName);
                _baby = JsonSerializer.Deserialize<List<Baby>>(jsonString);
                */
            }

            _baby.Add(baby);

            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, _baby, jsonOptions);
            await createStream.DisposeAsync();
            /*
            jsonString = JsonSerializer.Serialize(_baby, jsonOptions);
            File.WriteAllText(fileName, jsonString);
            */
        }
        public async Task AddItem(Item item)
        {
            string fileName = @"Suggestion-Files\Item-Ideas.json";
            List<Item> _item = new List<Item>();
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                _item = await JsonSerializer.DeserializeAsync<List<Item>>(openStream);
            }
            _item.Add(item);

            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, _item, jsonOptions);
            await createStream.DisposeAsync();
        }

        public async Task AddItemActive(ItemActive itemActive)
        {
            string fileName = @"Suggestion-Files\ItemActive-Ideas.json"; //changed item-active
            List<ItemActive> _itemActive = new List<ItemActive>();
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                _itemActive = await JsonSerializer.DeserializeAsync<List<ItemActive>>(openStream);
            }
            _itemActive.Add(itemActive);

            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, _itemActive, jsonOptions);
            await createStream.DisposeAsync();
        }

        public async Task AddEnemy(Enemy enemy)
        {
            string fileName = @"Suggestion-Files\Enemy-Ideas.json";
            List<Enemy> _enemy = new List<Enemy>();
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                _enemy = await JsonSerializer.DeserializeAsync<List<Enemy>>(openStream);
            }
            _enemy.Add(enemy);

            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, _enemy, jsonOptions);
            await createStream.DisposeAsync();
        }

        public async Task<int> GetIdeaCount(string type)
        {
            string fileName = @"Suggestion-Files\" + type + "-Ideas.json";
            int ideaCount = -1;
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                switch (type)
                {
                    case "Baby":
                        List<Baby> _baby = await JsonSerializer.DeserializeAsync<List<Baby>>(openStream);
                        ideaCount = _baby.Count;
                        break;
                    case "Item":
                        List<Item> _item = await JsonSerializer.DeserializeAsync<List<Item>>(openStream);
                        ideaCount = _item.Count;
                        break;
                    case "Item-Active":
                        List<ItemActive> _itemActive = await JsonSerializer.DeserializeAsync<List<ItemActive>>(openStream);
                        ideaCount = _itemActive.Count;
                        break;
                    case "Enemy":
                        List<Enemy> _enemy = await JsonSerializer.DeserializeAsync<List<Enemy>>(openStream);
                        ideaCount = _enemy.Count;
                        break;
                }
            }
            return ideaCount;
        }

        public async Task SetRating(ulong ideaId, string type, int rating)
        {
            //Console.WriteLine("ID= " + ideaId + "  Type= " + type  + "  Rating= " + rating);
            switch (type)
            {
                case "Baby": await SetRatingBaby(ideaId, rating); break;
                case "Item": await SetRatingItem(ideaId, rating); break;
                case "ItemActive": await SetRatingItemActive(ideaId, rating); break;
                case "Enemy": await SetRatingEnemy(ideaId, rating); break;
            }
            Console.WriteLine("Rating = " + rating);
        }

        public async Task<Baby> GetIdeaBaby(ulong id)
        {
            string fileName = @"Suggestion-Files\Baby-Ideas.json";
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                List<Baby> _baby = await JsonSerializer.DeserializeAsync<List<Baby>>(openStream);
                await openStream.DisposeAsync();
                foreach (Baby baby in _baby)
                {
                    if (baby.id == id)
                    {
                        return baby;
                    }
                }
            }
            return null;
        }
        public async Task<Item> GetIdeaItem(ulong id)
        {
            string fileName = @"Suggestion-Files\Item-Ideas.json";
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                List<Item> _item = await JsonSerializer.DeserializeAsync<List<Item>>(openStream);
                await openStream.DisposeAsync();
                foreach (Item item in _item)
                {
                    if (item.id == id)
                    {
                        return item;
                    }
                }
            }
            return null;
        }
        public async Task<ItemActive> GetIdeaItemActive(ulong id)
        {
            string fileName = @"Suggestion-Files\ItemActive-Ideas.json";
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                List<ItemActive> _itemActive = await JsonSerializer.DeserializeAsync<List<ItemActive>>(openStream);
                await openStream.DisposeAsync();
                foreach (ItemActive itemActive in _itemActive)
                {
                    if (itemActive.id == id)
                    {
                        return itemActive;
                    }
                }
            }
            return null;
        }
        public async Task<Enemy> GetIdeaEnemy(ulong id)
        {
            string fileName = @"Suggestion-Files\Enemy-Ideas.json";
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                List<Enemy> _enemy = await JsonSerializer.DeserializeAsync<List<Enemy>>(openStream);
                await openStream.DisposeAsync();
                foreach (Enemy enemy in _enemy)
                {
                    if (enemy.id == id)
                    {
                        return enemy;
                    }
                }
            }
            return null;
        }

        public async Task<Baby> GetBestRatedBaby(int ratingNo)
        {
            string fileName = @"Suggestion-Files\Baby-Ideas.json";
            int bestRating = 0;
            int secBestRating = 0;
            Baby bestBaby = null;
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                List<Baby> _baby = await JsonSerializer.DeserializeAsync<List<Baby>>(openStream);
                await openStream.DisposeAsync();
                foreach (Baby baby in _baby)
                {
                    if (baby.rating > bestRating)
                    {
                        bestRating = baby.rating;
                        bestBaby = baby;
                    }
                    if (baby.rating > secBestRating && secBestRating < bestRating)
                    {
                        secBestRating = baby.rating;
                    }
                }
            }
            return bestBaby;
        }

        private async Task SetRatingBaby(ulong id, int rating)
        {
            string fileName = @"Suggestion-Files\Baby-Ideas.json";
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                List<Baby> _baby = await JsonSerializer.DeserializeAsync<List<Baby>>(openStream);
                await openStream.DisposeAsync();
                foreach (Baby baby in _baby)
                {
                    if (baby.id == id)
                    {
                        baby.rating = rating;
                        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                        using FileStream createStream = File.Create(fileName);
                        await JsonSerializer.SerializeAsync(createStream, _baby, jsonOptions);
                        await createStream.DisposeAsync();
                        return;
                    }
                }
            }
        }
        private async Task SetRatingItem(ulong id, int rating)
        {
            string fileName = @"Suggestion-Files\Item-Ideas.json";
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                List<Item> _item = await JsonSerializer.DeserializeAsync<List<Item>>(openStream);
                await openStream.DisposeAsync();
                foreach (Item item in _item)
                {
                    if (item.id == id)
                    {
                        item.rating = rating;
                        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                        using FileStream createStream = File.Create(fileName);
                        await JsonSerializer.SerializeAsync(createStream, _item, jsonOptions);
                        await createStream.DisposeAsync();
                        return;
                    }
                }
            }
        }
        private async Task SetRatingItemActive(ulong id, int rating)
        {
            string fileName = @"Suggestion-Files\ItemActive-Ideas.json";
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                List<ItemActive> _itemActive = await JsonSerializer.DeserializeAsync<List<ItemActive>>(openStream);
                await openStream.DisposeAsync();
                foreach (ItemActive itemActive in _itemActive)
                {
                    if (itemActive.id == id)
                    {
                        itemActive.rating = rating;
                        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                        using FileStream createStream = File.Create(fileName);
                        await JsonSerializer.SerializeAsync(createStream, _itemActive, jsonOptions);
                        await createStream.DisposeAsync();
                        return;
                    }
                }
            }
        }
        private async Task SetRatingEnemy(ulong id, int rating)
        {
            string fileName = @"Suggestion-Files\Enemy-Ideas.json";
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                List<Enemy> _enemy = await JsonSerializer.DeserializeAsync<List<Enemy>>(openStream);
                await openStream.DisposeAsync();
                foreach (Enemy enemy in _enemy)
                {
                    if (enemy.id == id)
                    {
                        enemy.rating = rating;
                        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                        using FileStream createStream = File.Create(fileName);
                        await JsonSerializer.SerializeAsync(createStream, _enemy, jsonOptions);
                        await createStream.DisposeAsync();
                        return;
                    }
                }
            }
        }
    }
}
