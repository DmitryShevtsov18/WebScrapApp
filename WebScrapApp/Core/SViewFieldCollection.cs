using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace WebScrapApp.Core
{
    public class SViewFieldCollection : CollectionBase
    {
        public SViewFieldCollection()
        {

        }

        public SViewField this[int _index]
        {
            get
            {
                return (SViewField)this.List[_index];
            }
            set
            {
                this.List[_index] = value;
            }
        }

        public int IndexOf(SViewField _viewField)
        {
            int ret = -1;

            if (_viewField != null)
            {
                ret = base.List.IndexOf(_viewField);
            }

            return ret;
        }

        public int Add(SViewField _viewField)
        {
            int ret = -1;

            if (_viewField != null)
            {
                ret = this.List.Add(_viewField);
            }

            return ret;
        }

        public void Remove(SViewField _viewField)
        {
            this.InnerList.Remove(_viewField);
        }

        public void AddRange(SViewFieldCollection _collection)
        {
            if (_collection != null)
            {
                this.InnerList.AddRange(_collection);
            }
        }

        public void Insert(int _index, SViewField _viewField)
        {
            if (_index <= List.Count && _viewField != null)
            {
                this.List.Insert(_index, _viewField);
            }
        }

        public bool Contains(SViewField _viewField)
        {
            return this.List.Contains(_viewField);
        }

        public SViewFieldCollection Clone()
        {
            SViewFieldCollection ret = new SViewFieldCollection();

            foreach (SViewField viewField in this)
            {
                ret.Add((SViewField)viewField.Clone());
            }

            return ret;
        }

    }
}
