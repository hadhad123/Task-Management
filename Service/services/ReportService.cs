using Reports;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Service.services
{
   public class ReportService
    {
        public void DownloadReport()
        {
            Report report = new Report();
            MemoryStream stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Pdf, stream);


            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Emad.xlsx");
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.BinaryWrite(stream.ToArray());
            HttpContext.Current.Response.End();
        }
    }
}
