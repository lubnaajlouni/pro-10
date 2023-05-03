using lubnamaster.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Xunit.Abstractions;

namespace lubnamaster.Controllers
{
    public class HomeController : Controller
    {
        storemasterEntities db = new storemasterEntities();
        
        public ActionResult Home()
        {

            var items = db.Items.ToList();
            return View(items);
        }


        [Authorize]
        public ActionResult Userprofile(int? orderid , int total = 0)
        {
            var userId = User.Identity.GetUserId();
            var aspnet = db.AspNetUsers.Where(od => od.Id == userId).ToList();
            ViewBag.aspnet = aspnet;

            var order = db.Orders.Where(od => od.UserId == userId);
         
            var orderDetail = db.OrderDetails.Where(od=> od.OrderId == orderid );

            var orderinfo = db.Orders.Where(od => od.OrderId == orderid);
          

            var subtotal = orderDetail.Sum(od => od.Quantity * od.Itemeoffered.Item.ItemPrice);
            ViewBag.subtotal = subtotal;

            dynamic all = new ExpandoObject();
            all.o = order;
            all.d = orderDetail;
            all.of = orderinfo;
            all.t = subtotal;


            return View(all);
        }


        public ActionResult Items(string gender = "", int categoryId = 0)
        {
            var items = db.Items.AsQueryable();

            if (!string.IsNullOrEmpty(gender))
            {
                items = items.Where(x => x.Gender == gender);
            }

            if (categoryId != 0)
            {
                items = items.Where(x => x.CategoryId == categoryId);
            }

            return View(items.ToList());
        }

