using Shopping.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.BusinessLayer
{
    class VoucherResponse : IVoucherResponse
    {
        public bool Status { get; private set; }
        public decimal DiscountAmount { get; private set; }
        public VoucherResponse(bool status, decimal discountAmount)
        {
            Status = status;
            DiscountAmount = discountAmount;
        }

    }
}
