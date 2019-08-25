using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMS.Utilities;

namespace PMS.Services
{
    public class WriteToExcelService : IWriteToExcelService
    {
        public FileContentResult WriteToFile(string fileName, int[] fileIndexes, int[] dateIndexes, int[] rightIndexes, int[] centerIndexes, List<ExcelRowData> rowData, int noOfFields)
        {
            return null;
        }
    }
}