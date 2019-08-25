using PMS.Areas.PMS.Models;
using PMS.Areas.PMS.Models.Response;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PMS.Repository
{
    public class CorrespondenceRepository : ICorrespondenceRepository
    {
        public int Insert(tblCorrespondence correspondenceData)
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    correspondenceData.CorrespondenceId = Guid.NewGuid();
                    dbContext.tblCorrespondences.Add(correspondenceData);
                    dbContext.SaveChanges();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public int Update(tblCorrespondence correspondenceData)
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    dbContext.tblCorrespondences.Attach(correspondenceData);
                    dbContext.Entry(correspondenceData).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public List<SearchCorrespondenceModel> SearchCorrespondence(CorrespondenceSearchModel searchData)
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    var query = dbContext.tblCorrespondences.ToList();
                    if (query != null && query.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(searchData.ProjectCode))
                        {
                            query = query.Where(i => i.ProjectCode == searchData.ProjectCode).ToList();
                        }
                        if (searchData.CorrespondenceType >0)
                        {
                            query = query.Where(i => i.CorrespondenceType == searchData.CorrespondenceType).ToList();
                        }


                        if (!string.IsNullOrEmpty(searchData.LetterNo))
                        {
                            query = query.Where(i => i.LetterNo == searchData.LetterNo).ToList();
                        }

                        if (!string.IsNullOrEmpty(searchData.Keywords))
                        {
                            query = query.Where(i => i.Keywords.Contains(searchData.Keywords)).ToList();
                        }


                        if (query != null && query.Count > 0)
                        {
                            return query.Select(i => new SearchCorrespondenceModel
                            {
                                Id = i.Id,
                                CorrespondenceId = i.CorrespondenceId,
                                CorrespondenceType = (i.CorrespondenceType == 1 ? "InWard" : "OutWard"),
                                ProjectCode = i.ProjectCode,
                                Department = i.Department,
                                Subject = i.Subject,
                                LetterNo = i.LetterNo,
                                LetterDate = i.LetterDate,
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

        public List<tblCorrespondence> GetAll()
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    return dbContext.tblCorrespondences.ToList();
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public tblCorrespondence Get(int id)
        {
            tblCorrespondence correspondenceData = null;
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    correspondenceData = dbContext.tblCorrespondences.Where(x => x.Id == id).ToList().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
            }
            return correspondenceData;
        }
    }
}