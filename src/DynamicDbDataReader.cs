using System;
using System.Data.Common;
using System.Dynamic;

namespace RussellEast.DataAccessBuilder
{
    public class DynamicDbDataReader : DynamicObject
    {
        private readonly DbDataReader _reader;

        public DynamicDbDataReader(DbDataReader reader)
        {
            _reader = reader;
        }

        public bool Read()
        {
            return _reader != null && (!_reader.IsClosed) && _reader.Read();
        }

        public void NextResult()
        {
            _reader.NextResult();
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            try
            {
                var rawResult = _reader.GetValue(_reader.GetOrdinal(binder.Name));
                result = rawResult == DBNull.Value ? null : rawResult;
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                throw new ColumnNotFoundException(binder.Name);
            }
        }
    }
}