        [HttpGet]
        public ActionResult Items(string filter, string size, string color, int? categoryId, string gender = "" )
        {
            var items = db.Items.AsQueryable();

            if (!string.IsNullOrEmpty(gender))
            {
                items = items.Where(x => x.Gender == gender);
            }

            if (categoryId != null)
            {
                items = items.Where(x => x.CategoryId == categoryId);
                ViewBag.CategoryId = categoryId;
            }

            if (!string.IsNullOrEmpty(size))
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.Size == size));
            }

            if (!string.IsNullOrEmpty(color))
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == color));
            }

            if (filter == "1")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.Size == "S"));
            }
            else if (filter == "2")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.Size == "M"));
            }
            else if (filter == "3")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.Size == "L"));
            }
            else if (filter == "4")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.Size == "XL"));
            }
            else if (filter == "5")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "green"));
            }
            else if (filter == "6")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "black"));
            }
            else if (filter == "7")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "white"));
            }
            else if (filter == "8")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "blue"));
            }
            else if (filter == "9")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "red"));
            }
            else if (filter == "10")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "beg"));
            }
            else if (filter == "11")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "bink"));
            }

            return View(items.ToList());
        }









        public ActionResult MEN(int categoryId = 0)
        {
            
            var items = db.Items.Where(x => x.Gender == "MEN");

            if (categoryId != 0)
            {
                items = items.Where(x => x.CategoryId == categoryId);
            }

            return View(items.ToList());

        }

        [HttpGet]
        public ActionResult MEN(string filter, int? categoryId, string size, string color)
        {
            var items = db.Items.Where(x => x.Gender == "MEN");

            if (categoryId != null)
            {
                items = items.Where(x => x.CategoryId == categoryId);
                ViewBag.CategoryId = categoryId;
            }

            if (!string.IsNullOrEmpty(size))
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.Size == size));
            }

            if (!string.IsNullOrEmpty(color))
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == color));
            }

            if (filter == "1")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.Size == "S"));
            }
            else if (filter == "2")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.Size == "M"));
            }
            else if (filter == "3")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.Size == "L"));
            }
            else if (filter == "4")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.Size == "XL"));
            }
            else if (filter == "5")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "green"));
            }
            else if (filter == "6")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "black"));
            }
            else if (filter == "7")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "white"));
            }
            else if (filter == "8")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "blue"));
            }
            else if (filter == "9")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "red"));
            }
            else if (filter == "10")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "beg"));
            }
            else if (filter == "11")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "bink"));
            }

            return View(items.ToList());
        }








        public ActionResult WOMEN(int categoryId = 0)
        {
            var items = db.Items.Include(i => i.Itemeoffereds).Where(x => x.Gender == "WOMEN").ToList();
            if (categoryId != 0)
            {
                items = items.Where(x => x.CategoryId == categoryId).ToList();
            }

            return View(items);
        }




        [HttpGet]
        public ActionResult WOMEN(string filter, int? categoryId, string size, string color)
        {
            var items = db.Items.Where(x => x.Gender == "WOMEN");

            if (categoryId != null)
            {
                items = items.Where(x => x.CategoryId == categoryId);
                ViewBag.CategoryId = categoryId;
            }

            if (!string.IsNullOrEmpty(size))
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.Size == size));
            }

            if (!string.IsNullOrEmpty(color))
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == color));
            }

            if (filter == "1")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.Size == "S"));
            }
            else if (filter == "2")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.Size == "M"));
            }
            else if (filter == "3")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.Size == "L"));
            }
            else if (filter == "4")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.Size == "XL"));
            }
            else if (filter == "5")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "green"));
            }
            else if (filter == "6")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "black"));
            }
            else if (filter == "7")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "white"));
            }
            else if (filter == "8")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "blue"));
            }
            else if (filter == "9")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "red"));
            }
            else if (filter == "10")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "beg"));
            }
            else if (filter == "11")
            {
                items = items.Where(x => x.Itemeoffereds.Any(l => l.ColorName == "bink"));
            }

            return View(items.ToList());
        }







        public ActionResult shopdetails(int? id)
        {

           

            if (id == null)
            {
                return RedirectToAction("Items");
            }
            else
            {
                var item = db.Items.Find(id);

                var itemoffer = db.Itemeoffereds.Where(io => io.Item.Item_Id == id)
                  .GroupBy(io => io.ColorName)
                  .Select(ioGroup => ioGroup.FirstOrDefault());

                var itemm = db.Itemeoffereds.Where(x => x.ItemId == id);
                var itemmm = db.Itemeoffereds.Find(id);
               
                var reviews = db.Reviews.Where(r => r.ItemId == id && r.Approve == true).ToList();

                var itemsizes = db.Itemeoffereds
                  .Where(io => io.Item.Item_Id == id)
                  .GroupBy(io => io.Size)
                  .Select(ioGroup => ioGroup.FirstOrDefault());


                dynamic all = new ExpandoObject();
                all.m = item;
                all.w = itemm;
                all.q = itemmm;
                all.s = itemoffer;
                all.z = itemsizes;
                all.r = reviews; 
                
                return View(all);

            }
        }





        [HttpPost]
        [Authorize]
        public ActionResult shopdetails(int? id, int quantity, string ColorName, string Size)
        {
            var userId = User.Identity.GetUserId();
        

            // Retrieve the item based on Item_Id, ColorName, and Size
            var itemOffered = db.Itemeoffereds
                .Where(io => io.Item.Item_Id == id && io.ColorName == ColorName && io.Size == Size)
                .FirstOrDefault();

            if (itemOffered == null || itemOffered.QuantityLeft < quantity || quantity == 0)
            {
                // Item not found or not enough quantity left, handle the error
                TempData["swal_message"] = "Item not found";
                TempData["title"] = "Error";
                TempData["icon"] = "error";
                return RedirectToAction("shopdetails", new { id });
            }

            var cartItem = db.Carts.SingleOrDefault(c => c.ItemId == itemOffered.ColorId && c.UserId == userId);

            if (cartItem == null)
            {
                // Item not in cart, add new cart item
                cartItem = new Cart
                {
                    ItemId = itemOffered.ColorId,
                    UserId = userId,
                    Quantity = quantity
                };
                db.Carts.Add(cartItem);
            }
            else
            {
                // Item already in cart, update quantity
                cartItem.Quantity += quantity;
            }

            db.SaveChanges();

            return RedirectToAction("Shopcart");
        }













        public ActionResult GetQuantityLeft(string color, string size)
        {
            var itemOffered = db.Itemeoffereds.FirstOrDefault(io => io.ColorName == color && io.Size == size);

            if (itemOffered != null)
            {
                return Json(itemOffered.QuantityLeft, JsonRequestBehavior.AllowGet);
            }
            else
    {
        return Json(new { error = "Item not found" }, JsonRequestBehavior.AllowGet);
    }
        }




        [HttpPost]
        
        public ActionResult AddReview(int id, string description)
        {
            // Retrieve logged-in user information
            string userId = User.Identity.GetUserId();
            var user = db.AspNetUsers.Find(userId);

            // Check if the user is null
            if (user == null)
            {
                // Set a message for the user to log in
                TempData["Message"] = "Please log in to add a review.";

                // Generate a URL for the shopdetails action with the specified route values
                var url = Url.Action("shopdetails", new { id });

                // Redirect to the login page with the return URL parameter set to the generated URL
                return RedirectToAction("login", "account", new { returnUrl = url });
            }

            // Create a new review object
            var review = new Review
            {
                UserId = userId,
                ItemId = id,
                Description = description,
                Name = user.FirstName,
                Date = DateTime.Now,
                Approve = false // Set the value of the Approve column
            };

            // Add the review to the database
            db.Reviews.Add(review);
            db.SaveChanges();

            // Set a success message
            TempData["Message"] = "Thank you for your feedback!";

            // Redirect back to the shopdetails page
            return RedirectToAction("shopdetails", new { id });
        }








        public ActionResult Shopcart()
        { 
            var userId = User.Identity.GetUserId();         
            var cart = db.Carts.Where(c => c.UserId == userId).ToList();         
            return View(cart);
        }


        [HttpPost]
        public ActionResult UpdateQuantity(int cartId, int quantity)
        {
            var cartItem = db.Carts.FirstOrDefault(c => c.CartId == cartId); // Find the cart item by cart ID
            if (cartItem != null)
            {
                cartItem.Quantity = quantity; // Update the quantity
                db.SaveChanges(); // Save changes to the database
                var total = cartItem.Quantity * cartItem.Itemeoffered.Item.ItemPrice; // Calculate the total

                // Get the current user's cart item count from the database or any other data source
                string userId = User.Identity.GetUserId(); // Update with the correct method to get the currently logged in user's ID
                var cartItemCount = db.Carts
                    .Where(c => c.UserId == userId)
                    .Sum(c => c.Quantity);

                // If there are no items in the cart, set the count to 0
                if (cartItemCount == null)
                {
                    cartItemCount = 0;
                }

                return Json(new { total = total, cartItemCount = cartItemCount }); // Return the total and cart item count as the response
            }
            return Content("Error"); // Return an error message if cart item not found
        }


        [HttpPost]
        public ActionResult DeleteCartItem(int cartId)
        {
            var cartItem = db.Carts.FirstOrDefault(c => c.CartId == cartId); // Find the cart item by cart ID
            if (cartItem != null)
            {
                db.Carts.Remove(cartItem); // Remove the cart item from the database
                db.SaveChanges(); // Save changes to the database

                // Get the current user's cart item count from the database or any other data source
                string userId = User.Identity.GetUserId(); // Update with the correct method to get the currently logged in user's ID
                var cartItemCount = db.Carts
                    .Where(c => c.UserId == userId)
                    .Sum(c => c.Quantity);

                // If there are no items in the cart, set the count to 0
                if (cartItemCount == null)
                {
                    cartItemCount = 0;
                }

                return Json(new { success = true, message = "Cart item deleted successfully.", count = cartItemCount }); // Return a success message as JSON
            }

            return Json(new { success = false, message = "Error: Cart item not found." }); // Return an error message as JSON
        }




        public ActionResult GetCartItemCount()
        {
            // Get the current user's cart item count from the database or any other data source
            string userId = User.Identity.GetUserId(); // Update with the correct method to get the currently logged in user's ID
            var cartItemCount = db.Carts
                .Where(c => c.UserId == userId)
                .Sum(c => c.Quantity);

            // If there are no items in the cart, set the count to 0
            if (cartItemCount == null)
            {
                cartItemCount = 0;
            }

            // Return the cart item count as a JSON response
            return Json(new { count = cartItemCount }, JsonRequestBehavior.AllowGet);
        }





        public ActionResult Checkout()
        {
            // Retrieve logged-in user information
            string userId = User.Identity.GetUserId();
            var user = db.AspNetUsers.Find(userId);

            if (user != null)
            {
                // Pass user information to the view
                ViewBag.FirstName = user.FirstName;
                ViewBag.LastName = user.LastName;
                ViewBag.Email = user.Email;
            }

            // Add other necessary data to the view model or ViewBag

            //for city 
            ViewBag.Cities = db.Cities.ToList();

            // Retrieve cart items for the user
            var cartItems = db.Carts.Where(c => c.UserId == userId).ToList();

            // Calculate the sum of cart items' total
            decimal total = (decimal)cartItems.Sum(cart => cart.Quantity * cart.Itemeoffered.Item.ItemPrice);

            // Store the calculated total in ViewBag
            ViewBag.Total = total.ToString("F2");

            return View(cartItems);
        }






        [HttpPost]
        public ActionResult Checkout(string city, string Phone, string address)
        {
            // Retrieve logged-in user information
            string userId = User.Identity.GetUserId();
            var user = db.AspNetUsers.Find(userId);

            if (user != null)
            {
                // Pass user information to the view
                ViewBag.FirstName = user.FirstName;
                ViewBag.LastName = user.LastName;
                ViewBag.Email = user.Email;
            }      

            // Retrieve cart items for the user
            var cartItems = db.Carts.Where(c => c.UserId == userId).ToList();

            // Check if cart items and order details are not empty
            if (cartItems.Count == 0 || cartItems.Select(c => c.Quantity).Sum() == 0)
            {
                TempData["swal_message"] = "Your cart is empty. Please add items to your cart before placing an order";
                ViewBag.title = "Error";
                ViewBag.icon = "error";
                return View(); // Return to the checkout view to show the error message
            }

            // Create order
            Order order = new Order()
            {

                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserId = userId,
                PhoneNumber = Phone,
                City = city,
                Adress = address,
                PaymentMethod = "Cash On Delivery", // Example value, replace with actual payment method
                OrderDate = DateTime.Now
                // Add other necessary data for the order
            };
            db.Orders.Add(order);
            db.SaveChanges(); // Save changes to generate OrderId

            // Create order details for each cart item
            foreach (var cartItem in cartItems)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = order.OrderId,
                    UserId = userId,
                    ItemId = (int)cartItem.ItemId,
                    Quantity = (int)cartItem.Quantity,
                    // Populate other fields from the order and cart item
                    // such as ItemId, Quantity, etc.
                };
                db.OrderDetails.Add(orderDetail);

                // Update QuantityLeft in Itemeoffereds table
                var itemOffered = db.Itemeoffereds.Find(cartItem.ItemId);
                if (itemOffered != null)
                {
                    itemOffered.QuantityLeft -= cartItem.Quantity;
                    db.Entry(itemOffered).State = EntityState.Modified;
                }

                // Remove cart item
                db.Carts.Remove(cartItem);
            }

            db.SaveChanges(); // Save changes to commit the order details and remove cart items

            // Return success view
            TempData["swal_message"] = $"Your order has been placed successfully";
            ViewBag.title = "Success";
            ViewBag.icon = "success";

            return View("Checkout");
        }





        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }



        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]

        public ActionResult Contact(string name, string subject, string email, string message)
        {
            ViewBag.Message = "Your contact page.";
            MailMessage mail = new MailMessage();
            mail.To.Add("projectnvc99@gmail.com");
            mail.From = new MailAddress("projectnvc99@gmail.com");
            mail.Subject = subject + " " + email;

            mail.Body = message;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("projectnvc99", "cbhcmnlhnosyinag");
            smtp.Send(mail);

            TempData["swal_message"] = $"Your message has been sent successfully!";
            ViewBag.title = "Success";
            ViewBag.icon = "success";

            return View();
        }

       
    }
}