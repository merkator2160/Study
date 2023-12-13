namespace Common.Extensions
{
    public static class GrpcExtensions
    {
        public static Byte[] GetBytes(this DateTime dateTime)
        {
            return BitConverter.GetBytes(dateTime.ToBinary());
        }
        public static DateTime ToDateTime(this Byte[] bytes)
        {
            return DateTime.FromBinary(BitConverter.ToInt64(bytes));
        }

        public static Byte[] GetBytes(this Int64 value)
        {
            return BitConverter.GetBytes(value);
        }
        public static Int64 ToInt64(this Byte[] bytes)
        {
            return BitConverter.ToInt64(bytes);
        }
    }
}