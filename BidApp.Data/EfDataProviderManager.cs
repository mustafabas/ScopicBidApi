using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BidApp.Data
{
    public partial class EfDataProviderManager : BaseDataProviderManager
    {
        public EfDataProviderManager() : base()
        {
        }

        public override IDataProvider LoadDataProvider()
        {

            var providerName = "sqlserver";
            if (String.IsNullOrWhiteSpace(providerName))
                throw new ArgumentNullException("Data Settings doesn't contain a providerName");

            switch (providerName.ToLowerInvariant())
            {
                case "sqlserver":
                    return new SqlServerDataProvider();
                //case "sqlce":
                //    return new SqlCeDataProvider();
                default:
                    throw new ArgumentNullException(string.Format("Not supported dataprovider name: {0}", providerName));
            }
        }

    }
}
