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
    public class VendorRepository : IVendorRepository
    {
        private readonly IFilterService _filterService;

        public VendorRepository(IFilterService filterService)
        {
            _filterService = filterService;
        }
        public int Insert(tblVendorData data)
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    data.VendorId = Guid.NewGuid();
                    dbContext.tblVendorDatas.Add(data);
                    dbContext.SaveChanges();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public int Update(tblVendorData data)
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    dbContext.tblVendorDatas.Attach(data);
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
        //public List<SearchBGsResponseModel> Search(BGsSearchModel searchData)
        //{
        //    try
        //    {
        //        using (var dbContext = new PMSEntities())
        //        {
        //            var query = dbContext.tblBGsDatas.ToList();
        //            if (query != null && query.Count > 0)
        //            {
        //                if (!string.IsNullOrEmpty(searchData.ProjectCode))
        //                {
        //                    query = query.Where(i => i.ProjectCode == searchData.ProjectCode).ToList();
        //                }

        //                if (!string.IsNullOrEmpty(searchData.ProjectName))
        //                {
        //                    query = query.Where(i => i.ProjectName == searchData.ProjectName).ToList();
        //                }

        //                if (!string.IsNullOrEmpty(searchData.WONo))
        //                {
        //                    query = query.Where(i => i.WONo.Contains(searchData.WONo)).ToList();
        //                }

        //                if (!string.IsNullOrEmpty(searchData.BGNo))
        //                {
        //                    query = query.Where(i => i.BGNo.Contains(searchData.BGNo)).ToList();
        //                }

        //                if (query != null && query.Count > 0)
        //                {
        //                    return query.Select(i => new SearchBGsResponseModel
        //                    {
        //                        Id = i.Id,
        //                        BGsId = i.BGsId,
        //                        ProjectCode = i.ProjectCode,
        //                        ProjectName = i.ProjectName,
        //                        WONo = i.WONo,
        //                        BGNo = i.BGNo,
        //                        BGDate = i.BGDate,
        //                        FilePath = i.FilePath
        //                    }).ToList();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return null;
        //}

        public List<tblVendorData> GetAll()
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    return dbContext.tblVendorDatas.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public tblVendorData Get(int id)
        {
            tblVendorData data = null;
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    data = dbContext.tblVendorDatas.Where(x => x.Id == id).ToList().FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
            }
            return data;
        }
    }
}