using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRUD.Models;


namespace CRUD.Controllers
{
    public class ClienteAPIController : ApiController
    {
        private CRUDEEntities db = new CRUDEEntities();

        // GET: api/ClienteAPI
        public IQueryable<Cliente> GetCliente()
        {
            return db.Cliente;
        }
    }
}
