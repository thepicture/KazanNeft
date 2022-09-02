using KazanNeft.WebApi.Models.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace KazanNeft.WebApi.Controllers
{
    public class AssetGroupsController : ApiController
    {
        private readonly BaseEntities db = new BaseEntities();

        // GET: api/AssetGroups
        public IHttpActionResult GetAssetGroups()
        {
            return Ok(db.AssetGroups.ToList().ConvertAll(ag => new
            {
                ag.ID,
                ag.Name
            }));
        }

        // GET: api/AssetGroups/5
        [ResponseType(typeof(AssetGroup))]
        public IHttpActionResult GetAssetGroup(long id)
        {
            AssetGroup assetGroup = db.AssetGroups.Find(id);
            if (assetGroup == null)
            {
                return NotFound();
            }

            return Ok(assetGroup);
        }

        // PUT: api/AssetGroups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAssetGroup(long id, AssetGroup assetGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assetGroup.ID)
            {
                return BadRequest();
            }

            db.Entry(assetGroup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetGroupExists(id))
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

        // POST: api/AssetGroups
        [ResponseType(typeof(AssetGroup))]
        public IHttpActionResult PostAssetGroup(AssetGroup assetGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AssetGroups.Add(assetGroup);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = assetGroup.ID }, assetGroup);
        }

        // DELETE: api/AssetGroups/5
        [ResponseType(typeof(AssetGroup))]
        public IHttpActionResult DeleteAssetGroup(long id)
        {
            AssetGroup assetGroup = db.AssetGroups.Find(id);
            if (assetGroup == null)
            {
                return NotFound();
            }

            db.AssetGroups.Remove(assetGroup);
            db.SaveChanges();

            return Ok(assetGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssetGroupExists(long id)
        {
            return db.AssetGroups.Count(e => e.ID == id) > 0;
        }
    }
}