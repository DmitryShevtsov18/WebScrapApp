using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace WebScrapApp.Core
{
    public class SReportLineCollection : CollectionBase
    {
        public SReportLineCollection()
        {

        }

        public SReportLine this[int _index]
        {
            get
            {
                return (SReportLine)this.List[_index];
            }
            set
            {
                this.List[_index] = value;
            }
        }

        public int IndexOf(SReportLine _reportLine)
        {
            int ret = -1;

            if (_reportLine != null)
            {
                ret = base.List.IndexOf(_reportLine);
            }

            return ret;
        }

        public int Add(SReportLine _reportLine)
        {
            int ret = -1;

            if (_reportLine != null)
            {
                ret = this.List.Add(_reportLine);
            }

            return ret;
        }

        public void Remove(SReportLine _reportLine)
        {
            this.InnerList.Remove(_reportLine);
        }

        public void AddRange(SReportLineCollection _collection)
        {
            if (_collection != null)
            {
                this.InnerList.AddRange(_collection);
            }
        }

        public void Insert(int _index, SReportLine _reportLine)
        {
            if (_index <= List.Count && _reportLine != null)
            {
                this.List.Insert(_index, _reportLine);
            }
        }

        public bool Contains(SReportLine _reportLine)
        {
            return this.List.Contains(_reportLine);
        }

    }
}
