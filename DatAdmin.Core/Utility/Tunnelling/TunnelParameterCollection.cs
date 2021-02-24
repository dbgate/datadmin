using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace DatAdmin
{
    public class TunnelParameterCollection : DbParameterCollection
    {
        List<TunnelParameter> m_params = new List<TunnelParameter>();

        public override int Add(object value)
        {
            m_params.Add((TunnelParameter)value);
            return m_params.Count - 1;
        }

        public override void AddRange(Array values)
        {
            foreach (TunnelParameter par in values) Add(par);
        }

        public override void Clear()
        {
            m_params.Clear();
        }

        public override bool Contains(string parameterName)
        {
            return IndexOf(parameterName) >= 0;
        }

        public override bool Contains(object value)
        {
            return m_params.Contains((TunnelParameter)value);
        }

        public override void CopyTo(Array array, int index)
        {
            throw new NotImplementedError("DAE-00305");
        }

        public override int Count
        {
            get { return m_params.Count; }
        }

        public override System.Collections.IEnumerator GetEnumerator()
        {
            return m_params.GetEnumerator();
        }

        protected override DbParameter GetParameter(string parameterName)
        {
            foreach (TunnelParameter par in m_params)
            {
                if (par.ParameterName == parameterName) return par;
            }
            return null;
        }

        protected override DbParameter GetParameter(int index)
        {
            return m_params[index];
        }

        public override int IndexOf(string parameterName)
        {
            int index = 0;
            foreach (TunnelParameter par in m_params)
            {
                if (par.ParameterName == parameterName) return index;
                index++;
            }
            return -1;
        }

        public override int IndexOf(object value)
        {
            return m_params.IndexOf((TunnelParameter)value);
        }

        public override void Insert(int index, object value)
        {
            m_params.Insert(index, (TunnelParameter)value);
        }

        public override bool IsFixedSize
        {
            get { return false; }
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override bool IsSynchronized
        {
            get { return false; }
        }

        public override void Remove(object value)
        {
            m_params.Remove((TunnelParameter)value);
        }

        public override void RemoveAt(string parameterName)
        {
            m_params.RemoveAt(IndexOf(parameterName));
        }

        public override void RemoveAt(int index)
        {
            m_params.RemoveAt(index);
        }

        protected override void SetParameter(string parameterName, DbParameter value)
        {
            m_params[IndexOf(parameterName)] = (TunnelParameter)value;
        }

        protected override void SetParameter(int index, DbParameter value)
        {
            m_params[index] = (TunnelParameter)value;
        }

        public override object SyncRoot
        {
            get { return null; }
        }
    }
}
