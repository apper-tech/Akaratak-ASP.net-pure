using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using DynamicDataWebSite.Models;
using System.Web.Security;

namespace DynamicDataWebSite.Account.Role
{
    public partial class RolesManage : System.Web.UI.Page
    {
        UserManager<ApplicationUser> usermanager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlPermissions.Items.Add(new ListItem(Resources.Account.Choose, "-1"));
                SetRoles(Roles.GetAllRoles());
                if (lblDestenationCount.Text == "")
                    lblDestenationCount.Text = "0";
                if (lblSourceCount.Text == "")
                    lblSourceCount.Text = "0";
            }
            Page.Title = Resources.Account.Page_Role_Manage_Title;
            btnUpdateUsersPermissions.Text = Resources.Account.Update;
            btnAddSource.Text = Resources.Account.Add;
            btnRemoveSource.Text = Resources.Account.Remove;
        }
        public void SetRoles(string[] roles )
        {
            foreach (string item in roles)
            {
                ddlPermissions.Items.Add(new ListItem(GetGlobalResourceObject("Account",item).ToString(), item));
            }
        }
        protected void btnUpdateUsersPermissions_Click(object sender, EventArgs e)
        {
            //add to role
            foreach (ListItem item in lboxDestenation.Items)
            {
                if (!Roles.IsUserInRole(item.Value, ddlPermissions.SelectedItem.Value))
                    Roles.AddUserToRole(item.Value, ddlPermissions.SelectedItem.Value);
            }
            //remove from role
            foreach (ListItem item in lboxSource.Items)
            {
                if(Roles.IsUserInRole(item.Value, ddlPermissions.SelectedItem.Value))
                Roles.RemoveUserFromRole(item.Value, ddlPermissions.SelectedItem.Value);
            }
 
        }

        protected void btnRemoveSource_Click(object sender, EventArgs e)
        {
            if (lboxDestenation.Items.Count > 0)
            {
                List<ListItem> l = new List<ListItem>();
                foreach (ListItem item in lboxDestenation.Items)
                {
                    if (item.Selected)
                    {
                        l.Add(item);
                    }
                }
                foreach (ListItem item in l)
                {
                    lboxSource.Items.Add(item);
                    lboxDestenation.Items.Remove(item);
                }
                btnAddSource.Enabled = true;
                if (lboxSource.Items.Count == 0)
                    btnRemoveSource.Enabled = false;
                lblSourceCount.Text = lboxSource.Items.Count + "";
                lblDestenationCount.Text = lboxDestenation.Items.Count + "";

            }
        }

        protected void btnAddSource_Click(object sender, EventArgs e)
        {
            if(lboxSource.Items.Count>0)
            {
                List<ListItem> l = new List<ListItem>();
                foreach (ListItem item in lboxSource.Items)
                {
                    if(item.Selected)
                    {
                        l.Add(item);
                    }
                }
                foreach (ListItem item in l)
                {
                    lboxDestenation.Items.Add(item);
                    lboxSource.Items.Remove(item);
                }
                btnRemoveSource.Enabled = true;
                if (lboxSource.Items.Count == 0)
                    btnAddSource.Enabled = false;
                lblSourceCount.Text = lboxSource.Items.Count + "";
                lblDestenationCount.Text = lboxDestenation.Items.Count + "";

            }
        }
        protected void ddlPermissions_SelectedIndexChanged(object sender, EventArgs e)
        {
           usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            SetList(usermanager.Users);
        }
        public void SetList(IQueryable<IdentityUser> users)
        {
            lboxSource.Items.Clear();
            lboxDestenation.Items.Clear();
            foreach (IdentityUser item in users)
            {
                string username = UserProfile.GetFullName(item.Id);
                if (username != "NO_ERR" && username!="NULL_ERR")
                {
                    ListItem add = new ListItem(username, item.UserName);
                    if (Roles.IsUserInRole(item.UserName, ddlPermissions.SelectedItem.Value))
                    {
                        lboxDestenation.Items.Add(add);
                    }
                    else
                    {
                        lboxSource.Items.Add(add);
                    }
                }
            }
            //dont
            if(lboxSource.Items.Count==0)
            {
                btnAddSource.Enabled = true;
            }
            //has
            if(lboxDestenation.Items.Count==0)
            {
                btnRemoveSource.Enabled = true;
            }
            lblSourceCount.Text = lboxSource.Items.Count + "";
            lblDestenationCount.Text = lboxDestenation.Items.Count + "";
        }
    }
}