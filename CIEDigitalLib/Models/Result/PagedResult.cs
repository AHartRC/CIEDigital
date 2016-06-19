using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CIEDigitalLib.Models.View;

namespace CIEDigitalLib.Models.Result
{
    /// <summary>
    ///     Wrappes a list of items together with the total number of available items.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    [DataContract]
    public class PagedResult<T> : IEnumerable<T>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PagedResult&lt;T&gt;" /> class.
        /// </summary>
        public PagedResult()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PagedResult&lt;T&gt;" /> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="totalNumberOfItems">The total number of items.</param>
        /// <param name="paging">The paging.</param>
        public PagedResult(IEnumerable<T> items, int totalNumberOfItems, Paging paging)
        {
            Items = items;
            TotalNumberOfItems = totalNumberOfItems;
            Paging = paging;
        }

        /// <summary>
        ///     Gets the items.
        /// </summary>
        [DataMember]
        public IEnumerable<T> Items { get; private set; }

        /// <summary>
        ///     Gets the total number of items.
        /// </summary>
        [DataMember]
        public int TotalNumberOfItems { get; private set; }

        /// <summary>
        ///     Gets the total number of pages.
        /// </summary>
        /// <value>The total number of pages.</value>
        [IgnoreDataMember]
        public int TotalNumberOfPages
        {
            get
            {
                if (Paging == null || Paging.PageSize <= 0)
                {
                    return 0;
                }

                return (int) Math.Ceiling((double) TotalNumberOfItems/Paging.PageSize);
            }
        }

        /// <summary>
        ///     Gets the paging.
        /// </summary>
        [DataMember]
        public Paging Paging { get; private set; }

        /// <summary>
        ///     Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}