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
    public class WorkOrderDA
    {
        EPropertyEntities objPropertyEntities = null;
        public WorkOrderDA(bool isLazyLoadingEnable = true)
        {
            objPropertyEntities = EPropertyEntity.GetEntity();
            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public WorkOrderDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
            {
                objPropertyEntities = EPropertyEntity.GetEntity();
            }

            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public WorkOrder GetbyID(int id)
        {
            WorkOrder objWorkOrder = null;
            try
            {             
                var empQuery = from b in objPropertyEntities.WorkOrder
                               where b.Id == id
                               select b;

                objWorkOrder = empQuery.ToList().FirstOrDefault();

                
            }
            catch (Exception ex)
            {
               
            }
            return objWorkOrder;
        }
        public WorkOrder GetBySerial(string serial)
        {
            WorkOrder objWorkOrder = null;
            try
            {
                var empQuery = from b in objPropertyEntities.WorkOrder
                               where b.Serial == serial
                               select b;

                objWorkOrder = empQuery.ToList().FirstOrDefault();


            }
            catch (Exception ex)
            {

            }

            return objWorkOrder;
        }
        public bool Insert(WorkOrder objWorkOrder)
        {
            try
            {                
                objPropertyEntities.WorkOrder.Add(objWorkOrder);
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }      
        public bool Update(WorkOrder obj)
        {
            try
            {
                WorkOrder existing = objPropertyEntities.WorkOrder.Find(obj.Id);
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
                objPropertyEntities.WorkOrder.Remove(this.GetbyID(id));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteBySerial(string serial)
        {
            try
            {
                objPropertyEntities.WorkOrder.Remove(this.GetBySerial(serial));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<WorkOrder> GetAllInformation()
        {
            List<WorkOrder> listContactInformation = null;
            try
            {               
                var empQuery = from b in objPropertyEntities.WorkOrder
                               where b.Id > 0
                               select b;

                listContactInformation = empQuery.OrderBy(x => x.ContractName).ToList();
               
            }
            catch (Exception ex)
            {
               
            }
            return listContactInformation;
        }
        public List<WorkOrder> GetByOwner(string serial)
        {
            List<WorkOrder> listContactInformation = null;
            try
            {
                var empQuery = from b in objPropertyEntities.WorkOrder
                               where b.OwnerId == serial
                               select b;

                listContactInformation = empQuery.OrderBy(x => x.ContractName).ToList();

            }
            catch (Exception ex)
            {

            }
            return listContactInformation;
        }
        public List<WorkOrder> GetByPropertyLocation(string serial)
        {
            List<WorkOrder> listContactInformation = null;
            try
            {
                var empQuery = from b in objPropertyEntities.WorkOrder
                               where b.LocationId == serial
                               select b;

                listContactInformation = empQuery.OrderBy(x => x.ContractName).ToList();

            }
            catch (Exception ex)
            {

            }
            return listContactInformation;
        }
        public List<WorkOrder> GetBySearch(string owner, string manager, string location)
        {
            List<WorkOrder> listWorkOrder = null;
            try
            {
                var empQuery = from b in objPropertyEntities.WorkOrder
                               where b.OwnerId == owner 
                               select b;

                listWorkOrder = empQuery.OrderBy(x => x.ContractName).ToList();

                if (manager != string.Empty)
                {
                    listWorkOrder = listWorkOrder.Where(x=> x.BillTo == manager).ToList();
                }

                if (location != string.Empty)
                {
                    listWorkOrder = listWorkOrder.Where(x => x.LocationId == location).ToList();
                }

               
               
            }
            catch (Exception ex)
            {

            }
            return listWorkOrder;
        }
        public string MakeAutoGenLocation(string yourPrefix, string objName)
        {
            try
            {
                objPropertyEntities = EPropertyEntity.GetFreshEntity();
                var oupParam = new ObjectParameter("NewID", 0) { Value = DBNull.Value };
                objPropertyEntities.SP_GetID(objName, "Id", DateTime.Now.Year, null, null, oupParam);
                var sNumber = oupParam.Value.ToString().PadLeft(11, '0');
                var serial = string.Concat(yourPrefix, sNumber);

                return serial;
            }
            catch (Exception ex)
            {
                // ignored
            }

            return null;
        }

        #region Part

        public WorkOrderPartData GetWorkOrderPartById(int id)
        {
            WorkOrderPartData objWorkOrderPartData = null;
            try
            {
                var empQuery = from b in objPropertyEntities.WorkOrderPartData
                               where b.Id == id
                               select b;

                objWorkOrderPartData = empQuery.ToList().FirstOrDefault();


            }
            catch (Exception ex)
            {

            }
            return objWorkOrderPartData;
        }
        public bool InsertPart(WorkOrderPartData objWorkOrderPartData)
        {
            try
            {
                objPropertyEntities.WorkOrderPartData.Add(objWorkOrderPartData);
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdatePart(WorkOrderPartData obj)
        {
            try
            {
                WorkOrderPartData existing = objPropertyEntities.WorkOrderPartData.Find(obj.Id);
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
        public bool DeletePartByID(int id)
        {
            try
            {
                objPropertyEntities.WorkOrderPartData.Remove(this.GetWorkOrderPartById(id));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }       
        public List<WorkOrderPartData> GetPartDataByWorkOrder(string serial)
        {
            List<WorkOrderPartData> listWorkOrderPartData = null;
            try
            {
                var empQuery = from b in objPropertyEntities.WorkOrderPartData
                               where b.WorkOrderSerialId == serial
                               select b;

                listWorkOrderPartData = empQuery.OrderBy(x => x.PurchaseDate).ToList();

            }
            catch (Exception ex)
            {

            }
            return listWorkOrderPartData;
        }

        #endregion

    }
}
