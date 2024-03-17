namespace HBManager.Shared
{
    public static class ConsumerColor
    {
        public static readonly string ColorOne = "#1EC82CFF";
        public static readonly string ColorTwo = "#1EC8A5FF";
        public static readonly string ColorThree = "#2196F3FF";
        public static readonly string ColorFour = "#841EC8FF";

        public static readonly string ColorFive = "#C81E8DFF";
        public static readonly string ColorSix = "#C81E1EFF";
        public static readonly string ColorSeven = "#C8511EFF";
        public static readonly string ColorEight = "#C8C01EFF";

        public static readonly string ColorNine = "#C4C2C2FF";
        public static readonly string ColorTen = "#8A8A8AFF";
        public static readonly string ColorEleven = "#333333FF";
        public static readonly string ColorTwelve = "#241A16FF";

        public static List<string> Palette { get; set; } = new List<string>()
        {
            ColorOne, ColorTwo, ColorThree, ColorFour,
            ColorFive, ColorSix, ColorSeven, ColorEight,
            ColorNine, ColorTen , ColorEleven, ColorTwelve
        };




    }
}
