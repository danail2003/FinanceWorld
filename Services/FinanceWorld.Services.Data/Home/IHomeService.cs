namespace FinanceWorld.Services.Data.Home
{
    using System.Collections.Generic;

    public interface IHomeService
    {
        IEnumerable<T> GetLastThreeNews<T>();

        IEnumerable<T> GetLastThreeAnalyzes<T>();
    }
}
