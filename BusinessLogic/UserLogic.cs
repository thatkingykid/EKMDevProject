using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class UserLogic
    {
        public enum AuthType
        {
            Customer, 
            Admin,
            TotallyNotRealAuthType
        }

        /// <summary>
        /// Given a username, password and auth level, method creates and returns a new SysUser database object
        /// </summary>
        /// <param name="authType">The auth type to set the user as, can be "Customer" or "Admin"</param>
        /// <returns>The created SysUser db entity</returns>
        /// <exception cref="RequestErrorException">Thrown when AuthType is not a valid selection</exception>
        public SysUser CreateNewUser(string user, string password, AuthType authType)
        {
            using (EKMDemoEntities context = new EKMDemoEntities())
            {
                var authAccess = context.Auths.FirstOrDefault(x => x.AuthType == authType.ToString());
                if (authAccess == null)
                    throw new RequestErrorException("Auth type does not exist");

                var existingUser = context.SysUsers.FirstOrDefault(x => x.Username == user);
                if (existingUser != null)
                {
                    var count = context.SysUsers.Where(x => x.Username.Contains(user)).Count();
                    user += count.ToString();
                }

                var userEntity = new SysUser
                {
                    Username = user,
                    Password = password,
                    AuthID = authAccess.ID
                };
                context.SysUsers.Add(userEntity);
                context.SaveChanges();
                return userEntity;
            }
        }

        /// <summary>
        /// Given a username and password, will attempt to match them to a record in the SysUser database
        /// </summary>
        /// <returns>Bool indicating whether login was successful or not</returns>
        public bool Login(string userName, string password)
        {
            using (EKMDemoEntities context = new EKMDemoEntities())
            {
                var user = context.SysUsers.FirstOrDefault(x => x.Username == userName);
                if (user == null || user.Password != password)
                    return false;
                else
                    return true;
            }
        }
    }
}
