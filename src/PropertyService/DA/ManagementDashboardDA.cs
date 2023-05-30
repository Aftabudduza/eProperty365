using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using PropertyService.BO;
using PropertyService.ViewModel;

namespace PropertyService.DA
{
    public class ManagementDashboardDA
    {
        private EPropertyEntities _objPropertyEntities;
        private String error = String.Empty;
        private String Mass = String.Empty;
        //private ADONetDataConnection _objAdoNetDataConnection;
        private PropertyService.CommonDA _commonDa = new PropertyService.CommonDA();

        public ManagementDashboardDA(bool isLazyLoadingEnable = true)
        {
            _objPropertyEntities = EPropertyEntity.GetEntity();
            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public ManagementDashboardDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                _objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
                _objPropertyEntities = EPropertyEntity.GetEntity();
            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public bool Insert(EventManagement obj)
        {
            try
            {
                _objPropertyEntities.EventManagement.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update(EventManagement obj)
        {
            try
            {
                var existing = _objPropertyEntities.EventManagement.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(EventManagement obj)
        {
            EventManagement deletEventManagement = new EventManagement();
            try
            {
                deletEventManagement = _objPropertyEntities.EventManagement.FirstOrDefault(x => x.Id == obj.Id);
                _objPropertyEntities.EventManagement.Remove(deletEventManagement);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Get_EventManagement_Data_Result> GetEventData(EventManagement obj)
        {
            DateTime fromDateTime = Convert.ToDateTime(obj.FromDate).Date;
            DateTime toDateTime = Convert.ToDateTime(obj.ToDate).Date;
            return _objPropertyEntities.Get_EventManagement_Data(fromDateTime, toDateTime, obj.OwnerId, obj.PropertyManagerId, obj.LocationId, obj.UnitId, "All").ToList();
        }
        public OwnerProfile GetOwner(string ownerId)
        {
            return _objPropertyEntities.OwnerProfile.Where(x => x.Serial == ownerId).FirstOrDefault();
        }
        public List<PropertyManagerProfile> GetPropertyManagerProfile(string ownerId)
        {
            return _objPropertyEntities.PropertyManagerProfile.Where(x => x.OwnerId == ownerId).ToList();
        }
        public List<Location> GetLocationByOwnerId(string ownerId)
        {
            return _objPropertyEntities.Location.Where(x => x.OwnerId == ownerId).ToList();
        }
        public List<Location> GetLocationByOwnerPropertyManager(string ownerId, string propertyManagerId)
        {
            return _objPropertyEntities.Location.Where(x => x.OwnerId == ownerId && x.PropertyManagerId == propertyManagerId).ToList();
        }
        public ResidentialTenantSignIn ResidentialTenantByUnitId(string serial)
        {
            return _objPropertyEntities.ResidentialTenantSignIn.Where(x => x.UnitId == serial).FirstOrDefault();
        }

        public List<ResidentialUnit> GetResidentialUnitByOwnerId(string ownerId)
        {
            return _objPropertyEntities.ResidentialUnit.Where(x => x.OwnerId == ownerId).ToList();
        }
        public List<ResidentialUnit> GetResidentialUnitByOwnerPropertyManager(string ownerId,string propertyManagerId)
        {
            return _objPropertyEntities.ResidentialUnit.Where(x => x.OwnerId == ownerId && x.PropertyManagerSerialId == propertyManagerId).ToList();
        }
        public List<ResidentialUnit> GetResidentialUnitByOwnerPropertyManagerLocation(string ownerId, string propertyManagerId,string location)
        {
            return _objPropertyEntities.ResidentialUnit.Where(x => x.OwnerId == ownerId 
                                                                   && x.PropertyManagerSerialId == propertyManagerId && x.LocationSerialId==location).ToList();
        }

        public ManagementDashboardModel GettableData(string ownerId, string propertyManagerId, string location,string unit)
        {
            _objPropertyEntities = new EPropertyEntities();
            var lst = _objPropertyEntities.GetEventManagementDashboardTableInfo(ownerId, propertyManagerId, location, unit).ToList();

            //List<ManagementDashboardModel> lstDashboardModels = from getEventManagementDashboardTableInfoResult in lst.ToList()

            ManagementDashboardModel obj = new ManagementDashboardModel();
            if (lst.Count>0)
            {
                obj.Owner = lst[0].Owner;
                obj.ProperlyManager = lst[0].ProperlyManager;
                obj.PropertyLocation = lst[0].PropertyLocation;
                obj.Unit = lst[0].Unit;
                obj.RentalYTD = lst[0].RentalYTD.ToString();
                obj.MonthRental = lst[0].MonthRental.ToString();
                obj.Status = lst[0].Status;
                obj.OutstandingIssues = lst[0].OutstandingIssues.ToString();
                obj.EmergencyContact = lst[0].EmergencyContact;
                obj.EmergencyPhone = lst[0].EmergencyPhone;
            }
            
            return obj;
        }
    }
}
