using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using PropertyService.BO;
using PropertyService.ViewModel;

namespace PropertyService.DA
{
    public class ResidentialUnitDa
    {
        private EPropertyEntities _objPropertyEntities;

        public ResidentialUnitDa(bool isLazyLoadingEnable = true)
        {
            _objPropertyEntities = EPropertyEntity.GetEntity();
            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public ResidentialUnitDa(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                _objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
                _objPropertyEntities = EPropertyEntity.GetEntity();

            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public ResidentialUnit GetbyId(int id)
        {
            ResidentialUnit objResidentialUnit = null;
            try
            {
                var empQuery = from b in _objPropertyEntities.ResidentialUnit
                               where b.Id == id
                               select b;

                objResidentialUnit = empQuery.ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialUnit;
        }

        public ResidentialUnit GetbySerial(string serial)
        {
            ResidentialUnit objResidentialUnit = null;
            try
            {
                var empQuery = from b in _objPropertyEntities.ResidentialUnit
                               where b.Serial == serial
                               select b;

                objResidentialUnit = empQuery.ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialUnit;
        }

        public bool Insert(ResidentialUnit objResidentialUnit)
        {
            try
            {
                _objPropertyEntities.ResidentialUnit.Add(objResidentialUnit);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(ResidentialUnit obj)
        {
            try
            {
                var existing = _objPropertyEntities.ResidentialUnit.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            try
            {
                _objPropertyEntities.ResidentialUnit.Remove(GetbyId(id));
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<ResidentialUnit> GetAllInformation()
        {
            List<ResidentialUnit> listResidentialUnit = null;
            try
            {
                var empQuery = from b in _objPropertyEntities.ResidentialUnit
                               where b.Id > 0
                               select b;

                listResidentialUnit = empQuery.OrderBy(x => x.UnitName).ToList();
            }
            catch (Exception)
            {
                // ignored
            }
            return listResidentialUnit;
        }

        public List<ResidentialUnit> GetByOwner(string serial)
        {
            List<ResidentialUnit> listResidentialUnit = null;
            try
            {
                var empQuery = from b in _objPropertyEntities.ResidentialUnit
                               where b.OwnerId == serial
                               select b;

                listResidentialUnit = empQuery.OrderBy(x => x.UnitName).ToList();
            }
            catch (Exception)
            {
                // ignored
            }

            return listResidentialUnit;
        }
        public List<ResidentialUnit> GetByOwnerAndUnitName(string serial, string unitName)
        {
            List<ResidentialUnit> listResidentialUnit = null;
            try
            {
                var empQuery = from b in _objPropertyEntities.ResidentialUnit
                               where b.OwnerId == serial && b.UnitName.ToLower() == unitName.ToLower()
                               select b;

                listResidentialUnit = empQuery.OrderBy(x => x.UnitName).ToList();
            }
            catch (Exception)
            {
                // ignored
            }

            return listResidentialUnit;
        }

        public List<ResidentialUnit> GetByPropertyManager(string propertyManagerSerial)
        {
            List<ResidentialUnit> listResidentialUnit = null;
            try
            {
                var empQuery = from b in _objPropertyEntities.ResidentialUnit
                               where b.PropertyManagerSerialId == propertyManagerSerial
                               select b;

                listResidentialUnit = empQuery.OrderBy(x => x.UnitName).ToList();
            }
            catch (Exception)
            {
                // ignored
            }

            return listResidentialUnit;
        }
        public List<ResidentialUnit> GetByLocation(string locationSerial)
        {
            List<ResidentialUnit> listResidentialUnit = null;
            try
            {
                var empQuery = from b in _objPropertyEntities.ResidentialUnit
                               where b.LocationSerialId == locationSerial
                               select b;

                listResidentialUnit = empQuery.OrderBy(x => x.UnitName).ToList();
            }
            catch (Exception)
            {
                // ignored
            }

            return listResidentialUnit;
        }

        public string MakeAutoGenLocation(string yourPrefix, string objName)
        {
            try
            {
                _objPropertyEntities = EPropertyEntity.GetFreshEntity();
                var oupParam = new ObjectParameter("NewID", 0) { Value = DBNull.Value };
                _objPropertyEntities.SP_GetID(objName, "Id", DateTime.Now.Year, null, null, oupParam);
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

        public List<usp_GetResidentialFeatureSpecs_Result> GetAllFeatureList(string serial)
        {
            _objPropertyEntities = EPropertyEntity.GetFreshEntity();
            //  var oupParam = new ObjectParameter("NewID", 0) { Value = DBNull.Value };
            return _objPropertyEntities.usp_GetResidentialFeatureSpecs(serial).ToList();
            //_objPropertyEntities.SP_GetID(objName, "Id", DateTime.Now.Year, null, null, oupParam);
            //var sNumber = oupParam.Value.ToString().PadLeft(11, '0');
            //var serial = string.Concat(yourPrefix, sNumber);


            //return _objPropertyEntities?.ResidentialUnitSpecsChild
            //    .Where(rs => rs.ResidentialUnitSpecsSerialId.Equals(serial))
            //    .ToList();
        }

        public bool InsertFeature(ResidentialUnitSpecsChild obj)
        {
            try
            {
                _objPropertyEntities.ResidentialUnitSpecsChild.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateFeature(ResidentialUnitSpecsChild obj)
        {
            try
            {
                var existing = _objPropertyEntities.ResidentialUnitSpecsChild.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ResidentialUnitSpecsChild GetSpescFeaturebyId(int id)
        {
            return _objPropertyEntities
                .ResidentialUnitSpecsChild
                .Where(rsc => rsc.Id.Equals(id))
                .ToList()
                .FirstOrDefault();
        }

        public bool DeleteSpecFeatureById(int id)
        {
            try
            {
                _objPropertyEntities.ResidentialUnitSpecsChild.Remove(GetSpescFeaturebyId(id));
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Specs Tab
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResidentialUnitSpecs GetSpecsbyId(int id)
        {
            return _objPropertyEntities
                .ResidentialUnitSpecs
                .Where(rsc => rsc.Id.Equals(id))
                .ToList()
                .FirstOrDefault();
        }
        public ResidentialUnitSpecs GetSpecsbyIdEdit(string unitId)
        {
            return _objPropertyEntities
                .ResidentialUnitSpecs
                .Where(rsc => rsc.ResidentialUnitSerialId.Equals(unitId))
                .ToList()
                .FirstOrDefault();
        }
        public List<ResidentialUnitSpecs> GetAllSpecsList(string serial)
        {
            return _objPropertyEntities?.ResidentialUnitSpecs
                .Where(rs => rs.ResidentialUnitSerialId.Equals(serial))
                .ToList();
        }

        public bool DeleteSpecById(int id)
        {
            try
            {
                _objPropertyEntities.ResidentialUnitSpecs.Remove(GetSpecsbyId(id));
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool InsertSpecs(ResidentialUnitSpecs obj)
        {
            try
            {
                _objPropertyEntities.ResidentialUnitSpecs.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateSpecs(ResidentialUnitSpecs obj)
        {
            try
            {
                //var entry = _objPropertyEntities.Entry(obj);

                //if (entry.State == EntityState.Detached)
                //{
                //    _objPropertyEntities.ResidentialUnitSpecs.Attach(obj);
                //    entry.State = EntityState.Modified;
                //}


                var existing = _objPropertyEntities.ResidentialUnitSpecs.Find(obj.Id);
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

        //Webpage and Image
        public List<ResidentialUnitWebImage> GetAllWebpagesList(string serial)
        {
            return _objPropertyEntities?.ResidentialUnitWebImage
                .Where(rs => rs.ResidentialUnitSerialId.Equals(serial))
                .ToList();
        }
        public ResidentialUnitWebImage GetWebpagebyId(int id)
        {
            return _objPropertyEntities
                .ResidentialUnitWebImage
                .Where(rsc => rsc.Id.Equals(id))
                .ToList()
                .FirstOrDefault();
        }
        public List<ResidentialUnitWebImageChild> GetWebpageImagesbySerial(string serial)
        {
            return _objPropertyEntities
                .ResidentialUnitWebImageChild
                .Where(rsc => rsc.ResidentialUnitWebImageSerialId.Equals(serial))
                .ToList();
        }
        public bool InsertWebImage(ResidentialUnitWebImage obj)
        {
            try
            {
                _objPropertyEntities.ResidentialUnitWebImage.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateWebImage(ResidentialUnitWebImage obj)
        {
            try
            {
                var existing = _objPropertyEntities.ResidentialUnitWebImage.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<ResidentialUnitWebImage> GetAllWebList(string serial)
        {
            return _objPropertyEntities?.ResidentialUnitWebImage
                .Where(rs => rs.ResidentialUnitSerialId.Equals(serial) && (rs.IsDelete == null || rs.IsDelete == false))
                .ToList();
        }
        public bool DeleteWebImage(string serial, string UnitId, Int16 userId)
        {
            try
            {
                var obj =
                _objPropertyEntities.ResidentialUnitWebImage.FirstOrDefault(x => x.Serial == serial && x.ResidentialUnitSerialId == UnitId);
                obj.IsDelete = true;
                obj.UpdatedBy = userId;
                obj.UpdatedDate = DateTime.Now;
                UpdateWebImage(obj);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public bool InsertWebImageChild(ResidentialUnitWebImageChild obj)
        {
            try
            {
                _objPropertyEntities.ResidentialUnitWebImageChild.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateWebImageChild(ResidentialUnitWebImageChild obj)
        {
            try
            {
                var existing = _objPropertyEntities.ResidentialUnitWebImageChild.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteWebpagesById(int id)
        {
            var obj = GetWebpagebyId(id);
            if (obj == null) return false;
            _objPropertyEntities.ResidentialUnitWebImageChild.RemoveRange(GetWebpageImagesbySerial(obj.Serial));
            _objPropertyEntities.SaveChanges();
            _objPropertyEntities.ResidentialUnitWebImage.Remove(obj);
            _objPropertyEntities.SaveChanges();
            return true;
        }
        public bool DeleteWebpageSingleImageById(int id)
        {
            var obj = _objPropertyEntities.ResidentialUnitWebImageChild.Find(id);
            if (obj == null) return false;
            _objPropertyEntities.ResidentialUnitWebImageChild.Remove(obj);
            _objPropertyEntities.SaveChanges();
            return true;
        }
        //--- video save
        public bool InsertWebImageVideo(ResidentialUnitWebImageVideo obj)
        {
            try
            {
                _objPropertyEntities.ResidentialUnitWebImageVideo.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<ResidentialUnitWebImageVideo> GetAllvideo(string unitId)
        {
            return _objPropertyEntities?.ResidentialUnitWebImageVideo
                .Where(rs => rs.ResidentialUnitSerialId.Equals(unitId))
                .ToList();
        }
        public bool UpdateWebImageIsUseToFalseByUnitId(string ResidentialUnitId)
        {
            try
            {
                // var serial = "";
                _objPropertyEntities = EPropertyEntity.GetFreshEntity();
                //  var oupParam = new ObjectParameter("NewID", 0) { Value = DBNull.Value };
                _objPropertyEntities.SP_updateRecidentialWebImageIsUsedByUnitId(ResidentialUnitId);

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool UpdateResidentialUnitWebImageBulk(List<ResidentialUnitWebImage> obj)
        {
            try
            {
                foreach (ResidentialUnitWebImage aResidentialUnitWebImage in obj)
                {
                    UpdateWebImage(aResidentialUnitWebImage);
                }
            }
            catch (Exception)
            {

                return false;
            }

            //UpdateWebImage
            return true;
        }

        //--------- Equipment ----------//
        public List<RefCountries> GetCountrlList()
        {
            var lstOfCountry = new List<RefCountries>();
            try
            {
                lstOfCountry = _objPropertyEntities.RefCountries.ToList();
            }
            catch (Exception ex)
            {


            }
            return lstOfCountry;
        }
        public List<RefStates> GetStateList()
        {
            var lstOfStates = new List<RefStates>();
            try
            {
                lstOfStates = _objPropertyEntities.RefStates.ToList().Distinct().ToList();
            }
            catch (Exception ex)
            {


            }
            return lstOfStates;
        }
        public List<Cities> GetCityList()
        {
            var lstOfCities = new List<Cities>();
            try
            {
                lstOfCities = _objPropertyEntities.Cities.ToList().Distinct().ToList();
            }
            catch (Exception ex)
            {


            }
            return lstOfCities;
        }
        public bool InsertEquipmentImage(ResidentialUnitEquipmentImage obj)
        {
            try
            {
                _objPropertyEntities.ResidentialUnitEquipmentImage.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InsertEquipment(ResidentialUnitEquipment obj)
        {
            try
            {
                _objPropertyEntities.ResidentialUnitEquipment.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool UpdateEquipment(ResidentialUnitEquipment obj)
        {
            try
            {
                var existing = _objPropertyEntities.ResidentialUnitEquipment.Find(obj.Id);
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
        public List<ResidentialUnitEquipmentImage> GetAllRentalEqImage(string RentalunitSerialId)
        {
            var lstOfResidentialUnitEquipmentImage = new List<ResidentialUnitEquipmentImage>();
            try
            {

                lstOfResidentialUnitEquipmentImage =
                    _objPropertyEntities.ResidentialUnitEquipmentImage.Where(
                        x => x.ResidentialUnitSerialId == RentalunitSerialId).ToList();


            }
            catch (Exception ex)
            {

            }
            return lstOfResidentialUnitEquipmentImage;
        }
        public List<ResidentialUnitEquipmentImage> GetAllRentalEqImage_Single(int Id)
        {
            var lstOfResidentialUnitEquipmentImage = new List<ResidentialUnitEquipmentImage>();
            try
            {

                lstOfResidentialUnitEquipmentImage =
                    _objPropertyEntities.ResidentialUnitEquipmentImage.Where(
                        x => x.Id == Id).ToList();


            }
            catch (Exception ex)
            {

            }
            return lstOfResidentialUnitEquipmentImage;
        }
        public ResidentialUnitEquipment GetAllRentalEquipmentData(string RentalunitSerialId)
        {
            var lstOfResidentialUnitEquipment = new ResidentialUnitEquipment();
            try
            {

                lstOfResidentialUnitEquipment =
                    _objPropertyEntities.ResidentialUnitEquipment.FirstOrDefault(x => x.ResidentialUnitSerialId == RentalunitSerialId);


            }
            catch (Exception ex)
            {

            }
            return lstOfResidentialUnitEquipment;
        }
        public ResidentialUnitEquipment GetAllRentalEquipmentData_Id(int Id)
        {
            var lstOfResidentialUnitEquipment = new ResidentialUnitEquipment();
            try
            {

                lstOfResidentialUnitEquipment =
                    _objPropertyEntities.ResidentialUnitEquipment.FirstOrDefault(x => x.Id == Id);


            }
            catch (Exception ex)
            {

            }
            return lstOfResidentialUnitEquipment;
        }
        public List<usp_GetEquipmentListByUnitId_PropertyManager_LocationId_Result> GetEquipmentList(EventManagement aobj)
        {
            var lstOfResidentialUnitEquipment = new List<usp_GetEquipmentListByUnitId_PropertyManager_LocationId_Result>();
            try
            {

                lstOfResidentialUnitEquipment =
                    _objPropertyEntities.usp_GetEquipmentListByUnitId_PropertyManager_LocationId(aobj.Id, aobj.OwnerId, aobj.PropertyManagerId, aobj.LocationId, aobj.UnitId).ToList();


            }
            catch (Exception ex)
            {

            }
            return lstOfResidentialUnitEquipment;
        }
        public ResidentialUnitEquipment GetRentalEquipmentData(int Id)
        {
            var lstOfResidentialUnitEquipment = new ResidentialUnitEquipment();
            try
            {

                lstOfResidentialUnitEquipment =
                    _objPropertyEntities.ResidentialUnitEquipment.FirstOrDefault(x => x.Id == Id);


            }
            catch (Exception ex)
            {

            }
            return lstOfResidentialUnitEquipment;
        }

        #region Communication
        public ResidentialCommunication GetResidentialCommunication(int Id)
        {
            var lstOfResidentialCommunication = new ResidentialCommunication();
            try
            {

                lstOfResidentialCommunication =
                    _objPropertyEntities.ResidentialCommunication.FirstOrDefault(x => x.Id == Id);


            }
            catch (Exception ex)
            {

            }
            return lstOfResidentialCommunication;
        }
        public bool InsertResidentialUnitEquipment(ResidentialCommunication obj)
        {
            try
            {
                _objPropertyEntities.ResidentialCommunication.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool UpdateResidentialCommunication(ResidentialCommunication obj)
        {
            try
            {
                var existing = _objPropertyEntities.ResidentialCommunication.Find(obj.Id);
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

        public List<uspGetCommunicationGridData_Result> LoadCommunication(string serialId)
        {
            var lstOfResidentialCommunication = new List<uspGetCommunicationGridData_Result>();
            try
            {
                _objPropertyEntities = EPropertyEntity.GetFreshEntity();
                lstOfResidentialCommunication = _objPropertyEntities.uspGetCommunicationGridData(serialId).ToList();
                //var sNumber = oupParam.Value.ToString().PadLeft(11, '0');
                //var serial = string.Concat(yourPrefix, sNumber);

                return lstOfResidentialCommunication;

                //lstOfResidentialCommunication =
                //    _objPropertyEntities.ResidentialCommunication.Where(x=>x.PropertyManagerSerialId == serialId).ToList();


            }
            catch (Exception ex)
            {

            }
            return lstOfResidentialCommunication;
        }
        #endregion
        #region Residential Document
        public ResidentalDocumentListOfRental GetRentalDocumentDataById(int Id)
        {
            var lstOfResidentialDocument = new ResidentalDocumentListOfRental();
            try
            {

                lstOfResidentialDocument = _objPropertyEntities.ResidentalDocumentListOfRental.FirstOrDefault(x => x.Id == Id);
                //var sNumber = oupParam.Value.ToString().PadLeft(11, '0');
                //var serial = string.Concat(yourPrefix, sNumber);

                return lstOfResidentialDocument;

                //lstOfResidentialCommunication =
                //    _objPropertyEntities.ResidentialCommunication.Where(x=>x.PropertyManagerSerialId == serialId).ToList();


            }
            catch (Exception ex)
            {

            }
            return lstOfResidentialDocument;
        }
        public List<ResidentalDocumentListOfRental> GetRentalDocumentData(string serialId)
        {
            var lstOfResidentialDocument = new List<ResidentalDocumentListOfRental>();
            try
            {

                lstOfResidentialDocument = _objPropertyEntities.ResidentalDocumentListOfRental.Where(x => x.ResidentialUnitSerialId == serialId && x.DeletedBy == null).ToList();
                //var sNumber = oupParam.Value.ToString().PadLeft(11, '0');
                //var serial = string.Concat(yourPrefix, sNumber);

                return lstOfResidentialDocument;

                //lstOfResidentialCommunication =
                //    _objPropertyEntities.ResidentialCommunication.Where(x=>x.PropertyManagerSerialId == serialId).ToList();


            }
            catch (Exception ex)
            {

            }
            return lstOfResidentialDocument;
        }
        public bool UpdateResidentialRentalDocument(ResidentalDocumentListOfRental obj)
        {
            try
            {
                var existing = _objPropertyEntities.ResidentalDocumentListOfRental.Find(obj.Id);
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
        public bool InsertResidentialRentalDocument(ResidentalDocumentListOfRental obj)
        {
            try
            {
                _objPropertyEntities.ResidentalDocumentListOfRental.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public ResidentalDocumentListOfRental GetLastResidentialRentalDocumentId()
        {
            var ResidentialMaintainesManagerSchedule = new ResidentalDocumentListOfRental();
            try
            {
                ResidentialMaintainesManagerSchedule = _objPropertyEntities.ResidentalDocumentListOfRental.OrderByDescending(x => x.Id).FirstOrDefault();
                // _objPropertyEntities.SaveChanges();
                return ResidentialMaintainesManagerSchedule;
            }
            catch (Exception ex)
            {

                return ResidentialMaintainesManagerSchedule;
            }
        }
        public List<ResidentialDocument> GetDocumentData(string serialId)
        {
            var lstOfResidentialDocument = new List<ResidentialDocument>();
            try
            {

                lstOfResidentialDocument = _objPropertyEntities.ResidentialDocument.Where(x => x.PropertyManagerSerialId == serialId).ToList();
                //var sNumber = oupParam.Value.ToString().PadLeft(11, '0');
                //var serial = string.Concat(yourPrefix, sNumber);

                return lstOfResidentialDocument;

                //lstOfResidentialCommunication =
                //    _objPropertyEntities.ResidentialCommunication.Where(x=>x.PropertyManagerSerialId == serialId).ToList();


            }
            catch (Exception ex)
            {

            }
            return lstOfResidentialDocument;
        }
        public ResidentialDocument GetResidentialDocument(int Id)
        {
            var lstOfResidentialDocument = new ResidentialDocument();
            try
            {

                lstOfResidentialDocument =
                    _objPropertyEntities.ResidentialDocument.FirstOrDefault(x => x.Id == Id);


            }
            catch (Exception ex)
            {

            }
            return lstOfResidentialDocument;
        }
        public bool InsertResidentialDocument(ResidentialDocument obj)
        {
            try
            {
                _objPropertyEntities.ResidentialDocument.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool UpdateResidentialDocument(ResidentialDocument obj)
        {
            try
            {
                var existing = _objPropertyEntities.ResidentialDocument.Find(obj.Id);
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
        #endregion
        #region Maintenance manager

        public ResidentialMaintainesManagerImage GetResidentialMaintainesManagerImage(string unitId)
        {
            var ResidentialMaintainesManagerImage = new ResidentialMaintainesManagerImage();
            try
            {
                ResidentialMaintainesManagerImage =
                    _objPropertyEntities.ResidentialMaintainesManagerImage.FirstOrDefault(x => x.ResidentialUnitSerialId == unitId);


            }
            catch (Exception ex)
            {
                return ResidentialMaintainesManagerImage;
            }
            return ResidentialMaintainesManagerImage;
        }
        public List<ResidentialMaintainesManagerSchedule> GetResidentialMaintainesManagerSchedule(string unitId)
        {
            var ResidentialMaintainesManagerSchedule = new List<ResidentialMaintainesManagerSchedule>();
            try
            {
                ResidentialMaintainesManagerSchedule =
                    _objPropertyEntities.ResidentialMaintainesManagerSchedule.Where(x => x.ResidentialUnitSerialId == unitId && x.DeletedBy == null).ToList();

            }
            catch (Exception ex)
            {
                return ResidentialMaintainesManagerSchedule;
            }
            return ResidentialMaintainesManagerSchedule;
        }
        public ResidentialMaintainesManagerSchedule GetResidentialMaintainesManagerScheduleById(int id)
        {
            var ResidentialMaintainesManagerSchedule = new ResidentialMaintainesManagerSchedule();
            try
            {
                ResidentialMaintainesManagerSchedule =
                    _objPropertyEntities.ResidentialMaintainesManagerSchedule.FirstOrDefault(x => x.Id == id && x.DeletedBy == null);

            }
            catch (Exception ex)
            {
                return ResidentialMaintainesManagerSchedule;
            }
            return ResidentialMaintainesManagerSchedule;
        }
        public string SetSchedule(List<EventManagement> obj)
        {
            try
            {
                _objPropertyEntities.usp_SetScheduleWork_New_Delete(obj.FirstOrDefault().OwnerId,
                obj.FirstOrDefault().PropertyManagerId, obj.FirstOrDefault().LocationId, obj.FirstOrDefault().UnitId);
                foreach (EventManagement aobj in obj)
                {
                    try
                    {
                        _objPropertyEntities.usp_SetScheduleWork_New(aobj.OwnerId, aobj.PropertyManagerId, aobj.LocationId, aobj.UnitId, aobj.Title, aobj.Description, aobj.FromDate, aobj.ToDate);
                    }
                    catch (Exception ex)
                    {

                        return "false";
                    }
                }

            }
            catch (Exception ex)
            {

                return "false";
            }


            return "true";

        }
        public bool UpdateResidentialMaintainesManagerSchedule(ResidentialMaintainesManagerSchedule obj)
        {
            try
            {
                var existing = _objPropertyEntities.ResidentialMaintainesManagerSchedule.Find(obj.Id);
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
        public bool InsertResidentialSchedule(ResidentialMaintainesManagerSchedule obj)
        {
            try
            {
                _objPropertyEntities.ResidentialMaintainesManagerSchedule.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public ResidentialMaintainesManagerSchedule GetLastResidentialSchedule()
        {
            var ResidentialMaintainesManagerSchedule = new ResidentialMaintainesManagerSchedule();
            try
            {
                ResidentialMaintainesManagerSchedule = _objPropertyEntities.ResidentialMaintainesManagerSchedule.OrderByDescending(x => x.Id).FirstOrDefault(x => x.DeletedBy == null);
                // _objPropertyEntities.SaveChanges();
                return ResidentialMaintainesManagerSchedule;
            }
            catch (Exception ex)
            {

                return ResidentialMaintainesManagerSchedule;
            }
        }
        public List<ResidentialMaintainesManagerPartData> GetResidentialMaintainesManagerPartData(string unitId)
        {
            var ResidentialMaintainesManagerPartData = new List<ResidentialMaintainesManagerPartData>();
            try
            {
                ResidentialMaintainesManagerPartData =
                    _objPropertyEntities.ResidentialMaintainesManagerPartData.Where(x => x.ResidentialUnitSerialId == unitId).ToList();

            }
            catch (Exception ex)
            {
                return ResidentialMaintainesManagerPartData;
            }
            return ResidentialMaintainesManagerPartData;
        }

        public List<ResidentialMaintainesManagerVandorData> GetResidentialMaintainesManagerVandorData(string unitId)
        {
            var ResidentialMaintainesManagerVandorData = new List<ResidentialMaintainesManagerVandorData>();
            try
            {
                ResidentialMaintainesManagerVandorData =
                    _objPropertyEntities.ResidentialMaintainesManagerVandorData.Where(x => x.ResidentialUnitSerialId == unitId && x.DeletedDate == null).ToList();

            }
            catch (Exception ex)
            {
                return ResidentialMaintainesManagerVandorData;
            }
            return ResidentialMaintainesManagerVandorData;
        }
        public ResidentialMaintainesManagerMaster GetResidentialMaintainesManagerMaster(string unitId)
        {
            var ResidentialMaintainesManagerMaster = new ResidentialMaintainesManagerMaster();
            try
            {
                ResidentialMaintainesManagerMaster =
                    _objPropertyEntities.ResidentialMaintainesManagerMaster.FirstOrDefault(x => x.ResidentialUnitSerialId == unitId);

            }
            catch (Exception ex)
            {
                return ResidentialMaintainesManagerMaster;
            }
            return ResidentialMaintainesManagerMaster;
        }

        public bool InsertMaintenanceImage(ResidentialMaintainesManagerImage obj)
        {
            try
            {
                _objPropertyEntities.ResidentialMaintainesManagerImage.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Part
        public ResidentialMaintainesManagerPartData GetPartDataById(int id)
        {
            var ResidentialMaintainesManagerSchedule = new ResidentialMaintainesManagerPartData();
            try
            {
                ResidentialMaintainesManagerSchedule =
                    _objPropertyEntities.ResidentialMaintainesManagerPartData.FirstOrDefault(x => x.Id == id && x.DeletedBy == null);

            }
            catch (Exception ex)
            {
                return ResidentialMaintainesManagerSchedule;
            }
            return ResidentialMaintainesManagerSchedule;
        }
        public bool Updatepartdata(ResidentialMaintainesManagerPartData obj)
        {
            try
            {
                var existing = _objPropertyEntities.ResidentialMaintainesManagerPartData.Find(obj.Id);
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
        public bool InsertPart(ResidentialMaintainesManagerPartData obj)
        {
            try
            {
                _objPropertyEntities.ResidentialMaintainesManagerPartData.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public ResidentialMaintainesManagerPartData GetLastResidentialPartData()
        {
            var ResidentialMaintainesManagerPartData = new ResidentialMaintainesManagerPartData();
            try
            {
                ResidentialMaintainesManagerPartData = _objPropertyEntities.ResidentialMaintainesManagerPartData.OrderByDescending(x => x.Id).FirstOrDefault(x => x.DeletedBy == null);

                return ResidentialMaintainesManagerPartData;
            }
            catch (Exception ex)
            {

                return ResidentialMaintainesManagerPartData;
            }
        }

        public List<VendorProfile> LoadVendor(string unitId)
        {
            var lstOfVendor = new List<VendorProfile>();
            try
            {
                lstOfVendor = _objPropertyEntities.VendorProfile.Where(x => x.OwnerId == unitId).ToList();
            }
            catch (Exception ex)
            {


            }
            return lstOfVendor;
        }

        #endregion
        #region Vendor
        public ResidentialMaintainesManagerVandorData GetResidentialVendorById(int id)
        {
            var ResidentialMaintainesManagerVandorData = new ResidentialMaintainesManagerVandorData();
            try
            {
                ResidentialMaintainesManagerVandorData =
                    _objPropertyEntities.ResidentialMaintainesManagerVandorData.FirstOrDefault(x => x.Id == id && x.DeletedBy == null);

            }
            catch (Exception ex)
            {
                return ResidentialMaintainesManagerVandorData;
            }
            return ResidentialMaintainesManagerVandorData;
        }

        public bool UpdateVendordata(ResidentialMaintainesManagerVandorData obj)
        {
            try
            {
                var existing = _objPropertyEntities.ResidentialMaintainesManagerVandorData.Find(obj.Id);
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
        public bool InsertVendor(ResidentialMaintainesManagerVandorData obj)
        {
            try
            {
                _objPropertyEntities.ResidentialMaintainesManagerVandorData.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public ResidentialMaintainesManagerVandorData GetLastResidentialVendorData()
        {
            var ResidentialMaintainesManagerVandorData = new ResidentialMaintainesManagerVandorData();
            try
            {
                ResidentialMaintainesManagerVandorData = _objPropertyEntities.ResidentialMaintainesManagerVandorData.OrderByDescending(x => x.Id).FirstOrDefault(x => x.DeletedBy == null);
                // _objPropertyEntities.SaveChanges();
                return ResidentialMaintainesManagerVandorData;
            }
            catch (Exception ex)
            {

                return ResidentialMaintainesManagerVandorData;
            }
        }



        public List<ResidentialUnit> GetByOwnerData(string serial)
        {
            List<ResidentialUnit> listResidentialUnit = new List<ResidentialUnit>();
            try
            {
                listResidentialUnit =
                   _objPropertyEntities.ResidentialUnit.Where(x => x.OwnerId == serial)
                       .ToList()
                       .OrderBy(y => y.UnitName)
                       .ToList();

            }
            catch (Exception ex)
            {
                // ignored
            }

            return listResidentialUnit;
        }
        public List<ResidentialTenantSignIn> GetByTenantData(string serial)
        {
            List<ResidentialTenantSignIn> listResidentialUnit = new List<ResidentialTenantSignIn>();
            try
            {
                listResidentialUnit =
                    _objPropertyEntities.ResidentialTenantSignIn.ToList();

            }
            catch (Exception ex)
            {
                // ignored
            }

            return listResidentialUnit;
        }
        #endregion
        #region Residential Web Template
        public List<ResidentialUnitWebImage> GetAllWebpagesListbyIsTrue(string serial)
        {
            return _objPropertyEntities?.ResidentialUnitWebImage
                .Where(rs => rs.ResidentialUnitSerialId == serial && rs.IsUsed == true)
                .ToList();
        }

        public ResidentialUnitSpecs GetAllResidentialUnitSpecsList(string serial)
        {
            return _objPropertyEntities?.ResidentialUnitSpecs
                .FirstOrDefault(rs => rs.ResidentialUnitSerialId.Equals(serial));

        }
        #endregion

        public List<usp_GetsenderName_Result> GetSenderList(string OwnerId)
        {
            var res = new List<usp_GetsenderName_Result>();
            try
            {
                res = _objPropertyEntities.usp_GetsenderName(OwnerId).ToList();
            }
            catch (Exception)
            {


            }
            return res;
        }

        public List<usp_GetMessage_Owner_Result> GetOwnerMessage(string FromUser, string unidId, string RequestType, string SenderId)
        {
            var res = new List<usp_GetMessage_Owner_Result>();
            try
            {
                res = _objPropertyEntities.usp_GetMessage_Owner(FromUser, unidId, RequestType, SenderId).ToList();
            }
            catch (Exception ex)
            {


            }
            return res;
        }

        public communicationPanel GetCommunicationById(int id)
        {
            var communicationPanel = new communicationPanel();
            try
            {
                communicationPanel = _objPropertyEntities.communicationPanel.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception)
            {


            }
            return communicationPanel;
        }
        public bool InsertCommunication(communicationPanel obj)
        {
            try
            {
                _objPropertyEntities.communicationPanel.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteEquipmentById(int id)
        {
            var obj = GetAllRentalEquipmentData_Id(id);
            if (obj == null) return false;
            _objPropertyEntities.ResidentialUnitEquipment.Remove(obj);
            _objPropertyEntities.SaveChanges();
            return true;
        }

        #region Web Analytics

        public List<WebAnalyticsModel> GetWebAnalyticsData(WebAnalyticsModel obj)
        {
            _objPropertyEntities = new EPropertyEntities();
            List<WebAnalyticsModel> lst = new List<WebAnalyticsModel>();
            try
            {
                var from = Convert.ToDateTime(obj.from);
                var to = Convert.ToDateTime(obj.to);

                var result = _objPropertyEntities.Usp_GetWebAnalyticsData(obj.OwnerId, obj.PropertyManagerId,
                    obj.LocationId, obj.UnitId, from, to).ToList();
                if (result.Count > 0)
                {
                    foreach (var element in result)
                    {
                        WebAnalyticsModel el = new WebAnalyticsModel();
                        el.OwnerId = element.OwnerId;
                        el.PropertyManagerId = "";
                        el.LocationId = element.LocationId;
                        el.UnitId = element.UnitId;
                        el.MTDSchedules = element.MTDSchedules.ToString();
                        el.TotalSchedules = element.TotalSchedules.ToString();
                        el.MTDStart = element.MTDStart.ToString();
                        el.TotalStart = element.TotalStart.ToString();
                        el.MTDCompleted = element.MTDCompleted.ToString();
                        el.TotalCompleted = element.TotalCompleted.ToString();
                        el.MTDViews = element.MTDViews.ToString();
                        el.TotalViews = element.TotalViews.ToString();
                        lst.Add(el);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return lst;

        }

        public List<Barchart> GetWebAnalyticsBarChartData(WebAnalyticsModel obj)
        {
            _objPropertyEntities = new EPropertyEntities();
            List<Barchart> lst = new List<Barchart>();
            try
            {
                var result = _objPropertyEntities.Usp_GetWebAnalyticsBarChartData(obj.OwnerId, obj.PropertyManagerId,
                    obj.LocationId, obj.UnitId).ToList();
                if (result.Count > 0)
                {
                    foreach (var element in result)
                    {
                        Barchart el = new Barchart();
                        el.January = element.January > 0 ? element.January.ToString() : "0";
                        el.February = element.February > 0 ? element.February.ToString() : "0";
                        el.March = element.March > 0 ? element.March.ToString() : "0";
                        el.April = element.April > 0 ? element.April.ToString() : "0";
                        el.May = element.May > 0 ? element.May.ToString() : "0";
                        el.June = element.June > 0 ? element.June.ToString() : "0";
                        el.July = element.July > 0 ? element.July.ToString() : "0";
                        el.August = element.August > 0 ? element.August.ToString() : "0";
                        el.September = element.September > 0 ? element.September.ToString() : "0";
                        el.October = element.October > 0 ? element.October.ToString() : "0";
                        el.November = element.November > 0 ? element.November.ToString() : "0";
                        el.December = element.December > 0 ? element.December.ToString() : "0";
                        el.TOTAL = element.TOTAL > 0 ? element.TOTAL.ToString() : "0";
                        el.Name = element.Name;
                        lst.Add(el);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
            return lst;
        }

        #endregion

        public bool CreateDuplicateValue(int Id, string oldUnitId)
        {

            try
            {
                _objPropertyEntities.usp_DuplicateUnit(Id, oldUnitId);
            }
            catch (Exception ex)
            {

                return false;
            }
            return true;
        }

    }
}