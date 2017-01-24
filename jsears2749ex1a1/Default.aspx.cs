using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jsears2749ex1a1ef.Model;


namespace jsears2749ex1a1

{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.loadVendors();
                this.loadShipMethods();
                this.loadEmployee();
                this.loadPurchaseOrderHeaders();


            }

        }

        private void loadVendors()
        {

            List<Vendor> vendorList = new List<Vendor>();
            try
            {

                vendorList = Company.getVendors();

            }

            catch (Exception ex)
            {
                messageLabel.Text = ex.Message;
                messageLabel.CssClass = "control-label text-danger";

            }

            this.vendorDropDownList.DataSource = vendorList;
            this.vendorDropDownList.DataTextField = "ShortString";
            this.vendorDropDownList.DataValueField = "BusinessEntityID";
            this.vendorDropDownList.DataBind();
            this.vendorDropDownList.SelectedIndex = 1;

        }

        private void loadShipMethods()
        {

            List<ShipMethod> shipMethodList = new List<ShipMethod>();
            try
            {

                shipMethodList = Company.getShipMethods();

            }

            catch (Exception ex)
            {

                messageLabel.Text = ex.Message;
                messageLabel.CssClass = "control-label text-danger";

            }

            this.shipperDropDownList.DataSource = shipMethodList;
            this.shipperDropDownList.DataTextField = "Name";
            this.shipperDropDownList.DataValueField = "ShipMethodID";
            this.shipperDropDownList.DataBind();

        }

        private void loadEmployee()
        {

            List<Employee> employeeList = new List<Employee>();
            try
            {

                employeeList = Company.getEmployees();

            }

            catch (Exception ex)
            {

                messageLabel.Text = ex.Message;
                messageLabel.CssClass = "control-label text-danger";

            }

            this.employeeDropDownList.DataSource = employeeList;
            this.employeeDropDownList.DataTextField = "LastFirstName";
            this.employeeDropDownList.DataValueField = "BusinessEntityID";
            this.employeeDropDownList.DataBind();

        }

        protected void vendorDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

            loadPurchaseOrderHeaders();

        }

        private void loadPurchaseOrderHeaders()
        {
            clearPurchaseOrderControls();

            int vendorID = int.Parse(vendorDropDownList.SelectedValue);

            List<PurchaseOrderHeader> purchaseOrderList = new List<PurchaseOrderHeader>();

            try
            {

                purchaseOrderList = Company.getPurchaseOrderHeaders(vendorID);

            }

            catch (Exception ex)
            {

                messageLabel.Text = ex.Message;
                messageLabel.CssClass = "control-label text-danger";

            }

            //  detailsLabel.Text = purchaseOrderList.Count.ToString();  // Debug Display Count

            if (purchaseOrderList.Count() > 0)
            {
                this.purchaseOrderHeaderDropDownList.DataSource = purchaseOrderList;
                this.purchaseOrderHeaderDropDownList.DataTextField = "ShortString";
                this.purchaseOrderHeaderDropDownList.DataValueField = "PurchaseOrderID";
                this.purchaseOrderHeaderDropDownList.DataBind();
                this.purchaseOrderHeaderDropDownList.SelectedIndex = 0;

                // Set Field Values

                PurchaseOrderHeader purchaseOrderHeader = new PurchaseOrderHeader();

                int purchaseOrderID = int.Parse(purchaseOrderHeaderDropDownList.SelectedValue);

                purchaseOrderHeader = Company.getPurchaseOrderHeader(purchaseOrderID);

                if (purchaseOrderHeader != null)
                {
                    clearPurchaseOrderControls();

                    fillPurchaseOrderControls(purchaseOrderHeader);
                }
                else { detailsLabel.Text = "null"; }
            }
            else
            {
                purchaseOrderList.Clear();
                this.purchaseOrderHeaderDropDownList.DataSource = purchaseOrderList; // Set to Empty List
                this.purchaseOrderHeaderDropDownList.DataBind();
                this.purchaseOrderHeaderDropDownList.SelectedIndex = -1;

                //detailsLabel.Text = "-1"; // Debug
            }

        }

        protected void purchaseOrderHeaderDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            PurchaseOrderHeader purchaseOrderHeader = new PurchaseOrderHeader();

            // string purchaseOrderHeaderRow = purchaseOrderHeaderDropDownList.SelectedItem.ToString();
            // String[] words = purchaseOrderHeaderRow.Split(null);
            // int purchaseOrderID = Convert.ToInt32(words[0]);

            int purchaseOrderID = int.Parse(purchaseOrderHeaderDropDownList.SelectedValue);

            purchaseOrderHeader = Company.getPurchaseOrderHeader(purchaseOrderID);

            clearPurchaseOrderControls();

            fillPurchaseOrderControls(purchaseOrderHeader);

        }

        protected void clearPurchaseOrderControls()
        {

            messageLabel.Text = String.Empty;
            messageLabel.CssClass = "control-label";

            this.revisionNumberTextBox.Text = string.Empty;
            this.statusTextBox.Text = string.Empty;
            this.employeeDropDownList.SelectedIndex = -1;

            this.orderDateCalendar.SelectedDates.Clear();
            this.orderDateCalendar.VisibleDate = DateTime.Now;
            this.shipppingDateCalendar.SelectedDates.Clear();
            this.shipppingDateCalendar.VisibleDate = DateTime.Now;

            this.shipperDropDownList.SelectedIndex = -1;
            // this.detailsLabel.Text = string.Empty;
            this.subtotalLabel.Text = string.Empty;
            this.taxLabel.Text = string.Empty;
            this.freightTextBox.Text = string.Empty;
            this.totalLabel.Text = string.Empty;

        }

        protected void fillPurchaseOrderControls(PurchaseOrderHeader purchaseOrderHeader)
        {
            this.revisionNumberTextBox.Text = purchaseOrderHeader.RevisionNumber.ToString();
            this.statusTextBox.Text = purchaseOrderHeader.Status.ToString();
            this.employeeDropDownList.SelectedValue = purchaseOrderHeader.EmployeeID.ToString();
            this.orderDateCalendar.SelectedDate = purchaseOrderHeader.OrderDate;
            this.orderDateCalendar.VisibleDate = purchaseOrderHeader.OrderDate;
            this.shipppingDateCalendar.SelectedDates.Clear();
            this.shipppingDateCalendar.VisibleDate = DateTime.Now;

            if (purchaseOrderHeader.ShipDate != null)
            {
                shipppingDateCalendar.SelectedDate = (DateTime)purchaseOrderHeader.ShipDate;
                shipppingDateCalendar.VisibleDate = (DateTime)purchaseOrderHeader.ShipDate;
            }

            //  this.shipppingDateCalendar.SelectedDate = purchaseOrderHeader.OrderDate;
            //  this.shipppingDateCalendar.VisibleDate = purchaseOrderHeader.OrderDate;
            this.shipperDropDownList.SelectedValue = purchaseOrderHeader.ShipMethodID.ToString();
            // this.detailsLabel.Text = purchaseOrderHeader.PurchaseOrderDetails.ToString();
            this.subtotalLabel.Text = purchaseOrderHeader.SubTotal.ToString("n2");
            this.taxLabel.Text = purchaseOrderHeader.TaxAmt.ToString("n2");
            this.freightTextBox.Text = purchaseOrderHeader.Freight.ToString("n2");
            this.totalLabel.Text = (purchaseOrderHeader.SubTotal + purchaseOrderHeader.Freight + purchaseOrderHeader.TaxAmt).ToString("n2");

        }

        protected void saveButton_Click(object sender, EventArgs e)
        {

            messageLabel.Text = String.Empty;
            messageLabel.CssClass = "control-label";

            try
            {
                PurchaseOrderHeader purchaseOrderHeader = new PurchaseOrderHeader();
                int purchaseOrderID = int.Parse(purchaseOrderHeaderDropDownList.SelectedValue);
                purchaseOrderHeader = Company.getPurchaseOrderHeader(purchaseOrderID);

                purchaseOrderHeader.RevisionNumber = byte.Parse(revisionNumberTextBox.Text);
                purchaseOrderHeader.Status = byte.Parse(statusTextBox.Text);
                purchaseOrderHeader.EmployeeID = int.Parse(employeeDropDownList.SelectedValue);
                purchaseOrderHeader.OrderDate = orderDateCalendar.SelectedDate;
                purchaseOrderHeader.ShipMethodID = int.Parse(shipperDropDownList.SelectedValue);
                purchaseOrderHeader.SubTotal = decimal.Parse(subtotalLabel.Text);
                purchaseOrderHeader.TaxAmt = decimal.Parse(taxLabel.Text);
                purchaseOrderHeader.Freight = decimal.Parse(freightTextBox.Text);

                if (shipppingDateCalendar.SelectedDates.Count > 0)
                {

                    purchaseOrderHeader.ShipDate = shipppingDateCalendar.SelectedDate;

                }

                int countChanges = Company.saveChanges();

                if (countChanges > 0)
                {
                    messageLabel.Text = "Update Sucessful!";
                    messageLabel.CssClass = "control-label text-success";
                }

            }

            catch (Exception ex)
            {

                messageLabel.Text = ex.Message;
                messageLabel.CssClass = "control-label text-danger";

            }

        }


            protected void addButton_Click(object sender, EventArgs e)

            {
            clearPurchaseOrderControls();
            PurchaseOrderHeader purchaseOrderHeader = null;

            int vendorID = -1;
                
            vendorID  = int.Parse(vendorDropDownList.SelectedValue);

            if (vendorID != -1)
            {

                try

                {

                    purchaseOrderHeader = Company.newPurchaseOrderHeader(vendorID);

                }


                catch (Exception ex)

                {

                    messageLabel.Text = ex.Message;
                    messageLabel.CssClass = "control-label text-danger";
                }

            }

            if (purchaseOrderHeader != null)
            {

                fillPurchaseOrderControls(purchaseOrderHeader);
                purchaseOrderHeaderDropDownList.Items.Add(new ListItem(purchaseOrderHeader.ShortString, purchaseOrderHeader.PurchaseOrderID.ToString()));
                purchaseOrderHeaderDropDownList.SelectedValue = purchaseOrderHeader.PurchaseOrderID.ToString();
                messageLabel.Text = "New Purchase Order";
                messageLabel.CssClass = "control-label text-success";

            }

        }

        protected void removeButton_Click(object sender, EventArgs e)

        {
            clearPurchaseOrderControls();
            PurchaseOrderHeader purchaseOrderHeader = null;
            int countChanges = -1;
            int purchaseOrderID = int.Parse(purchaseOrderHeaderDropDownList.SelectedValue);
            int purchaseOrderCurrentSelectedIndex = purchaseOrderHeaderDropDownList.SelectedIndex;

            try
            {
                purchaseOrderHeader = Company.getPurchaseOrderHeader(purchaseOrderID);
                countChanges = Company.removePurchaseOrderHeader(purchaseOrderHeader);

            }

            catch (Exception ex)
            {

                messageLabel.Text = ex.Message;
                messageLabel.CssClass = "control-label text-danger";

            }

            if (countChanges == 1)
            {
                //detailsLabel.Text = countChanges.ToString(); // Debug
                //detailsLabel.Text = purchaseOrderCurrentSelectedIndex.ToString(); // Debug

                purchaseOrderHeaderDropDownList.Items.RemoveAt(purchaseOrderHeaderDropDownList.SelectedIndex);

               // this.loadPurchaseOrderHeaders();

                if (purchaseOrderHeaderDropDownList.Items.Count != 0)
                {

                    if (purchaseOrderHeaderDropDownList.Items.Count  >= purchaseOrderCurrentSelectedIndex)

                    {

                        purchaseOrderHeaderDropDownList.SelectedIndex = purchaseOrderCurrentSelectedIndex - 1;

                    }

                    purchaseOrderID = int.Parse(purchaseOrderHeaderDropDownList.SelectedValue);

                    purchaseOrderHeader = Company.getPurchaseOrderHeader(purchaseOrderID);

                    fillPurchaseOrderControls(purchaseOrderHeader);

                }

                messageLabel.Text = "Remove Successful";
                messageLabel.CssClass = "control-label text-success";


            }


        }


    }


}


