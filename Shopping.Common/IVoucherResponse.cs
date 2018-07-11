using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Common
{
    public interface IVoucherResponse
    {
        bool Status { get; }
        decimal DiscountAmount { get; }
    }
}
