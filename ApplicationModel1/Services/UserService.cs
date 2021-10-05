// using ApplicationModel1.Dao;
// using ApplicationModel1.Entities;
// using ApplicationModel1.Services.DTOs;
// using Microsoft.EntityFrameworkCore;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
//
// namespace ApplicationModel1.Services
// {
//     class UserService
//     {
//         private ApplicationContext db;
//
//         public UserService(ApplicationContext db)
//         {
//             this.db = db;
//         }
//
//         public void AddUser(UserDTO dto)
//         {
//             User model = new User
//             {
//               FirstName = dto.FirstName,
//               LastName = dto.LastName,
//               Email = dto.Email,
//               Password = dto.Password,
//               Phone = dto.Phone,
//               BirthDate = dto.BirthDate,
//               Country = dto.Country,
//               City = dto.City,
//               Salary = dto.Salary,
//               ImagName = dto.ImagName,
//               Username = dto.Username,
//             };
//             db.Add(model);
//             db.SaveChanges();
//         }
//
//         public void UpdateUser(UserDTO dto)
//         {//un
//             User model = new User
//             {
//                 ID = dto.ID,
//                 FirstName = dto.FirstName,
//                 LastName = dto.LastName,
//                 Email = dto.Email,
//                 Password = dto.Password,
//                 Phone = dto.Phone,
//                 BirthDate = dto.BirthDate,
//                 Country = dto.Country,
//                 City = dto.City,
//                 Salary = dto.Salary,
//                 ImagName = dto.ImagName,
//                 Username = dto.Username,
//             };
//             db.Update(model);
//             db.SaveChanges();
//         }
//
//         public void RemoveUser(int id)
//         {
//             var model = db.Users.Where(u => u.ID == id).SingleOrDefault();
//
//             db.Remove(model);
//             db.SaveChanges();
//         }
//
//         public UserDTO GetUSerById(int id)
//         {
//             var model = db.Users.Where(u => u.ID == id).SingleOrDefault();
//
//             UserDTO dto = new UserDTO
//             {
//                 FirstName = model.FirstName,
//                 LastName = model.LastName,
//                 Email = model.Email,
//                 Phone = model.Phone,
//                 BirthDate = model.BirthDate,
//                 Country = model.Country,
//                 City = model.City,
//                 Salary = model.Salary,
//                 ImagName = model.ImagName,
//                 Username = model.Username,
//             };
//
//             return dto;
//         }
//
//         public IEnumerable<UserDTO> GetAllCustomers()
//         {
//             var listModelUser = db.Users.Where(u => u.Role == null).ToList();
//
//             var listDTOUser = new List<UserDTO>();
//
//             foreach(var model in listModelUser)
//             {
//                 UserDTO dto = new UserDTO
//                 {
//                     FirstName = model.FirstName,
//                     LastName = model.LastName,
//                     Email = model.Email,
//                     Phone = model.Phone,
//                     BirthDate = model.BirthDate,
//                     Country = model.Country,
//                     City = model.City,
//                     Salary = model.Salary,
//                     ImagName = model.ImagName,
//                     Username = model.Username,
//                 };
//
//                 listDTOUser.Add(dto);
//             }
//             return listDTOUser;
//         }
//
//         // Service Reviews
//
//         public void AddReview(ReviewsDTO dto)
//         {
//             Review model = new Review
//             {
//                 Name = dto.Name,
//                 Subject = dto.Subject,
//                 Email = dto.Email,
//                 Massage = dto.Massage,
//                 UserId = dto.UserId,
//                 IsActiv = false,
//                 CreateAt = DateTime.Now
//             };
//             db.Add(model);
//             db.SaveChanges();
//         }
//
//         public void UpdateReview(int id)
//         {
//             var model = db.Reviews.Where(r => r.ID == id).SingleOrDefault();
//
//             model.IsActiv = true;
//            
//             db.Update(model);
//             db.SaveChanges();
//         }
//
//         public IEnumerable<ReviewsDTO> GetAllAllReviews()
//         {
//             var listModelRe = db.Reviews.AsNoTracking().ToList();
//
//             var listDTORe = new List<ReviewsDTO>();
//
//             foreach(var re in listModelRe)
//             {
//                 ReviewsDTO dto = new ReviewsDTO
//                 {
//                     Name = re.Name,
//                     Subject = re.Subject,
//                     Email = re.Email,
//                     Massage = re.Massage,
//                     IsActiv = re.IsActiv,
//                     
//                     User =
//                     {
//                      FirstName = re.User.FirstName,
//                      LastName = re.User.LastName,
//                      Email = re.User.Email,
//                      BirthDate =re.User.BirthDate,
//                      ImagName = re.User.ImagName,
//                      Country = re.User.Country,
//                      City = re.User.City,
//                      Phone = re.User.Phone,                
//                     }
//                 };
//                 listDTORe.Add(dto);
//             }
//
//             return listDTORe;
//         }
//
//
//         public IEnumerable<ReviewsDTO> GetApprovedReviews()
//         {
//             var listModelRe = db.Reviews.Where(r => r.IsActiv == true).ToList();
//
//             var listDTORe = new List<ReviewsDTO>();
//
//             foreach (var re in listModelRe)
//             {
//                 ReviewsDTO dto = new ReviewsDTO
//                 {
//                     Name = re.Name,
//                     Subject = re.Subject,
//                     Email = re.Email,
//                     Massage = re.Massage,
//                     IsActiv = re.IsActiv,
//
//                     User =
//                     {
//                      FirstName = re.User.FirstName,
//                      LastName = re.User.LastName,
//                      Email = re.User.Email,
//                      BirthDate =re.User.BirthDate,
//                      ImagName = re.User.ImagName,
//                      Country = re.User.Country,
//                      City = re.User.City,
//                      Phone = re.User.Phone,
//                     }
//                 };
//                 listDTORe.Add(dto);
//             }
//
//             return listDTORe;
//         }
//     }
// }
