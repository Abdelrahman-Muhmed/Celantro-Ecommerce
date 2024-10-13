using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace EcommerceCoffe_DAL.Prsistence.Data.Configruations
{
    public class EnumToStringConverter<TEnum> : Convention where TEnum : struct, Enum
    {
        public EnumToStringConverter()
        {
            Properties<TEnum>()
                .Configure(c => c.HasColumnType("nvarchar"));
        }
    }
}