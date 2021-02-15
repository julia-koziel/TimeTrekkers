using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static class Extensions
{

    public static T Next<T>(this T src) where T : struct
    {
        if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

        T[] Arr = (T[])Enum.GetValues(src.GetType());
        int j = Array.IndexOf<T>(Arr, src) + 1;
        return (Arr.Length==j) ? Arr[0] : Arr[j];            
    }

    /// <summary>
    /// Shuffles array in place
    /// </summary>
    /// <param name="rng"></param>
    /// <param name="array"></param>
    /// <typeparam name="T"></typeparam>
    public static void Shuffle<T> (this System.Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1) 
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    /// <summary>
    /// Shuffles array in place
    /// </summary>
    /// <param name="array"></param>
    /// <typeparam name="T"></typeparam>
    public static void Shuffle<T> (this T[] array)
    {
        var rng = new System.Random();
        rng.Shuffle(array);
    }

    /// <summary>
    /// Returns shuffled copy of array
    /// </summary>
    /// <param name="array"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T[] Shuffled<T>(this T[] array)
    {
        var newArray = array.Clone() as T[];
        newArray.Shuffle();
        return newArray;
    }

    public static void ForEach<T> (this IEnumerable<T> ie, Action<T> action)
    {
        foreach (var item in ie)
        {
            action(item);
        }
    }

    /// <summary>
    /// Between check <![CDATA[min <= value <= max]]> 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">the value to check</param>
    /// <param name="min">Inclusive minimum border</param>
    /// <param name="max">Inclusive maximum border</param>
    /// <returns>return true if the value is between the min & max else false</returns>
    public static bool IsBetweenII<T>(this T value, T min, T max) where T:IComparable<T>
    {
        return (min.CompareTo(value) <= 0) && (value.CompareTo(max) <= 0);
    }

    /// <summary>
    /// Between check <![CDATA[min < value <= max]]>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">the value to check</param>
    /// <param name="min">Exclusive minimum border</param>
    /// <param name="max">Inclusive maximum border</param>
    /// <returns>return true if the value is between the min & max else false</returns>
    public static bool IsBetweenEI<T>(this T value, T min, T max) where T:IComparable<T>
    {
        return (min.CompareTo(value) < 0) && (value.CompareTo(max) <= 0);
    }

    /// <summary>
    /// between check <![CDATA[min <= value < max]]>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">the value to check</param>
    /// <param name="min">Inclusive minimum border</param>
    /// <param name="max">Exclusive maximum border</param>
    /// <returns>return true if the value is between the min & max else false</returns>
    public static bool IsBetweenIE<T>(this T value, T min, T max) where T:IComparable<T>
    {
        return (min.CompareTo(value) <= 0) && (value.CompareTo(max) < 0);
    }

    /// <summary>
    /// between check <![CDATA[min < value < max]]>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">the value to check</param>
    /// <param name="min">Exclusive minimum border</param>
    /// <param name="max">Exclusive maximum border</param>
    /// <returns>return true if the value is between the min & max else false</returns>

    public static bool IsBetweenEE<T>(this T value, T min, T max) where T:IComparable<T>
    {
        return (min.CompareTo(value) < 0) && (value.CompareTo(max) < 0);
    }

    public static T[] ForEach<T>(this T[] input, Action<T> action)
    {
        for (int i = 0; i < input.Length; i++)
        {
            action(input[i]);
        }
        return input;
    }

    public static T[] ForEach<T>(this T[] input, Action<int, T> action)
    {
        for (int i = 0; i < input.Length; i++)
        {
            action(i, input[i]);
        }
        return input;
    }

    public static T[] ForEach<T>(this T[] input, Func<T, T> func)
    {
        for (int i = 0; i < input.Length; i++)
        {
            input[i] = func(input[i]);
        }
        return input;
    }

    public static T[] ForEach<T>(this T[] input, Func<int, T, T> func)
    {
        for (int i = 0; i < input.Length; i++)
        {
            input[i] = func(i, input[i]);
        }
        return input;
    }

    public static T Pop<T>(this List<T> input, int pos = -1)
    {
        pos = pos == -1 ? input.Count - 1 : pos;
        T popped = input[pos];
        input.RemoveAt(pos);
        return popped;
    }

    public static T[] Slice<T>(this T[] source, int start, int end)
    {
        // Handles negative ends.
        if (end < 0)
        {
            end = source.Length + end;
        }
        int len = end - start;

        // Return new array.
        T[] res = new T[len];
        for (int i = 0; i < len; i++)
        {
            res[i] = source[i + start];
        }
        return res;
    }

    public static float Average<T>(this float[] input)
    {
        float sum = 0;
        input.ForEach(v => sum += v);
        return sum / input.Length;
    }

    public static bool In<T>(this T obj, params T[] args)
    {
        return args.Contains(obj);
    }

    public static int ArgMax<T>(this IEnumerable<T> ie)
        where T : IComparable<T>
    {
        IEnumerator<T> e = ie.GetEnumerator();
        if (!e.MoveNext())
            return -1;

        int max_ix = 0;
        T t_try = e.Current;
        if (!e.MoveNext())
            return max_ix;

        T tx, max_val = e.Current;
        int i = 1;
        do
        {
            if ((tx = e.Current).CompareTo(max_val) > 0)
            {
                max_val = tx;
                max_ix = i;
            }
            i++;
        }
        while (e.MoveNext());
        return max_ix;
    }

    public static int ArgMin<T>(this IEnumerable<T> ie)
        where T : IComparable<T>
    {
        IEnumerator<T> e = ie.GetEnumerator();
        if (!e.MoveNext())
            return -1;

        int min_ix = 0;
        T t_try = e.Current;
        if (!e.MoveNext())
            return min_ix;

        T tx, min_val = e.Current;
        int i = 1;
        do
        {
            if ((tx = e.Current).CompareTo(min_val) < 0)
            {
                min_val = tx;
                min_ix = i;
            }
            i++;
        }
        while (e.MoveNext());
        return min_ix;
    }

    ///<summary>Finds the index of the first item matching an expression in an enumerable.</summary>
    ///<param name="items">The enumerable to search.</param>
    ///<param name="predicate">The expression to test the items against.</param>
    ///<returns>The index of the first matching item, or -1 if no items match.</returns>
    public static int FindIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate) {
        if (items == null) throw new ArgumentNullException("items");
        if (predicate == null) throw new ArgumentNullException("predicate");

        int retVal = 0;
        foreach (var item in items) {
            if (predicate(item)) return retVal;
            retVal++;
        }
        return -1;
    }
    ///<summary>Finds the index of the first occurrence of an item in an enumerable.</summary>
    ///<param name="items">The enumerable to search.</param>
    ///<param name="item">The item to find.</param>
    ///<returns>The index of the first matching item, or -1 if the item was not found.</returns>
    public static int IndexOf<T>(this IEnumerable<T> items, T item) { return items.FindIndex(i => EqualityComparer<T>.Default.Equals(item, i)); }

    public static string Join<T>(this string separator, IEnumerable<T> objects)
    {
        return string.Join(separator, objects);
    }

    public static T[] Row<T>(this T[,] matrix, int rowIdx)
    {
        if (rowIdx >= matrix.GetLength(0)) throw new IndexOutOfRangeException();

        T[] row = new T[matrix.GetLength(1)];
        for (int i = 0; i < row.Length; i++)
        {
            row[i] = matrix[rowIdx, i];
        }

        return row;
    }

    public static T[] Col<T>(this T[,] matrix, int colIdx)
    {
        if (colIdx >= matrix.GetLength(1)) throw new IndexOutOfRangeException();

        T[] col = new T[matrix.GetLength(0)];
        for (int i = 0; i < col.Length; i++)
        {
            col[i] = matrix[i, colIdx];
        }

        return col;
    }

    public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> ie, int count)
    {
        var length = ie.Count();
        if (count > length) throw new ArgumentOutOfRangeException();

        return ie.Take(length - count);
    }

    public static IEnumerable<T> Repeat<T>(this IEnumerable<T> ie, int count)
    {
        return Enumerable.Repeat(ie, count).SelectMany(x => x);
    }

    public static IEnumerable<T> RepeatForSize<T>(this IEnumerable<T> ie, int size)
    {
        var block = ie.Count();
        return ie.Repeat(size / block + 1).SkipLast(block - size % block);
    }
}
