using System;
using System.Collections.Generic;

namespace Counter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Some things to count
            var someApples = new List<Apple> { new Apple(), new Apple(), new Apple() };
            var boxOfApples = new Box<Apple>();
            boxOfApples.Add(new Apple());
            boxOfApples.Add(new Apple());
            var cart = new Cart<Apple>();
            cart.Add(boxOfApples);
            // Some counters
            var appleCounter = new Counter<Apple>();
            someApples.ForEach(appleCounter.Add);
            Console.WriteLine(appleCounter.Count); // Should be 3
            var cartCounter = new Counter<Cart<Apple>>();
            cartCounter.Add(cart);
            Console.WriteLine(cartCounter.Count); // Should be 2 (number of apples in the cart in total)
            var anythingCounter = new Counter<ICountable>();
            someApples.ForEach(anythingCounter.Add);
            anythingCounter.Add(cart);
            Console.WriteLine(anythingCounter.Count); // Should be 5 - sum of the above
            Console.ReadLine();
        }
    }

    public class Cart<T> : ICountable
    {

        List<Box<T>> boxesOfApples = new List<Box<T>>();

        public void Add(Box<T> box)
        {
            boxesOfApples.Add(box);
        }
        public int Count
        {
            get
            {
                int items = 0;
                foreach (var box in boxesOfApples)
                {
                   items += box.Count;
                }
                return items;
            }
        }
    }

   public class Box<T> : ICountable
    {
        List<Apple> boxOfApples = new List<Apple>();

        public void Add(Apple apple)
        {
            boxOfApples.Add(apple);
        }
        public int Count
        {
            get
            {
                return boxOfApples.Count;
            }
        }
    }
    public class Apple : ICountable
    {
        public int Count
        {
            get
            {
                return 1;
            }
        }
    }

    class Counter<T> where T : ICountable
    {
        public void Add(T item)
        {
            Count += item.Count;
        }

        public int Count { get; set; }


    }
    interface ICountable
    {
        int Count {get;}
    }
}