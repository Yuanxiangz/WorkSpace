using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
	public class ArrayQueue<T>
	{
		private T[] queue;
		private int length;
		private bool isEmpty, isFull;
		int enqueueIndex, dequeueIndex;

		public bool IsEmpty
		{
			get
			{
				return isEmpty;
			}
		}

		public bool IsFull
		{
			get
			{
				return isFull;
			}
		}

		public ArrayQueue(int n)
		{
			if (n < 1)
				throw new Exception("Queue length must bigger than 1!");

			length = n;
			queue = new T[n];
			enqueueIndex = 0;
			dequeueIndex = 0;
			isEmpty = true;
			isFull = false;
		}

		public void Enqueue(T t)
		{
			if (!isFull)
			{
				queue[enqueueIndex] = t;
				enqueueIndex++;
				if (enqueueIndex > length - 1)
				{
					enqueueIndex = enqueueIndex % length;
				}

				isFull = enqueueIndex == dequeueIndex ? true : false;
				if (isEmpty)
					isEmpty = false;
			}
			else
				throw new Exception("Queue is full!");
		}

		public T Dequeue()
		{
			T t = default(T);
			if (!isEmpty)
			{
				t = queue[dequeueIndex];
				queue[dequeueIndex] = default(T);
				dequeueIndex++;
				if (dequeueIndex > length - 1)
				{
					dequeueIndex = dequeueIndex % length;
				}

				isEmpty = enqueueIndex == dequeueIndex ? true : false;
				if (isFull)
					isFull = false;
			}
			else
				throw new Exception("Queue is empty!");

			return t;
		}
	}
}
