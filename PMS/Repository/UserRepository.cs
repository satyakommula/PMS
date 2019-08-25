using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PMS.Repository
{
    public class UserRepository : IUserRepository
    {
        public tblUser Get(int id)
        {
            tblUser data = null;
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    data = dbContext.tblUsers.Where(x => x.Id == id).ToList().FirstOrDefault();

                }

            }
            catch (Exception ex)
            {
            }
            return data;
        }

        public tblUser Get(string email)
        {
            tblUser data = null;
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    data = dbContext.tblUsers.Where(x => x.Email == email).ToList().FirstOrDefault();

                }

            }
            catch (Exception ex)
            {
            }
            return data;
        }

        public int Insert(tblUser data)
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    data.UserId = Guid.NewGuid();
                    dbContext.tblUsers.Add(data);
                    dbContext.SaveChanges();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public int Update(tblUser data)
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    dbContext.tblUsers.Attach(data);
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
    }
}