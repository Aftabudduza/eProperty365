using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyService;
using PropertyService.BO;


    public class BaseDA
    {
        EPropertyEntities objEPropertyEntities = null;
        /// <summary>
        /// isNewNewContext == true, if you need newcontext
        /// isLazyLoadingEnable = true, is you want to load all data e.g. parent + child
        /// </summary>
        /// <param name="isNewContext"></param>
        /// <param name="isLazyLoadingEnable"></param>
        protected BaseDA(bool isNewContext = false, bool isLazyLoadingEnable = true)
        {
            objEPropertyEntities = (isNewContext == false) ? EPropertyEntity.GetEntity() : EPropertyEntity.GetFreshEntity();
               

            objEPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        protected void Insert(object ob)
        {
            //object entity = ;
        }
    }
