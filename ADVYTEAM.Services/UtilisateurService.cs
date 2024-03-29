﻿using ADVYTEAM.Data;
using ADVYTEAM.Data.Infrastructure;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVYTEAM.Services
{
    public class UtilisateurService : Service<utilisateur> , IUtilisateurService
    {
        static IDataBaseFactory factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(factory);

        public UtilisateurService():base(utk)
        {

        }

        public IEnumerable<utilisateur> GetEmployes()
        {
            return GetMany(s => s.type == "employe");
        }

        public IEnumerable<utilisateur> GetEmployesByman(int manid)
        {
            return GetMany(s => s.type == "employe" && s.manager_id == manid) ;
        }

    }
}
