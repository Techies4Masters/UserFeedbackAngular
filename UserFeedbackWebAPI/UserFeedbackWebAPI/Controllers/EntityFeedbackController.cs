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
using UserFeedbackWebAPI.Models;
using System.Web.Http.Cors;

namespace UserFeedbackWebAPI.Controllers
{
    //[EnableCors (origins: "*", headers:"*", methods:"*")]
    public class EntityFeedbackController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/EntityFeedback
        public IQueryable<AnonymousDetails> GetAnonymousDetails()
        {
            return db.AnonymousDetails;
        }

        // GET: api/EntityFeedback/GetEntities
        
        public IQueryable<EntitySelectModel> GetEntities()
        {
            return db.Entity.Select(e => new EntitySelectModel
                                            {
                                                EntityID = e.EntityID,
                                                Description = e.Description
                                            });
        }

        // GET: api/EntityFeedback/5
        [ResponseType(typeof(AnonymousDetails))]
        public IHttpActionResult GetAnonymousDetails(int id)
        {
            AnonymousDetails anonymousDetails = db.AnonymousDetails.Find(id);
            if (anonymousDetails == null)
            {
                return NotFound();
            }

            return Ok(anonymousDetails);
        }

        // PUT: api/EntityFeedback/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAnonymousDetails(int id, AnonymousDetails anonymousDetails)
        {
            if (id != anonymousDetails.ID)
            {
                return BadRequest();
            }

            db.Entry(anonymousDetails).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnonymousDetailsExists(id))
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

        // POST: api/EntityFeedback
        [ResponseType(typeof(AnonymousDetails))]
        public IHttpActionResult PostAnonymousDetails(AnonymousDetails anonymousDetails)
        {
            db.AnonymousDetails.Add(anonymousDetails);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = anonymousDetails.ID }, anonymousDetails);
        }

        // DELETE: api/EntityFeedback/5
        [ResponseType(typeof(AnonymousDetails))]
        public IHttpActionResult DeleteAnonymousDetails(int id)
        {
            AnonymousDetails anonymousDetails = db.AnonymousDetails.Find(id);
            if (anonymousDetails == null)
            {
                return NotFound();
            }

            db.AnonymousDetails.Remove(anonymousDetails);
            db.SaveChanges();

            return Ok(anonymousDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnonymousDetailsExists(int id)
        {
            return db.AnonymousDetails.Count(e => e.ID == id) > 0;
        }
    }
    public class EntitySelectModel
    {
        public int EntityID { get; set; }
        public string Description { get; set; }
    }
}