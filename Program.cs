using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap
{
	public class Heap<T>
	{
		public Func<T, T, int> Comparer;

		protected T[] mainArray;
		protected T temp;
		protected int lastElementIndex = -1;

		public Heap(int capacity)
		{
			mainArray = new T[capacity];
		}

		protected void SiftDown(int currentIndex)
		{
			int left = 2 * currentIndex + 1;
			int right = 2 * currentIndex + 2;
			int largestElementIndex = currentIndex;
			if (left <= lastElementIndex && Comparer(mainArray[left], mainArray[largestElementIndex]) > 0)
				largestElementIndex = left;
			if (right <= lastElementIndex && Comparer(mainArray[right], mainArray[largestElementIndex]) > 0)
				largestElementIndex = right;
			if (largestElementIndex != currentIndex)
			{
				Swap(currentIndex, largestElementIndex);
				SiftDown(largestElementIndex);
			}
		}

		protected void SiftUp(int currentIndex)
		{
			while (currentIndex > -1)
			{
				int parent = (currentIndex - 1) / 2;
				if (Comparer(mainArray[currentIndex], mainArray[parent]) <= 0)
					return;
				Swap(currentIndex, parent);
				currentIndex = parent;
			}
		}

		protected void BuildHeap()
		{
			for (int i = lastElementIndex / 2; i >= 0; --i)
				SiftDown(i);
		}

		public void Add(T element)
		{
			mainArray[++lastElementIndex] = element;
			SiftUp(lastElementIndex);
		}

		public T ExtractTop()
		{
			if (lastElementIndex < 0)
				throw new ArgumentOutOfRangeException("There are no elements in heap");
			T result = (T)mainArray[0];
			Swap(0, lastElementIndex);
			--lastElementIndex;
			SiftDown(0);
			return result;
		}

		protected void Swap(int a, int b)
		{
			temp = mainArray[a];
			mainArray[a] = mainArray[b];
			mainArray[b] = temp;
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			var TestHeap = new Heap<int>(33) { Comparer = (a, b) => a - b };
			var rand = new Random();
			for (int i = 0; i < 33; ++i)
				TestHeap.Add(rand.Next() % 100);
			for (int i = 0; i < 33; ++i)
				Console.WriteLine(TestHeap.ExtractTop());
		}
	}
}
