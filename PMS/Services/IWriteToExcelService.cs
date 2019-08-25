using PMS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PMS.Services
{
  public  interface IWriteToExcelService
    {
        FileContentResult WriteToFile(string fileName,int[] fileIndexes,int[] dateIndexes,int[] rightIndexes,int[] centerIndexes,List<ExcelRowData> rowData,int noOfFields);
    }
}
