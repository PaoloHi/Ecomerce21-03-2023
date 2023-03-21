namespace EcommerceRealCVO.Tools
{
    public static class StringExtensionsBase
    {
        public static string Extract(this string input, int len)
        {
            if (string.IsNullOrEmpty(input) || input.Length < len)
            {
                return input;
            };

            return input.Substring(0, len);
        }
    }
}