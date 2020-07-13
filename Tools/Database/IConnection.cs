using System;
using System.Collections.Generic;
using System.Data;

namespace Tools.Database
{
    public interface IConnection
    {
        int ExecuteNonQuery(Command command);
        IEnumerable<TResult> ExecuteReader<TResult>(Command command, Func<IDataRecord, TResult> selector);
        object ExecuteScalar(Command command);
        DataSet GetDataSet(Command command);
        DataTable GetDataTable(Command command);
    }
}