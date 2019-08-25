using PMS.Areas.PMS.Models;
using PMS.Areas.PMS.Models.Response;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PMS.Repository
{
    public class ResumeRepository : IResumeRepository
    {
        public int Insert(tblResumeData resumeData)
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    resumeData.ResumeId = Guid.NewGuid();
                    dbContext.tblResumeDatas.Add(resumeData);
                    foreach(var obj in resumeData.tblEducations)
                    {
                        obj.ResumeId = resumeData.ResumeId;
                        obj.EducationId = Guid.NewGuid();
                        dbContext.tblEducations.Add(obj);
                    }


                    dbContext.SaveChanges();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public int Update(tblResumeData resumeData)
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    dbContext.tblResumeDatas.Attach(resumeData);
                    dbContext.Entry(resumeData).State = EntityState.Modified;
                    foreach (var obj in resumeData.tblEducations)
                    {
                        if (obj.Id != 0)
                        {
                            dbContext.tblEducations.Attach(obj);
                            dbContext.Entry(obj).State = EntityState.Modified;
                        }
                        else
                        {
                            obj.ResumeId = resumeData.ResumeId;
                            obj.EducationId = Guid.NewGuid();
                            dbContext.tblEducations.Add(obj);
                        }
                    }
                    dbContext.SaveChanges();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public List<SearchResumeResponseModel> SearchResume(ResumeSearchModel searchData)
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    var query = dbContext.tblResumeDatas.ToList();
                    if(query!=null && query.Count>0)
                    {
                        if(searchData.Designation != null && searchData.Designation.HasValue)
                        {
                            query = query.Where(i => i.Designation == searchData.Designation).ToList();
                        }
                        if (searchData.Department != null && searchData.Department.HasValue)
                        {
                            query = query.Where(i => i.Department == searchData.Department).ToList();
                        }


                        if (searchData.Years != null && searchData.Years.HasValue)
                        {
                            query = query.Where(i => i.ExpYears== searchData.Years).ToList();
                        }

                        if (!string.IsNullOrEmpty(searchData.Name))
                        {
                            query = query.Where(i => i.Name.Contains(searchData.Name)).ToList();
                        }

                        if (!string.IsNullOrEmpty(searchData.Skills))
                        {
                            query = query.Where(i => i.Keywords.Contains(searchData.Skills)).ToList();
                        }
                        if (query != null && query.Count > 0)
                        {
                          return  query.Select(i => new SearchResumeResponseModel
                            {
                                Id = i.Id,
                                ResumeId = i.ResumeId,
                                ResumeDate = i.ResumeDate,
                                Name = i.Name,
                                Keywords = i.Keywords,
                                ExpYears = i.ExpYears,
                                ExpMonths = i.ExpMonths,
                                PresentSalary = i.PresentSalary,
                                ExpectedSalary = i.ExpectedSalary,
                                Mobile = i.Mobile,
                                Email = i.Email,
                                CVCompletePath = i.CVCompletePath
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

        public List<tblResumeData> GetAll()
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    return dbContext.tblResumeDatas.ToList();
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public tblResumeData Get(int id)
        {
            tblResumeData resumedata = null;
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    resumedata= dbContext.tblResumeDatas.Where(x=>x.Id==id).ToList().FirstOrDefault();

                    resumedata.tblEducations = dbContext.tblEducations.Where(i => i.ResumeId == resumedata.ResumeId).ToList();
                }

            }
            catch (Exception ex)
            {
            }
            return resumedata;
        }
    }
}