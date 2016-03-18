using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;
using Newtonsoft.Json.Linq;

namespace Domain.Concrete
{
    public class JsonRepositoryFacturas : IRepositoryFacturas
    {
        private string urlWS;
        public JsonRepositoryFacturas(string urlWS)
        {
            this.urlWS = urlWS;
        }
        public IEnumerable<Factura> FacturasCompletas
        {
            get { throw new NotImplementedException(); }
        }
        public IEnumerable<Factura> Facturas
        {
            get
            {
                IEnumerable<Factura> jFacturas = null;
                WebClient cliente = new WebClient();
                var data = cliente.DownloadString(urlWS);
                JObject jObject = JObject.Parse(data);
                jFacturas = from i in jObject["items"] select createFactura(i);
                return jFacturas.ToList();
            }
        }        
        private Factura createFactura(JToken item)
        {
            return new Factura()
            {
                IntIdFactura = item["intIdFactura"].ToString(),
                Items = getConceptos(item["conceptos"])
            };          
        }
        private List<ItemFactura> getConceptos(JToken conceptos)
        {
            var jConceptos = from concepto in conceptos select createConcepto(concepto);                           
            return jConceptos.ToList();
        }

        private ItemFactura createConcepto(JToken concepto)
        {
            var c = new ItemFactura();
            c.Rfc = concepto["RFC"].ToString();
            c.ItemName = concepto["ITEM"].ToString();
            c.Descripcion = concepto["DESCRIPCION"].ToString();
            c.Unidad = concepto["UNIDAD"].ToString();
            c.PrecioUnitario = parseFloat(concepto["PRECIO UNITARIO"].ToString());
            c.CantidadEnPieza = concepto["CANTIDAD EN PIEZA"].ToString();
            c.Importe = parseFloat(concepto["IMPORTE"].ToString());

            return c;
        }

        private float parseFloat(string p)
        {
            float result = 0;
            float.TryParse(p, out result);
            return result;
        }        
        public void Register(Factura factura)
        {
            throw new NotImplementedException();
        }

        public void Delete(Factura factura)
        {
            throw new NotImplementedException();
        }

        public void Update(Factura factura)
        {
            throw new NotImplementedException();
        }                              
    }  
}
