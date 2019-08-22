using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.Enitities;
using static WebAppIMaster.Models.NewManagerModels.ReviewModel;

namespace WebAppIMaster.Models.NewManagerManage
{
    public class ReviewManagerModel
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ReviewManagerModel(ApplicationDbContext db) => this.db = db;

        public List<ReviewSelect> Select()
        {
            var item = (from cl in db.Reviews
                        select new ReviewSelect
                        {
                            Id = cl.Id,
                            Rating = cl.Rating,
                            ReviewText = cl.ReviewText,
                            ExecutorId = cl.ExecutorId,
                            CustomerId = cl.CustomerId,
                            Executor = cl.Executor.User.FirstName + cl.Executor.User.LastName,
                            Customer = cl.Customer.ApplicationUser.FirstName + cl.Customer.ApplicationUser.LastName
                        }).OrderBy(p=>p.Rating).ToList();

            return item;
        }

        public ReviewDetail Details(int Id)
        {
            //Review rev = db.Reviews.Find(Id);

            //ReviewDetail rd = new ReviewDetail
            //{
            //    Id = rev.Id,
            //    ReviewText = rev.ReviewText,
            //    Executor = rev.Executor.User.FatherName + rev.Executor.User.LastName,
            //    Customer = rev.Customer.ApplicationUser.FirstName + rev.Customer.ApplicationUser.LastName,
            //    ExecutorId = rev.ExecutorId,
            //    CustomerId = rev.CustomerId,
            //    Rating = rev.Rating
            //};

            var item = (from cl in db.Reviews
                        where cl.Id == Id
                        select new ReviewDetail
                        {
                            Id = cl.Id,
                            Rating = cl.Rating,
                            ReviewText = cl.ReviewText,
                            ExecutorId = cl.ExecutorId,
                            CustomerId = cl.CustomerId,
                            Executor = cl.Executor.User.FirstName + cl.Executor.User.LastName,
                            Customer = cl.Customer.ApplicationUser.FirstName + cl.Customer.ApplicationUser.LastName
                        }).SingleOrDefault();

            return item;
        }
    }
}