using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace CarShowroom
{
    public class Grouping<K, T> : ObservableCollection<T>
    {
        public K Name { get; private set; }
        public Grouping(K name, IEnumerable<T> items)
        {
            Name = name;
            foreach (T item in items)
                Items.Add(item);
        }
    }
}
