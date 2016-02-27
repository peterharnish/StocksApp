using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Stocks.Entity
{
    public class PurchasesList : List<Purchase>, IBindingList
    {
         /// <summary>
        /// Constructor. 
        /// </summary>
        /// <param name="collection"> Collection of purchases. </param>
        public PurchasesList(IEnumerable<Purchase> collection)
        {
            this.AddRange(collection);
        }

        public void AddIndex(PropertyDescriptor property)
        {
            throw new NotImplementedException();
           
        }

        public object AddNew()
        {
            Purchase purchase = new Purchase() { PurchaseDate = DateTime.Now, TrailingStop = 0.25m };
            this.Add(purchase);
            return purchase;
        }

        public bool AllowEdit
        {
            get { return true; }
        }

        public bool AllowNew
        {
            get { return true; }
        }

        public bool AllowRemove
        {
            get { return true; }
        }

        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            throw new NotImplementedException();
        }

        public int Find(PropertyDescriptor property, object key)
        {
            throw new NotImplementedException();
        }

        public bool IsSorted
        {
            get { throw new NotImplementedException(); }
        }

        public event ListChangedEventHandler ListChanged;

        public void RemoveIndex(PropertyDescriptor property)
        {
            throw new NotImplementedException();
        }

        public void RemoveSort()
        {
            throw new NotImplementedException();
        }

        public ListSortDirection SortDirection
        {
            get { throw new NotImplementedException(); }
        }

        public PropertyDescriptor SortProperty
        {
            get { throw new NotImplementedException(); }
        }

        public bool SupportsChangeNotification
        {
            get { return true; }
        }

        public bool SupportsSearching
        {
            get { throw new NotImplementedException(); }
        }

        public bool SupportsSorting
        {
            get { return false; }
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public bool IsFixedSize
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }
    }
}
