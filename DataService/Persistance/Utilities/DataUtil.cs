using System;
using System.ComponentModel;
using System.Data;

namespace Persistance.Utilities
{
    public class DataUtil
    {
        public static T GetDataReaderValue<T>(string keyName, IDataReader reader)
        {
            return GetDataReaderValue<T>(reader.GetOrdinal(keyName), reader);
        }

        public static T GetDataReaderValue<T>(int columnOrdinal, IDataReader reader)
        {
            object o = reader[columnOrdinal];
            if (o != DBNull.Value)
            {
                return ((T)ChangeType(o, typeof(T)));
            }
            return default(T);
        }

        public static object ChangeType(object value, Type conversionType)
        {
            if (conversionType == null)
                throw new ArgumentNullException("conversionType");

            if (conversionType.IsGenericType &&
                conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                NullableConverter nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }

            if (conversionType.IsEnum)
            {
                return System.Enum.Parse(conversionType, value.ToString(), true);
            }
            else
            {
                return Convert.ChangeType(value, conversionType);
            }
        }
    }
}