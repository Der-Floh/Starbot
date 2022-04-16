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
        public async Task WriteBaby(List<Baby> _baby)
        {
            string fileName = @"Suggestion-Files/Baby-Ideas.json";

            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, _baby, jsonOptions);
            await createStream.DisposeAsync();
        }
        public async Task WriteItem(List<Item> _item)
        {
            string fileName = @"Suggestion-Files/Item-Ideas.json";

            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, _item, jsonOptions);
            await createStream.DisposeAsync();
        }
        public async Task WriteItemActive(List<ItemActive> _itemActive)
        {
            string fileName = @"Suggestion-Files/ItemActive-Ideas.json";

            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, _itemActive, jsonOptions);
            await createStream.DisposeAsync();
        }
        public async Task WriteEnemy(List<Enemy> _enemy)
        {
            string fileName = @"Suggestion-Files/Enemy-Ideas.json";

            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, _enemy, jsonOptions);
            await createStream.DisposeAsync();
        }

        public async Task<List<Baby>> GetBabies()
        {
            string fileName = @"Suggestion-Files/Baby-Ideas.json";
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                List<Baby> _baby = await JsonSerializer.DeserializeAsync<List<Baby>>(openStream);
                await openStream.DisposeAsync();
                return _baby;
            }
            else
            {
                Console.WriteLine("Couldn't find Babies location");
            }
            return null;
        }
        public async Task<List<Item>> GetItems()
        {
            string fileName = @"Suggestion-Files/Baby-Ideas.json";
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                List<Item> _item = await JsonSerializer.DeserializeAsync<List<Item>>(openStream);
                await openStream.DisposeAsync();
                return _item;
            }
            else
            {
                Console.WriteLine("Couldn't find Items location");
            }
            return null;
        }
        public async Task<List<ItemActive>> GetItemsActive()
        {
            string fileName = @"Suggestion-Files/Baby-Ideas.json";
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                List<ItemActive> _itemActive = await JsonSerializer.DeserializeAsync<List<ItemActive>>(openStream);
                await openStream.DisposeAsync();
                return _itemActive;
            }
            else
            {
                Console.WriteLine("Couldn't find Active Items location");
            }
            return null;
        }
        public async Task<List<Enemy>> GetEnemies()
        {
            string fileName = @"Suggestion-Files/Baby-Ideas.json";
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                List<Enemy> _enemy = await JsonSerializer.DeserializeAsync<List<Enemy>>(openStream);
                await openStream.DisposeAsync();
                return _enemy;
            }
            else
            {
                Console.WriteLine("Couldn't find Enemies location");
            }
            return null;
        }
        public async Task<List<ExistingBaby>> GetExistingBabies()
        {
            string fileName = @"Recources/irule/babies.json";
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                List<ExistingBaby> _existingBaby = await JsonSerializer.DeserializeAsync<List<ExistingBaby>>(openStream);
                await openStream.DisposeAsync();
                return _existingBaby;
            }
            else
            {
                Console.WriteLine("Couldn't find existing Babies location");
            }
            return null;
        }
        public async Task<List<ExistingItem>> GetExistingItems()
        {
            string fileName = @"Recources/irule/items.json";
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                List<ExistingItem> _existingItem = await JsonSerializer.DeserializeAsync<List<ExistingItem>>(openStream);
                await openStream.DisposeAsync();
                return _existingItem;
            }
            else
            {
                Console.WriteLine("Couldn't find existing Items location");
            }
            return null;
        }
        public async Task<List<ExistingItemActive>> GetExistingItemsActive()
        {
            string fileName = @"Recources/irule/itemsActive.json";
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                List<ExistingItemActive> _existingItemActive = await JsonSerializer.DeserializeAsync<List<ExistingItemActive>>(openStream);
                await openStream.DisposeAsync();
                return _existingItemActive;
            }
            else
            {
                Console.WriteLine("Couldn't find existing Active Items location");
            }
            return null;
        }
        public async Task<List<ExistingEnemy>> GetExistingEnemies()
        {
            string fileName = @"Recources/irule/enemies.json";
            if (File.Exists(fileName))
            {
                using FileStream openStream = File.OpenRead(fileName);
                List<ExistingEnemy> _existingEnemy = await JsonSerializer.DeserializeAsync<List<ExistingEnemy>>(openStream);
                await openStream.DisposeAsync();
                return _existingEnemy;
            }
            else
            {
                Console.WriteLine("Couldn't find existing enemies location");
            }
            return null;
        }
    }
}
