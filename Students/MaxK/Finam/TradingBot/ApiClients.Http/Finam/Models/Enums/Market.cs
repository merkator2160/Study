namespace ApiClientsHttp.Finam.Models.Enums
{
    public enum Market : Byte
    {
        /// <summary>
        /// Moscow Stock Exchange stock market
        /// </summary>
        Stock,

        /// <summary>
        /// Moscow Exchange futures market
        /// </summary>
        Forts,

        /// <summary>
        /// St. Petersburg Stock Exchange
        /// </summary>
        Spbex,

        /// <summary>
        /// US stock market
        /// </summary>
        Mma,

        /// <summary>
        /// Moscow Exchange currency market
        /// </summary>
        Ets,

        /// <summary>
        /// Moscow Exchange bond market
        /// </summary>
        Bonds,

        /// <summary>
        /// Moscow Stock Exchange options market
        /// </summary>
        Options
    }
}