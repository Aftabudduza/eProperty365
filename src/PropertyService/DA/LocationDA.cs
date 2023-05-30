using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyService.BO;
using System.Data.Entity.Core.Objects;

namespace PropertyService.DA
{
    public class LocationDA
    {
        EPropertyEntities objPropertyEntities = null;
        public LocationDA(bool isLazyLoadingEnable = true)
        {
            objPropertyEntities = EPropertyEntity.GetEntity();
            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public LocationDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
            {
                objPropertyEntities = EPropertyEntity.GetEntity();
            }

            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public Location GetbyID(int id)
        {
            Location objContactInformation = null;
            try
            {             
                var empQuery = from b in objPropertyEntities.Location
                               where b.Id == id
                               select b;

                objContactInformation = empQuery.ToList().FirstOrDefault();

                
            }
            catch (Exception ex)
            {
               
            }
            return objContactInformation;
        }
        public Location GetbySerial(string sSerial)
        {
            Location objLocation = null;
            try
            {
                var empQuery = from b in objPropertyEntities.Location
                               where b.Serial == sSerial
                               select b;

                objLocation = empQuery.ToList().FirstOrDefault();


            }
            catch (Exception ex)
            {

            }
            return objLocation;
        }

        public bool Insert(Location objContactInformation)
        {
            try
            {                
                objPropertyEntities.Location.Add(objContactInformation);
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }      
        public bool Update(Location obj)
        {
            try
            {
                Location existing = objPropertyEntities.Location.Find(obj.Id);
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
                objPropertyEntities.Location.Remove(this.GetbyID(id));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }       
        public List<Location> GetAllInformation()
        {
            List<Location> listContactInformation = null;
            try
            {               
                var empQuery = from b in objPropertyEntities.Location
                               where b.Id > 0
                               select b;

                listContactInformation = empQuery.OrderBy(x => x.LocationName).ToList();
               
            }
            catch (Exception ex)
            {
               
            }
            return listContactInformation;
        }
        public List<Location> GetByOwner(string serial)
        {
            List<Location> listContactInformation = null;
            try
            {
                var empQuery = from b in objPropertyEntities.Location
                               where b.OwnerId == serial
                               select b;

                listContactInformation = empQuery.OrderBy(x => x.LocationName).ToList();

            }
            catch (Exception ex)
            {

            }

            return listContactInformation;
        }

        public List<Location> GetByPropertyManager(string serial)
        {
            List<Location> listContactInformation = null;
            try
            {
                var empQuery = from b in objPropertyEntities.Location
                               where b.PropertyManagerId == serial
                               select b;

                listContactInformation = empQuery.OrderBy(x => x.LocationName).ToList();

            }
            catch (Exception ex)
            {

            }

            return listContactInformation;
        }

        public List<Location> GetByPropertyLocation(string serial)
        {
            List<Location> listContactInformation = null;
            try
            {
                var empQuery = from b in objPropertyEntities.Location
                               where b.Serial == serial
                               select b;

                listContactInformation = empQuery.OrderBy(x => x.LocationName).ToList();

            }
            catch (Exception ex)
            {

            }
            return listContactInformation;
        }
        public List<Location> GetBySearch(string owner, string manager)
        {
            List<Location> listLocation = null;
           
            try
            {
                if (manager != string.Empty && manager != "-1")
                {
                    var empQuery = from b in objPropertyEntities.Location
                                   where b.OwnerId == owner && b.PropertyManagerId == manager
                                   select b;

                    listLocation = empQuery.OrderBy(x => x.LocationName).ToList();
                }
                else
                {
                    var empQuery = from b in objPropertyEntities.Location
                                   where b.OwnerId == owner 
                                   select b;

                    listLocation = empQuery.OrderBy(x => x.LocationName).ToList();
                }
            
                

            }
            catch (Exception ex)
            {
            listLocation = null;
            }
            return listLocation;
        }
        public string MakeAutoGenLocation(string yourPrefix, string objName)
        {
            string serial = "";
            try
            {
                objPropertyEntities = EPropertyEntity.GetFreshEntity();
                string prefix = "";
                prefix = DateTime.Now.Year.ToString();
                ObjectParameter oupParam = new ObjectParameter("NewID", 0);
                oupParam.Value = DBNull.Value;
                objPropertyEntities.SP_GetID(objName, "Id", DateTime.Now.Year, null, null, oupParam);
                string sNumber = oupParam.Value.ToString().PadLeft(5, '0');
                serial = string.Concat(yourPrefix, sNumber);

                return serial;
            }
            catch (Exception ex)
            {

            }

            return null;

        }

    }
}
