using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PropertyService.BO;

namespace eProperty.Models
{
    public class CommonModel
    {

    }

    public class Feature
    {
        public string id { get; set; }
    }

    public class FeatureItem
    {
        public List<Feature> Feature { get; set; }
    }

    public class ComboData
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public int SelectedValue { get; set; }
        public string Id2 { get; set; }
        public string Id3 { get; set; }        
        public string SelectedField { get; set; }
    }

    public class ResidentialMaintenenceAll
    {
        public ResidentialUnitEquipment ResidentialUnitEquipment { get; set; }
        public ResidentialMaintainesManagerMaster ResidentialMaintainesManagerMaster { get; set; }
        public ResidentialMaintainesManagerImage ResidentialMaintainesManagerImage { get; set; }
        public List<ResidentialMaintainesManagerSchedule> ListOfResidentialMaintainesManagerSchedules { get; set; }
        public List<ResidentialMaintainesManagerPartData> ListOfResidentialMaintainesManagerPartDatas { get; set; }
        public List<ResidentialMaintainesManagerVandorData> ListOfResidentialMaintainesManagerVandorDatas { get; set; }

    }

    public class MessageSearch
    {
        public string MonthName { get; set; }
        public string RequestType { get; set; }
    }
}