using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starbot.Types
{
    public static class Idea
    {
        private static List<Baby> _baby;
        private static List<Item> _item;
        private static List<ItemActive> _itemActive;
        private static List<Enemy> _enemy;
        private static bool changedBaby;
        private static bool changedItem;
        private static bool changedItemActive;
        private static bool changedEnemy;
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
            Baby[] bestBaby = new Baby[bestRating.Length];
            List<Baby> babies = new List<Baby>();

            foreach (Baby baby in _baby)
            {
                if (baby.rating > bestRating[0])
                {
                    bestRating[0] = baby.rating;
                    bestBaby[0] = baby;
                }

                for (int i = 1; i < bestRating.Length; i++)
                {
                    if (baby.rating > bestRating[i] && /*bestRating[i]*/baby.rating < bestRating[i - 1])
                    {
                        bestRating[i] = baby.rating;
                        bestBaby[i] = baby;
                    }
                }
            }
            foreach (Baby baby in bestBaby)
            {
                if (baby != null)
                {
                    babies.Add(baby);
                }
            }
            return babies;
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
