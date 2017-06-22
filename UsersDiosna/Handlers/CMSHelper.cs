using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UsersDiosna.Models;

namespace UsersDiosna.Handlers
{
    public class CMSHandler
    {
        /// <summary>
        /// To Get dropdown list of sections to choose one
        /// </summary>
        /// <param name="bakeryId">Id of the bakery from session state</param>
        /// <returns>List of dropdownlist items (for model)</returns>
        public List<SelectListItem> GetDropDownListSections(int bakeryId, string selected = null)
        {
            CMSDataContext db = new CMSDataContext();
            List<SelectListItem> Sections = new List<SelectListItem>();
            foreach (Section section in db.Sections)
            {
                if (Roles.IsUserInRole(section.Role) && section.BakeryId == bakeryId)
                {
                    SelectListItem item = new SelectListItem();
                    if (section.Name == selected)
                        item.Selected = true;
                    item.Value = section.Id.ToString();
                    item.Text = section.Name;
                    Sections.Add(item);
                }
            }
            return Sections;
        }
        /// <summary>
        /// To Get dropdown list of bakery ids to choose one
        /// </summary>
        /// <param name="bakeryId">Id of the bakery from session state</param>
        /// <returns>List of dropdownlist items (for model)</returns>
        public List<SelectListItem> GetDropDownListBakeryIDs(int? bakerySelected = null)
        {
            int bakeryId;
            List<SelectListItem> IDs = new List<SelectListItem>();
            AddRoleDataContext addRole = new AddRoleDataContext();
            foreach (string role in Roles.GetAllRoles())
            {
                string roleDescription = addRole.aspnet_Roles.Single(p => p.RoleName == role.ToString()).Description;

                if (int.TryParse(role, out bakeryId))
                {
                    SelectListItem id = new SelectListItem();
                    if (bakeryId == bakerySelected)
                    {
                        id.Selected = true;
                    }

                    id.Value = bakeryId.ToString();
                    if (roleDescription != null)
                    {
                        id.Text = roleDescription;
                    }
                    else
                    {
                        id.Text = bakeryId.ToString();
                    }
                    IDs.Add(id);
                }
            }
            return IDs;
        }
        /// <summary>
        /// To Get dropdown list of roles to choose one
        /// </summary>
        /// <returns>List of dropdownlist items (for model)</returns>
        public List<SelectListItem> GetDropDownListRoles(string selected = null)
        {
            int bakeryId;
            bool first = true;
            List<SelectListItem> rolesList = new List<SelectListItem>();
            AddRoleDataContext addRole = new AddRoleDataContext();
            foreach (string role in Roles.GetAllRoles())
            {
                SelectListItem roleItem = new SelectListItem();
                if (role == selected)
                    roleItem.Selected = true;
                roleItem.Value = role;
                string roleDescription = addRole.aspnet_Roles.Single(p => p.RoleName == role.ToString()).Description;
                if (roleDescription != null)
                {
                    roleItem.Text = roleDescription;
                }
                else
                {
                    roleItem.Text = role;
                }
                rolesList.Add(roleItem);
                first = false;
            }
            return rolesList;
        }
    }
}