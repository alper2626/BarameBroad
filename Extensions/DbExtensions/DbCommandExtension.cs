using System.Data.Common;

namespace Extensions.DbExtensions
{
    public static class DbCommandExtension
    {
        public static void AddWithValue(this DbCommand command, string parameterName, object parameterValue)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            command.Parameters.Add(parameter);
        }
    }
}