using PMS.Areas.PMS.Models;
using PMS.Areas.PMS.Models.Response;
using PMS.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PMS.Repository
{
    public class DrawingRepository : IDrawingRepository
    {
        private readonly IFilterService _filterService;

        public DrawingRepository(IFilterService filterService)
        {
            _filterService = filterService;
        }
        public int Insert(tblDrawing data)
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    data.DrawingId = Guid.NewGuid();
                    dbContext.tblDrawings.Add(data);
                    dbContext.SaveChanges();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public int Update(tblDrawing data)
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    dbContext.tblDrawings.Attach(data);
                    dbContext.Entry(data).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public List<SearchDrawingResponseModel> Search(DrawingSearchModel searchData)
        {
            try
            {
                List<FilterEntity> listFilters = _filterService.GetFilters();
                var listDepartment = listFilters.Where(filter => filter.FilterType == "Department").ToList();

                using (var dbContext = new PMSEntities())
                {
                    var query = dbContext.tblDrawings.ToList();
                    if (query != null && query.Count > 0)
                    {
                      
                        if (!string.IsNullOrEmpty(searchData.ProjectCode))
                        {
                            query = query.Where(i => i.ProjectCode.Contains(searchData.ProjectCode)).ToList();
                        }

                        if (!string.IsNullOrEmpty(searchData.DrawingTitle))
                        {
                            query = query.Where(i => i.DrawingTitle.Contains(searchData.DrawingTitle)).ToList();
                        }
                        if (searchData.Department>0)
                        {
                            query = query.Where(i => i.Department==searchData.Department).ToList();
                        }
                        if (!string.IsNullOrEmpty(searchData.DrawingNo))
                        {
                            query = query.Where(i => i.DrawingNo.Contains(searchData.DrawingNo)).ToList();
                        }
                        if (query != null && query.Count > 0)
                        {
                            return query.Select(i => new SearchDrawingResponseModel
                            {
                                Id = i.Id,
                                DrawingId = i.DrawingId,
                                ProjectCode = i.ProjectCode,
                                Department = listDepartment.Where(x=>x.FilterCode==i.Department.Value.ToString()).FirstOrDefault().FilterName,
                                DrawingNo = i.DrawingNo,
                                DrawingDate = i.DrawingDate.HasValue?i.DrawingDate.Value.ToString("dd-MM-yyyy"):"",
                                DrawingTitle = i.DrawingTitle,
                                FilePath=i.FilePath
                            }).ToList();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public List<tblDrawing> GetAll()
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    return dbContext.tblDrawings.ToList();
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public tblDrawing Get(int id)
        {
            tblDrawing data = null;
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    data= dbContext.tblDrawings.Where(x=>x.Id==id).ToList().FirstOrDefault();

                }

            }
            catch (Exception ex)
            {
            }
            return data;
        }
    }
}