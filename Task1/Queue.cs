using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    /// <summary>
    /// A simple Queue of generic objects
    /// </summary>
    public class Queue<T> : IEnumerable<T>, IEnumerable
    {
        #region Private Fields
        private T[] array;
        private int head;
        private int tail;
        private int size;

        private const int defaultCapacity = 4;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the number of elements contained in the Queue<T>
        /// </summary>
        public int Count
        {
            get { return size; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Queue<T> class that is empty and has the default initial capacity
        /// </summary>
        public Queue()
        {
            array = new T[0];
        }

        /// <summary>
        /// nitializes a new instance of the Queue<T> class that is empty and has the specified initial capacity
        /// </summary>
        /// <param name="capacity"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Queue(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            array = new T[capacity];
            head = 0;
            tail = 0;
            size = 0;

        }

        /// <summary>
        /// Initializes a new instance of the Queue<T> class that contains elements copied from the specified
        /// collection and has sufficient capacity to accommodate the number of elements copied
        /// </summary>
        /// <param name="collection"></param>
        public Queue(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            array = new T[defaultCapacity];
            size = 0;

            using (IEnumerator<T> en = collection.GetEnumerator())
            {
                while (en.MoveNext())
                {
                    Enqueue(en.Current);
                }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Removes all objects from the Queue<T>
        /// </summary>
        public void Clear()
        {
            if (head < tail)
                Array.Clear(array, head, size);
            else
            {
                Array.Clear(array, head, array.Length - head);
                Array.Clear(array, 0, tail);
            }

            head = 0;
            tail = 0;
            size = 0;
        }
        /// <summary>
        /// Returns an enumerator that iterates through the Queue<T>
        /// </summary>
        /// <returns>System.Collections.Generic.Queue<T>.Enumerator</returns>
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        /// <summary>
        /// Removes and returns the object at the beginning of the Queue<T>
        /// </summary>
        /// <returns>The object that is removed from the beginning of the Queue<T></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Dequeue()
        {
            if (size == 0)
                throw new InvalidOperationException("Queue is empty");

            T removed = array[head];
            array[head] = default(T);
            head = (head + 1) % array.Length;
            size--;
            return removed;
        }
        /// <summary>
        /// Adds an object to the end of the Queue<T>
        /// </summary>
        /// <param name="item">The object to add to the Queue<T></param>
        public void Enqueue(T item)
        {
            if (size == array.Length)
            {
                if (size == array.Length)
                {
                    int newcapacity = array.Length * 2;
                    SetCapacity(newcapacity);
                }
            }

            array[tail] = item;
            tail = (tail + 1) % array.Length;
            size++;
        }
        /// <summary>
        /// Returns the object at the beginning of the Queue<T> without removing it
        /// </summary>
        /// <returns>The object at the beginning of the Queue<T></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Peek()
        {
            if (size == 0)
                throw new InvalidOperationException("CustomQueue is empty");

            return array[head];
        }
        /// <summary>
        /// Returns the item at a particular location in the queue
        /// </summary>
        /// <param name="i">The number of the member in the queue whose value is required to return</param>
        public T GetElement(int i)
        {
            return array[(head + i) % array.Length];
        }
        #endregion

        #region Private Members
        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

        private void SetCapacity(int capacity)
        {
            T[] newarray = new T[capacity];
            if (size > 0)
            {
                if (head < tail)
                {
                    Array.Copy(array, head, newarray, 0, size);
                }
                else
                {
                    Array.Copy(array, head, newarray, 0, array.Length - head);
                    Array.Copy(array, 0, newarray, array.Length - head, tail);
                }
            }

            array = newarray;
            head = 0;
            tail = (size == capacity) ? 0 : size;
        }
        #endregion

        public struct Enumerator : IEnumerator<T>
        {
            private readonly Queue<T> q;
            private int index;   // -1 = not started, -2 = ended/disposed
            private T currentElement;

            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator
            /// </summary>
            /// <exception cref="InvalidOperationException"></exception>
            public T Current
            {
                get
                {
                    if (index < 0)
                    {
                        if (index == -1)
                            throw new InvalidOperationException("Enumerator not started");
                        else
                            throw new InvalidOperationException();
                    }
                    return currentElement;
                }
            }

            internal Enumerator(Queue<T> q)
            {
                this.q = q;
                index = -1;
                currentElement = default(T);
            }
            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, 
            /// or resetting unmanaged resources
            /// </summary>
            public void Dispose()
            {
                index = -2;
                currentElement = default(T);
            }
            /// <summary>
            /// Advances the enumerator to the next element of the collection
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                if (index == -2)
                    return false;

                index++;

                if (index == q.size)
                {
                    index = -2;
                    currentElement = default(T);
                    return false;
                }

                currentElement = q.GetElement(index);
                return true;
            }        

            object IEnumerator.Current => Current;
            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            public void Reset()
            {
               index = -1;
            }
        }
    }
}
