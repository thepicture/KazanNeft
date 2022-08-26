using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using KazanNeft.WebApi.Models.Entities;

namespace KazanNeft.WebApi.Controllers
{
    public class AssetsController : ApiController
    {
        private BaseEntities db = new BaseEntities();

        // GET: api/Assets
        public IHttpActionResult GetAssets()
        {
            return Ok(db.Assets
                .ToList()
                .ConvertAll(asset => new
                {
                    asset.ID,
                    asset.AssetName,
                    asset.AssetSN,
                    DepartmentName = asset.DepartmentLocation.Department.Name,
                    AssetGroupName =  asset.AssetGroup.Name,
                    asset.WarrantyDate
                }));
        }

        // GET: api/Assets/5
        [ResponseType(typeof(Asset))]
        public IHttpActionResult GetAsset(long id)
        {
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return NotFound();
            }

            return Ok(asset);
        }

        // PUT: api/Assets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAsset(long id, Asset asset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asset.ID)
            {
                return BadRequest();
            }

            db.Entry(asset).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Assets
        [ResponseType(typeof(Asset))]
        public IHttpActionResult PostAsset(Asset asset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Assets.Add(asset);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = asset.ID }, asset);
        }

        // DELETE: api/Assets/5
        [ResponseType(typeof(Asset))]
        public IHttpActionResult DeleteAsset(long id)
        {
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return NotFound();
            }

            db.Assets.Remove(asset);
            db.SaveChanges();

            return Ok(asset);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssetExists(long id)
        {
            return db.Assets.Count(e => e.ID == id) > 0;
        }
    }
}