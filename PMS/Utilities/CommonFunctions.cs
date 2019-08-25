using Autofac.Integration.Mvc;
using Ionic.Zip;
using PMS.Areas.PMS.Models;
using PMS.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace PMS.Utilities
{
    public class CommonFunctions
    {

        private readonly IFilterService _filterService;

        private static CommonFunctions instance = null;
        public static CommonFunctions Instance
        {
            get
            {
                if (instance == null)
                {

                    var service = DependencyResolver.Current.GetService<IFilterService>();
                    instance = new CommonFunctions(service);
                }
                return instance;
            }
        }
        //Private Constructor.  
        private CommonFunctions(IFilterService filterService)
        {
            _filterService = filterService;
        }
        public List<FilterEntity> getQualifications()
        {
            var filters = _filterService.GetFilters();

            if (filters != null && filters.Count > 0)
            {
                return filters.Where(i => i.FilterType == FilterType.Qualification.ToString()).ToList();
            }
            return filters;
        }

        public byte[] WriteToFile(string fileName, int[] fileIndexes, int[] dateIndexes, int[] rightIndexes, int[] centerIndexes, List<ExcelRowData> rowData, int noOfFields, List<string> headerList, out string downloadName )
        {
            string type = "WO";
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbook mWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet mWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            mWorkBook = xlApp.Workbooks.Add(misValue);
            mWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)mWorkBook.Worksheets.get_Item(1);

            for (int i = 1; i <= noOfFields; i++)
            {
                mWorkSheet.Cells[1, i] = headerList[i - 1];
            }
            string tempFolderName = type+  DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + "_temp";//date time added to be sure there are no name conflicts
            string tempFOLDER = AppDomain.CurrentDomain.BaseDirectory+"tempData\\" + tempFolderName;//date time added to be sure there are no name conflicts

          var info=  Directory.CreateDirectory(tempFOLDER);
            
            string tempPath = info.FullName+"\\"+type;//date time added to be sure there are no name conflicts

            int index = 2;
            foreach (var obj in rowData)
            {
                for (int i = 1; i <= noOfFields; i++)
                {
                    if (fileIndexes != null && fileIndexes.Count() > 0 && fileIndexes.Contains(i - 1))
                    {
                        //  mWorkSheet.Cells[index, i] = obj.Fields[i - 1];
                        string existfileName = Path.GetFileName(obj.Fields[i - 1]);
                        File.Copy(obj.Fields[i - 1], info.FullName+"\\" +existfileName);

                        mWorkSheet.Hyperlinks.Add(mWorkSheet.Cells[index, i], existfileName, Type.Missing, existfileName, existfileName);
                    }
                    else if (dateIndexes != null && dateIndexes.Count() > 0 && dateIndexes.Contains(i - 1))
                    {
                        mWorkSheet.Cells[index, i] = obj.Fields[i - 1];
                        //apply date format to current cell
                    }
                    else
                    {
                        mWorkSheet.Cells[index, i] = obj.Fields[i - 1];
                    }
                }
                index++;
            }

            mWorkBook.SaveAs(tempPath, mWorkBook.FileFormat);//create temporary file from the workbook
            tempPath = mWorkBook.FullName;//name of the file with path and extension
            mWorkBook.Close(true, misValue, misValue);
           

            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(info.FullName);//Zip file inside filename  
                zip.Save(tempFOLDER+".zip");//location and name for creating zip file  

            }
            byte[] result = File.ReadAllBytes(tempFOLDER + ".zip");//change to byte[]
            downloadName = Path.GetFileName(tempFOLDER + ".zip");
            File.Delete(tempFOLDER + ".zip");//delete temporary file

            var files = Directory.GetFiles(tempFOLDER);
            foreach(var obj in files)
            {
                File.Delete(obj);
            }

            Directory.Delete(tempFOLDER);//delete temporary file
            xlApp.Quit();

            Marshal.ReleaseComObject(mWorkSheet);
            Marshal.ReleaseComObject(mWorkBook);
            Marshal.ReleaseComObject(xlApp);

            return result;
        }
    }
}