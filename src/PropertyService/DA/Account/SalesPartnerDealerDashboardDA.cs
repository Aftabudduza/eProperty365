using PropertyService.BO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using PropertyService.ViewModel;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace PropertyService.DA.Account
{
    public class SalesPartnerDealerDashboardDA
    {
        private EPropertyEntities _objEPropertyEntities;
        private PropertyEntities _objPropertyEntities ;
        public SalesPartnerDealerDashboardDA(bool isLazyLoadingEnable = true)
        {
            _objEPropertyEntities = EPropertyEntity.GetEntity();
            _objEPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
            _objPropertyEntities = PropertyEntity.GetEntity();
        }

        public SalesPartnerDealerDashboardDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
            {
                _objPropertyEntities = PropertyEntity.GetFreshEntity();
                _objEPropertyEntities = EPropertyEntity.GetFreshEntity();
            }
              
            else
            {
                _objPropertyEntities = PropertyEntity.GetEntity();
                _objEPropertyEntities = EPropertyEntity.GetEntity();
            }
                

            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
            _objEPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public bool Insert(DealerSalesPartnerModel obj)
        {
            try
            {
                Dealer_SalesPartner objDealerSalesPartner = new Dealer_SalesPartner();
                objDealerSalesPartner = GetObject(obj);
                _objPropertyEntities = new PropertyEntities();
                _objPropertyEntities.Dealer_SalesPartner.Add(objDealerSalesPartner);
                _objPropertyEntities.SaveChanges();
                UserProfile objUserProfile = new UserProfile();
                objUserProfile = GetUserObject(objDealerSalesPartner, obj.Password,obj.userProfileId);
                _objPropertyEntities = new PropertyEntities();
                _objPropertyEntities.UserProfile.Add(objUserProfile);
                _objPropertyEntities.SaveChanges();

                _objEPropertyEntities.UserProfile.Add(objUserProfile);
                    _objEPropertyEntities.SaveChanges();
                //}
                if (obj.ListDetails != null)
                {
                    int id = objDealerSalesPartner.id;
                    List< Dealer_SalesPartner_DetailsZipCodeCoverage> lstCoverages = new List<Dealer_SalesPartner_DetailsZipCodeCoverage>();
                    foreach (var d in obj.ListDetails)
                    {
                        Dealer_SalesPartner_DetailsZipCodeCoverage objDealerSalesPartnerDetailsZipCodeCoverage = new Dealer_SalesPartner_DetailsZipCodeCoverage();
                       objDealerSalesPartnerDetailsZipCodeCoverage =GetObjectZipCode(d, objDealerSalesPartner.serialCode);
                        lstCoverages.Add(objDealerSalesPartnerDetailsZipCodeCoverage);
                        //_objPropertyEntities.Dealer_SalesPartner_DetailsZipCodeCoverage.Add(
                        //    objDealerSalesPartnerDetailsZipCodeCoverage);
                        //_objPropertyEntities.SaveChanges();
                    }
                    _objPropertyEntities = new PropertyEntities();
                    _objPropertyEntities.Dealer_SalesPartner_DetailsZipCodeCoverage.AddRange(lstCoverages);
                    _objPropertyEntities.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private Dealer_SalesPartner_DetailsZipCodeCoverage GetObjectZipCode(DealerSalesPartnerDetailsZipCodeCoverageModel objDetails, string id)
        {
            Dealer_SalesPartner_DetailsZipCodeCoverage detail = new Dealer_SalesPartner_DetailsZipCodeCoverage()
            {
                dealerSalesPartnerId = id.ToString(),
                commissionRate = objDetails.CommissionRate,
                zipCode = objDetails.ZipCode
            };
            if (objDetails.Id > 0)
            {
                detail.id = objDetails.Id;
            }
            return detail;
        }

        public bool AlreadyExistMail(DealerSalesPartnerModel obj)
        {
            try
            {
                Dealer_SalesPartner objDealerSales = _objPropertyEntities.Dealer_SalesPartner
                    .Where(x => x.email == obj.Email).FirstOrDefault();
                if (objDealerSales != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private Dealer_SalesPartner GetObject(DealerSalesPartnerModel obj)
        {
            Dealer_SalesPartner objDealerSalesPartner = new Dealer_SalesPartner()
            {
                id = obj.Id,
                address1 = obj.Address1,
                address2 = obj.Address2,
                city = obj.City,
                commissionRate = obj.CommissionRate,
                countryId = obj.CountryId,
                createDate = obj.CreateDate,
                email = obj.Email,
                firstName = obj.FirstName,
                lastName = obj.LastName,
                joinDate = string.IsNullOrEmpty(obj.JoinDate) ? DateTime.Now : Convert.ToDateTime(obj.JoinDate),
                mobilePhoneNo = obj.MobilePhoneNo,
                primaryPhoneNo = obj.PrimaryPhoneNo,
                //password = obj.Password,
                region = obj.Region,
                userType = obj.ProfileName.Equals("SalesPartnerProfile") ? 9 : obj.ProfileName.Equals("DealerProfile") ? 10 : 0,
                serialCode = obj.SerialCode,
                stateId = obj.StateId,
                zipCode = obj.ZipCode,
                routingNo = obj.RoutingNo,
                accountNo = obj.AccountNo
        };
            return objDealerSalesPartner;
        }
        public bool Update(DealerSalesPartnerModel obj)
        {
            try
            {
                Dealer_SalesPartner objDealerSalesPartner = new Dealer_SalesPartner();
                objDealerSalesPartner = GetObject(obj);
                var existing = _objPropertyEntities.Dealer_SalesPartner.Find(objDealerSalesPartner.id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(objDealerSalesPartner).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                /* Eproperty Db */
                UserProfile objUserProfile2 = new UserProfile();
                objUserProfile2 = GetUserObject(objDealerSalesPartner, obj.Password, obj.userProfileId);
                var existingUser2 = _objPropertyEntities.UserProfile.Find(objUserProfile2.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existingUser2);
                _objPropertyEntities.Entry(objUserProfile2).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();
                /* Owner Db 1*/
                int objUserProfile2id = _objEPropertyEntities.UserProfile.Where(x => x.Email == objUserProfile2.Email).FirstOrDefault().Id;
                UserProfile objUserProfile = new UserProfile();
                objUserProfile = GetUserObject(objDealerSalesPartner, obj.Password, objUserProfile2id);
                var existingUser = _objEPropertyEntities.UserProfile.Find(objUserProfile2id);
                ((IObjectContextAdapter)_objEPropertyEntities).ObjectContext.Detach(existingUser);
                _objEPropertyEntities.Entry(objUserProfile).State = EntityState.Modified;
                _objEPropertyEntities.SaveChanges();

                if (obj.ListDetails != null)
                    {
                        string id = objDealerSalesPartner.serialCode;
                        foreach (var d in obj.ListDetails)
                        {
                            Dealer_SalesPartner_DetailsZipCodeCoverage objDealerSalesPartnerDetailsZipCodeCoverage = new Dealer_SalesPartner_DetailsZipCodeCoverage();
                           objDealerSalesPartnerDetailsZipCodeCoverage = GetObjectZipCode(d, id);
                            if (objDealerSalesPartnerDetailsZipCodeCoverage.id > 0)
                            {
                                var existingDetails = _objPropertyEntities.Dealer_SalesPartner_DetailsZipCodeCoverage.Find(objDealerSalesPartnerDetailsZipCodeCoverage.id);
                                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existingDetails);
                            _objPropertyEntities.Entry(objDealerSalesPartnerDetailsZipCodeCoverage).State = EntityState.Modified;
                            _objPropertyEntities.SaveChanges();
                            }
                            else
                            {
                            _objPropertyEntities.Dealer_SalesPartner_DetailsZipCodeCoverage.Add(
                                    objDealerSalesPartnerDetailsZipCodeCoverage);
                            _objPropertyEntities.SaveChanges();
                            }

                        }
                    //}
                }
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private UserProfile GetUserObject(Dealer_SalesPartner obj,string password, int userProfileId)
        {
            UserProfile user = new UserProfile()
            {
                Id = userProfileId,
                Username = obj.firstName + " "+obj.lastName,
                Title = obj.userType.Equals(9) ? "Sales Partner" : "Dealer",
                Password = Utility.base64Encode(password),
                Email = obj.email,
                UserType = obj.userType.ToString(),
                Phone = obj.primaryPhoneNo,
                CanLogin = true,
                IsActive = true,
                IsAdmin = false,
                CreatedDate = DateTime.Now,
                SecurityLevel = "2 and Up",
                HasAccountSystem = false,
                HasCompletedFullProfile =  false,
                HasContactProfile =  false,
                HasFinishedTenantImport = false,
                HasDocuments = false,
                HasLedgerCode = false,
                HasOwnerProfile = false,
                HasPropertyLocation = false,
                HasPropertyManagerProfile = false,
                HasPropertyUnit = false,
                HasSystemInfo = false,
                HasUserProfile = false,
                HasVendorProfile = false,
                IsDeleted = false
            };
            return user;
        }
        
        public bool Delete(Dealer_SalesPartner_DetailsZipCodeCoverage obj)
        {
            try
            {
                Dealer_SalesPartner_DetailsZipCodeCoverage existing = new Dealer_SalesPartner_DetailsZipCodeCoverage();
                existing = _objPropertyEntities.Dealer_SalesPartner_DetailsZipCodeCoverage.Find(obj.id);
                _objPropertyEntities.Dealer_SalesPartner_DetailsZipCodeCoverage.Remove(existing);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<DealerSalesPartnerModel> GetDealerSalesData(UserProfile userProfile)
        {
            List <DealerSalesPartnerModel> lst = new List<DealerSalesPartnerModel>();
            if (userProfile.UserType != "1")
            {
                DealerSalesPartnerModel obj = new DealerSalesPartnerModel();
                Dealer_SalesPartner objDealer_SalesPartner = _objPropertyEntities.Dealer_SalesPartner.Where(x => x.email == userProfile.Email).FirstOrDefault();
                if (objDealer_SalesPartner != null)
                {
                    obj = SetDealerSalesData(objDealer_SalesPartner, null);
                }
                obj.Password = Utility.base64Decode(userProfile.Password);
                obj.UserType = userProfile.UserType;
                obj.Email = userProfile.Email;
                obj.userProfileId = userProfile.Id;
                if (obj.SerialCode == null)
                {
                    string objName = userProfile.UserType == "9" ? "SalesPartnerProfile" : userProfile.UserType == "10" ? "DealerProfile" : "";
                    obj.SerialCode = MakeAutoGenNumber("1", objName);
                }

                obj.IsAdmin = false;
                List<Dropdown> locDropdowns = new List<Dropdown>();
               
                if (obj.UserType == "9")
                {
                    List<OwnerProfile> OwnerProfileLst = _objPropertyEntities.OwnerProfile
                        .Where(x => x.SalesPartnerId == objDealer_SalesPartner.serialCode).ToList();
                    List<ResidentialUnit> RecidentUnitLst = _objEPropertyEntities.ResidentialUnit.ToList();
                    List<Location> LocationList = _objEPropertyEntities.Location.ToList();
                    locDropdowns = (from O in OwnerProfileLst
                        join re in RecidentUnitLst on O.Serial equals re.OwnerId
                        join location in LocationList on re.LocationSerialId equals location.Serial
                        select new Dropdown()
                        {
                            Id2 = location.Serial,
                            Data = location.LocationName
                        }).ToList();
                }
                else if (obj.UserType == "10")
                {
                    locDropdowns =
                        (from O in _objEPropertyEntities.Location.Where(x => x.Zip == obj.ZipCode).ToList()
                        
                            select new Dropdown()
                            {
                                Id2 = O.Serial,
                                Data = O.LocationName
                            })
                        .GroupBy(car => car.Id2)
                        .Select(g => g.First())
                        .ToList();
                }
                
                obj.ObjAccounts = new DealerSalesPartnerAccounts()
                {
                    PartnerName = userProfile.UserType == "9" ? "Sales Partner Name : "+ userProfile.Username : userProfile.UserType == "10" ? "Dealer Name : "+ userProfile.Username : "",
                    Location = locDropdowns
                };
                lst.Add(obj);
            }
            else
            {
                List<Dealer_SalesPartner> lstObjSalesPartners=new List<Dealer_SalesPartner>();
                lstObjSalesPartners = _objPropertyEntities.Dealer_SalesPartner.ToList();
                if (lstObjSalesPartners.Count > 0)
                {
                    foreach (var objSalesPartner in lstObjSalesPartners)
                    {
                        UserProfile objUserProfile =new UserProfile();
                       objUserProfile = _objPropertyEntities.UserProfile.Where(x => x.Email == objSalesPartner.email).FirstOrDefault();
                        if (objUserProfile != null)
                        {
                            List<Dealer_SalesPartner_DetailsZipCodeCoverage> lstDtl =
                                _objPropertyEntities.Dealer_SalesPartner_DetailsZipCodeCoverage.Where(x => x.dealerSalesPartnerId == objSalesPartner.serialCode).ToList();
                            DealerSalesPartnerModel obj = new DealerSalesPartnerModel();
                            obj = SetDealerSalesData(objSalesPartner, lstDtl);
                            obj.Password = Utility.base64Decode(objUserProfile.Password);
                            obj.UserType = objUserProfile.UserType;
                            obj.Email = objUserProfile.Email;
                            obj.userProfileId = userProfile.Id;
                            obj.IsAdmin = true;
                            lst.Add(obj);
                        }
                        
                    }
                }
                else
                {
                    DealerSalesPartnerModel obj = new DealerSalesPartnerModel();
                    obj.Password = Utility.base64Decode(userProfile.Password);
                    obj.UserType = userProfile.UserType;
                    obj.Email = userProfile.Email;
                    obj.userProfileId = userProfile.Id;
                    obj.IsAdmin = true;
                    lst.Add(obj);
                }
            }
            return lst;
        }
        private DealerSalesPartnerModel SetDealerSalesData(Dealer_SalesPartner objDealer_SalesPartner, 
            List<Dealer_SalesPartner_DetailsZipCodeCoverage> objDetailsList)
        {
            DealerSalesPartnerModel obj = new DealerSalesPartnerModel()
                {
                    FirstName = objDealer_SalesPartner.firstName,
                    LastName = objDealer_SalesPartner.lastName,
                    Address1 = objDealer_SalesPartner.address1,
                    Address2 = objDealer_SalesPartner.address2,
                    City = objDealer_SalesPartner.city,
                    CommissionRate = objDealer_SalesPartner.commissionRate,
                    CountryId = objDealer_SalesPartner.countryId,
                    CreateDate = objDealer_SalesPartner.createDate,
                    Id = objDealer_SalesPartner.id,
                    JoinDate = Convert.ToDateTime(objDealer_SalesPartner.joinDate).ToString("MM/dd/yyyy"),
                    MobilePhoneNo = objDealer_SalesPartner.mobilePhoneNo,
                    PrimaryPhoneNo = objDealer_SalesPartner.primaryPhoneNo,
                    Region = objDealer_SalesPartner.region,
                    SerialCode = objDealer_SalesPartner.serialCode,
                    StateId = objDealer_SalesPartner.stateId,
                    ZipCode = objDealer_SalesPartner.zipCode,
                    RoutingNo = objDealer_SalesPartner.routingNo,
                    AccountNo = objDealer_SalesPartner.accountNo
            };
                obj.ListDetails = new List<DealerSalesPartnerDetailsZipCodeCoverageModel>();
            if (objDetailsList != null)
            {
                foreach (var objDtl in objDetailsList)
                {
                    DealerSalesPartnerDetailsZipCodeCoverageModel objDetails =
                        new DealerSalesPartnerDetailsZipCodeCoverageModel()
                        {
                            Id = objDtl.id,
                            ZipCode = objDtl.zipCode,
                            CommissionRate = objDtl.commissionRate,
                            DealerSalesPartnerId = objDtl.dealerSalesPartnerId
                        };
                    obj.ListDetails.Add(objDetails);
                }
            }
                
            
            return obj;
        }

        public string MakeAutoGenNumber(string yourPrefix, string objName)
        {
            try
            {
                _objPropertyEntities = new PropertyEntities();
                //_objEPropertyEntities = EPropertyEntity.GetFreshEntity();
                var oupParam = new ObjectParameter("NewID", 0) { Value = DBNull.Value };
                //_objEPropertyEntities.SP_GetID(objName, "Id", DateTime.Now.Year, null, null, oupParam);
                _objPropertyEntities.SP_GetID(objName, "Id", DateTime.Now.Year, null, null, oupParam);
                var sNumber = oupParam.Value.ToString().PadLeft(5, '0');
                var serial = string.Concat(yourPrefix, sNumber);

                return serial;
            }
            catch (Exception ex)
            {
                // ignored
            }

            return null;
        }

        public DealerSalesPartnerModel GetDataById(DealerSalesPartnerModel getObj)
        {
            int id = getObj.Id;
            Dealer_SalesPartner objSalesPartner = new Dealer_SalesPartner();
            objSalesPartner =_objPropertyEntities.Dealer_SalesPartner.Where(x => x.id == id).FirstOrDefault();

            UserProfile objUserProfile = _objPropertyEntities.UserProfile.Where(x => x.Email == objSalesPartner.email).FirstOrDefault();
            List<Dealer_SalesPartner_DetailsZipCodeCoverage> lstDtl =
                _objPropertyEntities.Dealer_SalesPartner_DetailsZipCodeCoverage.Where(x => x.dealerSalesPartnerId == objSalesPartner.serialCode).ToList();

            DealerSalesPartnerModel obj = new DealerSalesPartnerModel();
            obj = SetDealerSalesData(objSalesPartner, lstDtl);
            obj.Password = Utility.base64Decode(objUserProfile.Password);
            obj.UserType = objUserProfile.UserType;
            obj.Email = objUserProfile.Email;
            obj.userProfileId = objUserProfile.Id;
            return obj;
        }

        public UserProfile GetUpdateUser(string email)
        {
            UserProfile objUserProfileLogin = new UserProfile();
            //UserProfile objUserProfile = _objPropertyEntities.UserProfile.Where(x => x.Email == email).FirstOrDefault();
            objUserProfileLogin = _objPropertyEntities.UserProfile.Where(x => x.Email == email).FirstOrDefault();
            //var existingUser2 = objPropertyEntities.UserProfile.Find(objUserProfileLogin.Id);
            //((IObjectContextAdapter)objPropertyEntities).ObjectContext.Detach(existingUser2);
            //objPropertyEntities.Entry(objUserProfile).State = EntityState.Modified;
            //objPropertyEntities.SaveChanges();

            return objUserProfileLogin;
        }

        public DealerSalesPartnerAccounts GetOwnerByLocationId(UserProfile userProfile, string locId)
        {
            DealerSalesPartnerAccounts obj = new DealerSalesPartnerAccounts();
            Dealer_SalesPartner objDealer_SalesPartner = _objPropertyEntities.Dealer_SalesPartner.Where(x => x.email == userProfile.Email).FirstOrDefault();

            List<Dropdown> distinct = new List<Dropdown>();
            List<ResidentialUnit> RecidentUnitLst = _objEPropertyEntities.ResidentialUnit.Where(x => x.LocationSerialId == locId).ToList();
            List<Location> LocationList = _objEPropertyEntities.Location.Where(x => x.Serial.Equals(locId)).ToList();
            if (objDealer_SalesPartner.userType == 9)
            {
                List<OwnerProfile> OwnerProfileLst = _objEPropertyEntities.OwnerProfile
                    .Where(x => x.SalesPartnerId == objDealer_SalesPartner.serialCode).ToList();
                distinct =
                    (from O in OwnerProfileLst
                        join re in RecidentUnitLst on O.Serial equals re.OwnerId
                        join location in LocationList on re.LocationSerialId equals location.Serial
                        where location.Serial.Equals(locId)
                        select new Dropdown()
                        {
                            Id2 = O.Serial,
                            Data = O.FirstName + " " + O.LastName
                        })
                    .GroupBy(car => car.Id2)
                    .Select(g => g.First())
                    .ToList();
            }
            else if (objDealer_SalesPartner.userType == 10)
            {
                distinct =
                    (from location in LocationList
                        join re in RecidentUnitLst on location.Serial equals re.LocationSerialId
                        join O in _objEPropertyEntities.OwnerProfile.ToList() on re.OwnerId equals O.Serial
                        where location.Serial.Equals(locId)
                        select new Dropdown()
                        {
                            Id2 = O.Serial,
                            Data = O.FirstName + " " + O.LastName
                        })
                    .GroupBy(car => car.Id2)
                    .Select(g => g.First())
                    .ToList();
            }
            obj.Owner = distinct;
            return obj;
        }

        public List<GetDealerSalesAccounts_Result> GetSearchData(DealerSalesPartnerAccounts obj)
        {
            List<GetDealerSalesAccounts_Result> lst = _objEPropertyEntities.GetDealerSalesAccounts(obj.OwnerId).ToList();
            return lst;
        }

        public List<Dealer_SalesPartner> GetAllSalesPartnerDealer(int usertype)
        {
            List<Dealer_SalesPartner> listDealerSalesPartner = _objPropertyEntities.Dealer_SalesPartner.Where(x => x.userType == usertype).ToList();
            return listDealerSalesPartner;
        }

        public Dealer_SalesPartner GetBySerial(string serial)
        {
            Dealer_SalesPartner objDealerSalesPartner = null;
            try
            {
                var empQuery = from b in _objPropertyEntities.Dealer_SalesPartner
                               where b.serialCode == serial
                               select b;

                objDealerSalesPartner = empQuery.ToList().FirstOrDefault();


            }
            catch (Exception ex)
            {

            }

            return objDealerSalesPartner;
        }
    }
}
