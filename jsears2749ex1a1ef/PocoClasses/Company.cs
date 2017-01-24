using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jsears2749ex1a1ef.Model
{
    public class Company
    {
        private static AdventureWorksEFEntities dbContext =
            new AdventureWorksEFEntities();
        public static List<Vendor> getVendors()
        {

            List<Vendor> vendorList = new List<Vendor>();


            try
            {
                vendorList =
                    (from v in dbContext.Vendors
                     orderby v.Name
                     select v).ToList();

            }

            catch (Exception ex)
            {

                throw ex;

            }

            return vendorList;

        }

        public static List<ShipMethod> getShipMethods()
        {

            List<ShipMethod> shipMethodList = new List<ShipMethod>();


            try
            {
                shipMethodList =
                    (from s in dbContext.ShipMethods
                     orderby s.Name
                     select s).ToList();

            }

            catch (Exception ex)
            {

                throw ex;

            }

            return shipMethodList;

        }

        public static List<Employee> getEmployees()
        {

            List<Employee> employeeList = new List<Employee>();


            try
            {
                employeeList =
                    (from e in dbContext.Employees.Include("Person")
                     orderby e.Person.LastName, e.Person.FirstName
                     select e).ToList();

            }

            catch (Exception ex)
            {

                throw ex;

            }

            return employeeList;

        }

        public static List<PurchaseOrderHeader> getPurchaseOrderHeaders(int vendorID)
        {

            List<PurchaseOrderHeader> purchaseOrderHearerList = new List<PurchaseOrderHeader>();

            try
            {
                purchaseOrderHearerList =
                    (from p in dbContext.PurchaseOrderHeaders
                     orderby p.PurchaseOrderID
                     where p.VendorID == vendorID
                     select p).ToList();


            }

            catch (Exception ex)
            {

                throw ex;

            }
            return purchaseOrderHearerList;

        }

        public static PurchaseOrderHeader getPurchaseOrderHeader(int purchaseOrderID)
        {

            //PurchaseOrderHeader purchaseOrderHeader = new PurchaseOrderHeader();
            PurchaseOrderHeader purchaseOrderHeader = null;


            try
            {
                purchaseOrderHeader =
                    (from p in dbContext.PurchaseOrderHeaders
                     where p.PurchaseOrderID == purchaseOrderID
                     // select p).SingleOrDefault();
                     select p).FirstOrDefault();


            }

            catch (Exception ex)
            {

                throw ex;

            }
            return purchaseOrderHeader;



        }

        public static int saveChanges()

        {

            int countChanges = 0;

            try
            {

                countChanges = dbContext.SaveChanges();

            }

            catch (Exception ex)
            {

                throw ex;

            }

            return countChanges;
        }

        public static PurchaseOrderHeader newPurchaseOrderHeader(int vendorID)
        {

            PurchaseOrderHeader newPOrderHeader = dbContext.PurchaseOrderHeaders.Create();

            // set default property values
            
            newPOrderHeader.RevisionNumber = (byte) 0;
            newPOrderHeader.Status = 1;
            newPOrderHeader.EmployeeID = 258;
            newPOrderHeader.OrderDate = DateTime.Now;
            newPOrderHeader.VendorID = vendorID;
            newPOrderHeader.ShipMethodID = 5;
            newPOrderHeader.TaxAmt = 0m;
            newPOrderHeader.Freight = 0m;
            newPOrderHeader.SubTotal = 0m;
            newPOrderHeader.ModifiedDate = DateTime.Now;


            dbContext.PurchaseOrderHeaders.Add(newPOrderHeader);
            try
            {

                int countChanges = Company.saveChanges();

            }

            catch (Exception ex)
            {

                throw ex;

            }
            return newPOrderHeader;

        }

        public static int removePurchaseOrderHeader(PurchaseOrderHeader purchaseOrderHeader)
        {

                int countChanges = -1;
            
            dbContext.PurchaseOrderHeaders.Remove(purchaseOrderHeader);

            try
                {
                
                countChanges = Company.saveChanges();

                }

                catch (Exception ex)
                {

                    throw ex;

                }

                return countChanges;
        }

    }

}
