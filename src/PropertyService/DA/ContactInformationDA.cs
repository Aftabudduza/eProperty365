using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyService.BO;


namespace PropertyService.DA
{
    public class ContactInformationDA
    {
        EPropertyEntities objPropertyEntities = null;
        public ContactInformationDA(bool isLazyLoadingEnable = true)
        {
            objPropertyEntities = EPropertyEntity.GetEntity();
            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public ContactInformationDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
            {
                objPropertyEntities = EPropertyEntity.GetEntity();
            }

            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public ContactInformation GetbyID(int id)
        {
            ContactInformation objContactInformation = null;
            try
            {             
                var empQuery = from b in objPropertyEntities.ContactInformation
                               where b.Id == id
                               select b;

                objContactInformation = empQuery.ToList().FirstOrDefault();

                
            }
            catch (Exception ex)
            {
               
            }
            return objContactInformation;
        }      
        public bool Insert(ContactInformation objContactInformation)
        {
            try
            {                
                objPropertyEntities.ContactInformation.Add(objContactInformation);
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }      
        public bool Update(ContactInformation obj)
        {
            try
            {
                ContactInformation existing = objPropertyEntities.ContactInformation.Find(obj.Id);
                ((IObjectContextAdapter)objPropertyEntities).ObjectContext.Detach(existing);
                objPropertyEntities.Entry(obj).State = EntityState.Modified;
                objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteByID(int id)
        {
            try
            {
                objPropertyEntities.ContactInformation.Remove(this.GetbyID(id));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }       
        public List<ContactInformation> GetAllContactInformation()
        {
            List<ContactInformation> listContactInformation = null;
            try
            {               
                var empQuery = from b in objPropertyEntities.ContactInformation
                               where b.Id > 0
                               select b;

                listContactInformation = empQuery.OrderBy(x => x.Title).ToList();
               
            }
            catch (Exception ex)
            {
               
            }
            return listContactInformation;
        }
        public List<ContactInformation> GetByOwner(string serial)
        {
            List<ContactInformation> listContactInformation = null;
            try
            {
                var empQuery = from b in objPropertyEntities.ContactInformation
                               where b.OwnerId == serial
                               select b;

                listContactInformation = empQuery.OrderBy(x => x.Title).ToList();

            }
            catch (Exception ex)
            {

            }
            return listContactInformation;
        }
        public List<ContactInformation> GetBySearch(string sSearchQuery)
        {
            List<ContactInformation> contacts = null;
            try
            {
                string sSQL = string.Empty;
                sSQL = " select * from  ContactInformation where Id <> -1 ";

                try
                {
                    if (!string.IsNullOrEmpty(sSearchQuery))
                    {
                        sSQL += " and (Name like '%" + sSearchQuery + "%' or Title like '%" + sSearchQuery + "%'  or Email like '%" + sSearchQuery + "%'  or Phone like '%" + sSearchQuery + "%')";

                    }


                }
                catch (Exception ex)
                {

                }

                var empQuery = objPropertyEntities.ContactInformation.SqlQuery(sSQL).ToList<ContactInformation>();
                contacts = empQuery.OrderBy(x => x.Name).ToList();

            }
            catch (Exception ex)
            {

            }

            return contacts;
        }

        public List<ContactInformation> GetBySearchAndOwnerId(string sSearchQuery, string sOwnerID)
        {
            List<ContactInformation> contacts = null;
            try
            {
                string sSQL = string.Empty;
                sSQL = " select * from  ContactInformation where Id <> -1  and OwnerId = '" + sOwnerID + "' ";

                try
                {
                    if (!string.IsNullOrEmpty(sSearchQuery))
                    {
                        sSQL += " and (Name like '%" + sSearchQuery + "%' or Title like '%" + sSearchQuery + "%'  or Email like '%" + sSearchQuery + "%'  or Phone like '%" + sSearchQuery + "%')";

                    }


                }
                catch (Exception ex)
                {

                }

                var empQuery = objPropertyEntities.ContactInformation.SqlQuery(sSQL).ToList<ContactInformation>();
                contacts = empQuery.OrderBy(x => x.Name).ToList();

            }
            catch (Exception ex)
            {

            }

            return contacts;
        }

    }
}
