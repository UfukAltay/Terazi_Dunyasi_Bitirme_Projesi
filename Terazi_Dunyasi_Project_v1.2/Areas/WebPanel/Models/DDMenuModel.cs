using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Terazi_Dunyasi_Project_v12.Areas.WebPanel.Models
{
    public class DDMenuModel
    {
        public SelectList DDMenuListModel { get; set; }

        public SelectList DDAltMenuListModel { get; set; }
    }
}