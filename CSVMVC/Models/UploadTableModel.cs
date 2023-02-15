using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSVMVC.Models
{
    public class UploadTableModel
    {
             public List<UploadModel> Rows { get; set; } = new List<UploadModel>();
    }
}