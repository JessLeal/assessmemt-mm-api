namespace MoneyMeAPI.Constants
{
    
    public static class DefaultValues
    {
        public static double AMOUNT_MAX { get; set; } = 15000;
        public static double AMOUNT_MIN { get; set; } = 2100;

        public static int TERM_MAX { get; set; } = 60;
        public static int TERM_MIN { get; set; } = 1;

        public static string DEFAULT_PRODUCT { get; set; } = "ProductA";

        public static string MOBILE { get; set; } = "mobile";
        public static string DOMAIN { get; set; } = "domain";
    }
}
