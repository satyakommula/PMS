using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PMS.Areas.PMS.Models;
using PMS.Areas.PMS.Models.Response;

namespace PMS.Repository
{
    public class WorkOrderRepository : IWorkOrderRepository
    {
        public List<ClientDataModel> getCleints()
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    return dbContext.tblInwardWOes.Select(x => new ClientDataModel() { ClientName = x.ClientName,
                        Address1 = x.Address1,
                        Address2 = x.Address2,
                        State = x.State,
                        PinCode = x.PinCode,
                        ContactPerson1 = x.ContactPerson1,
                        ContactPerson2 = x.ContactPerson2,
                        Mobile1 = x.Mobile1,
                        Mobile2 = x.Mobile2,
                        Email1 = x.Email1,
                        Email2 = x.Email2,
                    }).Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return new List<ClientDataModel>();
        }

        public int Insert(tblInwardWO inwardData)
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    inwardData.WOId = Guid.NewGuid();
                    dbContext.tblInwardWOes.Add(inwardData);
                    dbContext.SaveChanges();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public int Update(tblInwardWO inwardData)
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    dbContext.tblInwardWOes.Attach(inwardData);
                    dbContext.Entry(inwardData).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public int SaveOutward(tblOutwardWO outWardData)
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    outWardData.WOId = Guid.NewGuid();
                    dbContext.tblOutwardWOes.Add(outWardData);
                    dbContext.SaveChanges();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public int UpdateOutward(tblOutwardWO outWardData)
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    dbContext.tblOutwardWOes.Attach(outWardData);
                    dbContext.Entry(outWardData).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<SearchInwardResponseModel> SearchInward(InwardSearchModel searchData)
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    var query = dbContext.tblInwardWOes.ToList();
                    if (query != null && query.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(searchData.ProjectCode))
                        {
                            query = query.Where(i => i.ProjectCode.Equals(searchData.ProjectCode,StringComparison.InvariantCultureIgnoreCase)).ToList();
                        }

                        if (!string.IsNullOrEmpty(searchData.BranchCode))
                        {
                            query = query.Where(i => i.BranchCode == searchData.BranchCode).ToList();
                        }

                        if (!string.IsNullOrEmpty(searchData.ClientName))
                        {
                            query = query.Where(i => i.ClientName.Contains(searchData.ClientName)).ToList();
                        }

                        if (!string.IsNullOrEmpty(searchData.WorkOrderNo))
                        {
                            query = query.Where(i => i.WorkOrderNo.Contains(searchData.WorkOrderNo)).ToList();
                        }

                        if (query != null && query.Count > 0)
                        {
                            return query.Select(i => new SearchInwardResponseModel
                            {
                                Id = i.Id,
                                WOId = i.WOId,
                                ProjectCode = i.ProjectCode,
                                BranchCode = i.BranchCode,
                                ClientName = i.ClientName,
                                WorkOrderNo = i.WorkOrderNo,
                                FilePath = i.WOPath
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

        public List<tblInwardWO> GetAll()
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    return dbContext.tblInwardWOes.ToList();
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public tblInwardWO Get(int id)
        {
            tblInwardWO inwardData = null;
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    inwardData = dbContext.tblInwardWOes.Where(x => x.Id == id).ToList().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
            }
            return inwardData;
        }
    }
}