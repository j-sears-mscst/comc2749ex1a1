using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jsears2749ex1a1ef.Model
{
    public partial class Vendor
    {
        public string ShortString
        {

            get
            {

                return this.BusinessEntityID.ToString() + " " + this.Name;

            }

        }

    }

}
