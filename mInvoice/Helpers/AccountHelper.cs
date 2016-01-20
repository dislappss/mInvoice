using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mInvoice.Controllers;
using mInvoice.Models;

namespace mInvoice.Helpers
{
    public static class AccountHelper 
    {
        public static int? getClientIDByUserName(string UserName, ref string AspNetUsers_id)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(UserName))
                {
                    myinvoice_dbEntities _db = new myinvoice_dbEntities();

                    string _AspNetUsers_id = _db.v_AspNetUsers.FirstOrDefault(x => x.UserName == UserName).Id;

                    AspNetUsers_id = _AspNetUsers_id;

                    if (_db.Clients.Count(p => p.AspNetUsers_id == _AspNetUsers_id) > 0)
                    {
                        var clients_id = _db.Clients.FirstOrDefault(p => p.AspNetUsers_id == _AspNetUsers_id).Id;

                        return clients_id;
                    }
                    else
                    {
                        // need create Customer
                        return -2;
                    }
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}