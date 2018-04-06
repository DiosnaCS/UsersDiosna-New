using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UsersDiosna.DBML;
using UsersDiosna.Models;

namespace UsersDiosna.Handlers
{
    public class CMSHandler
    {
        /// <summary>
        /// Extension to check that string is not too long for view in the table
        /// </summary>
        /// <param name="toCheck"></param>
        /// <param name="maxLength"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string maxLength(string toCheck, int maxLength, string url = null)
        {
            if (toCheck != null)
            {
                if (toCheck.Length > maxLength)
                {
                    if (toCheck.LastIndexOf(" ") != -1)
                    {
                        toCheck = toCheck.Substring(0, maxLength);
                        if (url != null)
                        {
                            toCheck += string.Format("<a href=\"{0}\"><b>...</b></a>", url);
                        }
                    }
                }
            }
            return toCheck;
        }
        /// <summary>
        /// Returns getbakeryName from bakjeryId
        /// </summary>
        /// <param name="bakeryId">int bakeryId</param>
        /// <returns>string bakeryName</returns>
        public string getBakeryName(int bakeryId)
        {
            string bakeryName = bakeryId.ToString();
            AddRoleDataContext roles = new AddRoleDataContext();
            //ReleName is bakeryId and Description is bakery name
            if (roles.aspnet_Roles.Single(p => p.RoleName == bakeryId.ToString()).Description.Length > 0) 
            {
                bakeryName = roles.aspnet_Roles.Single(p => p.RoleName == bakeryId.ToString()).Description;
            }
            return bakeryName;
        }
        /// <summary>
        /// To Get dropdown list of sections to choose one
        /// </summary>
        /// <param name="bakeryId">Id of the bakery from session state</param>
        /// <returns>List of dropdownlist items (for model)</returns>
        public List<SelectListItem> getDropDownListSections(int bakeryId, string selected = null)
        {
            CMSDataContext db = new CMSDataContext();
            List<SelectListItem> Sections = new List<SelectListItem>();
            foreach (Section section in db.Sections)
            {
                if (Roles.IsUserInRole(section.Role) && section.BakeryId == bakeryId)
                {
                    SelectListItem item = new SelectListItem();
                    if (section.Name == selected)
                    {
                        item.Selected = true;
                    }
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
        public List<SelectListItem> getDropDownListBakeryIDs(int? bakerySelected = null)
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
        public List<SelectListItem> getDropDownListRoles(string selected = null)
        {
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
            }
            return rolesList;
        }

        /// <summary>
        /// Get dropdown list for all files in download section + empty selection
        /// </summary>
        /// <param name="networkPath"></param>
        /// <returns></returns>
        public List<SelectListItem> getDropDownListFiles(int id, string networkPath, string[] roles)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<string> files = new List<string>();
            FileHelper fileHelper = new FileHelper();
            List<string> masks = new List<string>();
            masks = fileHelper.selectMasks(id, roles);
            SelectListItem firstItem = new SelectListItem();
            firstItem.Value = null;
            firstItem.Text = "----- No file ----";
            list.Add(firstItem);
            foreach (string mask in masks) {
                files = fileHelper.findFilesOnServer(networkPath, mask);
                foreach (string filePath in files)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = filePath;
                    item.Text = filePath.Substring(filePath.LastIndexOf('/')+1);
                    list.Add(item);
                }
            }
            return list;
        }
    }
}