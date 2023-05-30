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
    public class VendorDA
    {
        EPropertyEntities objPropertyEntities = null;
        public VendorDA(bool isLazyLoadingEnable = true)
        {
            objPropertyEntities = EPropertyEntity.GetEntity();
            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public VendorDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
            {
                objPropertyEntities = EPropertyEntity.GetEntity();
            }

            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public VendorProfile GetbyID(int id)
        {
            VendorProfile objContactInformation = null;
            try
            {             
                var empQuery = from b in objPropertyEntities.VendorProfile
                               where b.Id == id
                               select b;

                objContactInformation = empQuery.ToList().FirstOrDefault();

                
            }
            catch (Exception ex)
            {
               
            }
            return objContactInformation;
        }      
        public bool Insert(VendorProfile objContactInformation)
        {
            try
            {                
                objPropertyEntities.VendorProfile.Add(objContactInformation);
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }      
        public bool Update(VendorProfile obj)
        {
            try
            {
                VendorProfile existing = objPropertyEntities.VendorProfile.Find(obj.Id);
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
                objPropertyEntities.VendorProfile.Remove(this.GetbyID(id));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }       
        public List<VendorProfile> GetAllInformation()
        {
            List<VendorProfile> listContactInformation = null;
            try
            {               
                var empQuery = from b in objPropertyEntities.VendorProfile
                               where b.Id > 0
                               select b;

                listContactInformation = empQuery.OrderBy(x => x.ContractName).ToList();
               
            }
            catch (Exception ex)
            {
               
            }
            return listContactInformation;
        }
        public List<VendorProfile> GetByOwner(string serial)
        {
            List<VendorProfile> listContactInformation = null;
            try
            {
                var empQuery = from b in objPropertyEntities.VendorProfile
                               where b.OwnerId == serial
                               select b;

                listContactInformation = empQuery.OrderBy(x => x.ContractName).ToList();

            }
            catch (Exception ex)
            {

            }
            return listContactInformation;
        }
        public List<VendorProfile> GetByPropertyLocation(string serial)
        {
            List<VendorProfile> listContactInformation = null;
            try
            {
                var empQuery = from b in objPropertyEntities.VendorProfile
                               where b.PropertyLocationId == serial
                               select b;

                listContactInformation = empQuery.OrderBy(x => x.ContractName).ToList();

            }
            catch (Exception ex)
            {

            }
            return listContactInformation;
        }
        public List<VendorProfile> GetBySearch(string owner, string manager, string location)
        {
            List<VendorProfile> listVendorProfile = null;
            try
            {
                var empQuery = from b in objPropertyEntities.VendorProfile
                               where b.OwnerId == owner 
                               select b;

                listVendorProfile = empQuery.OrderBy(x => x.ContractName).ToList();

                if (manager != string.Empty)
                {
                    listVendorProfile = listVendorProfile.Where(x=> x.PropertyManagerId == manager).ToList();
                }

                if (location != string.Empty)
                {
                    listVendorProfile = listVendorProfile.Where(x => x.PropertyLocationId == location).ToList();
                }

               
               
            }
            catch (Exception ex)
            {

            }
            return listVendorProfile;
        }
    }
}
