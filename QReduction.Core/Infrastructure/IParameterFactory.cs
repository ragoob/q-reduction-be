using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace QReduction.Core.Infrastructure
{
    public interface IParameterFactory
    {
        DbParameter GetParameter(string parameterName, object parameterValue, ParameterDirection direction = ParameterDirection.Input);
        DbParameter GetParameter(string parameterName, object parameterValue, DbType dbType, ParameterDirection direction = ParameterDirection.Input);
        DbParameter GetParameter(string parameterName, object parameterValue, int size, ParameterDirection direction = ParameterDirection.Input);
        DbParameter GetParameter(string parameterName, object parameterValue, DbType dbType, int size, ParameterDirection direction = ParameterDirection.Input);
    }
}
