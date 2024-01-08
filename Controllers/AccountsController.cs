using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Data;
using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AccountsController : Controller
    {
        string connection;
        SqlConnection con;
        public AccountsController()
        {
            var dbconfig = new ConfigurationBuilder().
               SetBasePath(Directory.GetCurrentDirectory()).
               AddJsonFile("appsettings.json").Build();
            connection = dbconfig["ConnectionStrings:DefaultConn"];
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel obj)
        {
            try
            {
                using (con = new SqlConnection(connection))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_login", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Emailid", obj.Emailid);
                    cmd.Parameters.AddWithValue("@Password", obj.Password);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        HttpContext.Session.SetString("LoginName", dr["name"].ToString());
                        HttpContext.Session.SetString("LoginTime",System.DateTime.Now.ToString());


                        // return RedirectToAction("Home", "Accounts");
                        return RedirectToAction("Verify OTP", "Accounts");

                    }
                    else
                    {
                        ViewData["msg"] = "EmailID or Password is not correct";
                        return View();

                    }

                }

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }

            return View();
        }



        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel obj)
       
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (con = new SqlConnection(connection))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_inserted", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Rollno", Convert.ToInt32(obj.RollNo));
                        cmd.Parameters.AddWithValue("@Name", obj.Name);
                        cmd.Parameters.AddWithValue("@EmailId", obj.EmailId);

                        cmd.Parameters.AddWithValue("@Password", obj.Password);

                        cmd.Parameters.AddWithValue("@dob", Convert.ToDateTime(obj.dob));

                        cmd.Parameters.AddWithValue("@Mobile", obj.Mobile);

                        cmd.Parameters.AddWithValue("@gender", obj.gender);
                        cmd.Parameters.AddWithValue("@Fee", Convert.ToDouble(obj.Fee));

                        cmd.Parameters.AddWithValue("@dept", obj.dept);
                        cmd.Parameters.AddWithValue("@status", Convert.ToBoolean(obj.status));
                        int x = cmd.ExecuteNonQuery();
                        if (x > 0)
                        {
                            return RedirectToAction("Login", "Accounts");
                        }
                        else
                        {

                            return View();
                        }




                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
               
            }
            return View();


        }
        [SettingsessionGlobally]
        public IActionResult Home()
        {
            return View();
        }
        [SettingsessionGlobally]
        public IActionResult About()
        {
            return View();
        }
        [SettingsessionGlobally]
        [OutputCache(Duration = 60)]
        public IActionResult Display()
        {
            List<DisplayModel> obj=GetData();
            return View(obj);
        }
        public List<DisplayModel> GetData()
        {
            List<DisplayModel> obj= new List<DisplayModel>();
            using (con = new SqlConnection(connection))
            {
                SqlDataAdapter da = new SqlDataAdapter("sp_getallData", con);
                DataTable dt=new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    obj.Add(
                        new DisplayModel
                        {
                            Id = Convert.ToInt32(dr["id"].ToString()),
                            RollNo = Convert.ToInt32(dr["id"].ToString()),
                            Name = dr["name"].ToString(),
                            EmailId = dr["Emailid"].ToString(),
                            Password = dr["Password"].ToString(),
                            dob = Convert.ToDateTime(dr["dob"].ToString()),
                            Mobile = dr["Mobile"].ToString(),
                            gender = dr["gender"].ToString(),
                            Fee = Convert.ToDouble(dr["Fee"].ToString()),
                            dept = dr["dept"].ToString(),
                            status = Convert.ToBoolean(dr["status"].ToString()),
                        }
                        );
                }
                return obj;
            }
        }
        public IActionResult Delete(int id)
        {
            using (con = new SqlConnection(connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete from login where id=@id", con);
                cmd.Parameters.AddWithValue("id", id);
                int x=cmd.ExecuteNonQuery();
                if(x > 0) { 
                return RedirectToAction("Display","Accounts");
                }
            }
                return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            UpdateModel obj = getAlldatabyid(id);
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(UpdateModel obj)
        {
            try
            {
                using (con = new SqlConnection(connection))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_Update", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Rollno", Convert.ToInt32(obj.RollNo));
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@EmailId", obj.EmailId);

                    cmd.Parameters.AddWithValue("@Password", obj.Password);

                    cmd.Parameters.AddWithValue("@dob", Convert.ToDateTime(obj.dob));

                    cmd.Parameters.AddWithValue("@Mobile", obj.Mobile);

                    cmd.Parameters.AddWithValue("@gender", obj.gender);
                    cmd.Parameters.AddWithValue("@Fee", Convert.ToDouble(obj.Fee));

                    cmd.Parameters.AddWithValue("@dept", obj.dept);
                    cmd.Parameters.AddWithValue("@status", Convert.ToBoolean(obj.status));
                    cmd.Parameters.AddWithValue("@id", obj.Id);
                    int x = cmd.ExecuteNonQuery();
                    if (x > 0)
                    {
                        return RedirectToAction("Display", "Accounts");
                    }
                    else
                    {

                        return View();
                    }




                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return View(obj);
        }
        public UpdateModel getAlldatabyid(int id)
        {
            UpdateModel obj = null;
            using (con = new SqlConnection(connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from login where id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter da= new SqlDataAdapter();
                DataSet ds= new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    obj =new UpdateModel();
                    obj.Id= Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                    obj.RollNo = Convert.ToInt32(ds.Tables[0].Rows[i]["RollNo"].ToString());
                    obj.Name =ds.Tables[0].Rows[i]["Name"].ToString();
                    obj.EmailId = ds.Tables[0].Rows[i]["EmailId"].ToString();
                    obj.Password = ds.Tables[0].Rows[i]["Password"].ToString();
                    obj.dob = Convert.ToDateTime(ds.Tables[0].Rows[i]["dob"].ToString());
                    obj.Mobile = ds.Tables[0].Rows[i]["mobile"].ToString();
                    obj.gender = ds.Tables[0].Rows[i]["gender"].ToString();
                    obj.Fee = Convert.ToDouble(ds.Tables[0].Rows[i]["Fee"].ToString());
                    obj.dept = ds.Tables[0].Rows[i]["dept"].ToString();
                    obj.status = Convert.ToBoolean(ds.Tables[0].Rows[i]["status"].ToString());
                }
                return obj;
            }
           
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login","Accounts");
        }

        public IActionResult VerifyOTP()
        {

            return View();
        }


    }
}
