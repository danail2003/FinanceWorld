namespace FinanceWorld.Services.Data.Home
{
    using System.Collections.Generic;

    public interface IHomeService
    {
        List<T> GetLastThreeNews<T>();

        List<T> GetLastThreeAnalyzes<T>();
    }
}
