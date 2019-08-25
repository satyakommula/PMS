using PMS.Areas.PMS.Models;
using PMS.Areas.PMS.Models.Response;
using PMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Services
{
    public class DrawingService : IDrawingService
    {
        private readonly IDrawingRepository _drawingRepository;
        public DrawingService(IDrawingRepository drawingRepository)
        {
            _drawingRepository = drawingRepository;
        }
        public int Insert(tblDrawing data)
        {
            return _drawingRepository.Insert(data);
        }

        public int Update(tblDrawing data)
        {
            return _drawingRepository.Update(data);
        }

        public List<SearchDrawingResponseModel> Search(DrawingSearchModel searchData)
        {
            return _drawingRepository.Search(searchData);
        }

        public List<tblDrawing> GetAll()
        {
            return _drawingRepository.GetAll();
        }

        public tblDrawing Get(int id)
        {
            return _drawingRepository.Get(id);
        }
    }
}