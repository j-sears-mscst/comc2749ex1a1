using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jsears2749ex1a1ef.Model
{
    public partial class PurchaseOrderHeader
    {
        public string ShortString
        {

            get
            {

                return this.PurchaseOrderID.ToString("00000") + " " + this.OrderDate.ToShortDateString(); 

            }

        }

    }
}
