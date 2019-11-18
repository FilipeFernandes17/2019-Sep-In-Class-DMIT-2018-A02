using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.Admin.Security;

namespace WebApp
{
    public partial class OrderShipping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //only allow suppliers to use/acess this page
            if (!Request.IsAuthenticated || !User.IsInRole(Settings.SupplierRole))
                Response.Redirect("~",true);

            if(!IsPostBack) //GET - first visit to the page
            {
                //Load up the info on the supplier
                //TODO: replace hard-coded supplierID with

            }
        }
    }
}