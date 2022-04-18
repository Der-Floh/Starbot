using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starbot.Types
{
    public static class Idea
    {
        //there could be some translation integration and a command to bring up wiki pages though
        private static List<Baby> _baby;
        private static List<Item> _item;
        private static List<ItemActive> _itemActive;
        private static List<Enemy> _enemy;
        private static List<ExistingBaby> _existingBaby;
        private static List<ExistingItem> _existingItem;
        private static List<ExistingItemActive> _existingItemActive;
        private static List<ExistingEnemy> _existingEnemy;
        private static bool changedBaby;
        private static bool changedItem;
        private static bool changedItemActive;
        private static bool changedEnemy;
        public static ulong deleteBaby;
        public static ulong deleteItem;
        public static ulong deleteItemActive;
        public static ulong deleteEnemy;
        public static ulong deleteBabyUserID;
        public static ulong deleteItemUserID;
        public static ulong deleteItemActiveUserID;
        public static ulong deleteEnemyUserID;
        private static Timer timer;

        public static async Task InitIdea()
        {
            JsonHandler jsonHandler = new JsonHandler();

            _baby = await jsonHandler.GetBabies();
            if (_baby == null)
            {
                _baby = new List<Baby>();
            }

            _item = await jsonHandler.GetItems();
            if (_item == null)
            {
                _item = new List<Item>();
            }

            _itemActive = await jsonHandler.GetItemsActive();
            if (_itemActive == null)
            {
                _itemActive = new List<ItemActive>();
            }

            _enemy = await jsonHandler.GetEnemies();
            if (_enemy == null)
            {
                _enemy = new List<Enemy>();
            }

            _existingBaby = await jsonHandler.GetExistingBabies();
            if (_existingBaby == null)
            {
                Console.WriteLine("Failed to load existing Babies");
                _existingBaby = new List<ExistingBaby>();
            }

            _existingItem = await jsonHandler.GetExistingItems();
            if (_existingItem == null)
            {
                Console.WriteLine("Failed to load existing Items");
                _existingItem = new List<ExistingItem>();
            }

            _existingItemActive = await jsonHandler.GetExistingItemsActive();
            if (_existingItemActive == null)
            {
                Console.WriteLine("Failed to load existing Active Items");
                _existingItemActive = new List<ExistingItemActive>();
            }

            _existingEnemy = await jsonHandler.GetExistingEnemies();
            if (_existingEnemy == null)
            {
                Console.WriteLine("Failed to load existing Enemies");
                _existingEnemy = new List<ExistingEnemy>();
            }

            timer = new Timer(timerTick, new AutoResetEvent(false), 60000, 60000);
            //timer.Tick += new EventHandler(timerTick);
            //timer.Interval = 60000;
            //timer.Start();
        }

        private static async void timerTick(object state)
        {
            Console.WriteLine("Check for Changes");
            await WriteToJson();
        }

        public static async Task AddBaby(Baby baby)
        {
            _baby.Add(baby);
            changedBaby = true;
        }
        public static async Task AddItem(Item item)
        {
            _item.Add(item);
            changedItem = true;
        }
        public static async Task AddItemActive(ItemActive itemActive)
        {
            _itemActive.Add(itemActive);
            changedItemActive = true;
        }
        public static async Task AddEnemy(Enemy enemy)
        {
            _enemy.Add(enemy);
            changedEnemy = true;
        }

        public static async Task<Baby> GetBaby(string msg)
        {
            ulong id = 0;
            if (ulong.TryParse(msg, out id))
            {
                foreach (Baby baby in _baby)
                {
                    if (baby.id == id)
                    {
                        return baby;
                    }
                }
            }
            else
            {
                foreach (Baby baby in _baby)
                {
                    if (baby.name == msg)
                    {
                        return baby;
                    }
                }
            }
            return null;
        }
        public static async Task<Item> GetItem(string msg)
        {
            ulong id = 0;
            if (ulong.TryParse(msg, out id))
            {
                foreach (Item item in _item)
                {
                    if (item.id == id)
                    {
                        return item;
                    }
                }
            }
            else
            {
                foreach (Item item in _item)
                {
                    if (item.name == msg)
                    {
                        return item;
                    }
                }
            }
            return null;
        }
        public static async Task<ItemActive> GetItemActive(string msg)
        {
            ulong id = 0;
            if (ulong.TryParse(msg, out id))
            {
                foreach (ItemActive itemActive in _itemActive)
                {
                    if (itemActive.id == id)
                    {
                        return itemActive;
                    }
                }
            }
            else
            {
                foreach (ItemActive itemActive in _itemActive)
                {
                    if (itemActive.name == msg)
                    {
                        return itemActive;
                    }
                }
            }
            return null;
        }
        public static async Task<Enemy> GetEnemy(string msg)
        {
            ulong id = 0;
            if (ulong.TryParse(msg, out id))
            {
                foreach (Enemy enemy in _enemy)
                {
                    if (enemy.id == id)
                    {
                        return enemy;
                    }
                }
            }
            else
            {
                foreach (Enemy enemy in _enemy)
                {
                    if (enemy.name == msg)
                    {
                        return enemy;
                    }
                }
            }
            return null;
        }

        public static async Task<ExistingBaby> GetExistingBaby(string msg)
        {
            ulong id = 0;
            if (ulong.TryParse(msg, out id))
            {
                foreach (ExistingBaby existingBaby in _existingBaby)
                {
                    if (existingBaby.id == id)
                    {
                        return existingBaby;
                    }
                }
            }
            else
            {
                foreach (ExistingBaby existingBaby in _existingBaby)
                {
                    if (existingBaby.name == msg)
                    {
                        return existingBaby;
                    }
                }
            }
            return null;
        }
        public static async Task<ExistingItem> GetExistingItem(string msg)
        {
            ulong id = 0;
            if (ulong.TryParse(msg, out id))
            {
                foreach (ExistingItem existingItem in _existingItem)
                {
                    if (existingItem.id == id)
                    {
                        return existingItem;
                    }
                }
            }
            else
            {
                foreach (ExistingItem existingItem in _existingItem)
                {
                    if (existingItem.name == msg)
                    {
                        return existingItem;
                    }
                }
            }
            return null;
        }
        public static async Task<ExistingItemActive> GetExistingItemActive(string msg)
        {
            ulong id = 0;
            if (ulong.TryParse(msg, out id))
            {
                foreach (ExistingItemActive existingItemActive in _existingItemActive)
                {
                    if (existingItemActive.id == id)
                    {
                        return existingItemActive;
                    }
                }
            }
            else
            {
                foreach (ExistingItemActive existingItemActive in _existingItemActive)
                {
                    if (existingItemActive.name == msg)
                    {
                        return existingItemActive;
                    }
                }
            }
            return null;
        }
        public static async Task<ExistingEnemy> GetExistingEnemy(string msg)
        {
            ulong id = 0;
            if (ulong.TryParse(msg, out id))
            {
                foreach (ExistingEnemy existingEnemy in _existingEnemy)
                {
                    if (existingEnemy.id == id)
                    {
                        return existingEnemy;
                    }
                }
            }
            else
            {
                foreach (ExistingEnemy existingEnemy in _existingEnemy)
                {
                    if (existingEnemy.name == msg)
                    {
                        return existingEnemy;
                    }
                }
            }
            return null;
        }

        public static async Task SetRatingBaby(ulong id, int rating)
        {
            foreach (Baby baby in _baby)
            {
                if (baby.id == id)
                {
                    baby.rating = rating;
                    changedBaby = true;
                    Console.WriteLine("Rating= " + rating);
                    return;
                }
            }
        }
        public static async Task SetRatingItem(ulong id, int rating)
        {
            foreach (Item item in _item)
            {
                if (item.id == id)
                {
                    item.rating = rating;
                    changedItem = true;
                    Console.WriteLine("Rating= " + rating);
                    return;
                }
            }
        }
        public static async Task SetRatingItemActive(ulong id, int rating)
        {
            foreach (ItemActive itemActive in _itemActive)
            {
                if (itemActive.id == id)
                {
                    itemActive.rating = rating;
                    changedItemActive = true;
                    Console.WriteLine("Rating= " + rating);
                    return;
                }
            }
        }
        public static async Task SetRatingEnemy(ulong id, int rating)
        {
            foreach (Enemy enemy in _enemy)
            {
                if (enemy.id == id)
                {
                    enemy.rating = rating;
                    changedEnemy = true;
                    Console.WriteLine("Rating= " + rating);
                    return;
                }
            }
        }

        public static async Task<List<Baby>> GetBestRatedBabies()
        {
            int[] bestRating = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<Baby> sortedBaby = _baby.OrderByDescending(b => b.rating).ToList();
            List<Baby> babies = new List<Baby>();
            int i = 0;

            foreach (Baby baby in sortedBaby)
            {
                if (i == 0 && baby.rating > bestRating[0])
                {
                    bestRating[0] = baby.rating;
                    babies.Add(baby);
                }
                
                if (i != 0 && baby.rating >= bestRating[i] && baby.rating <= bestRating[i - 1])
                {
                    bestRating[i] = baby.rating;
                    babies.Add(baby);
                }
                i++;
                if (i == 10) break;
            }
            return babies;
        }
        public static async Task<List<Item>> GetBestRatedItems()
        {
            int[] bestRating = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<Item> sortedItem = _item.OrderByDescending(i => i.rating).ToList();
            List<Item> items = new List<Item>();
            int i = 0;

            foreach (Item item in sortedItem)
            {
                if (i == 0 && item.rating > bestRating[0])
                {
                    bestRating[0] = item.rating;
                    items.Add(item);
                }

                if (i != 0 && item.rating >= bestRating[i] && item.rating <= bestRating[i - 1])
                {
                    bestRating[i] = item.rating;
                    items.Add(item);
                }
                i++;
                if (i == 10) break;
            }
            return items;
        }
        public static async Task<List<ItemActive>> GetBestRatedItemsActive()
        {
            int[] bestRating = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<ItemActive> sortedItemActive = _itemActive.OrderByDescending(i => i.rating).ToList();
            List<ItemActive> itemsActive = new List<ItemActive>();
            int i = 0;

            foreach (ItemActive itemActive in sortedItemActive)
            {
                if (i == 0 && itemActive.rating > bestRating[0])
                {
                    bestRating[0] = itemActive.rating;
                    itemsActive.Add(itemActive);
                }

                if (i != 0 && itemActive.rating >= bestRating[i] && itemActive.rating <= bestRating[i - 1])
                {
                    bestRating[i] = itemActive.rating;
                    itemsActive.Add(itemActive);
                }
                i++;
                if (i == 10) break;
            }
            return itemsActive;
        }
        public static async Task<List<Enemy>> GetBestRatedEnemies()
        {
            int[] bestRating = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<Enemy> sortedEnemy = _enemy.OrderByDescending(b => b.rating).ToList();
            List<Enemy> enemies = new List<Enemy>();
            int i = 0;

            foreach (Enemy enemy in sortedEnemy)
            {
                if (i == 0 && enemy.rating > bestRating[0])
                {
                    bestRating[0] = enemy.rating;
                    enemies.Add(enemy);
                }

                if (i != 0 && enemy.rating >= bestRating[i] && enemy.rating <= bestRating[i - 1])
                {
                    bestRating[i] = enemy.rating;
                    enemies.Add(enemy);
                }
                i++;
                if (i == 10) break;
            }
            return enemies;
        }

        public static async Task DeleteBaby(Baby baby)
        {
            _baby.Remove(baby);
            changedBaby = true;
        }
        public static async Task DeleteItem(Item item)
        {
            _item.Remove(item);
            changedItem = true;
        }
        public static async Task DeleteItemActive(ItemActive itemActive)
        {
            _itemActive.Remove(itemActive);
            changedItemActive = true;
        }
        public static async Task DeleteEnemy(Enemy enemy)
        {
            _enemy.Remove(enemy);
            changedEnemy = true;
        }

        public static async Task DeleteAllBaby(int delRating)
        {
            _baby.RemoveAll(baby => baby.rating <= delRating);
            changedBaby = true;
        }
        public static async Task DeleteAllItem(int delRating)
        {
            _item.RemoveAll(item => item.rating <= delRating);
            changedItem = true;
        }
        public static async Task DeleteAllItemActive(int delRating)
        {
            _itemActive.RemoveAll(itemActive => itemActive.rating <= delRating);
            changedItemActive = true;
        }
        public static async Task DeleteAllEnemy(int delRating)
        {
            _enemy.RemoveAll(enemy => enemy.rating <= delRating);
            changedEnemy = true;
        }

        public static async Task WriteToJson()
        {
            JsonHandler jsonHandler = new JsonHandler();
            if (_baby.Count != 0 && changedBaby)
            {
                await jsonHandler.WriteBaby(_baby);
                changedBaby = false;
                Console.WriteLine("Wrote Baby");
            }
            if (_item.Count != 0 && changedItem)
            {
                await jsonHandler.WriteItem(_item);
                changedItem = false;
                Console.WriteLine("Wrote Item");
            }
            if (_itemActive.Count != 0 && changedItemActive)
            {
                await jsonHandler.WriteItemActive(_itemActive);
                changedItemActive = false;
                Console.WriteLine("Wrote ItemActive");
            }
            if (_enemy.Count != 0 && changedEnemy)
            {
                await jsonHandler.WriteEnemy(_enemy);
                changedEnemy = false;
                Console.WriteLine("Wrote Enemy");
            }
        }
    }
}
