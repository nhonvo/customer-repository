namespace Applications.Utils
{
    public static class StringHash
    {
        public static string Hash(this string inputString)
        {
            var result = BCrypt.Net.BCrypt.HashPassword(inputString);

            return result;
        }
    }
}

