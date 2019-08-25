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
    public class ReportsRepository : IReportsRepository
    {
        private readonly IFilterService _filterService;

        public ReportsRepository(IFilterService filterService)
        {
            _filterService = filterService;
        }
        public int Insert(tblReport data)
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    data.ReportId = Guid.NewGuid();
                    dbContext.tblReports.Add(data);
                    dbContext.SaveChanges();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public int Update(tblReport data)
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    dbContext.tblReports.Attach(data);
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
        public List<SearchReportsResponseModel> Search(ReportsSearchModel searchData)
        {
            try
            {
                List<FilterEntity> listFilters = _filterService.GetFilters();
                var listDepartment = listFilters.Where(filter => filter.FilterType == "Department").ToList();

                using (var dbContext = new PMSEntities())
                {
                    var query = dbContext.tblReports.ToList();
                    if (query != null && query.Count > 0)
                    {

                        if (!string.IsNullOrEmpty(searchData.ProjectCode))
                        {
                            query = query.Where(i => i.ProjectCode.Contains(searchData.ProjectCode)).ToList();
                        }

                        if (!string.IsNullOrEmpty(searchData.ReportTitle))
                        {
                            query = query.Where(i => i.ReportTitle.Contains(searchData.ReportTitle)).ToList();
                        }
                        if (searchData.Department > 0)
                        {
                            query = query.Where(i => i.Department == searchData.Department).ToList();
                        }
                        if (!string.IsNullOrEmpty(searchData.ReportNo))
                        {
                            query = query.Where(i => i.ReportNo.Contains(searchData.ReportNo)).ToList();
                        }
                        if (query != null && query.Count > 0)
                        {
                            return query.Select(i => new SearchReportsResponseModel
                            {
                                Id = i.Id,
                                ReportId = i.ReportId,
                                ProjectCode = i.ProjectCode,
                                Department = listDepartment.Where(x => x.FilterCode == i.Department.Value.ToString()).FirstOrDefault().FilterName,
                                ReportNo = i.ReportNo,
                                ReportDate = i.ReportDate.HasValue ? i.ReportDate.Value.ToString("dd-MM-yyyy") : "",
                                ReportTitle = i.ReportTitle,
                                FilePath = i.FilePath
                            }).ToList();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return new List<SearchReportsResponseModel>();
        }

        public List<tblReport> GetAll()
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    return dbContext.tblReports.ToList();
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public tblReport Get(int id)
        {
            tblReport data = null;
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    data = dbContext.tblReports.Where(x => x.Id == id).ToList().FirstOrDefault();

                }

            }
            catch (Exception ex)
            {
            }
            return data;
        }
    }
}