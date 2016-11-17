using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EscolaAppRest.Controllers
{
    public class EscolaController : ApiController
    {
        // GET api/escola
        public IEnumerable<Models.Escola> Get()
        {
            Models.EscolaDataContext dc = new Models.EscolaDataContext();
            var r = from f in dc.Escolas select f;
            return r.ToList();
        }

        // POST api/escola
        public void Post([FromBody] string value)
        {
            List<Models.Escola> x = JsonConvert.DeserializeObject
            <List<Models.Escola>>(value);
            Models.EscolaDataContext dc = new Models.EscolaDataContext();
            dc.Escolas.InsertAllOnSubmit(x);
            dc.SubmitChanges();
        }

        // PUT api/escola/5
        public void Put(int id, [FromBody] string value)
        {
            Models.Escola x = JsonConvert.DeserializeObject
            <Models.Escola>(value);
            Models.EscolaDataContext dc = new Models.EscolaDataContext();
            Models.Escola esc = (from f in dc.Escolas
                                     where f.Id == id
                                     select f).Single();
            esc.Id = x.Id;
            esc.Nome = x.Nome;
            esc.UF = x.UF;
            esc.CienciasNatureza = x.CienciasNatureza;
            esc.CienciasHumanas = x.CienciasHumanas;
            esc.LinguagensCodigos = x.LinguagensCodigos;
            esc.Matematica = x.Matematica;
            esc.Redacao = x.Redacao;
            dc.SubmitChanges();
        }

        // DELETE api/escola/5
        public void Delete(int id)
        {
            Models.EscolaDataContext dc = new Models.EscolaDataContext();
            Models.Escola esc = (from f in dc.Escolas
                                     where f.Id == id
                                     select f).Single();
            dc.Escolas.DeleteOnSubmit(esc);
            dc.SubmitChanges();
        }
    }
}
