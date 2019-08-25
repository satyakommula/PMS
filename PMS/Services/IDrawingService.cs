using PMS.Areas.PMS.Models;
using PMS.Areas.PMS.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services
{
   public interface IDrawingService
    {
        int Insert(tblDrawing data);
        int Update(tblDrawing data);
        List<SearchDrawingResponseModel> Search(DrawingSearchModel searchData);
        List<tblDrawing> GetAll();
        tblDrawing Get(int id);
    }
}
