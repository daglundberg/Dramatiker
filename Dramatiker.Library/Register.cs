using System.Collections;

namespace Dramatiker.Library;

public class Register<T> : IEnumerable<T>
{
	private readonly EqualityComparer<T> comparer = EqualityComparer<T>.Default;
	private T[] _items;

	public Register(int size = 8)
	{
		if (size < 2)
			size = 2;

		_items = new T[size];
	}

	// Sets or Gets the element at the given index.
	// 
	public T this[int index]
	{
		get
		{
			// Following trick can reduce the range check by one
			if ((uint) index >= (uint) Count)
				throw new ArgumentOutOfRangeException();

			return _items[index];
		}

		set
		{
			if ((uint) index >= (uint) Count)
				throw new ArgumentOutOfRangeException();

			_items[index] = value;
		}
	}

	public int Count { get; private set; }

	private int Capacity
	{
		get => _items.Length;
		set
		{
			if (value <= _items.Length) return;

			var newItems = new T[value];

			if (Count > 0)
				Array.Copy(_items, 0, newItems, 0, Count);

			_items = newItems;
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public IEnumerator<T> GetEnumerator()
	{
		return new Enum<T>(_items, Count);
	}

	public event EventHandler<ItemAddedEventArgs>? ItemAdded;

	private void EnsureCapacity(int min)
	{
		if (_items.Length < min)
			Capacity = _items.Length * 2;
	}

	public void Add(T item)
	{
		if (item == null)
			return;

		if (Contains(item))
			return;

		if (Count == _items.Length)
			EnsureCapacity(Count + 1);

		_items[Count++] = item;
		ItemAdded?.Invoke(this, new ItemAddedEventArgs(item));
	}

	public void AddRange(T[] items)
	{
		foreach (var item in items) Add(item);
	}

	public bool Contains(T item)
	{
		for (var i = 0; i < Count; i++)
			if (comparer.Equals(_items[i], item))
				return true;

		return false;
	}

	public T? Find(Predicate<T> match)
	{
		if (match == null)
			throw new ArgumentNullException();

		for (var i = 0; i < Count; i++)
			if (match(_items[i]))
				return _items[i];

		return default;
	}
}

public class ItemAddedEventArgs : EventArgs
{
	public object Item;

	public ItemAddedEventArgs(object item)
	{
		Item = item;
	}
}

// When you implement IEnumerable, you must also implement IEnumerator.
public class Enum<T> : IEnumerator<T>
{
	private readonly T[] _items;
	private readonly int _size;

	// Enumerators are positioned before the first element
	// until the first MoveNext() call.
	private int position = -1;

	public Enum(T[] list, int size)
	{
		_items = list;
		_size = size;
	}

	public bool MoveNext()
	{
		position++;
		return position < _size;
	}

	public void Reset()
	{
		position = -1;
	}

	object? IEnumerator.Current => Current;

	public T Current
	{
		get
		{
			try
			{
				return _items[position];
			}
			catch (IndexOutOfRangeException)
			{
				throw new InvalidOperationException();
			}
		}
	}

	public void Dispose()
	{
	}
}